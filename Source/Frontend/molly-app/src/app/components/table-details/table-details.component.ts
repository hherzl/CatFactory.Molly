import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { SingleResponse } from 'src/app/services/common';
import { MollyClientService, TableDetailsModel } from 'src/app/services/molly-client.service';
import { EditTableDescriptionDialogComponent } from '../edit-table-description-dialog/edit-table-description-dialog.component';

@Component({
  selector: 'app-table-details',
  templateUrl: './table-details.component.html',
  styleUrls: ['./table-details.component.css']
})
export class TableDetailsComponent {
  constructor(private activatedRoute: ActivatedRoute, private router: Router, public dialog: MatDialog, private mollyClient: MollyClientService) {
  }

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
      });
    });
  }

  editDescription(): void {
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
}
