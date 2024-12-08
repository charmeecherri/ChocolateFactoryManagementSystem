import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { of } from 'rxjs';  // For handling default values in case of errors
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  standalone: true,
  imports: [CommonModule,FormsModule],
  selector: 'app-recipe-batch-management',
  templateUrl: './recipe-batch-management.component.html',
  styleUrls: ['./recipe-batch-management.component.css'],
})
export class RecipeBatchManagementComponent implements OnInit {
  public recipes: any[] = [];
  public rawMaterials: any[] = [];
  public availableProducts:any[]=[];

  public newRecipe = {
    recipeId: 0,
    productId: 0,
    ingredients: [] as any[],
    quantityPerBatch: '',
    instructions: '',
  };
  public quantities: { [key: string]: number } = {};

  constructor(private http: HttpClient,private router:Router) {}

  ngOnInit(): void {
    this.fetchRecipes();
    this.fetchRawMaterials();
    this.fetchAvailableProducts();
  }

  fetchRecipes(): void {
    this.http.get<any[]>('https://localhost:7214/api/Recipe', { responseType: 'json' })
      .pipe(
        catchError(error => {
          console.error('Error fetching recipes:', error);
          return of([]);
        })
      )
      .subscribe((recipes: any[]) => {
          this.recipes = recipes;
        });
      } 

      fetchAvailableProducts(): void {
        this.http.get<any[]>('https://localhost:7214/api/Product', { responseType: 'json' })
          .pipe(
            catchError(error => {
              console.error('Error fetching products:', error);
              return of([]);
            })
          )
          .subscribe((products: any[]) => {
            this.availableProducts = products;
          });
      }
  
  fetchRawMaterials(): void {
    this.http.get('https://localhost:7214/api/RawMaterial', { responseType: 'json' })
      .pipe(
        catchError(error => {
          console.error('Error fetching raw materials:', error);
          return of([]);
        })
      )
      .subscribe((data: any) => {
          this.rawMaterials = data;
      });
  }

  isIngredientSelected(materialId: number): boolean {
    return this.newRecipe.ingredients.some(ing => ing.materialId === materialId);
  }

  toggleIngredientSelection(material: any, event: Event): void {
    const isChecked = (event.target as HTMLInputElement).checked;

    if (isChecked) {
      this.newRecipe.ingredients.push({
        materialId: material.materialId,
        name: material.name,
        quantity: 0,
      });
    } else {
      this.newRecipe.ingredients = this.newRecipe.ingredients.filter(
        (ingredient) => ingredient.materialId !== material.materialId
      );
    }
  }

  updateIngredientQuantity(material: any, event: Event): void {
    const quantity = +(event.target as HTMLInputElement).value || 0;
    const ingredient = this.newRecipe.ingredients.find(
      (item) => item.materialId === material.materialId
    );

    if (ingredient) {
      ingredient.quantity = quantity;
    }
  }

  addRecipe(): void {
    if (this.newRecipe.ingredients.length === 0) {
      alert('Please select at least one ingredient.');
      return;
    }

    const recipePayload = {
      recipeId: 0,
      productId: this.newRecipe.productId,
      ingredients: this.newRecipe.ingredients.map(ingredient=>({
        materialId: ingredient.materialId,
        name: ingredient.name,
        quantity: ingredient.quantity
      })),
      quantityPerBatch: this.newRecipe.quantityPerBatch,
      instructions: this.newRecipe.instructions,
    };

    this.http.post('https://localhost:7214/api/Recipe', recipePayload,{ responseType:'text',observe:'response'})
      .pipe(
        catchError(error => {
          console.error('Error adding recipe:', error);
          alert('Failed to add recipe.');
          return of(null);
        })
      )
      .subscribe(response => {
          if (response && response?.status === 201 ) {
          alert( response.body || 'Recipe added successfully!');
          this.resetForm();
        }
        else{
          alert('Failed to add recipe. Please try again.');
        }  
      });
  
  }

  private resetForm(): void {
    this.newRecipe = {
      recipeId: 0,
      productId: 0,
      ingredients: [],
      quantityPerBatch: '',
      instructions: '',
    };
    this.quantities = {};
  }

  navigateToRecipes(): void {
    this.router.navigate(['/recipes']);
  }
}
