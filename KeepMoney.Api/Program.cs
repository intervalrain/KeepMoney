using KeepMoney.Api;
using KeepMoney.Application;
using KeepMoney.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    var port = Environment.GetEnvironmentVariable("PORT") ?? "3000";
    builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(
            policy =>
            {
                policy.WithOrigins("*")
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
    });

    builder.Services.AddApplication()
                    .AddPresentation()
                    .AddInfrastructure(builder.Configuration);
}

var app = builder.Build();
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    app.UseCors();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseHttpsRedirection();
    app.MapControllers();

    app.Run();
}

