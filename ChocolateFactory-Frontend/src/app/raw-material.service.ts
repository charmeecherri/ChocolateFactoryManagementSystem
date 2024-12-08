import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RawMaterial } from './models/raw-material.model';

import { RawMaterialsComponent } from './raw-materials/raw-materials.component';
import {map} from 'rxjs/operators'

@Injectable({
  providedIn: 'root'
})
export class RawMaterialService {
  private apiUrl = 'https://localhost:7214/api/RawMaterial';

  constructor(private http:HttpClient) { }

  addRawMaterial(materialRequestDto:any): Observable<any> {
    return this.http.post('https://localhost:7214/api/RawMaterial',materialRequestDto,{responseType:'text'}); 
  }

  

  getRawMaterials(): Observable<RawMaterial[]> {
    return this.http.get<RawMaterial[]>(this.apiUrl)
    
    //   map((materials: any[]) =>
    //     materials.map((material) => ({
    //       ...material,
    //       materialName: material.name, // Map `name` to `materialName`
    //     }))
    //   )
    // );
  }
}
