import React from 'react';

function EditStudentForm({ onSubmit, values, onChange }) {
  return (
    <form onSubmit={onSubmit}>
      <div className="form-group row">
        <label htmlFor="name" className="col-sm-2 col-form-label">
          Name:
        </label>
        <div className="col-sm-10">
          <input
            type="text"
            className="form-control"
            id="name"
            name="name"
            value={values.name}
            onChange={onChange}
          />
        </div>
      </div>

      <div className="form-group row">
        <label htmlFor="email" className="col-sm-2 col-form-label">
          Email:
        </label>
        <div className="col-sm-10">
          <input
            type="email"
            className="form-control"
            id="email"
            name="email"
            value={values.email}
            onChange={onChange}
          />
        </div>
      </div>
    </form>
  );
}

export default EditStudentForm;
