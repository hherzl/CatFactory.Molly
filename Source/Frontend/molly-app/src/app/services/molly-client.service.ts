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

  public getTable(db: string, table: string): Observable<SingleResponse<TableDetailsModel>> {
    const url = `${this.endpoint}/database/${db}/table/${table}`;
    return this.http.get<SingleResponse<TableDetailsModel>>(url);
  }

  public getView(db: string, view: string): Observable<SingleResponse<ViewDetailsModel>> {
    const url = `${this.endpoint}/database/${db}/view/${view}`;
    return this.http.get<SingleResponse<ViewDetailsModel>>(url);
  }

  public importDatabase(request: ImportDatabaseRequest): Observable<Response> {
    const url = `${this.endpoint}/import-database`;
    return this.http.post<Response>(url, request);
  }
}

export class DatabaseItemModel {
  public name!: string;
  public dbms!: string;
  public tablesCount!: number;
  public viewsCount!: number;
}

export class DatabaseDetailsModel {
  public name!: string;
  public dbms!: string;
  public description!: string;

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
  public description!: string;
}

export class ViewItemModel {
  public schema!: string;
  public name!: string;
  public type!: string;
  public fullName!: string;
  public columnsCount!: number;
  public identity!: string;
  public description!: string;
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

export class ImportDatabaseRequest {
  public name!: string;
  public connectionString!: string;
  public importTables!: boolean;
  public importViews!: boolean;
}

export class TableDetailsModel {
  public fullName!: string;
  public schema!: string;
  public name!: string;
  public description!: string;
  public identity!: IdentityDetailsModel;
  public columns!: ColumnItemModel[];
  public primaryKey!: PrimaryKeyDetailsModel;
  public foreignKeys!: ForeignKeyItemModel[];
  public uniques!: UniqueItemModel[];
  public checks!: CheckItemModel[];
  public defaults!: DefaultItemModel[];
  public indexes!: IndexItemModel[];
}

export class ViewDetailsModel {
  public fullName!: string;
  public schema!: string;
  public name!: string;
  public description!: string;
  public identity!: IdentityDetailsModel;
  public columns!: ColumnItemModel[];
  public indexes!: IndexItemModel[];
}

export class IdentityDetailsModel {
  public name!: string;
  public seed!: number;
  public increment!: number;
}

export class ColumnItemModel {
  public name!: string;
  public type!: string;
  public length!: number;
  public prec!: number;
  public nullable!: number;
  public collation!: string;
  public description!: string;
}

export class PrimaryKeyDetailsModel {
  public constraintName!: string;
  public key!: string[];
}

export class ForeignKeyItemModel {
  public name!: string;
  public key!: string[];
  public references!: string;
}

export class UniqueItemModel {
  public name!: string;
  public key!: string[];
}

export class CheckItemModel {
  public name!: string;
  public key!: string[];
  public expression!: string;
}

export class DefaultItemModel {
  public name!: string;
  public key!: string[];
  public value!: string;
}

export class IndexItemModel {
  public name!: string;
  public description!: string[];
  public keys!: string;
}
