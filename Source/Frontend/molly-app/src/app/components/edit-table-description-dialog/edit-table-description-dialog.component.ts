import { Component, ElementRef, Inject, ViewChild } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MollyClientService, UpdateDescriptionRequest } from 'src/app/services/molly-client.service';

@Component({
  selector: 'app-edit-table-description-dialog',
  templateUrl: './edit-table-description-dialog.component.html',
  styleUrls: ['./edit-table-description-dialog.component.css']
})
export class EditTableDescriptionDialogComponent {
  @ViewChild('description', { static: true }) descriptionElement: ElementRef;
  description: string = '';

  constructor(
    public dialogRef: MatDialogRef<EditTableDescriptionDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: EditTableDescriptionDialogData,
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
    request.description = this.descriptionElement.nativeElement.value;

    if (request.description.length == 0) {
      return;
    }

    this.mollyClient.updateTableDescription(this.data?.databaseName, this.data?.tableName, request).subscribe(result => {
      this.snackBar.open(result.message, 'Edit description', {
        duration: 3000
      });
      this.dialogRef.close({ description: request.description });
    });
  }
}

export class EditTableDescriptionDialogData {
  public title!: string;
  public databaseName!: string;
  public tableName!: string;
  public description!: string;
}
