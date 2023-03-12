using LagDinCv.Api;
using LagDinCv.Api.ApiGroups;

var builder = WebApplication.CreateBuilder(args);

// Add Services
builder.Services.AddPdfBuilder();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapApi();
app.UseCors();

app.Run();
