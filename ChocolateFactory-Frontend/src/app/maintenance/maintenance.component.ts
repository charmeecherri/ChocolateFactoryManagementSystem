import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-maintenance',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './maintenance.component.html',
  styleUrl: './maintenance.component.css'
})
export class MaintenanceComponent {
  maintenanceRecords = [
    {
      recordId: 1,
      equipmentId: 'EQ001',
      maintenanceDate: '2024-11-30',
      technician: 'John Doe',
      details: 'Replaced worn-out mixer blades.',
      nextScheduledDate: '2024-12-15',
    },
    {
      recordId: 2,
      equipmentId: 'EQ002',
      maintenanceDate: '2024-11-25',
      technician: 'Jane Smith',
      details: 'Cleaned and calibrated molding machine.',
      nextScheduledDate: '2024-12-20',
    },
  ];
}
