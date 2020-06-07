import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DbRequestHelper, DocumentationService, SingleResponse } from '../documentation.service';

@Component({
  selector: 'app-edit-description',
  templateUrl: './edit-description.component.html',
  styleUrls: ['./edit-description.component.css']
})
export class EditDescriptionComponent implements OnInit {
  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder,
    public dialog: MatDialog,
    private documentationService: DocumentationService) {
  }

  title: string;
  form: FormGroup;
  id: string;
  response: SingleResponse<any>;
  working: boolean;

  ngOnInit() {
    this.form = this.formBuilder.group({
      description: new FormControl('', [])
    });
    this.activatedRoute.params.forEach((params: Params) => {
      this.id = params['id'];
      const request = DbRequestHelper.createFromId(this.id);
      if (request.isTable()) {
        this.documentationService.getTable(request).subscribe((data: SingleResponse<any>) => {
          this.response = data;
          if (request.isColumn()) {
            const name = [this.response.model.schema, this.response.model.name, request.column].join('.');
            this.title = 'Edit description for \'' + name + '\' column';
            this.response.model.columns.forEach(column => {
              if (column.name === request.column) {
                this.form.get('description').setValue(column.description);
              }
            });
          } else {
            this.title = 'Edit description for \'' + this.response.model.fullName + '\' table';
            this.form.get('description').setValue(this.response.model.description);
          }
        });
      } else if (request.isView()) {
        this.documentationService.getView(request).subscribe((data: SingleResponse<any>) => {
          this.response = data;
          if (request.isColumn()) {
            const name = [this.response.model.schema, this.response.model.name, request.column].join('.');
            this.title = 'Edit description for \'' + name + '\' column';
            this.response.model.columns.forEach(column => {
              if (column.name === request.column) {
                this.form.get('description').setValue(column.description);
              }
            });
          } else {
            this.title = 'Edit description for \'' + this.response.model.fullName + '\' view';
            this.form.get('description').setValue(this.response.model.description);
          }
        });
      }
    });
  }

  save(): void {
    this.working = true;
    const request = DbRequestHelper.createFromId(this.id);
    request.description = this.form.get('description').value;
    this.documentationService.editDescription(request).subscribe((data: SingleResponse<any>) => {
      if (request.isTable()) {
        this.router.navigate(request.getTableRoute());
      } else if (request.isView()) {
        this.router.navigate(request.getViewRoute());
      }
    }, data => {
      this.working = false;
      const dialogRef = this.dialog.open(EditDescriptionDialogComponent, {
        width: '500px',
        data: {
          error: data.error
        }
      });
      dialogRef.afterClosed().subscribe(result => {});
    });
  }

  back(): void {
    const request = DbRequestHelper.createFromId(this.id);
    if (request.isTable()) {
      this.router.navigate(request.getTableRoute());
    } else if (request.isView()) {
      this.router.navigate(request.getViewRoute());
    }
  }
}

export class EditDescriptionDialogData {
  public data: object;
}

@Component({
  selector: 'app-edit-description-dialog',
  templateUrl: 'edit-description-dialog.component.html',
})
export class EditDescriptionDialogComponent {
  constructor(
    public dialogRef: MatDialogRef<EditDescriptionDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: EditDescriptionDialogData) {
    }

  onNoClick(): void {
    this.dialogRef.close();
  }
}
