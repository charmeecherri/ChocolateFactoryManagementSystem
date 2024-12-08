import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-warehouse-management',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './warehouse-management.component.html',
  styleUrl: './warehouse-management.component.css'
})
export class WarehouseManagementComponent {
  warehouses = [
    { warehouseId: 1, location: 'Warehouse A', capacity: 1000, managerId: 1, currentStockLevel: 500 },
    { warehouseId: 2, location: 'Warehouse B', capacity: 800, managerId: 2, currentStockLevel: 300 },
  ];

  newWarehouse = {
    warehouseId: 0,
    location: '',
    capacity: 0,
    managerId: 0,
    currentStockLevel: 0,
  };

  addWarehouse() {
    this.warehouses.push({ ...this.newWarehouse });
    this.newWarehouse = { warehouseId: 0, location: '', capacity: 0, managerId: 0, currentStockLevel: 0 };
  }
}
