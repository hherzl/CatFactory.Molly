import { Component, ElementRef, Inject, ViewChild } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UpdateDescriptionRequest, MollyClientService } from 'src/app/services/molly-client.service';

@Component({
  selector: 'app-edit-database-description-dialog',
  templateUrl: './edit-database-description-dialog.component.html',
  styleUrls: ['./edit-database-description-dialog.component.css']
})
export class EditDatabaseDescriptionDialogComponent {
  @ViewChild('description', { static: true }) descriptionElement: ElementRef;
  description: string = '';

  constructor(
    public dialogRef: MatDialogRef<EditDatabaseDescriptionDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: EditDatabaseDescriptionDialogData,
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
    request.description = this.descriptionElement.nativeElement.value;

    if (request.description.length == 0) {
      return;
    }

    this.mollyClient.updateDatabaseDescription(request).subscribe(result => {
      this.snackBar.open(result.message, 'Edit description', {
        duration: 3000
      });
      this.dialogRef.close({ description: request.description });
    });
  }
}

export class EditDatabaseDescriptionDialogData {
  public title!: string;
  public databaseName!: string;
  public description!: string;
}
