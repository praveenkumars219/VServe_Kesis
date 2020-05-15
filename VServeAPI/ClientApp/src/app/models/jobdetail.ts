import {
  jobcode
} from './jobcode';
import {
  location
} from './location';

export interface jobdetail {
  id ?: string;
  email: string;
  phoneNo: string;
  jobProfile: jobcode;
  location: location;
  jobDescription: string;
  additionalDetails: string;
  userId: string;
  startDate ?: string;
  endDate ?: string;
  isValid: boolean;
  isValidEndDate ?: string;
}
