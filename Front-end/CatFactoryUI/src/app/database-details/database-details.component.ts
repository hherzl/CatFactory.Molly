import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { DbService } from '../db.service';
import { DatabaseDetail, DbRequest, SingleResponse } from '../models';

@Component({
  selector: 'app-database-details',
  templateUrl: './database-details.component.html',
  styleUrls: ['./database-details.component.css']
})
export class DatabaseDetailsComponent implements OnInit {
  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private dbService: DbService) {
  }

  title: string;
  response: SingleResponse<DatabaseDetail>;
  columnsForTables: string[];
  columnsForViews: string[];
  columnsForMappings: string[];

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
    this.columnsForMappings = [
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

      this.dbService.getDatabaseDetail(model).subscribe((data: SingleResponse<DatabaseDetail>) => {
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
    this.router.navigate(['dashboard']);
  }
}
