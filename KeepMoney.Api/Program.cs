using KeepMoney.Api;
using KeepMoney.Application;
using KeepMoney.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddApplication()
                    .AddPresentation()
                    .AddInfrastructure(builder.Configuration);
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseAuthentication();
    app.UseAuthorization();

    app.UseHttpsRedirection();
    app.MapControllers();

    app.Run();
}

