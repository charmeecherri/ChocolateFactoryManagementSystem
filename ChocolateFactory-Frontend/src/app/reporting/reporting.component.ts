import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-reporting',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './reporting.component.html',
  styleUrl: './reporting.component.css'
})
export class ReportingComponent {
  reports = [
    {
      reportId: 101,
      type: 'Production Efficiency',
      generatedDate: '2024-12-02',
      createdBy: 'Admin',
    },
    {
      reportId: 102,
      type: 'Sales Performance',
      generatedDate: '2024-12-01',
      createdBy: 'Manager',
    },
  ];

  realTimeMetrics = {
    productionOutput: '5000 Units',
    salesTargets: '95% Achieved',
    defectRates: '2%',
  };
}
