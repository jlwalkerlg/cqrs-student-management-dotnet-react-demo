import React, { useReducer } from 'react';

const initialState = {
  courses: [],
  students: [],
};

const store = React.createContext(initialState);

const { Provider } = store;

const StateProvider = ({ children }) => {
  const [state, dispatch] = useReducer((state, action) => {
    switch (action.type) {
      case 'LOAD_COURSES':
        return { ...state, courses: action.courses };
      case 'LOAD_STUDENTS':
        return { ...state, students: action.students };
      case 'UNREGISTER_STUDENT':
        return {
          ...state,
          students: state.students.filter((s) => s.id !== action.studentId),
        };
      case 'REGISTER_STUDENT':
        return {
          ...state,
          students: [action.student, ...state.students],
        };
      case 'UPDATE_STUDENT':
        return {
          ...state,
          students: state.students.map((s) => {
            if (s.id !== action.studentId) return s;
            return {
              ...s,
              name: action.values.name,
              email: action.values.email,
            };
          }),
        };
      case 'DISENROLL_STUDENT':
        return {
          ...state,
          students: state.students.map((s) => {
            if (s.id !== action.studentId) return s;
            return {
              ...s,
              enrollments: s.enrollments.filter(
                (e) => e.course.id !== action.courseId
              ),
            };
          }),
        };
      case 'ENROLL_STUDENT':
        return {
          ...state,
          students: state.students.map((s) => {
            if (s.id !== action.studentId) return s;
            return {
              ...s,
              enrollments: [
                ...s.enrollments,
                {
                  course: state.courses.filter(
                    (c) => c.id === action.courseId
                  )[0],
                  grade: action.grade,
                },
              ],
            };
          }),
        };
      case 'TRANSFER_STUDENT':
        return {
          ...state,
          students: state.students.map((s) => {
            if (s.id !== action.studentId) return s;
            return {
              ...s,
              enrollments: s.enrollments.map((e) => {
                if (e.course.id !== action.oldCourseId) return e;
                return {
                  course: state.courses.filter(
                    (c) => c.id === action.newCourseId
                  )[0],
                  grade: action.grade,
                };
              }),
            };
          }),
        };
      default:
        return state;
    }
  }, initialState);

  return <Provider value={{ state, dispatch }}>{children}</Provider>;
};

export { store, StateProvider as Provider };
