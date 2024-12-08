import { Component } from '@angular/core';
import { RawMaterialService } from '../raw-material.service';
import { RawMaterial } from '../models/raw-material.model';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { DatePipe } from '@angular/common';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-raw-materials',
  standalone: true,
  imports: [FormsModule, CommonModule],
  providers: [DatePipe],
  templateUrl: './raw-materials.component.html',
  styleUrls: ['./raw-materials.component.css']
})
export class RawMaterialsComponent {

  rawMaterials: RawMaterial[] = [];
  newMaterial: RawMaterial = {
    materialId: 0,
    name: '',
    stockQuantity: 0,
    unit: '',
    expiryDate: '',
    supplierId: 0,  // Corrected key name
    costPerUnit: 0
  };

  constructor(private rawMaterialService: RawMaterialService, private router: Router, private datePipe: DatePipe,private activatedRoute:ActivatedRoute) {}

  addMaterial() {
    console.log('Material to add:', this.newMaterial);

    // Log the data being sent
  
    // Check if required fields are filled
    if (!this.newMaterial.name.trim() || !this.newMaterial.unit.trim() || !this.newMaterial.supplierId || !this.newMaterial.expiryDate || !this.newMaterial.costPerUnit || !this.newMaterial.stockQuantity) {
      alert('All fields are required. Please fill out the form correctly.');
      return;
    }

  
    if (this.newMaterial.stockQuantity <= 0 || this.newMaterial.costPerUnit <= 0) {
      alert('Stock Quantity and Cost Per Unit must be greater than zero.');
      return;
    }
  
   
    const formattedDate = this.datePipe.transform(this.newMaterial.expiryDate, 'yyyy-MM-dd');
    if (!formattedDate) {
      console.error('Invalid expiry date format');
      alert('Invalid expiry date. Please enter a valid date.');
      return;
    }
  
    this.newMaterial.expiryDate = formattedDate;

    
    const materialRequestDto = {
      name: this.newMaterial.name,
      stockQuantity: this.newMaterial.stockQuantity,
      unit: this.newMaterial.unit,
      expiryDate: this.newMaterial.expiryDate,
      supplierId: this.newMaterial.supplierId, // Corrected key name to 'supplierId'
      costPerUnit: this.newMaterial.costPerUnit,
    };

    this.rawMaterialService.addRawMaterial(materialRequestDto).subscribe({
      next: (response) => {
        console.log('Material added successfully:', response);
        // Add the new material to the rawMaterials array
        if(response==="Material is created"){
          console.log('Material added successfylly');
        this.rawMaterials.push(
          {
            ...this.newMaterial,
            materialId: this.rawMaterials.length + 1, // Update materialId logic if needed
          });
        // Reset the form
        this.newMaterial = {
          materialId: 0,
          name: '',
          stockQuantity: 0,
          unit: '',
          expiryDate: '',
          supplierId: 0,
          costPerUnit: 0
        };
      }else{
        console.error('Unexpected response', response);
        alert('Error: Unexpected response from the server');
      }
    },
    error:(error)=>{
      console.error('Error adding material:', error);
      alert('An error occurred while adding the material.');
    }

  });
}

  viewRawMaterials() {
    this.router.navigate(['/view-raw-materials']);
  }
  navigateTo(route: string): void {
    this.router.navigate([route], { relativeTo: this.activatedRoute.parent });
  }

}
