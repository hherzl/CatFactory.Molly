import { Component, ElementRef, Inject, ViewChild } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MollyClientService, UpdateDescriptionRequest } from 'src/app/services/molly-client.service';

@Component({
  selector: 'app-edit-table-column-description-dialog',
  templateUrl: './edit-table-column-description-dialog.component.html',
  styleUrls: ['./edit-table-column-description-dialog.component.css']
})
export class EditTableColumnDescriptionDialogComponent {
  @ViewChild('description', { static: true }) descriptionElement: ElementRef;
  description: string = '';

  constructor(
    public dialogRef: MatDialogRef<EditTableColumnDescriptionDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: EditTableColumnDescriptionDialogData,
    private snackBar: MatSnackBar,
    descriptionElement: ElementRef,
    private mollyClient: MollyClientService) {
    this.descriptionElement = descriptionElement;
    this.dialogRef.disableClose = true;
  }

  cancel(): void {
    this.dialogRef.close({ description: this.data?.description });
  }

  save(): void {
    let request = new UpdateDescriptionRequest();
    request.databaseName = this.data?.databaseName;
    request.tableName = this.data?.tableName;
    request.columnName = this.data?.columnName;
    request.description = this.descriptionElement.nativeElement.value;

    if (request.description.length == 0) {
      return;
    }

    this.mollyClient.updateTableColumnDescription(request).subscribe(result => {
      this.snackBar.open(result.message, 'Edit description', {
        duration: 3000
      });
      this.dialogRef.close({ description: request.description });
    });
  }
}

export class EditTableColumnDescriptionDialogData {
  public title!: string;
  public databaseName!: string;
  public tableName!: string;
  public columnName!: string;
  public description!: string;
}
