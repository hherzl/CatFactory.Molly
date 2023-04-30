import { Component, Inject } from "@angular/core";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";

export class ConfirmDialogData {
    constructor() {
    }

    public message!: string;
}

@Component({
    selector: 'confirm-dialog',
    templateUrl: 'confirm-dialog.component.html',
})
export class ConfirmDialogComponent {
    constructor(@Inject(MAT_DIALOG_DATA) public data: ConfirmDialogData, public dialogRef: MatDialogRef<ConfirmDialogComponent>) {
    }

    onNo(): void {
        this.dialogRef.close(false);
    }

    onYes(): void {
        this.dialogRef.close(true);
    }
}
