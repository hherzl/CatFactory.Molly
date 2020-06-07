import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ImportDatabaseRequest, DbRequest } from './requests';

@Injectable({
  providedIn: 'root'
})
export class DocumentationService {
  private baseUrl: string;

  constructor(private httpClient: HttpClient) {
    this.baseUrl = 'http://localhost:8400/api/v1/Documentation';
  }

  public getImportedDatabases(): Observable<object> {
    return this.httpClient.get([this.baseUrl, 'ImportedDatabases'].join('/'));
  }

  public importDatabase(model: ImportDatabaseRequest): Observable<object> {
    return this.httpClient.post([this.baseUrl, 'ImportDatabase'].join('/'), model);
  }

  public getDatabaseDetail(model: DbRequest): Observable<object> {
    return this.httpClient.post([this.baseUrl, 'DatabaseDetail'].join('/'), model);
  }

  public getTable(model: DbRequest): Observable<object> {
    return this.httpClient.post([this.baseUrl, 'Table'].join('/'), model);
  }

  public getView(model: DbRequest): Observable<object> {
    return this.httpClient.post([this.baseUrl, 'View'].join('/'), model);
  }

  public editDescription(model: DbRequest): Observable<object> {
    return this.httpClient.post([this.baseUrl, 'EditDescription'].join('/'), model);
  }
}
