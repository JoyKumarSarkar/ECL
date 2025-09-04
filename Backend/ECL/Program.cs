using ECL.BLL;
using ECL.BLL.Implementation;
using ECL.BLL.Signature;
using ECL.Data.ECLContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<IJsonHttpService, JsonHttpService>();
builder.Services.AddScoped<IPythonService, PythonService>(); // Register your Python service too

builder.Services.AddDbContext<ECLDBContext>(options =>
    options.UseSqlServer("Data Source=acer\\sqlexpress;Initial Catalog=ECL;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"));
builder.Services.AddScoped<DbContext, ECLDBContext>();
builder.Services.AddScoped<IBLLBase, BLLCommon>();


var app = builder.Build();

app.UseCors("corsApp");
_ = app.UseCors(builder =>
{
    _ = builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
