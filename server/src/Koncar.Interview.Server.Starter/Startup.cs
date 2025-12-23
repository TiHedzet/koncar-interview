namespace Koncar.Interview.Server.Starter;

internal sealed class Startup
{
    public void ConfigureServices(WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);


    }

    public void Configure(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHsts();
        }

        app.UseRouting();

        app.MapControllers();
        
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }
    }
}
