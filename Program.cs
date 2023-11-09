using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder();

// eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJhdXRoX3NhbXBsZSIsImF1ZCI6ImF1dGhfc2FtcGxlIiwiaWF0IjoxNjk5NDk4NTgxLCJleHAiOjE2OTk1MDU5NjEsInJvbGUiOiJhZG1pbiIsImNsYWltVHlwZSI6ImNsYWltVmFsdWUxIn0.GNPJU9R5L6iKymEoLUIGahJNDZras44i9YOXjYVIxyY
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey("authauthauthauthauthauthauthauth"u8.ToArray()),
            ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha256 },
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = "auth_sample",
            ValidAudience = "auth_sample",
            
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("MyPolicy", policyBuilder =>
    {
        policyBuilder.RequireRole("admin");
        policyBuilder.RequireClaim("claimType", "claimValue1", "claimValue2");
    });
});

builder.Services.AddControllers();

var webApplication = builder.Build();

webApplication.UseAuthentication()
    .UseAuthorization();

webApplication.MapControllers();

webApplication.Run();