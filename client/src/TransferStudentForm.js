import React from 'react';

const grades = ['A', 'B', 'C', 'D', 'F'];

function TransferStudentForm({ onSubmit, courses, values, onChange }) {
  return (
    <form onSubmit={onSubmit}>
      <div className="form-group row">
        <label htmlFor="courseId" className="col-sm-2 col-form-label">
          Course:
        </label>
        <div className="col-sm-10">
          <select
            id="courseId"
            className="form-control"
            name="courseId"
            value={values.courseId}
            onChange={onChange}
          >
            <option value="" disabled></option>
            {courses.map((course) => (
              <option key={course.id} value={course.id}>
                {course.title}
              </option>
            ))}
          </select>
        </div>
      </div>

      <div className="form-group row">
        <label htmlFor="grade" className="col-sm-2 col-form-label">
          Grade:
        </label>
        <div className="col-sm-10">
          <select
            id="grade"
            className="form-control"
            name="grade"
            value={values.grade}
            onChange={onChange}
          >
            <option value="" disabled></option>
            {grades.map((grade) => (
              <option key={grade} value={grade}>
                {grade}
              </option>
            ))}
          </select>
        </div>
      </div>
    </form>
  );
}

export default TransferStudentForm;
