import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { User } from '../models/User';
import { UserDetail } from '../models/UserDetail';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  createuser(user:User){
   return this.http.post(this.baseUrl + 'api/User',user).pipe(
      map(u=>{ 
        return u;
      })
    )
  }

  updateuser(user:User){
    return this.http.put(this.baseUrl + 'api/User',user).pipe(
       map(u=>{ 
         return u;
       })
     )
   }

   getuser(userId){
    return this.http.get<User>(this.baseUrl + 'api/User/'+userId).pipe(
       map(u=>{ 
         return u;
       })
     )
   }

   getuserFromDb(userId){
    const options = userId ? {
      params: new HttpParams().set('id', userId)
    } : {};
    return this.http.get<UserDetail>(this.baseUrl + 'api/userdetails/getbyUserId',options).pipe(
       map(u=>{ 
         return u;
       })
     )
   }

   updateUserDetails(user){
    return this.http.post(this.baseUrl + 'api/userdetails',user).pipe(
       map(u=>{ 
         console.log(u);
         return u;
       })
     )
   }

}
