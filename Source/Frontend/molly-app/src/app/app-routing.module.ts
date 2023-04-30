import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { DatabaseListComponent } from './components/database-list/database-list.component';
import { DatabaseDetailsComponent } from './components/database-details/database-details.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'database', component: DatabaseListComponent },
  { path: 'database/:id', component: DatabaseDetailsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
