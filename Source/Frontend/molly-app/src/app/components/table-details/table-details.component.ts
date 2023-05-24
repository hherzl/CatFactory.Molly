import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { SingleResponse } from 'src/app/services/common';
import { CheckItemModel, ColumnItemModel, DefaultItemModel, ForeignKeyItemModel, IndexItemModel, MollyClientService, TableDetailsModel, UniqueItemModel } from 'src/app/services/molly-client.service';
import { EditTableDescriptionDialogComponent } from '../edit-table-description-dialog/edit-table-description-dialog.component';
import { EditTableColumnDescriptionDialogComponent } from '../edit-table-column-description-dialog/edit-table-column-description-dialog.component';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-table-details',
  templateUrl: './table-details.component.html',
  styleUrls: ['./table-details.component.css']
})
export class TableDetailsComponent {
  constructor(private activatedRoute: ActivatedRoute, private router: Router, public dialog: MatDialog, private mollyClient: MollyClientService) {
  }

  tableColumns = ['name', 'type', 'length', 'prec', 'nullable', 'collation'];
  descriptionColumns = ['name', 'description'];
  foreignKeyColumns = ['name', 'key', 'references'];
  uniqueColumns = ['name', 'key'];
  checkColumns = ['name', 'key', 'expression'];
  defaultColumns = ['name', 'key', 'value'];
  indexColumns = ['name', 'description', 'keys'];

  tableColumnsDataSource!: MatTableDataSource<ColumnItemModel>;
  foreignKeysDataSource!: MatTableDataSource<ForeignKeyItemModel>;
  uniquesDataSource!: MatTableDataSource<UniqueItemModel>;
  checksDataSource!: MatTableDataSource<CheckItemModel>;
  defaultsDataSource!: MatTableDataSource<DefaultItemModel>;
  indexesDataSource!: MatTableDataSource<IndexItemModel>;

  public loading!: boolean;
  private db!: string;
  private table!: string;
  public response!: SingleResponse<TableDetailsModel>;

  ngOnInit(): void {
    this.loading = true;

    this.activatedRoute.params.forEach((params: Params) => {
      this.db = params['db'];
      this.table = params['table'];

      this.mollyClient.getTable(this.db, this.table).subscribe(result => {
        this.loading = false;
        this.response = result;
        this.tableColumnsDataSource = new MatTableDataSource<ColumnItemModel>(this.response?.model?.columns);
        this.foreignKeysDataSource = new MatTableDataSource<ForeignKeyItemModel>(this.response?.model?.foreignKeys);
        this.uniquesDataSource = new MatTableDataSource<UniqueItemModel>(this.response?.model?.uniques);
        this.checksDataSource = new MatTableDataSource<CheckItemModel>(this.response?.model?.checks);
        this.defaultsDataSource = new MatTableDataSource<DefaultItemModel>(this.response?.model?.defaults);
        this.indexesDataSource = new MatTableDataSource<IndexItemModel>(this.response?.model?.indexes);
      });
    });
  }

  editTableDescription(): void {
    this.dialog
      .open(EditTableDescriptionDialogComponent, {
        width: '500px',
        data: {
          title: 'Edit Table Description',
          databaseName: this.db,
          tableName: this?.response?.model?.fullName,
          description: this.response?.model?.description
        }
      })
      .afterClosed()
      .subscribe(result => {
        this.response.model.description = result?.description;
      });
  }

  editColumnDescription(item: ColumnItemModel): void {
    this.dialog
      .open(EditTableColumnDescriptionDialogComponent, {
        width: '500px',
        data: {
          title: 'Edit Table Column Description',
          databaseName: this.db,
          tableName: this?.response?.model?.fullName,
          columnName: item.name,
          description: item.description
        }
      })
      .afterClosed()
      .subscribe(result => {
        item.description = result?.description;
      });
  }
}
