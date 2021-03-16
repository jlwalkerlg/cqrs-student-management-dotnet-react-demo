# Domain Objects

Course (aggregate root)

- Id
- Title (CourseTitle)
- Credits (Credits)

Student (aggregate root)

- Id
- Name (StudentName)
- Email (EmailAddress)
- Enrollments (IList<Enrollment>)

* Enroll(Course, Grade)
* Disenroll(Course)
* Transfer(Course from, Course to, Grade grade)

Enrollment (entity)

- Id
- Grade (Grade)
- Course (Id)

* UpdateGrade(Grade)

# Notes

- If a student disenrolls from a course, it deletes the enrollment.
- If a student transfers to a course it is already enrolled in, it updates the enrollment grade.
- If a student transfers to a different course, it disenrolls from the current one and enrolls in the new one.
- A student can not be enrolled in more than 2 courses.
- A student should be notified when they register, unregister, enroll, disenroll, or transfer.
