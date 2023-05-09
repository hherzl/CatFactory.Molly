import { Component, ElementRef, Inject, ViewChild } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UpdateDescriptionRequest, MollyClientService } from 'src/app/services/molly-client.service';

@Component({
  selector: 'app-edit-description-dialog',
  templateUrl: './edit-description-dialog.component.html',
  styleUrls: ['./edit-description-dialog.component.css']
})
export class EditDescriptionDialogComponent {
  @ViewChild('description', { static: true }) descriptionElement: ElementRef;
  description: string = '';

  constructor(public dialogRef: MatDialogRef<EditDescriptionDialogComponent>, @Inject(MAT_DIALOG_DATA) public data: EditDescriptionDialogData, private snackBar: MatSnackBar, descriptionElement: ElementRef, private mollyClient: MollyClientService) {
    this.descriptionElement = descriptionElement;
    this.dialogRef.disableClose = true;
  }

  close(): void {
    this.dialogRef.close({ description: this.data?.description });
  }

  updateDescription(): void {
    let request = new UpdateDescriptionRequest();
    request.description = this.descriptionElement.nativeElement.value;

    if (request.description.length == 0) {
      return;
    }

    this.mollyClient.updateDatabaseDescription(this.data?.databaseName, request).subscribe(result => {
      this.snackBar.open(result.message, 'Edit description', {
        duration: 3000
      });
      this.dialogRef.close({ description: request.description });
    });
  }
}

export class EditDescriptionDialogData {
  public databaseName!: string;
  public description!: string;
}
