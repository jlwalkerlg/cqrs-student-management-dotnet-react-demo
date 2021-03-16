import axios from 'axios';

export async function getAllStudents() {
  const response = await axios.get('/students');
  return response.data.data;
}

export function unregisterStudent(id) {
  return axios.delete(`/students/${id}`);
}

export async function registerStudent(values) {
  const response = await axios.post('/students', {
    name: values.name,
    email: values.email,
  });

  const id = response.data.data.id;

  const student = {
    id: id,
    name: values.name,
    email: values.email,
    enrollments: [],
  };

  return student;
}

export async function filterStudents(filters) {
  const response = await axios.get('/students', {
    params: {
      numberOfCourses: filters.numberOfCourses || null,
      enrolledIn: filters.course || null,
    },
  });

  return response.data.data;
}

export function updateStudentDetails(studentId, values) {
  return axios.put(`/students/${studentId}`, {
    name: values.name,
    email: values.email,
  });
}

export function disenrollStudent(studentId, courseId) {
  return axios.post(`/students/${studentId}/disenrollments`, {
    courseId: courseId,
    comment: 'Comment!',
  });
}

export function enrollStudent(studentId, courseId, grade) {
  return axios.post(`/students/${studentId}/courses`, {
    courseId: courseId,
    grade: grade,
  });
}

export function transferStudent(studentId, oldCourseId, newCourseId, grade) {
  return axios.post(`/students/${studentId}/transfers`, {
    fromCourseId: oldCourseId,
    toCourseId: newCourseId,
    grade: grade,
  });
}
