import axios from 'axios';

export async function getAllCourses() {
  const response = await axios.get('/courses');
  return response.data.data;
}
