export class ImportDatabaseRequest {
    public name: string;
    public connectionString: string;
    public importTables: boolean;
    public importViews: boolean;
}

export class DbRequest {
    public name: string;
    public connectionString: string;
    public type: string;
    public table: string;
    public view: string;
    public column: string;
    public description: string;
    public isTable(): boolean {
        return this.type === 'table';
    }

    public isView(): boolean {
        return this.type === 'view';
    }

    public isColumn(): boolean {
        return this.column ? true : false;
    }

    public getTableRoute(): string[] {
        return ['table-details', [this.name, this.type, this.table].join('|')];
    }
    public getViewRoute(): string[] {
        return ['view-details', [this.name, this.type, this.view].join('|')];
    }
}

export class DbRequestHelper {
    public static createFromId(id: string): DbRequest {
        const values = id.split('|');
        const request = new DbRequest();
        request.name = values[0];
        request.type = values[1];
        if (request.isTable()) {
            request.table = values[2];
        } else if (request.isView()) {
          request.view = values[2];
        }
        if (values.length === 4) {
            request.column = values[3];
        }
        return request;
    }

    public static getDbName(id: string): string {
        const values = id.split('|');
        return values[0];
    }
}
