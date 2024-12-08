import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-package-section',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './package-section.component.html',
  styleUrl: './package-section.component.css'
})
export class PackageSectionComponent {

  approvedProducts: any[] = [];
  selectedProduct: any = null;
  packagingForm: any = {
    companyName: '',
    quantity: '',
    packagingDate: '',
    expiryDate: '',
    warehouseLocation: ''
  };
  finishedGoods: any[] = [];
  isFactoryManager: boolean = false;
  
  constructor(private http: HttpClient) {}
  
  ngOnInit():void {
    this.checkUserRoleAndFetchData();
  }

  checkUserRoleAndFetchData(): void {
    const role = localStorage.getItem('role');
    console.log('User ID:', localStorage.getItem('userId'));

    if (role === 'FactoryManager') {
      this.isFactoryManager = true;
      console.log('Access granted: Factory Manager role detected.');
      this.fetchApprovedProducts();
      this.fetchFinishedGoods();
    } else {
      this.isFactoryManager = false;
      console.warn('Access denied: Only Factory Managers can access this component.');
      alert('You are not authorized to access this component. Redirecting to the homepage...');
    }
  }
  
  fetchApprovedProducts() {
    this.http.get('https://localhost:7214/api/Quality').subscribe({
      next: (response: any) => {
        this.approvedProducts = response.filter((product:any)=>product.testResults==='passed'
        && product.status==='approved');
      },
      error: (error: any) => {
        console.error('Error fetching approved products:', error);
      }
    });
  }
  
  fetchFinishedGoods() {
    this.http.get('https://localhost:7214/api/Packaging').subscribe({
      next: (response: any) => {
        this.finishedGoods = response;
      },
      error: (error: any) => {
        console.error('Error fetching finished goods:', error);
      }
    });
  }
  
  selectProduct(product: any) {
    this.selectedProduct = product;
  }
  
  submitPackaging() {
    console.log(this.selectProduct);
    if (!this.selectedProduct || !this.selectedProduct.batch) {
      alert('No product selected or the product is not valid.');
      return;
    }

    if (!this.packagingForm.quantity || !this.packagingForm.expiryDate || !this.packagingForm.packagingDate || !this.packagingForm.warehouseLocation) {
      alert('Please fill in all the packaging details.');
      return;
    }



    const packagingData = {
      productId: this.selectedProduct.batch.product.productId,
      batchId: this.selectedProduct.batchId,
      quantity: this.packagingForm.quantity,
      expiryDate: this.packagingForm.expiryDate,
      packagingDate: this.packagingForm.packagingDate,
      warehouseLocation: this.packagingForm.warehouseLocation
    };
  
    this.http.post('https://localhost:7214/api/Packaging', packagingData,{responseType:'text'}).subscribe({
      next: (response:any) => {
        console.log('API Response:',response);
        alert('Packaging successfully submitted!');
        this.fetchFinishedGoods();
        this.selectedProduct = null;
        this.packagingForm = {};
      },
      error: (error: any) => {
        console.error('Error submitting packaging:', error);
        alert('An error occurred while submitting the packaging. Please try again later.');
      
      }
    });
  }
  
}
