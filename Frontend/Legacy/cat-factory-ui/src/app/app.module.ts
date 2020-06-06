import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppMaterialModule } from './app-material/app-material.module';

import { DashboardComponent } from './dashboard/dashboard.component';
import { DatabaseDetailsComponent } from './database-details/database-details.component';
import { ImportDatabaseComponent } from './import-database/import-database.component';
import { TableDetailsComponent } from './table-details/table-details.component';
import { ViewDetailsComponent } from './view-details/view-details.component';
import { EditDescriptionComponent } from './edit-description/edit-description.component';
import { NavigationComponent } from './navigation/navigation.component';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    DatabaseDetailsComponent,
    ImportDatabaseComponent,
    TableDetailsComponent,
    ViewDetailsComponent,
    EditDescriptionComponent,
    NavigationComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AppMaterialModule
  ],
  exports: [
    AppMaterialModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
