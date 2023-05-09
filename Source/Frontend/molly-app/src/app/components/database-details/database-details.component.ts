import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { SingleResponse } from 'src/app/services/common';
import { DatabaseDetailsModel, MollyClientService, TableItemModel, ViewItemModel } from 'src/app/services/molly-client.service';
import { EditDescriptionDialogComponent } from '../edit-description-dialog/edit-description-dialog.component';

@Component({
  selector: 'app-database-details',
  templateUrl: './database-details.component.html',
  styleUrls: ['./database-details.component.css']
})
export class DatabaseDetailsComponent implements OnInit {
  constructor(private activatedRoute: ActivatedRoute, private router: Router, public dialog: MatDialog, private mollyClient: MollyClientService) {
  }

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
      });
    });
  }

  editDescription(): void {
    this.dialog
      .open(EditDescriptionDialogComponent, {
        width: '500px',
        data: {
          databaseName: this.response?.model?.name,
          description: this.response?.model?.description
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
