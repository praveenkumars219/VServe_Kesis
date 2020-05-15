import {
  Injectable,
  Inject
} from '@angular/core';
import {
  HttpClient,
  HttpParams
} from '@angular/common/http';
import {
  jobcode
} from '../models/jobcode';
import {
  jobdetail
} from '../models/jobdetail';
import {
  Observable
} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class JobdetailService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {

  }

  saveJobDetails(job: jobdetail) {
    return this.http.post(this.baseUrl + 'api/JobDetail', job);
  }

  updateJobDetails(id: string, job: jobdetail) {
    const options = id ? {
      params: new HttpParams().set('id', id)
    } : {};

    return this.http.post(this.baseUrl + 'api/JobDetail', job, options);
  }

  getJobDetails(userId: string): Observable < jobdetail[] > {
    const options = userId ? {
      params: new HttpParams().set('id', userId)
    } : {};
    return this.http.get < jobdetail[] > (this.baseUrl + 'api/JobDetail', options).pipe();
  }

  getAllJobDetails(): Observable < jobdetail[] > {
    return this.http.get < jobdetail[] > (this.baseUrl + 'api/JobDetail').pipe();
  }


  getJobDetailsByType(jobtype, location): Observable < jobdetail[] > {
    let params = new HttpParams();
    params = params.append('jobtype', jobtype.JobTitle);
    params = params.append('location', location.locationName);
    const options = {
      params: params
    }
    return this.http.get < jobdetail[] > (this.baseUrl + 'api/JobDetail/GetJobByType', options).pipe();
  }

}
