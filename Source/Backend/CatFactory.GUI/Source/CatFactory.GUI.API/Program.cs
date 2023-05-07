using CatFactory.GUI.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<CodeFactoryService>();
builder.Services.AddScoped<GUISettings>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("molly", builder =>
    {
        builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().WithOrigins("http://localhost:7441", "https://localhost:7441");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("molly");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
