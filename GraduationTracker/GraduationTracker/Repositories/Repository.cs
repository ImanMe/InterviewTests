using System.Linq;

namespace GraduationTracker.Repositories
{
    public class Repository : IRepository
    {
        public Student GetStudent(int id)
        {
            return Seed.GetStudents().FirstOrDefault(s => s.Id == id);
        }

        public Diploma GetDiploma(int id)
        {
            return Seed.GetDiplomas().FirstOrDefault(d => d.Id == id);
        }

        public Requirement GetRequirement(int id)
        {
            return Seed.GetRequirements().FirstOrDefault(r => r.Id == id);
        }
    }
}
