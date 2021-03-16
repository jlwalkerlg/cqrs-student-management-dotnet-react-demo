import React, { useState, useContext, useEffect } from 'react';
import { store } from './store';
import StudentTable from './StudentTable';
import SearchBar from './SearchBar';
import { getAllCourses } from './repositories/courses';
import {
  getAllStudents,
  unregisterStudent,
  registerStudent,
  filterStudents,
  updateStudentDetails,
  disenrollStudent,
  enrollStudent,
  transferStudent,
} from './repositories/students';
import RegisterStudentModal from './RegisterStudentModal';
import EditStudentModal from './EditStudentModal';
import EnrollStudentModal from './EnrollStudentModal';
import DisenrollStudentModal from './DisenrollStudentModal';
import TransferModal from './TransferModal';

function App() {
  const {
    state: { courses, students },
    dispatch,
  } = useContext(store);
  const [loading, setLoading] = useState(true);
  const [searching, setSearching] = useState(false);
  const [processingStudents, setProcessingStudents] = useState(false);
  const [selectedStudent, setSelectedStudent] = useState(null);
  const [oldCourse, setOldCourse] = useState(null);
  const [registerStudentModalActive, setRegisterStudentModalActive] = useState(
    false
  );
  const [editStudentModalActive, setEditStudentModalActive] = useState(false);
  const [disenrollModalActive, setDisenrollStudentModalActive] = useState(
    false
  );
  const [enrollModalActive, setEnrollModalActive] = useState(false);
  const [transferModalActive, setTransferModalActive] = useState(false);

  useEffect(() => {
    async function loadInitialData() {
      setLoading(true);
      const [courses, students] = await Promise.all([
        getAllCourses(),
        getAllStudents(),
      ]);
      dispatch({ type: 'LOAD_COURSES', courses });
      dispatch({ type: 'LOAD_STUDENTS', students });
      setLoading(false);
    }

    loadInitialData();
  }, [dispatch]);

  async function onUnregisterStudentClick() {
    if (!selectedStudent || processingStudents) return;
    setProcessingStudents(true);
    await unregisterStudent(selectedStudent.id);
    dispatch({ type: 'UNREGISTER_STUDENT', studentId: selectedStudent.id });
    setSelectedStudent(null);
    setProcessingStudents(false);
  }

  function onRegisterStudentClick() {
    setRegisterStudentModalActive(true);
  }

  async function onRegisterStudentSubmit(values) {
    if (processingStudents) return;
    setProcessingStudents(true);
    const student = await registerStudent(values);
    dispatch({ type: 'REGISTER_STUDENT', student });
    closeRegisterStudentModal();
    setSelectedStudent(null);
    setProcessingStudents(false);
  }

  function closeRegisterStudentModal() {
    setRegisterStudentModalActive(false);
  }

  async function onSearch(values) {
    if (searching) return;
    setSearching(true);
    const students = await filterStudents(values);
    dispatch({ type: 'LOAD_STUDENTS', students });
    setSearching(false);
  }

  function onEditStudentClick() {
    setEditStudentModalActive(true);
  }

  async function onEditStudentSubmit(values) {
    if (processingStudents) return;
    setProcessingStudents(true);
    await updateStudentDetails(selectedStudent.id, values);
    dispatch({ type: 'UPDATE_STUDENT', studentId: selectedStudent.id, values });
    closeEditStudentModal();
    setSelectedStudent(null);
    setProcessingStudents(false);
  }

  function closeEditStudentModal() {
    setEditStudentModalActive(false);
  }

  function onDisenrollClick(student, course) {
    if (processingStudents) return;
    setSelectedStudent(student);
    setOldCourse(course);
    setDisenrollStudentModalActive(true);
  }

  async function onDisenrollStudentSubmit({ comment }) {
    if (processingStudents) return;
    setProcessingStudents(true);
    await disenrollStudent(selectedStudent.id, oldCourse.id, comment);
    dispatch({
      type: 'DISENROLL_STUDENT',
      studentId: selectedStudent.id,
      courseId: oldCourse.id,
      comment,
    });
    closeDisenrollStudentModal();
    setSelectedStudent(null);
    setProcessingStudents(false);
  }

  function closeDisenrollStudentModal() {
    setDisenrollStudentModalActive(false);
  }

  function onEnrollClick(student) {
    setSelectedStudent(student);
    setEnrollModalActive(true);
  }

  async function onEnrollStudentSubmit({ courseId, grade }) {
    if (processingStudents) return;
    setProcessingStudents(true);
    await enrollStudent(selectedStudent.id, courseId, grade);
    dispatch({
      type: 'ENROLL_STUDENT',
      studentId: selectedStudent.id,
      courseId,
      grade,
    });
    closeEnrollStudentModal();
    setSelectedStudent(null);
    setProcessingStudents(false);
  }

  function closeEnrollStudentModal() {
    setEnrollModalActive(false);
  }

  function onTransferClick(student, course) {
    if (processingStudents) return;
    setSelectedStudent(student);
    setOldCourse(course);
    setTransferModalActive(true);
  }

  async function onTransferStudentSubmit({ courseId, grade }) {
    if (processingStudents) return;
    setProcessingStudents(true);
    await transferStudent(selectedStudent.id, oldCourse.id, courseId, grade);
    dispatch({
      type: 'TRANSFER_STUDENT',
      studentId: selectedStudent.id,
      oldCourseId: oldCourse.id,
      newCourseId: courseId,
      grade,
    });
    closeTransferModal();
    setSelectedStudent(null);
    setProcessingStudents(false);
  }

  function closeTransferModal() {
    setTransferModalActive(false);
  }

  return (
    <div className="container py-4">
      <h1>Student Management</h1>

      {loading && <div>Loading data...</div>}

      {!loading && (
        <>
          <div className="mt-4">
            <SearchBar
              courses={courses}
              searching={searching || processingStudents}
              onSubmit={onSearch}
            />

            <div className="mt-3">
              <button
                className="btn btn-success"
                onClick={onRegisterStudentClick}
              >
                Register Student
              </button>
              <button
                className="btn btn-info ml-3"
                disabled={!selectedStudent}
                onClick={onEditStudentClick}
              >
                Edit Personal Info
              </button>
              <button
                className="btn btn-danger ml-3"
                disabled={!selectedStudent || processingStudents}
                onClick={onUnregisterStudentClick}
              >
                Unregister Student
              </button>
            </div>
          </div>

          <div className="mt-4">
            <StudentTable
              students={students}
              processing={processingStudents}
              searching={searching}
              selectedStudentId={selectedStudent ? selectedStudent.id : null}
              onSelectStudent={setSelectedStudent}
              onDisenroll={onDisenrollClick}
              onEnroll={onEnrollClick}
              onTransfer={onTransferClick}
            />
          </div>

          {registerStudentModalActive && (
            <RegisterStudentModal
              processing={processingStudents}
              onSubmit={onRegisterStudentSubmit}
              onClose={closeRegisterStudentModal}
            />
          )}

          {editStudentModalActive && (
            <EditStudentModal
              student={selectedStudent}
              processing={processingStudents}
              onSubmit={onEditStudentSubmit}
              onClose={closeEditStudentModal}
            />
          )}

          {disenrollModalActive && (
            <DisenrollStudentModal
              processing={processingStudents}
              onSubmit={onDisenrollStudentSubmit}
              onClose={closeDisenrollStudentModal}
            />
          )}

          {enrollModalActive && (
            <EnrollStudentModal
              processing={processingStudents}
              courses={courses}
              onSubmit={onEnrollStudentSubmit}
              onClose={closeEnrollStudentModal}
            />
          )}

          {transferModalActive && (
            <TransferModal
              processing={processingStudents}
              courses={courses}
              onSubmit={onTransferStudentSubmit}
              onClose={closeTransferModal}
            />
          )}
        </>
      )}
    </div>
  );
}

export default App;
