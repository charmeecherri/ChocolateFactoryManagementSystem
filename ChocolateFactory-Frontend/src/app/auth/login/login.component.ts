import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, RouterModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  username: string = '';
  password: string = '';
  role: string = '';
  errorMessage:string='';

  constructor(private authService: AuthService, private router: Router) {}


  onSubmit() {
   
      const data = {
        username: this.username,
        password: this.password,
        role: this.role,
      };
  
      console.log(data);
     
      this.authService.login(data).subscribe({
        next: (response: any) => {
          console.log('Login Successful:', response);
            // Store JWT token and user details in localStorage
            localStorage.setItem('token', response.token);
            localStorage.setItem('username', data.username);
            localStorage.setItem('role', data.role);
            localStorage.setItem('userId',response.userId.toString());
            console.log('Storing User ID:', response.userId);
  
            // Navigate to the correct dashboard based on role
            if (this.role === 'FactoryManager') {
              this.router.navigate(['/factory-manager']);
            } else if (this.role === 'QualityController') {
              this.router.navigate(['/quality']);
            } else if (this.role === 'ProductionSupervisor') {
              this.router.navigate(['/production-dashboard']);
            }else if (this.role === 'SalesRepresentative') {
              this.router.navigate(['/order-management']); 
            }

              else {
              this.router.navigate(['/']);
            }
        },
        error: (error: any) => {
          console.error('Login Failed:', error);
        },
      });
    
    } 
  }
