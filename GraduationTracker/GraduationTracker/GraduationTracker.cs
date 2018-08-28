using System.Linq;
using GraduationTracker.Repositories;

namespace GraduationTracker
{
    public partial class GraduationTracker
    {
        private readonly IRepository _repository;

        // TODO: Instantiation to be replaced by injection
        public GraduationTracker()
        {
            _repository = new Repository();
        }

        public StudentResult HasGraduated(Diploma diploma, Student student)
        {
            var credits = 0;
            var sumOfMarks = 0;

            foreach (var requirementId in diploma.Requirements)
            {
                foreach (var studentCourse in student.Courses)
                {
                    var requirement = _repository.GetRequirement(requirementId);

                    sumOfMarks = SumOfMarksCalculator(requirement, studentCourse, sumOfMarks);

                    credits = CreditCalculator(requirement, studentCourse, credits);
                }
            }

            var average = sumOfMarks / student.Courses.Length;

            var standing = DefineStanding(average);

            return EvaluateStudent(standing);
        }

        private static int CreditCalculator(Requirement requirement, Course studentCourse, int credits)
        {
            if (studentCourse.Mark > requirement.MinimumMark)
                credits += requirement.Credits;

            return credits;
        }

        private static int SumOfMarksCalculator(Requirement requirement, Course studentCourse, int sumOfMarks)
        {
            sumOfMarks += requirement.Courses
                .Where(requiredCourseId => requiredCourseId == studentCourse.Id)
                .Sum(requiredCourseId => studentCourse.Mark);

            return sumOfMarks;
        }

        private static Standing DefineStanding(int average)
        {
            Standing standing;

            if (average < 50)
                standing = Standing.Remedial;
            else if (average < 80)
                standing = Standing.Average;
            else if (average < 95)
                standing = Standing.SumaCumLaude;
            else
                standing = Standing.MagnaCumLaude;

            return standing;
        }

        private static StudentResult EvaluateStudent(Standing standing)
        {
            switch (standing)
            {
                case Standing.Remedial:
                    return new StudentResult { Standing = standing, Pass = false };
                case Standing.Average:
                    return new StudentResult { Standing = standing, Pass = true };
                case Standing.SumaCumLaude:
                    return new StudentResult { Standing = standing, Pass = true };
                case Standing.MagnaCumLaude:
                    return new StudentResult { Standing = standing, Pass = true };

                default:
                    return new StudentResult { Standing = standing, Pass = false };
            }
        }
    }
}
