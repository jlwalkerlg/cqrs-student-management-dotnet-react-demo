using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using StudentManagement.Application.Students;
using StudentManagement.Application.Courses;

namespace StudentManagement.Infrastructure.Data.Dapper.Repositories
{
    public class DapperStudentDtoRepository : DapperRepository, IStudentDtoRepository
    {
        public DapperStudentDtoRepository(IDbConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }

        private class StudentCourseRow
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public Guid CourseId { get; set; }
            public string Title { get; set; }
            public int Credits { get; set; }
            public string Grade { get; set; }
        }

        public async Task<List<StudentDto>> GetStudents(GetStudentsFilter filter)
        {
            using (var conn = Connection)
            {
                var query = @"
                    SELECT
                        s.Id,
                        s.Name,
                        s.Email,
                        e.Grade,
                        c.Id as CourseId,
                        c.Title,
                        c.Credits
                    FROM
                        Students s
                        LEFT JOIN Enrollment e ON s.Id = e.StudentId
                        LEFT JOIN Courses c ON e.CourseId = c.Id";

                var rows = await conn.QueryAsync<StudentCourseRow>(query);

                var students = new Dictionary<Guid, StudentDto>();

                foreach (var row in rows)
                {
                    if (!students.ContainsKey(row.Id))
                    {
                        var student = new StudentDto
                        {
                            Id = row.Id,
                            Name = row.Name,
                            Email = row.Email,
                        };
                        students.Add(row.Id, student);

                        if (row.CourseId != Guid.Empty)
                        {
                            student.Enrollments.Add(new EnrollmentDto
                            {
                                Course = new CourseDto
                                {
                                    Id = row.CourseId,
                                    Title = row.Title,
                                    Credits = row.Credits,
                                },
                                Grade = row.Grade
                            });
                        }
                    }
                    else
                    {
                        students[row.Id].Enrollments.Add(new EnrollmentDto
                        {
                            Course = new CourseDto
                            {
                                Id = row.CourseId,
                                Title = row.Title,
                                Credits = row.Credits,
                            },
                            Grade = row.Grade
                        });
                    }
                }

                var result = students.Values.ToList();

                if (filter.NumberOfCourses.HasValue)
                {
                    result = result.Where(x => x.Enrollments.Count == filter.NumberOfCourses.Value).ToList();
                }

                if (filter.EnrolledIn.HasValue)
                {
                    result = result.Where(x =>
                        x.Enrollments.Any(e => e.Course.Id == filter.EnrolledIn.Value)).ToList();
                }

                return result;
            }
        }
    }
}
