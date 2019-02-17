export class ListResponse<TModel> {
    public message: string;
    public didError: boolean;
    public errorMessage: string;
    public model: TModel[];
}

export class SingleResponse<TModel> {
    public message: string;
    public didError: boolean;
    public errorMessage: string;
    public model: TModel;
}

export class ImportDatabaseResponse {
    public message: string;
    public didError: boolean;
    public errorMessage: string;
}

export class ImportedDatabase {
    public name: string;
    public tablesCount: number;
    public viewsCount: number;
    public details: string;
}

export class DatabaseTypeMap {
    public databaseType: string;
    public allowsLengthInDeclaration: boolean;
    public clrFullNameType: string;
}

export class DatabaseDetail {
    public name: string;
    public tables: TableDetail[];
    public views: ViewDetail[];
    public mappings: DatabaseTypeMap[];
}

export class TableDetail {
    public Schema: string;
    public Name: string;
    public ColumnsCount: number;
    public PrimaryKey: string;
    public Identity: string;
    public Details: string;
}

export class ViewDetail {
    public Schema: string;
    public Name: string;
    public ColumnsCount: number;
    public Identity: string;
    public Details: string;
}
