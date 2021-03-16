import React, { useState } from 'react';

function SearchBar({ courses, searching, onSubmit }) {
  const [values, setValues] = useState({ course: '', numberOfCourses: '' });

  function onChange(e) {
    setValues({ ...values, [e.target.name]: e.target.value });
  }

  function handleSubmit() {
    onSubmit({
      course: values.course,
      numberOfCourses: +values.numberOfCourses,
    });
  }

  return (
    <div className="row">
      <div className="col-md d-md-flex align-items-center">
        <label htmlFor="enrolled-in">Enrolled in:</label>
        <div className="flex-grow-1 ml-3">
          <select
            className="form-control"
            id="enrolled-in"
            name="course"
            value={values.course}
            onChange={onChange}
          >
            <option value="">Any</option>
            {courses.map((course) => (
              <option key={course.id} value={course.id}>
                {course.title}
              </option>
            ))}
          </select>
        </div>
      </div>

      <div className="col-md d-md-flex align-items-center">
        <label htmlFor="number-of-courses">Number of courses:</label>
        <div className="flex-grow-1 ml-3">
          <select
            className="form-control"
            id="number-of-courses"
            name="numberOfCourses"
            value={values.numberOfCourses}
            onChange={onChange}
          >
            <option value="">Any</option>
            {courses.map((_, index) => (
              <option key={index} value={index + 1}>
                {index + 1}
              </option>
            ))}
          </select>
        </div>
      </div>

      <div className="col-md">
        <button
          className="btn btn-primary"
          disabled={searching}
          onClick={handleSubmit}
        >
          Search
        </button>
      </div>
    </div>
  );
}

export default SearchBar;
