namespace FoxholeIntelboard.Configuration
{
    public static class ConfigureMiddleware
    {
        public static void ConfigurePipeline(this WebApplication app)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();
        }
    }
}
