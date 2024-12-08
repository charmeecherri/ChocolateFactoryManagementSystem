export interface RawMaterial {
    materialId: number;     // Unique ID of the material
    name: string;
    stockQuantity: number;  
    unit: string;           
    expiryDate: string;     
    supplierId: number;       
    costPerUnit: number;   
    
  }