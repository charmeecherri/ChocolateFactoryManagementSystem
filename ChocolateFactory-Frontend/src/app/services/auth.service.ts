import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = 'https://localhost:7214/api/Auth';


  constructor(private http: HttpClient) {}

  
  login(credentials: { username: string; password: string; role:string;}): Observable<any> {
    return this.http.post(`${this.apiUrl}/login`, credentials);
  }

  register(user: { name :string;username: string; password: string; email: string; role: string }): Observable<any> {
    
    return this.http.post(`${this.apiUrl}/register`, user);
  }

  storeUserDetails(response: any) {
    localStorage.setItem('token', response.token);
    localStorage.setItem('username', response.username);
    localStorage.setItem('role', response.role);
    localStorage.setItem('userId', response.userId.toString()); // Store userId
    console.log('Storing User ID:', response.userId);
  }
  






}

console.log("")
