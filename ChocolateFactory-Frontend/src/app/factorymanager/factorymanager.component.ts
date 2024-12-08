import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ProductComponent } from '../product/product.component';
import { OrderManagementComponent } from '../order-management/order-management.component';
import { ProductionPlanningComponent } from '../production-planning/production-planning.component';
import { PackageSectionComponent } from '../package-section/package-section.component';

@Component({
  selector: 'app-factorymanager',
  standalone: true,
  imports: [CommonModule,RouterModule,FormsModule,OrderManagementComponent,ProductionPlanningComponent,ProductComponent,PackageSectionComponent],
  templateUrl: './factorymanager.component.html',
  styleUrl: './factorymanager.component.css'
})
export class FactorymanagerComponent {
    isLoggedOut=false;
  
    menuItems = [
      {name:'products',route:'product'},
      {name:'Orders', route:'order-management'},
      { name:'Raw Materials', route: 'raw-materials' },
      { name:'Production', route: 'production-planning' },
      { name:'Recipes', route: 'recipe-batch-management' },
      { name:'Quality', route: 'quality' },
      { name:'Packaging', route:'package'},
    ];

    constructor(private router:Router){}
    navigateTo(route: string) {
      this.router.navigate([route],{relativeTo:this.router.routerState.root});
    }

  logout() {
      // Clear session storage or local storage
      localStorage.clear();
      this.isLoggedOut = true;
      this.router.navigate(['/login']);
  }
}

