using api_relation.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( options => 
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    // options.OperationFilter<SecurityRequirementsOperationFilter>();
});


builder.Services.AddDbContext<DataDbContext>
(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddCors(options => options.AddPolicy("MyCors", policy => 
{
    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
}));


builder.Services.AddAuthentication();
builder.Services.AddIdentityApiEndpoints<IdentityUser>().AddEntityFrameworkStores<DataDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapIdentityApi<IdentityUser>();

app.UseCors("MyCors");

app.MapControllers();



app.UseHttpsRedirection();


app.Run();