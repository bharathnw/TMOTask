import api from './api';

export const getBranches = async () => {
  const response = await api.get('branch');
  return response.data;
};
