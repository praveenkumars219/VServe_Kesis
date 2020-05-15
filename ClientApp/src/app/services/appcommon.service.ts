import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { AuthService } from './auth.service';
import { environment } from 'src/environments/environment';
import { jobcode } from '../models/jobcode';
import { location } from '../models/location';
import { isString, isNullOrUndefined } from 'util';

@Injectable({
  providedIn: 'root'
})
export class AppcommonService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private authService: AuthService) {

  }

  searchLocations(location: string) {
    const locationKey = isString(location) && !isNullOrUndefined(location) ? location.trim() : location;
    const options = locationKey ?
    { params: new HttpParams().set('searchkey', locationKey) } : {};

    return this.http.get<location[]>(this.baseUrl + 'api/commonservice/GetLocations', options);
  }

  searchJobType(work: string) {
    const url = 'https://vserve-app.firebaseio.com/Jobtitles/-M3dTrMN2KOabAzBSMfD.json';
    const key = this.authService.currentUserV;
    const options =
    { params: new HttpParams().set('auth', environment.firebasekey) };
    return this.http.get<jobcode[]>(url, options);
  }

}
