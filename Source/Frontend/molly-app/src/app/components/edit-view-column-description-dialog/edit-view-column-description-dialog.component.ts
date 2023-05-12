import { Component, ElementRef, Inject, ViewChild } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MollyClientService, UpdateDescriptionRequest } from 'src/app/services/molly-client.service';

@Component({
  selector: 'app-edit-view-column-description-dialog',
  templateUrl: './edit-view-column-description-dialog.component.html',
  styleUrls: ['./edit-view-column-description-dialog.component.css']
})
export class EditViewColumnDescriptionDialogComponent {
  @ViewChild('description', { static: true }) descriptionElement: ElementRef;
  description: string = '';

  constructor(
    public dialogRef: MatDialogRef<EditViewColumnDescriptionDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: EditViewColumnDescriptionDialogData,
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
    request.viewName = this.data?.viewName;
    request.columnName = this.data?.columnName;
    request.description = this.descriptionElement.nativeElement.value;

    if (request.description.length == 0) {
      return;
    }

    this.mollyClient.updateViewColumnDescription(request).subscribe(result => {
      this.snackBar.open(result.message, 'Edit description', {
        duration: 3000
      });
      this.dialogRef.close({ description: request.description });
    });
  }
}

export class EditViewColumnDescriptionDialogData {
  public title!: string;
  public databaseName!: string;
  public viewName!: string;
  public columnName!: string;
  public description!: string;
}
