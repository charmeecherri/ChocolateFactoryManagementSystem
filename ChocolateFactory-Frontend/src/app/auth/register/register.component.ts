import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule,RouterModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  name: string = '';
  email: string = '';
  username: string = '';
  password: string = '';
  confirmPassword: string = '';
  role: string = '';
  constructor(private authService: AuthService, private router: Router) {}  // Inject Router here

  

  onSubmit() {
    // Basic validation
    if (this.password !== this.confirmPassword) {
      console.error('Passwords do not match!');
      return;
    }

    // Check if all fields are filled
    if (!this.name || !this.email || !this.username || !this.password || !this.role) {
      console.error('All fields are required!');
      return;
    }

    // Prepare the user data
    const userData = {
      name: this.name,
      email: this.email,
      username: this.username,
      password: this.password,
      role: this.role,
    };

    // Call the register method in AuthService
    this.authService.register(userData).subscribe({
      next: (response) => {
        alert('Registration successful!');
        this.router.navigate(['/login']); // Redirect to login page after successful registration
      },
      error: (error) => {
        console.error('Registration Failed:', error);
        alert('Registration failed. Please try again later.');
      },
    });
  }

}
