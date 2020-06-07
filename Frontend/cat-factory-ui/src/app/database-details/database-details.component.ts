import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { DatabaseDetail, DbRequest, DocumentationService, SingleResponse } from '../documentation.service';

@Component({
  selector: 'app-database-details',
  templateUrl: './database-details.component.html',
  styleUrls: ['./database-details.component.css']
})
export class DatabaseDetailsComponent implements OnInit {
  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private documentationService: DocumentationService) {
  }

  title: string;
  response: SingleResponse<DatabaseDetail>;
  columnsForTables: string[];
  columnsForViews: string[];
  columnsForDatabaseTypeMaps: string[];

  ngOnInit() {
    this.columnsForTables = [
      'schema',
      'name',
      'columnsCount',
      'primaryKey',
      'identity',
      'details'
    ];
    this.columnsForViews = [
      'schema',
      'name',
      'columnsCount',
      'identity',
      'details'
    ];
    this.columnsForDatabaseTypeMaps = [
      'databaseType',
      'allowsLengthInDeclaration',
      'allowsPrecInDeclaration',
      'clrFullNameType',
      'clrAliasType',
      'isUserDefined',
      'parentDatabaseType'
    ];

    this.activatedRoute.params.forEach((params: Params) => {
      const model = new DbRequest();
      model.name = params['id'];

      this.documentationService.getDatabaseDetail(model).subscribe((data: SingleResponse<DatabaseDetail>) => {
        this.response = data;
        this.title = 'Details for \'' + this.response.model.name + '\' database';
      });
    });
  }

  showTable(element: any): void {
    const id = [this.response.model.name, element.type, element.fullName].join('|');
    this.router.navigate(['table-details', id]);
  }

  showView(element: any): void {
    const id = [this.response.model.name, element.type, element.fullName].join('|');
    this.router.navigate(['view-details', id]);
  }

  back(): void {
    this.router.navigate(['home']);
  }
}
