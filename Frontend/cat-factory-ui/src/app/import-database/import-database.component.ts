import { Component, OnInit, Inject } from '@angular/core';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { DocumentationService , ImportDatabaseRequest, ImportDatabaseResponse} from '../documentation.service';

@Component({
  selector: 'app-import-database',
  templateUrl: './import-database.component.html',
  styleUrls: ['./import-database.component.css']
})
export class ImportDatabaseComponent implements OnInit {
  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private documentationService: DocumentationService,
    public dialog: MatDialog) {
  }

  title: string;
  form: FormGroup;
  importing: boolean;
  response: ImportDatabaseResponse;

  ngOnInit() {
    this.title = 'Import Database';
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

    this.documentationService.importDatabase(request).subscribe((data: ImportDatabaseResponse) => {
      this.response = data;
      this.router.navigate(['home']);
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
  public data: object;
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
