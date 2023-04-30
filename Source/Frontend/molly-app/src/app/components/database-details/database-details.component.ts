import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { SingleResponse } from 'src/app/services/common';
import { DatabaseDetailsModel, MollyClientService, TableItemModel } from 'src/app/services/molly-client.service';

@Component({
  selector: 'app-database-details',
  templateUrl: './database-details.component.html',
  styleUrls: ['./database-details.component.css']
})
export class DatabaseDetailsComponent implements OnInit {
  constructor(private activatedRoute: ActivatedRoute, private router: Router, private mollyClient: MollyClientService) {
  }

  public loading!: boolean;
  private id!: string;
  public response!: SingleResponse<DatabaseDetailsModel>;

  ngOnInit(): void {
    this.loading = true;

    this.activatedRoute.params.forEach((params: Params) => {
      this.id = params['id'];

      this.mollyClient.getDatabase(this.id).subscribe(result => {
        this.loading = false;
        this.response = result;
      });
    });
  }

  details(item: TableItemModel): void {
    this.router.navigate([`database/${this.id}/table/${item.fullName}`]);
  }
}
