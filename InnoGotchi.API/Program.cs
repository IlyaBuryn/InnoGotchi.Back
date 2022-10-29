using InnoGotchi.API.Settings;
using InnoGotchi.BusinessLogic.Components;
using InnoGotchi.BusinessLogic.MappingProfiles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = JwtSettings.GetJwtOptions(builder.Configuration);
    });

builder.Services.AddCors();



builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<PetMapProfile>();
    cfg.AddProfile<FarmMapProfile>();
    cfg.AddProfile<UserMapProfile>();
    cfg.AddProfile<RoleMapProfile>();
    cfg.AddProfile<PageMapProfile>();
});

builder.Services.ConfigurationBusinessLogicManagers(
    builder.Configuration.GetConnectionString("InnoGotchiDbConnection"));

var app = builder.Build();

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();   
app.MapControllers();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

//app.UseMiddleware<JwtMiddleware>();
app.UseEndpoints(x => x.MapControllers());


app.Run();
