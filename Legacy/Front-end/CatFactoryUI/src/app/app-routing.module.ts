import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { DatabaseDetailsComponent } from './database-details/database-details.component';
import { TableDetailsComponent } from './table-details/table-details.component';
import { ViewDetailsComponent } from './view-details/view-details.component';
import { ImportComponent } from './import/import.component';
import { EditDescriptionComponent } from './edit-description/edit-description.component';

const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'database-details/:id', component: DatabaseDetailsComponent },
  { path: 'table-details/:id', component: TableDetailsComponent },
  { path: 'view-details/:id', component: ViewDetailsComponent },
  { path: 'import', component: ImportComponent },
  { path: 'edit-description/:id', component: EditDescriptionComponent }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { enableTracing: false })
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule {
}
