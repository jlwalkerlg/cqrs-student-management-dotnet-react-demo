import React, { useState } from 'react';
import Modal from './Modal';
import EnrollStudentForm from './EnrollStudentForm';

function EnrollStudentModal({ onClose, onSubmit, courses, processing }) {
  const [values, setValues] = useState({
    courseId: '',
    grade: '',
  });

  function onChangeValue(e) {
    setValues({ ...values, [e.target.name]: e.target.value });
  }

  function handleSubmit() {
    onSubmit(values);
  }

  return (
    <Modal
      loading={processing}
      onClose={onClose}
      Header={() => (
        <>
          <h5 className="modal-title">Enroll Student</h5>
          <button
            type="button"
            className="close"
            aria-label="Close"
            onClick={onClose}
          >
            <span aria-hidden="true">&times;</span>
          </button>
        </>
      )}
      Footer={() => (
        <>
          <button type="button" className="btn btn-secondary" onClick={onClose}>
            Cancel
          </button>
          <button
            type="button"
            disabled={processing}
            className="btn btn-success"
            onClick={handleSubmit}
          >
            Enroll
          </button>
        </>
      )}
    >
      <EnrollStudentForm
        onSubmit={handleSubmit}
        courses={courses}
        values={values}
        onChange={onChangeValue}
      />
    </Modal>
  );
}

export default EnrollStudentModal;
