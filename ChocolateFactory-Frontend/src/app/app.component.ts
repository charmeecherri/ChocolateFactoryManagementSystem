import { Component } from '@angular/core';
import { RouterOutlet,Router } from '@angular/router';
import { HomeComponent } from './home/home.component';

import { CommonModule } from '@angular/common';
import { FooterComponent } from './footer/footer.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [HomeComponent,RouterOutlet,CommonModule,FooterComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  isLoggedIn:boolean = false;

  constructor() {
    this.isLoggedIn = !!localStorage.getItem('token');
  }
}



