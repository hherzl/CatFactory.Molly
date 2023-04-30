import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { SingleResponse } from 'src/app/services/common';
import { DatabaseDetailsModel, MollyClientService } from 'src/app/services/molly-client.service';

@Component({
  selector: 'app-database-details',
  templateUrl: './database-details.component.html',
  styleUrls: ['./database-details.component.css']
})
export class DatabaseDetailsComponent implements OnInit {
  constructor(private activatedRoute: ActivatedRoute, private mollyClient: MollyClientService) {
  }

  private id!: string;
  public response!: SingleResponse<DatabaseDetailsModel>;

  ngOnInit(): void {
    this.activatedRoute.params.forEach((params: Params) => {
      this.id = params['id'];
    });

    this.mollyClient.getDatabase(this.id).subscribe(result => {
      this.response = result;
    })
  }
}
