import { Component } from '@angular/core';

@Component({
  selector: 'app-main-content',
  standalone: true,
  imports: [],
  templateUrl: './main-content.component.html',
  styleUrl: './main-content.component.css'
})
export class MainContentComponent {

  menuItems = [
    { name: 'Raw Materials', route: '/login' },
    { name: 'Production', route: '/login' },
    { name: 'Recipes', route: '/login' },
    { name: 'Quality', route: '/login' },
    { name: 'Packaging', route: '/login' },
    { name: 'Sales and Orders', route: '/login' },
  ];

  navigateTo(route: string): void {
    // Navigate logic here
  }

}
