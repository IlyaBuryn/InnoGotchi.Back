using InnoGotchi.BusinessLogic.Components;
using InnoGotchi.BusinessLogic.MappingProfiles;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigurationBusinessLogicManagers(
    builder.Configuration.GetConnectionString("InnoGotchiDbConnection"));

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<PetMapProfile>();
    cfg.AddProfile<FarmMapProfile>();
});

var app = builder.Build();

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
