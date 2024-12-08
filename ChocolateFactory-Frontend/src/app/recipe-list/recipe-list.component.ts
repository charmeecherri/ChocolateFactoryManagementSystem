
import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';

@Component({
  standalone:true,
  imports:[CommonModule],
  selector: 'app-recipe-list',
  templateUrl: './recipe-list.component.html',
  styleUrls: ['./recipe-list.component.css']
})
export class RecipeListComponent implements OnInit {
  recipes: any[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    // Fetch the recipes from the API
    this.http.get('https://localhost:7214/api/Recipe', { responseType: 'json' })
      .subscribe((data: any) => {
          if(Array.isArray(data)){
            this.recipes = data;
          } else {
            console.error('Unexpected data format', data);
          }
      });
  }
}

