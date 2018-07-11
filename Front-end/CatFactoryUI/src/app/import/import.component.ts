import { Component, OnInit, Inject } from '@angular/core';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { DbService } from '../db.service';
import { Router } from '@angular/router';
import { ImportDatabaseRequest, ImportDatabaseResponse } from '../models';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'app-import',
  templateUrl: './import.component.html',
  styleUrls: ['./import.component.css']
})
export class ImportComponent implements OnInit {
  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    public dialog: MatDialog,
    private dbService: DbService) {
  }

  title: string;
  form: FormGroup;
  importing: boolean;
  response: ImportDatabaseResponse;

  ngOnInit() {
    this.title = 'Import';
    this.form = this.formBuilder.group({
      name: new FormControl('', [Validators.required]),
      connectionString: new FormControl('', [Validators.required]),
      importTables: new FormControl('', []),
      importViews: new FormControl('', [])
    });
    this.form.get('name').setValue('');
    this.form.get('connectionString').setValue('');
    this.form.get('importTables').setValue(true);
    this.form.get('importViews').setValue(true);
  }

  getRequiredErrorMessage(name: string): string {
    return this.form.get(name).hasError('required') ? '(*) Required' : '';
  }

  import(): void {
    this.importing = true;
    const request = new ImportDatabaseRequest();
    request.name = this.form.get('name').value;
    request.connectionString = this.form.get('connectionString').value;
    request.importTables = this.form.get('importTables').value;
    request.importViews = this.form.get('importViews').value;

    this.dbService.importDatabase(request).subscribe((data: ImportDatabaseResponse) => {
      this.response = data;
      this.router.navigate(['dashboard']);
    }, data => {
      this.importing = false;
      const dialogRef = this.dialog.open(ImportDialogComponent, {
        width: '500px',
        data: {
          error: data.error
        }
      });
      dialogRef.afterClosed().subscribe(result => {});
    });
  }
}

export class ImportDialogData {
  public data: Object;
}

@Component({
  selector: 'app-import-dialog',
  templateUrl: 'import-dialog.component.html',
})
export class ImportDialogComponent {
  constructor(
    public dialogRef: MatDialogRef<ImportDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ImportDialogData) {
    }

  onNoClick(): void {
    this.dialogRef.close();
  }
}
