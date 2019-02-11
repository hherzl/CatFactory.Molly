import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppMaterialModule } from './app-material.module';
import { DbService } from './db.service';
import { AppComponent } from './app.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { DatabaseDetailsComponent } from './database-details/database-details.component';
import { EditDescriptionComponent, EditDescriptionDialogComponent } from './edit-description/edit-description.component';
import { ImportComponent, ImportDialogComponent } from './import/import.component';
import { TableDetailsComponent } from './table-details/table-details.component';
import { ViewDetailsComponent } from './view-details/view-details.component';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    DatabaseDetailsComponent,
    EditDescriptionComponent,
    EditDescriptionDialogComponent,
    ImportComponent,
    ImportDialogComponent,
    TableDetailsComponent,
    ViewDetailsComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AppMaterialModule,
    AppRoutingModule
  ],
  exports: [
    AppMaterialModule
  ],
  providers: [
    DbService
  ],
  entryComponents: [
    ImportDialogComponent,
    EditDescriptionDialogComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
