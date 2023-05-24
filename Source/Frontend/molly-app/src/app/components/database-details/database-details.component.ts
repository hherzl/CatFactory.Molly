import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { SingleResponse } from 'src/app/services/common';
import { DatabaseDetailsModel, DatabaseTypeMapItemModel, MollyClientService, TableItemModel, ViewItemModel } from 'src/app/services/molly-client.service';
import { EditDatabaseDescriptionDialogComponent } from '../edit-database-description-dialog/edit-database-description-dialog.component';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-database-details',
  templateUrl: './database-details.component.html',
  styleUrls: ['./database-details.component.css']
})
export class DatabaseDetailsComponent implements OnInit {
  constructor(private activatedRoute: ActivatedRoute, private router: Router, public dialog: MatDialog, private mollyClient: MollyClientService) {
  }

  databaseTypeMapColumns = ['databaseType', 'allowsLengthInDeclaration', 'clrFullNameType', 'clrAliasType', 'isUserDefined', 'parentDatabaseType', 'collation'];

  databaseTypeMapsDataSource!: MatTableDataSource<DatabaseTypeMapItemModel>;

  public loading!: boolean;
  private id!: string;
  public response!: SingleResponse<DatabaseDetailsModel>;

  ngOnInit(): void {
    this.loading = true;

    this.activatedRoute.params.forEach((params: Params) => {
      this.id = params['id'];

      this.mollyClient.getDatabase(this.id).subscribe(result => {
        this.loading = false;
        this.response = result;
        this.databaseTypeMapsDataSource = new MatTableDataSource<DatabaseTypeMapItemModel>(this.response?.model?.databaseTypeMaps);
      });
    });
  }

  editDescription(): void {
    this.dialog
      .open(EditDatabaseDescriptionDialogComponent, {
        width: '500px',
        data: {
          title: 'Edit Database Description',
          databaseName: this.response?.model?.name,
          description: this.response?.model?.description,
          isDatabase: true
        }
      })
      .afterClosed()
      .subscribe(result => {
        this.response.model.description = result?.description;
      });
  }

  tableDetails(item: TableItemModel): void {
    this.router.navigate([`database/${this.id}/table/${item.fullName}`]);
  }

  viewDetails(item: ViewItemModel): void {
    this.router.navigate([`database/${this.id}/view/${item.fullName}`]);
  }
}
