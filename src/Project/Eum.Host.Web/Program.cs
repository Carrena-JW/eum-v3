using Eum.Core.Service;
using Eum.Core.Module;
using Eum.Core.Shared;
using Eum.Core;
using Eum.Core.Shared.Infra.Identity.JwtAuth;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddJwtAuth();
builder.Services.AddAuthClient();
builder.Services.AddServiceDeskClient();
builder.Services.ConfigureRepositories();
builder.Services.ConfigureServices();
new SharedModule().ConfigureServices(builder.Services);

var app = builder.Build();
app.ConfigureEumCore();
new SharedModule().ApplicationLoaded(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<AuthorizationHeaderMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


//인증처리 

//    커스텀 라우터 

//    api/mail/cal 



//    -> getCalList()