namespace IntelboardAPI.Extensions
{
    public static class ConfigureApiMiddleware
    {
        // Middleware Injection to Program.cs
        public static void ConfigureApiPipeline(this WebApplication app)
        {
            app.UseCors("AllowFrontend");

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
        }
    }
}
