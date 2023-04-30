import { Component } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { SingleResponse } from 'src/app/services/common';
import { MollyClientService, ViewDetailsModel } from 'src/app/services/molly-client.service';

@Component({
  selector: 'app-view-details',
  templateUrl: './view-details.component.html',
  styleUrls: ['./view-details.component.css']
})
export class ViewDetailsComponent {
  constructor(private activatedRoute: ActivatedRoute, private router: Router, private mollyClient: MollyClientService) {
  }

  public loading!: boolean;
  private db!: string;
  private view!: string;
  public response!: SingleResponse<ViewDetailsModel>;

  ngOnInit(): void {
    this.loading = true;

    this.activatedRoute.params.forEach((params: Params) => {
      this.db = params['db'];
      this.view = params['view'];

      this.mollyClient.getView(this.db, this.view).subscribe(result => {
        this.loading = false;
        this.response = result;
      });
    });
  }
}
