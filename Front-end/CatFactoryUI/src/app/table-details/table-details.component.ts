import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { DbService } from '../db.service';
import { DbRequestHelper, SingleResponse } from '../models';

@Component({
  selector: 'app-table-details',
  templateUrl: './table-details.component.html',
  styleUrls: ['./table-details.component.css']
})
export class TableDetailsComponent implements OnInit {
  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private dbService: DbService) {
  }

  title: string;
  id: string;
  response: SingleResponse<any>;
  rowGuidColumn: any;
  columnsForColumns: string[];
  columnsForDescriptions: string[];
  columnsForIndexes: string[];
  columnsForForeignKeys: string[];
  columnsForUniques: string[];
  columnsForTableReferences: string[];

  ngOnInit() {
    this.columnsForColumns = [
      'name',
      'type',
      'length',
      'prec',
      'scale',
      'nullable',
      'collation'
    ];
    this.columnsForDescriptions = ['name', 'description'];
    this.columnsForIndexes = ['indexName', 'indexDescription', 'indexKeys'];
    this.columnsForForeignKeys = ['constraintName', 'key', 'references'];
    this.columnsForUniques = ['constraintName', 'key'];
    this.columnsForTableReferences = ['referenceDescription'];

    this.activatedRoute.params.forEach((params: Params) => {
      this.id = params['id'];
      const request = DbRequestHelper.createFromId(this.id);

      this.dbService.getTable(request).subscribe((data: SingleResponse<any>) => {
        this.response = data;
        this.title = 'Details for \'' + this.response.model.fullName + '\' table';
        this.response.model.columns.forEach(column => {
          if (this.response.model.rowGuidCol.name === column.name) {
            this.rowGuidColumn = column;
          }
        });
      });
    });
  }

  editTableDescription(): void {
    this.router.navigate(['edit-description', this.id]);
  }

  editColumnDescription(column: any): void {
    const values = this.id.split('|');
    values.push(column.name);
    this.router.navigate(['edit-description', values.join('|')]);
  }

  back(): void {
    const db = DbRequestHelper.getDbName(this.id);
    this.router.navigate(['database-details', db]);
  }
}
