using BLGDLab.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder?.Configuration["Jwt:Issuer"]?.ToString() ?? "",
                ValidAudience = builder?.Configuration["Jwt:Audience"]?.ToString() ?? "",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder?.Configuration["Jwt:Key"]?.ToString() ?? ""))
            };
        });

builder.Services.AddCors(options =>
{
    //options.AddPolicy("AllowSpecificOrigin",
    //    builder =>
    //    {
    //        builder.WithOrigins("http://specificUrl.Com").AllowAnyHeader().AllowAnyMethod();
    //    }
    //);

    

    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });

});


string blgdConnString = SampleLibOut.LibOut.Out(1);
string ablgdConnString = SampleLibOut.LibOut.Out(2);

blgdConnString = string.IsNullOrWhiteSpace(blgdConnString) ? builder.Configuration["ConnectionString:BLGDConnection"] ?? string.Empty : blgdConnString;

builder.Services.AddDataAccessLayer(builder.Configuration, blgdConnString);
builder.Services.AddBLAccessLayer();

var app = builder.Build();

// Configure the HTTP request pipeline.

//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.MapOpenApi();
    app.UseDeveloperExceptionPage();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "BLGDLab API v1");
        options.RoutePrefix = "swagger";
    });
}

app.UseRouting();

app.UseCors();

app.UseAuthentication();

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
