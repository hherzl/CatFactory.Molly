import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ImportDatabaseRequest, MollyClientService } from 'src/app/services/molly-client.service';
import { ConfirmDialogComponent } from '../shared/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-import-database',
  templateUrl: './import-database.component.html',
  styleUrls: ['./import-database.component.css']
})
export class ImportDatabaseComponent {
  importDatabaseForm = this.fb.group({
    name: [null, Validators.required],
    connectionString: [null, Validators.required],
    importTables: true,
    importViews: true
  });

  hasUnitNumber = false;
  loading = false;

  constructor(private fb: FormBuilder, private router: Router, private dialog: MatDialog, private mollyClient: MollyClientService) { }

  importDatabase(): void {
    this.loading = true;

    let request = new ImportDatabaseRequest();

    request.name = String(this.importDatabaseForm.get('name')?.value);
    request.connectionString = String(this.importDatabaseForm.get('connectionString')?.value);
    request.importTables = Boolean(this.importDatabaseForm.get('importTables')?.value);
    request.importViews = Boolean(this.importDatabaseForm.get('importViews')?.value);

    this.mollyClient.importDatabase(request).subscribe(result => {
      this.loading = false;
      this.router.navigate(['database']);
    });
  }

  onSubmit(): void {
    if (!this.importDatabaseForm.valid) {
      return;
    }

    this.dialog.open(ConfirmDialogComponent, {
      width: '500px',
      data: {
        message: 'You\'ll import the database.'
      }
    }).afterClosed().subscribe(result => {
      if (result == true) {
        this.importDatabase();
      }
    });
  }
}
