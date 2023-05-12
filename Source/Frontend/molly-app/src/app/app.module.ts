import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDialogModule } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatRadioModule } from '@angular/material/radio';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBarModule } from '@angular/material/snack-bar'

import { ConfirmDialogComponent } from './components/shared/confirm-dialog/confirm-dialog.component';

import { HomeComponent } from './components/home/home.component';
import { DatabaseListComponent } from './components/database-list/database-list.component';
import { DatabaseDetailsComponent } from './components/database-details/database-details.component';
import { ImportDatabaseComponent } from './components/import-database/import-database.component';
import { TableDetailsComponent } from './components/table-details/table-details.component';
import { ViewDetailsComponent } from './components/view-details/view-details.component';
import { EditDatabaseDescriptionDialogComponent } from './components/edit-database-description-dialog/edit-database-description-dialog.component';
import { EditTableDescriptionDialogComponent } from './components/edit-table-description-dialog/edit-table-description-dialog.component';
import { EditViewDescriptionDialogComponent } from './components/edit-view-description-dialog/edit-view-description-dialog.component';
import { EditTableColumnDescriptionDialogComponent } from './components/edit-table-column-description-dialog/edit-table-column-description-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    ConfirmDialogComponent,
    HomeComponent,
    DatabaseListComponent,
    DatabaseDetailsComponent,
    ImportDatabaseComponent,
    TableDetailsComponent,
    ViewDetailsComponent,
    EditDatabaseDescriptionDialogComponent,
    EditTableDescriptionDialogComponent,
    EditViewDescriptionDialogComponent,
    EditTableColumnDescriptionDialogComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    LayoutModule,
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatCardModule,
    MatDividerModule,
    MatInputModule,
    MatSelectModule,
    MatRadioModule,
    MatProgressSpinnerModule,
    MatSnackBarModule,
    ReactiveFormsModule,
    MatCheckboxModule,
    MatDialogModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
