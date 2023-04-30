import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ListResponse, SingleResponse } from './common';

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

  public getDatabase(id: string): Observable<SingleResponse<DatabaseDetailsModel>> {
    const url = `${this.endpoint}/database/${id}`;
    return this.http.get<SingleResponse<DatabaseDetailsModel>>(url);
  }
}

export class DatabaseItemModel {
  
  public name!: string;
  public dbms!: string;
  public tablesCount!: number;
  public viewsCount!: number;

  public details!: string;
}

export class DatabaseDetailsModel {
  public name!: string;
  public dbms!: string;
  public tables!: TableItemModel[];
  public views!: ViewItemModel[];
  public databaseTypeMaps!: DatabaseTypeMap[];
}

export class TableItemModel {
  public schema!: string;
  public name!: string;
  public type!: string;
  public fullName!: string;
  public columnsCount!: number;
  public primaryKey!: string;
  public identity!: string;
}

export class ViewItemModel {
  public schema!: string;
  public name!: string;
  public type!: string;
  public fullName!: string;
  public columnsCount!: number;
  public identity!: string;
}

export class DatabaseTypeMap {
  public databaseType!: string;
  public allowsLengthInDeclaration!: boolean;
  public allowsPrecInDeclaration!: boolean;
  public allowsScaleInDeclaration!: boolean;
  public clrFullNameType!: string;
  public hasClrFullNameType!: boolean;
  public clrAliasType!: string;
  public hasClrAliasType!: boolean;
  public allowClrNullable!: boolean;
  public dbTypeEnum!: number;
  public isUserDefined!: boolean;
  public parentDatabaseType!: string;
  public collation!: string;
  public importBag!: any;
}
