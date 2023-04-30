import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { DatabaseListComponent } from './components/database-list/database-list.component';
import { DatabaseDetailsComponent } from './components/database-details/database-details.component';
import { TableDetailsComponent } from './components/table-details/table-details.component';
import { ViewDetailsComponent } from './components/view-details/view-details.component';
import { ImportDatabaseComponent } from './components/import-database/import-database.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'database', component: DatabaseListComponent },
  { path: 'database/:id', component: DatabaseDetailsComponent },
  { path: 'database/:db/table/:table', component: TableDetailsComponent },
  { path: 'database/:db/view/:view', component: ViewDetailsComponent },
  { path: 'import-database', component: ImportDatabaseComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
