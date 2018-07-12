import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DbRequest, ImportDatabaseRequest } from './models';

@Injectable({
  providedIn: 'root'
})
export class DbService {

  private baseUrl: string;

  constructor(private httpClient: HttpClient) {
    this.baseUrl = 'http://localhost:8400/api/v1/Db';
  }

  public getImportedDatabases(): Observable<Object> {
    return this.httpClient.get([this.baseUrl, 'ImportedDatabases'].join('/'));
  }

  public importDatabase(model: ImportDatabaseRequest): Observable<Object> {
    return this.httpClient.post([this.baseUrl, 'ImportDatabase'].join('/'), model);
  }

  public getDatabaseDetail(model: DbRequest): Observable<Object> {
    return this.httpClient.post([this.baseUrl, 'DatabaseDetail'].join('/'), model);
  }

  public getTable(model: DbRequest): Observable<Object> {
    return this.httpClient.post([this.baseUrl, 'Table'].join('/'), model);
  }

  public getView(model: DbRequest): Observable<Object> {
    return this.httpClient.post([this.baseUrl, 'View'].join('/'), model);
  }

  public editDescription(model: DbRequest): Observable<Object> {
    return this.httpClient.post([this.baseUrl, 'EditDescription'].join('/'), model);
  }
}
