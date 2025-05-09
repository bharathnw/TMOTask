import axios from 'axios';

const api = axios.create({
  baseURL: "https://localhost:5000/api/",
  headers: {
    'Content-Type': 'application/json'
  },
});

api.interceptors.request.use(
  (config) => {
    return config;
  },
  (error) => Promise.reject(error)
);

export default api;