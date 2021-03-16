import React from 'react';
import Spinner from './Spinner';
import EnrollmentCell from './EnrollmentCell';

function StudentTable({
  students,
  processing,
  searching,
  selectedStudentId,
  onSelectStudent,
  onDisenroll,
  onEnroll,
  onTransfer,
}) {
  function selectStudent(student) {
    onSelectStudent(student);
  }

  return (
    <div style={{ position: 'relative' }}>
      <table className="table">
        <thead>
          <tr>
            <th>Name</th>
            <th>Email</th>
            <th>First Course</th>
            <th>Second Course</th>
          </tr>
        </thead>
        <tbody>
          {students.map((student) => {
            const enrollment1 = student.enrollments[0];
            const enrollment2 = student.enrollments[1];

            return (
              <tr
                key={student.id}
                className={
                  selectedStudentId === student.id ? 'bg-light' : undefined
                }
                onClick={() => selectStudent(student)}
              >
                <td>{student.name}</td>
                <td>{student.email}</td>
                <EnrollmentCell
                  enrollment={enrollment1}
                  student={student}
                  onDisenroll={onDisenroll}
                  onEnroll={onEnroll}
                  onTransfer={onTransfer}
                />
                <EnrollmentCell
                  enrollment={enrollment2}
                  student={student}
                  onDisenroll={onDisenroll}
                  onEnroll={onEnroll}
                  onTransfer={onTransfer}
                />
              </tr>
            );
          })}
        </tbody>
      </table>

      {(processing || searching) && <Spinner />}
    </div>
  );
}

export default StudentTable;
