import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ListResponse, ImportedDatabase } from '../responses';
import { DocumentationService } from '../documentation.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  constructor(
    private router: Router,
    private documentationService: DocumentationService) {
  }

  title: string;
  response: ListResponse<ImportedDatabase>;
  columnsForImportedDatabases: string[];

  ngOnInit() {
    this.title = 'Dashboard';
    this.columnsForImportedDatabases = [
      'name',
      'dbms',
      'tablesCount',
      'viewsCount',
      'details'
    ];
    this.documentationService.getImportedDatabases().subscribe((data: ListResponse<ImportedDatabase>) => {
      this.response = data;
    });
  }

  details(db: ImportedDatabase) {
    this.router.navigate(['database-details', db.name]);
  }
}
