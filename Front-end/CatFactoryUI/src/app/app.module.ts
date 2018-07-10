import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { DashboardDetailsComponent } from './dashboard-details/dashboard-details.component';
import { EditDescriptionComponent } from './edit-description/edit-description.component';
import { ImportComponent } from './import/import.component';
import { TableDetailsComponent } from './table-details/table-details.component';
import { ViewDetailsComponent } from './view-details/view-details.component';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    DashboardDetailsComponent,
    EditDescriptionComponent,
    ImportComponent,
    TableDetailsComponent,
    ViewDetailsComponent
  ],
  imports: [
    BrowserModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
