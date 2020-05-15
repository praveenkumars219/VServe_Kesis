import { jobdetail } from './jobdetail';

export interface Assignment {
    id: string;
    assignedJob: jobdetail;
    workStatus: number;
    assignedUserId: string;
    amount?: number;
}
