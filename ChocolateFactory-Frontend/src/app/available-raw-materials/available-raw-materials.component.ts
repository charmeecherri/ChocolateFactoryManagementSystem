import { Component, OnInit,Output,EventEmitter } from '@angular/core';
import { RawMaterialService } from '../raw-material.service';
import { RawMaterial } from '../models/raw-material.model';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-available-raw-materials',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './available-raw-materials.component.html',
  styleUrls: ['./available-raw-materials.component.css']
})
export class AvailableRawMaterialsComponent implements OnInit {
  rawMaterials: RawMaterial[] = [];
  @Output() rawMaterialsFetched = new EventEmitter<RawMaterial[]>();

  constructor(private rawMaterialService: RawMaterialService) {}
    ngOnInit(): void {
      this.viewRawMaterials(); // Fetch raw materials when the component is initialized
    }
  
    // Fetch raw materials from the service
    viewRawMaterials() {
      this.rawMaterialService.getRawMaterials().subscribe({
        next: (data) => {
          this.rawMaterials = data;
          this.rawMaterialsFetched.emit(this.rawMaterials);
        },
        error: (error) => {
          console.error('Error fetching raw materials:', error);
        }
      });
  }
}
