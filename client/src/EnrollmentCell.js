import React from 'react';

function EnrollmentCell({
  enrollment,
  student,
  onDisenroll,
  onEnroll,
  onTransfer,
}) {
  function handleDisenroll() {
    onDisenroll(student, enrollment.course);
  }

  function handleEnroll() {
    onEnroll(student);
  }

  function handleTransfer() {
    onTransfer(student, enrollment.course);
  }

  if (!enrollment) {
    return (
      <td>
        <button
          className="btn btn-sm btn-outline-success"
          onClick={handleEnroll}
        >
          Enroll
        </button>
      </td>
    );
  }

  return (
    <td>
      <div>{enrollment.course.title}</div>
      <div>Grade: {enrollment.grade}</div>
      <div>Credits: {enrollment.course.credits}</div>
      <div>
        <button
          className="btn btn-sm btn-outline-info"
          onClick={handleTransfer}
        >
          Transfer
        </button>
        <button
          className="btn btn-sm btn-outline-danger ml-2"
          onClick={handleDisenroll}
        >
          Disenroll
        </button>
      </div>
    </td>
  );
}

export default EnrollmentCell;
