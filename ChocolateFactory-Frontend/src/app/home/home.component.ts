import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterModule,CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  title = 'ChocolateFactoryManagement';

  menuItems = [
    { name: 'Raw Materials', route: '/login' },
    { name: 'Production', route: '/login' },
    { name: 'Recipes', route: '/login' },
    { name: 'Quality', route: '/login' },
    { name: 'Packaging', route: '/login' },
    { name: 'Sales and Orders', route: '/login' },
  ];

  constructor(private router: Router) {}

  navigateTo(route: string): void {
    this.router.navigate([route]);
  }




}
