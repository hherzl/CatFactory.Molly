import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ListResponse } from './common';

@Injectable({
  providedIn: 'root'
})
export class MollyClientService {

  private endpoint: string;

  constructor(private http: HttpClient) {
    this.endpoint = 'https://localhost:7440/api/v1';
  }

  public getDatabases(): Observable<ListResponse<DatabaseItemModel>> {
    const url = `${this.endpoint}/database`;
    return this.http.get<ListResponse<DatabaseItemModel>>(url);
  }

  public getDatabase(id: string): Observable<ListResponse<DatabaseItemModel>> {
    const url = `${this.endpoint}/database/${id}`;
    return this.http.get<ListResponse<DatabaseItemModel>>(url);
  }
}

export class DatabaseItemModel {
  public id!: number;
  public name!: string;
  public dbms!: string;
  public tablesCount!: number;
  public viewsCount!: number;
}

export class DatabaseDetailsModel {

}