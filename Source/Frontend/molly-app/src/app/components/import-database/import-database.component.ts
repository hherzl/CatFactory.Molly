import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ImportDatabaseRequest, MollyClientService } from 'src/app/services/molly-client.service';

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

  constructor(private fb: FormBuilder, private router: Router, private mollyClient: MollyClientService) { }

  onSubmit(): void {
    if (!this.importDatabaseForm.valid)
      return;

    let request = new ImportDatabaseRequest();

    request.name = String(this.importDatabaseForm.get('name')?.value);
    request.connectionString = String(this.importDatabaseForm.get('connectionString')?.value);
    request.importTables = Boolean(this.importDatabaseForm.get('importTables')?.value);
    request.importViews = Boolean(this.importDatabaseForm.get('importViews')?.value);

    this.mollyClient.importDatabase(request).subscribe(result => {
      this.router.navigate(['database']);
    });
  }
}
