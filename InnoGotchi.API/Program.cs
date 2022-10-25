using InnoGotchi.API.AuthOptions;
using InnoGotchi.BusinessLogic.Components;
using InnoGotchi.BusinessLogic.MappingProfiles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = AuthApiOptions.ISSUER,

        ValidateAudience = true,
        ValidAudience = AuthApiOptions.AUDIENCE,

        ValidateLifetime = true,

        IssuerSigningKey = AuthApiOptions.GetSymmetricSecurityKey(),
        ValidateIssuerSigningKey = true,
    };
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigurationBusinessLogicManagers(
    builder.Configuration.GetConnectionString("InnoGotchiDbConnection"));

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<PetMapProfile>();
    cfg.AddProfile<FarmMapProfile>();
    cfg.AddProfile<UserMapProfile>();
    cfg.AddProfile<RoleMapProfile>();
});

var app = builder.Build();

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
