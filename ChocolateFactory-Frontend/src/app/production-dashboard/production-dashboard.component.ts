import { Component,OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-production-dashboard',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './production-dashboard.component.html',
  styleUrl: './production-dashboard.component.css'
})
export class ProductionDashboardComponent  implements OnInit{
  isProductionSupervisor: boolean = false;
  isLoggedOut:boolean=false;

  constructor(private router: Router) {}

  ngOnInit(): void {
    this.checkUserRole();
  }

  checkUserRole(): void {
    const role = localStorage.getItem('role');
    if (role === 'ProductionSupervisor') {
      this.isProductionSupervisor = true;
    } else {
      this.isProductionSupervisor = false;
      this.router.navigate(['/login']);
    }
  }

  logout(): void {
   
    localStorage.removeItem('role');

    // Redirect to login page
    this.router.navigate(['/login']);
    this.isLoggedOut=true;
  }


  navigateTo(route: string): void {
    this.router.navigate([route]);
  }
}
