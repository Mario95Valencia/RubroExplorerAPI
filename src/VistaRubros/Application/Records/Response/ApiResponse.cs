namespace vistarubros.Application.Records.Response
{
    public record ApiResponse<T>
    {
        public Guid Id { get; init; } = new();
        public string Type { get; init; } = string.Empty;
        public T? Data { get; init; }
        public string? Message { get; init; } = string.Empty;
        // NUEVO CAMPO: cantidad de elementos devueltos
        public int? Count { get; init; }
        public ApiResponse(Guid Id, string Type, T? Data, string? Message, int? Count = null)
        {
            this.Id = Id;
            this.Type = Type;
            this.Data = Data;
            this.Message = Message;
            this.Count = Count;
        }
        public ApiResponse() { }
    }
}
