import React from 'react';

function DisenrollStudentForm({ onSubmit, values, onChange }) {
  return (
    <form onSubmit={onSubmit}>
      <div className="form-group row">
        <label htmlFor="comment" className="col-sm-2 col-form-label">
          Comment:
        </label>
        <div className="col-sm-10">
          <input
            type="text"
            className="form-control"
            id="comment"
            name="comment"
            value={values.comment}
            onChange={onChange}
          />
        </div>
      </div>
    </form>
  );
}

export default DisenrollStudentForm;
