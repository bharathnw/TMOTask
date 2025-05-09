import { SellerReport } from '../types/SellerReport';
import api from './api';



export const getSellerPerformanceReportWithBranch = async (branch: string) => {
    const response = await api.get<SellerReport[]>(`PerformanceReport/${branch}`);
    return response.data;
};

export const getSellerPerformanceReport = async () => {
    const response = await api.get<SellerReport[]>('PerformanceReport/');
    return response.data;
};