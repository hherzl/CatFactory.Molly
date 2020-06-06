import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { HttpClientModule } from '@angular/common/http';

import { AngularMaterialModule } from './angular-material/angular-material.module';

import { HomeComponent } from './home/home.component';

import { DocumentationService } from './documentation.service';
import { DatabaseDetailsComponent } from './database-details/database-details.component';
import { TableDetailsComponent } from './table-details/table-details.component';
import { ViewDetailsComponent } from './view-details/view-details.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    DatabaseDetailsComponent,
    TableDetailsComponent,
    ViewDetailsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    AngularMaterialModule
  ],
  providers: [
    DocumentationService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
