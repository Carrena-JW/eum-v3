using Eum.Core.Service;
using Eum.Core.Module;
using Eum.Core.Shared;
using Eum.Core;
using Eum.Core.Shared.Infra.Identity.JwtAuth;
using Eum.Core.Shared.Infra.Mvc.Filters;
using Eum.Core.Shared.Infra.Mvc;
using Eum.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://0.0.0.0:5000");

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
builder.Host.UseEumLogging();
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

//app.UseHttpsRedirection();

app.UseMiddleware<AuthorizationHeaderMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


//인증처리 

//    커스텀 라우터 

//    api/mail/cal 



//    -> getCalList()