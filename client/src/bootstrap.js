import axios from 'axios';

axios.defaults.baseURL = 'http://localhost:5000';
axios.interceptors.response.use(
  (response) => response,
  (error) => {
    console.dir(error);
    throw error;
  }
);
