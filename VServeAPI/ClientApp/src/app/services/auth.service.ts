import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs';
import { User } from '../models/User';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private currentUserSubject: BehaviorSubject<User>;
  public currentUser: Observable<User>;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { 
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserV(): User {
    return this.currentUserSubject.value;
}

  Login(userid,password){
    const user:User = {email:userid, password:password,displayName:userid,confirmPassword:password };
    return this.http.post<User>(this.baseUrl + 'api/Login',user).pipe(
      map(u=>{ 
        localStorage.setItem('currentUser', JSON.stringify(u));
        this.currentUserSubject.next(u);
        return u;
      })
    );
  }

  logout() {
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
}

}