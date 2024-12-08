import { Routes } from '@angular/router';
import { RegisterComponent } from './auth/register/register.component';
import { LoginComponent } from './auth/login/login.component';
import { OrderManagementComponent } from './order-management/order-management.component';
import { RawMaterialsComponent } from './raw-materials/raw-materials.component';
import { AvailableRawMaterialsComponent } from './available-raw-materials/available-raw-materials.component';
import { ProductionPlanningComponent } from './production-planning/production-planning.component';
import { FactorymanagerComponent } from './factorymanager/factorymanager.component';
import { ProductComponent } from './product/product.component';
import { RecipeBatchManagementComponent } from './recipe-batch-management/recipe-batch-management.component';
import { RecipeListComponent } from './recipe-list/recipe-list.component';
import { QualityControlComponent } from './quality-control/quality-control.component';
import { ProductionDashboardComponent } from './production-dashboard/production-dashboard.component';
import { PackageSectionComponent } from './package-section/package-section.component';
import { HeroComponent } from './hero/hero.component';



export const routes: Routes = [

    {path:'',component:LoginComponent},
    {path:'login',component:LoginComponent},
    {path:'register',component:RegisterComponent},
    {path:'product',component:ProductComponent},

    {
    path:'factory-manager',component:FactorymanagerComponent,
    children:[
    
    {path:'order-management',component:OrderManagementComponent},
    {path:'quality',component:QualityControlComponent},
    { path:'raw-materials', component: RawMaterialsComponent},
    { path: 'production-planning', component: ProductionPlanningComponent },
    { path: 'recipe-batch-management', component: RecipeBatchManagementComponent },
    {path:'recipes',component:RecipeListComponent},
    {path:'product',component:ProductComponent},
     {path:'package',component:PackageSectionComponent},
    { path: '', component:HeroComponent},
    ],
   },
   {path:'order-management',component:OrderManagementComponent},
   { path:'raw-materials', component: RawMaterialsComponent},
    {path:'quality',component:QualityControlComponent},
    {path:'view-raw-materials',component:AvailableRawMaterialsComponent},
    { path: 'production-planning', component: ProductionPlanningComponent },
    {path:'package',component:PackageSectionComponent},
    {path:'production-dashboard', component:ProductionDashboardComponent},
    { path: 'recipe-batch-management', component: RecipeBatchManagementComponent },
    {path:'recipes',component:RecipeListComponent},
   
];


