import { Address } from "./address";

export interface UserDetail {
    id: string;
    userId: string;
    firstName: string;
    lastName: string;
    dateofBirth: Date;
    gender: string;
    location: string;
    languages?: string[];
    address?: Address;
    skills?: string[];
    profession: string;
    email: string;
    phone: string;
    description: string;
  }