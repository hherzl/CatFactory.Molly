import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { DatabaseListComponent } from './components/database-list/database-list.component';
import { DatabaseDetailsComponent } from './components/database-details/database-details.component';
import { ImportDatabaseComponent } from './components/import-database/import-database.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'database', component: DatabaseListComponent },
  { path: 'database/:id', component: DatabaseDetailsComponent },
  { path: 'import-database', component: ImportDatabaseComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
