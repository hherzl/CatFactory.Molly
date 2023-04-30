export class Response {
    public message!: string;
}

export class ListResponse<TModel> {
    public model!: TModel[];
}

export class SingleResponse<TModel> {
    public model!: TModel;
}
