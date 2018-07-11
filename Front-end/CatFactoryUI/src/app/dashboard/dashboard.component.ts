import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DbService } from '../db.service';
import { ImportedDatabase, ListResponse } from '../models';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  constructor(
    private router: Router,
    private dbService: DbService) {
  }

  title: string;
  response: ListResponse<ImportedDatabase>;
  columnsForImportedDatabases: string[];

  ngOnInit() {
    this.title = 'Welcome to CatFactory.UI! ==^^==';
    this.columnsForImportedDatabases = [
      'name',
      'dbms',
      'tablesCount',
      'viewsCount',
      'details'
    ];
    this.dbService.getImportedDatabases().subscribe((data: ListResponse<ImportedDatabase>) => {
      this.response = data;
    });
  }

  details(db: ImportedDatabase) {
    this.router.navigate(['database-details', db.name]);
  }
}
