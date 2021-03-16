import React, { useState } from 'react';
import Modal from './Modal';
import RegisterStudentForm from './RegisterStudentForm';

function RegisterStudentModal({ onClose, onSubmit, processing }) {
  const [values, setValues] = useState({
    name: '',
    email: '',
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
          <h5 className="modal-title">Register Student</h5>
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
            Register
          </button>
        </>
      )}
    >
      <RegisterStudentForm
        onSubmit={handleSubmit}
        values={values}
        onChange={onChangeValue}
      />
    </Modal>
  );
}

export default RegisterStudentModal;
