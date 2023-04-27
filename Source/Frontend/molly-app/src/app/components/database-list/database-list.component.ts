import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable } from '@angular/material/table';
import { DatabaseListDataSource } from './database-list-datasource';
import { DatabaseItemModel, MollyClientService } from 'src/app/services/molly-client.service';
import { ListResponse } from 'src/app/services/common';

@Component({
  selector: 'app-database-list',
  templateUrl: './database-list.component.html',
  styleUrls: ['./database-list.component.css']
})
export class DatabaseListComponent implements AfterViewInit {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatTable) table!: MatTable<DatabaseItemModel>;
  dataSource!: DatabaseListDataSource;

  /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
  displayedColumns = ['name', 'dbms', 'tablesCount'];

  response!: ListResponse<DatabaseItemModel>;

  constructor(private mollyClient: MollyClientService) {
  }

  ngAfterViewInit(): void {
    this.mollyClient.getDatabases().subscribe(result => {
      this.response = result;
      this.dataSource = new DatabaseListDataSource(this.response.model);

      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.paginator;
      this.table.dataSource = this.dataSource;
    });
  }
}
