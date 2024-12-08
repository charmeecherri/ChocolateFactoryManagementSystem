import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Router } from '@angular/router';

@Component({
  selector: 'app-quality-control',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './quality-control.component.html',
  styleUrl: './quality-control.component.css'
})
export class QualityControlComponent implements OnInit {
  userRole:string='';
  completedProducts:any[]=[];
  selectedProduct:any=null;

  inspectionForm = 
    {
      batchId:0,
      inspectorId:0,
      inspectionDate: '',
      testResults: '',
    };

  
  // isQualityController: boolean = false;

  // ngOnInit() {
  //   // Check the role in local storage
  //   const role = localStorage.getItem('role');
  //   console.log(role,"QualityController")
  //   if (role == "QualityController") {
  //     this.isQualityController = true;
  //   }
  // }

  

  constructor(private router: Router,private http:HttpClient) {}

  ngOnInit(): void {
    this.checkUserRoleAndFetchData();
  }
  checkUserRoleAndFetchData(): void {
    this.userRole = localStorage.getItem('role') || ''; 
    console.log('User Role:', this.userRole);

    if (!this.userRole) {
      console.error('No role found in localStorage. Ensure the role is correctly set.');
      alert('Access Denied: Role not found.');
      this.router.navigate(['/']);
      return;
    }

  
    // Role-based access check
    if (this.userRole === 'FactoryManager' ||this.userRole === 'QualityController') {
      console.log('Access granted: Role is either Factory Manager or Quality Controller.');
      this.fetchCompletedProducts(); // Fetch data as access is granted
    } else {
      console.warn('Access denied: Role is not authorized.');
      alert('Access Denied: You do not have permission to access this page.');
      this.router.navigate(['/']); // Redirect unauthorized users
    }
  }



  fetchCompletedProducts() {
    this.http.get<any[]>(`${environment.apiUrl}ProductionSchedule/completed`)
      .subscribe({
        next: (response) => {
          this.completedProducts =response;
          console.log('Completed products:', this.completedProducts);
        },
        error: (error) => {
          console.error('Error fetching completed products:', error);
        }
      });
  }

  selectProduct(product: any) {
    this.selectedProduct = product;
    this.inspectionForm.batchId = product.scheduleId; // Assuming scheduleId as batchId
  }
  submitInspection() {

    if (this.selectedProduct?.status?.toLowerCase().trim()!== 'completed') {
      alert('You can only inspect products with the status "completed".');
      return;
    }
    console.log('selected product status:',this.selectedProduct?.status);
    const inspectionData = {
      batchId: this.inspectionForm.batchId,
      inspectorId: this.inspectionForm.inspectorId,
      inspectionDate: this.inspectionForm.inspectionDate,
      testResults: this.inspectionForm.testResults
    };

    this.http.post(`${environment.apiUrl}Quality`, inspectionData,{responseType:'text'})
      .subscribe({
        next: (response:string) => {
          console.log('Inspection submitted successfully:', response);
          alert(response);
          if (inspectionData.testResults === 'passed') {
            // Navigate to packaging component or show success message
            alert('Product moved to Packaging Section!');
          }
          this.resetForm();
        },
        error: (error) => {
          console.error('Error submitting inspection:', error);
          alert('Failed to submit inspection.');
        }
      });
  }

  resetForm() {
    this.selectedProduct = null;
    this.inspectionForm = {
      batchId: 0,
      inspectorId: 0,
      inspectionDate: '',
      testResults: ''
    };
  }

}
