using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace GraduationTracker.Tests.Unit
{
    [TestClass]
    public class GraduationTrackerTests
    {
        private Diploma _diploma;
        private GraduationTracker _sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _sut = new GraduationTracker();

            _diploma = new Diploma
            {
                Id = 1,
                Credits = 4,
                Requirements = new[] { 100, 102, 103, 104 }
            };
        }

        [TestMethod]
        public void ShouldFailAndStandRemedial()
        {
            var student = new Student
            {
                Id = 3,
                Courses = new[]
                {
                    new Course {Id = 1, Name = "Math", Mark = 23},
                    new Course {Id = 2, Name = "Science", Mark = 33},
                    new Course {Id = 3, Name = "Literature", Mark = 44},
                    new Course {Id = 4, Name = "Physichal Education", Mark = 70}
                }
            };

            var result = _sut.HasGraduated(_diploma, student);

            Assert.AreEqual(Standing.Remedial, result.Standing);
            Assert.IsFalse(result.Pass);
        }

        [TestMethod]
        public void ShouldPassAndStandAverage()
        {
            var student = new Student
            {
                Id = 3,
                Courses = new[]
                {
                    new Course {Id = 1, Name = "Math", Mark = 66},
                    new Course {Id = 2, Name = "Science", Mark = 70},
                    new Course {Id = 3, Name = "Literature", Mark = 60},
                    new Course {Id = 4, Name = "Physichal Education", Mark = 70}
                }
            };

            var result = _sut.HasGraduated(_diploma, student);

            Assert.AreEqual(Standing.Average, result.Standing);
            Assert.IsTrue(result.Pass);
        }

        [TestMethod]
        public void ShouldPassAndStandSumaCumLaude()
        {
            var student = new Student
            {
                Id = 3,
                Courses = new[]
                {
                    new Course {Id = 1, Name = "Math", Mark = 80},
                    new Course {Id = 2, Name = "Science", Mark = 75},
                    new Course {Id = 3, Name = "Literature", Mark = 90},
                    new Course {Id = 4, Name = "Physichal Education", Mark = 95}
                }
            };

            var result = _sut.HasGraduated(_diploma, student);

            Assert.AreEqual(Standing.SumaCumLaude, result.Standing);
            Assert.IsTrue(result.Pass);
        }

        [TestMethod]
        public void ShouldStandMagnaCumLaude()
        {
            var student = new Student
            {
                Id = 3,
                Courses = new[]
                {
                    new Course {Id = 1, Name = "Math", Mark = 90},
                    new Course {Id = 2, Name = "Science", Mark = 95},
                    new Course {Id = 3, Name = "Literature", Mark = 100},
                    new Course {Id = 4, Name = "Physichal Education", Mark = 95}
                }
            };

            var result = _sut.HasGraduated(_diploma, student);

            Assert.AreEqual(Standing.MagnaCumLaude, result.Standing);
            Assert.IsTrue(result.Pass);
        }
        
        [TestMethod]
        public void TestHasCredits()
        {
            var students = new[]
            {
               new Student
               {
                   Id = 1,
                   Courses = new[]
                   {
                        new Course{Id = 1, Name = "Math", Mark=95 },
                        new Course{Id = 2, Name = "Science", Mark=95 },
                        new Course{Id = 3, Name = "Literature", Mark=95 },
                        new Course{Id = 4, Name = "Physichal Education", Mark=95 }
                   }
               },
               new Student
               {
                   Id = 2,
                   Courses = new[]
                   {
                        new Course{Id = 1, Name = "Math", Mark=80 },
                        new Course{Id = 2, Name = "Science", Mark=80 },
                        new Course{Id = 3, Name = "Literature", Mark=80 },
                        new Course{Id = 4, Name = "Physichal Education", Mark=80 }
                   }
               },
                new Student
                {
                   Id = 3,
                   Courses = new[]
                   {
                        new Course{Id = 1, Name = "Math", Mark=50 },
                        new Course{Id = 2, Name = "Science", Mark=50 },
                        new Course{Id = 3, Name = "Literature", Mark=50 },
                        new Course{Id = 4, Name = "Physichal Education", Mark=50 }
                   }
                },
                new Student
                {
                   Id = 4,
                   Courses = new[]
                   {
                        new Course{Id = 1, Name = "Math", Mark=40 },
                        new Course{Id = 2, Name = "Science", Mark=40 },
                        new Course{Id = 3, Name = "Literature", Mark=40 },
                        new Course{Id = 4, Name = "Physichal Education", Mark=40 }
                   }
                }            
        };

            var graduated = new List<StudentResult>();

            foreach (var student in students)
            {
                graduated.Add(_sut.HasGraduated(_diploma, student));
            }

            Assert.IsTrue(graduated.Any());
        }
    }
}
