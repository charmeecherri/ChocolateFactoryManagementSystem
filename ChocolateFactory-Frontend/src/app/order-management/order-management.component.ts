import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router'; 
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { environment } from '../../environments/environment';


@Component({
  selector: 'app-order-management',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './order-management.component.html',
  styleUrl: './order-management.component.css'
})
export class OrderManagementComponent {
  isAuthorized: boolean = false;
  newOrder = {
    customerId: 0,
    productId: 0,
    quantity: 0,
    orderDate: '',
    deliveryDate: '',
    status: '',
  };
  orders: any[] = [];
  availableProducts:any[] =[];
  selectedProductId:number|null=null;

  constructor(private http: HttpClient, private router: Router) {}
  ngOnInit(): void {
    this.checkUserRoleAnDFetchData();
    this.fetchProducts();
  }

  checkUserRoleAnDFetchData(): void {
    const userRole = localStorage.getItem('role');
    console.log('Retrieved role from localStorage:',userRole );
    if (userRole === 'SalesRepresentative' || userRole === 'FactoryManager') {
      this.isAuthorized = true;
      console.log('Access granted for role:',userRole);
      this.fetchOrders();
    } else {
      this.isAuthorized = false;
       // Provide detailed debugging information
       if (!userRole) {
        console.warn('Access denied: No role found in localStorage.');
    } else {
        console.warn(
            `Access denied: Retrieved role "${userRole}" does not match "SalesRepresentative".`
        );
    }
      alert('Access denied: Only Sales Representatives can access this page.');
      this.router.navigate(['/login']);
    }
  }

  fetchOrders(): void {
    this.http.get(`${environment.apiUrl}Order`).subscribe({next:
      (data: any) => {
        this.orders = data;
      },
      error:(error) => {
        console.error('Error fetching orders:', error);
        alert('Failed to fetch orders.');
      }
    }
    );
  }
  fetchProducts(): void {
    this.http.get(`${environment.apiUrl}Product`).subscribe({
      next: (response: any) => {
        this.availableProducts = response;
      },
      error: (error) => {
        console.error('Error fetching products:', error);
        alert('Failed to fetch available products.');
      },
    });
  }

  onSubmit(): void {

    if (this.selectedProductId===null || this.selectedProductId===undefined) {
      alert('Please select a product.');
      return;
    }
    this.newOrder.productId = this.selectedProductId;

    this.http.post(`${environment.apiUrl}Order`, this.newOrder, { responseType: 'text' }).subscribe({
      next: (response) => {
        alert('Order created successfully.');
        this.fetchOrders();
        this.resetForm();
      },
      error: (error) => {
        console.error('Error creating order:', error);
        alert('Failed to create order.');
      },
    });
  }

  resetForm(): void {
    this.newOrder = {
      customerId: 0,
      productId: 0,
      quantity: 0,
      orderDate: '',
      deliveryDate: '',
      status: '',
    };
    this.selectedProductId = null; // Reset dropdown selection
  }
}


