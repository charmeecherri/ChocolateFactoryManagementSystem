import { Component, OnInit } from '@angular/core'; 
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

interface ProductionSchedule {
  scheduleId?: number;
  productId: number;
  startDate: string;
  endDate: string;
  shift: string;
  supervisorId: number;
  status: string;
}

interface Product {
  productId: number;
  productName: string;
}

@Component({
  selector: 'app-production-planning',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './production-planning.component.html',
  styleUrls: ['./production-planning.component.css']
})
export class ProductionPlanningComponent implements OnInit {
  newSchedule: ProductionSchedule = {
    productId: 0,
    startDate: '',
    endDate: '',
    shift: 'Morning',
    supervisorId:0,
    status: 'Scheduled'
  };
  
 

  productionSchedules: ProductionSchedule[] = [];
  isFactoryManager:boolean=false;
  isProductionSupervisor: boolean = false;
  products: Product[] = [];
  currentSupervisorId: number = 0;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.checkUserRoleAndFetchData();
  }

  checkUserRoleAndFetchData(): void {
    const role = localStorage.getItem('role');
    console.log('User ID:',localStorage.getItem('userId'));

    if (role === 'ProductionSupervisor' || role === 'FactoryManager') {
      this.isProductionSupervisor = true;
      console.log('Access granted: Production Supervisor role detected.');
      this.fetchProductionSchedules();
      this.fetchProducts();
      this.fetchSupervisorId();
    } else {
      this.isProductionSupervisor = false;
      console.warn('Access denied: Only Production Supervisors can access this component.');
    }
  }

  fetchSupervisorId(): void {
    const userId = localStorage.getItem('userId');
    if (userId) {
      this.currentSupervisorId = parseInt(userId, 10);
      this.newSchedule.supervisorId = this.currentSupervisorId;
      console.log(`User ID: ${this.currentSupervisorId}`);
    }
    else{
      console.warn('user id not found in local storage');
      this.currentSupervisorId = 0;
    }
  }

  fetchProducts(): void {
    this.http.get<Product[]>(`${environment.apiUrl}product`).subscribe((products) => {
      this.products = products;
    }, (error) => {
      console.error('Error fetching products:', error);
      alert('Failed to fetch products.');
    });
  }

  fetchProductionSchedules(): void {
    this.http.get<ProductionSchedule[]>(`${environment.apiUrl}ProductionSchedule`).subscribe((schedules) => {
      this.productionSchedules = schedules;
    }, (error) => {
      console.error('Error fetching production schedules:', error);
      alert('Failed to fetch production schedules.');
    });
  }

  onSubmit(): void {
    if (this.newSchedule.productId ===0 || !this.newSchedule.startDate.trim() || !this.newSchedule.endDate.trim() || !this.newSchedule.supervisorId) {
      alert('Please fill all required fields.');
      return;
    }
    const schedule: ProductionSchedule = { ...this.newSchedule };
      console.log(schedule);
    this.http.post(`${environment.apiUrl}ProductionSchedule`, schedule, { responseType: 'text' }).subscribe({
      next: (response) => {
        console.log(response);
        alert(response);
        this.resetForm();
      },
      error: (error) => {
        console.error('Error creating production schedule:', error);
        alert('Failed to create production schedule.');
      },
      complete: () => {
        console.log('Production schedule creation complete.');
      }
    });
  }

  onEditStatus(scheduleId: number | undefined, newStatus: string) {
    if (scheduleId !== undefined) {
      this.http.put(`${environment.apiUrl}ProductionSchedule`,null,
        {
          params: { id: scheduleId.toString(), status: newStatus },
          responseType: 'text'
        }
      ).subscribe({
        next: (response:string) => {
          console.log(response);
          alert(response);
          this.fetchProductionSchedules(); // Refresh schedule list
        },
        error: (error) => {
          console.error('Error updating production schedule status:', error);
          alert('Failed to update production schedule status.');
        }
      });
    } else {
      console.error('Invalid schedule ID');
    }
  }

  onMarkAsCompleted(scheduleId: number | undefined): void {
    if (scheduleId === undefined) {
      console.error('Invalid schedule ID');
      return;
    }
    this.onEditStatus(scheduleId, 'Completed');
  }

  resetForm(): void {
    this.newSchedule = {
      productId: 0,
      startDate: '',
      endDate: '',
      shift: 'Morning',
      supervisorId: this.currentSupervisorId,
      status: 'Scheduled'
    };
  }
}
