import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import {routes} from './app.routes';
import { HttpClientModule, provideHttpClient } from '@angular/common/http';
import { importProvidersFrom } from '@angular/core';
import { ReactiveFormsModule,FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
//import { RouterModule } from '@angular/router';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';


export const appConfig: ApplicationConfig = {
  providers: [
    importProvidersFrom(
      BrowserModule,
      FormsModule, 
      HttpClientModule,
      ReactiveFormsModule, 
    ),
    provideRouter(routes), // Provide routes
    provideHttpClient(),
     provideAnimationsAsync(),
  ],

};
