import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-product',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './product.component.html',
  styleUrl: './product.component.css'
})
export class ProductComponent {
  products: any[] = [];
  newProductName: string = '';
  isFactoryManager: boolean = false;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.checkUserRole();
    this.fetchProducts();
  }

  checkUserRole(): void {
    const role = localStorage.getItem('role');
    this.isFactoryManager = role === 'FactoryManager';
  }

  fetchProducts(): void {
    this.http.get<any[]>(`https://localhost:7214/api/Product`).subscribe({next:
      (response: any) => {
        this.products = response;
      },
      error:(error) => {
        console.error('Error fetching products:', error);
        alert('Failed to fetch products.');
      }
    }
    );
  }

  addProduct(): void {
    if (!this.newProductName.trim()) {
      alert('Please enter a valid product name.');
      return;
    }
    const apiUrl = `https://localhost:7214/api/Product?name=${encodeURIComponent(this.newProductName)}`;
    this.http.post(apiUrl, null, { responseType: 'text' }).subscribe({next:
      (response: string) => {
        alert(response);
        this.newProductName = '';
        this.fetchProducts();
      },
      error:(error) => {
        console.error('Error adding product:', error);
        alert('Failed to add product.');
      }
    }
    );
  }

  deleteProduct(productId: number): void {
    if (!confirm('Are you sure you want to delete this product?')) return;

    this.http.delete(`/api/Product?id=${productId}`, { responseType: 'text' }).subscribe(
      (response: string) => {
        alert(response);
        this.fetchProducts();
      },
      (error) => {
        console.error('Error deleting product:', error);
        alert('Failed to delete product.');
      }
    );
  }
}


