import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { SingleResponse } from 'src/app/services/common';
import { ColumnItemModel, MollyClientService, ViewDetailsModel } from 'src/app/services/molly-client.service';
import { EditViewDescriptionDialogComponent } from '../edit-view-description-dialog/edit-view-description-dialog.component';
import { EditViewColumnDescriptionDialogComponent } from '../edit-view-column-description-dialog/edit-view-column-description-dialog.component';

@Component({
  selector: 'app-view-details',
  templateUrl: './view-details.component.html',
  styleUrls: ['./view-details.component.css']
})
export class ViewDetailsComponent {
  constructor(private activatedRoute: ActivatedRoute, private router: Router, public dialog: MatDialog, private mollyClient: MollyClientService) {
  }

  public loading!: boolean;
  private db!: string;
  private view!: string;
  public response!: SingleResponse<ViewDetailsModel>;

  ngOnInit(): void {
    this.loading = true;

    this.activatedRoute.params.forEach((params: Params) => {
      this.db = params['db'];
      this.view = params['view'];

      this.mollyClient.getView(this.db, this.view).subscribe(result => {
        this.loading = false;
        this.response = result;
      });
    });
  }

  editViewDescription(): void {
    this.dialog
      .open(EditViewDescriptionDialogComponent, {
        width: '500px',
        data: {
          title: 'Edit View Description',
          databaseName: this.db,
          viewName: this.response?.model?.fullName,
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
      .open(EditViewColumnDescriptionDialogComponent, {
        width: '500px',
        data: {
          title: 'Edit View Column Description',
          databaseName: this.db,
          viewName: this?.response?.model?.fullName,
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
