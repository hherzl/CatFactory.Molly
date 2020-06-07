import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DocumentationService, ImportedDatabase, ListResponse } from '../documentation.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  constructor(
    private router: Router,
    private documentationService: DocumentationService) {
  }

  title: string;
  response: ListResponse<ImportedDatabase>;
  columnsForImportedDatabases: string[];

  ngOnInit() {
    this.title = 'Home';
    this.columnsForImportedDatabases = [
      'name',
      'dbms',
      'tablesCount',
      'viewsCount',
      'details'
    ];
    this.documentationService.getImportedDatabases().subscribe((data) => {
      this.response = data;
    });
  }

  details(db: ImportedDatabase) {
    this.router.navigate(['database-details', db.name]);
  }
}
