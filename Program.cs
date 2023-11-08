var builder = WebApplication.CreateBuilder();

builder.Services.AddAuthentication()
    .AddCookie();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("MyPolicy", policyBuilder =>
    {
        policyBuilder.RequireRole("admin");
        policyBuilder.RequireClaim("claimType", "claimValue1", "claimValue1");
    });
});

builder.Services.AddControllers();

var webApplication = builder.Build();

webApplication.UseAuthentication()
    .UseAuthorization();

webApplication.MapControllers();

webApplication.Run();