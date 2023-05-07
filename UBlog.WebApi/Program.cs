using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using UBlog;
using UBlog.EntityFramework;
using UBlog.Services;

var builder = WebApplication.CreateBuilder(args);

var conf = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddAuthorization(); 
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateAudience = true,
            ValidAudience = AuthOptions.Audience,
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.Issuer,
            ValidateLifetime = true,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
        };
        options.Events = new JwtBearerEvents()
        {
            OnChallenge = context =>
            {
                context.HandleResponse();
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";

                // Ensure we always have an error and error description.
                if (string.IsNullOrEmpty(context.Error))
                    context.Error = "invalid_token";
                if (string.IsNullOrEmpty(context.ErrorDescription))
                    context.ErrorDescription = "This request requires a valid JWT access token to be provided";

                // Add some extra context for expired tokens.
                if (context.AuthenticateFailure != null && context.AuthenticateFailure.GetType() == typeof(SecurityTokenExpiredException))
                {
                    var authenticationException = context.AuthenticateFailure as SecurityTokenExpiredException;
                    context.Response.Headers.Add("x-token-expired", authenticationException.Expires.ToString("o"));
                    context.ErrorDescription = $"The token expired on {authenticationException.Expires.ToString("o")}";
                }

                return context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    error = context.Error,
                    error_description = context.ErrorDescription
                }));
            }
        };
    });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbSupport();
builder.Services.AddServices();

var app = builder.Build();

app.UseCors(o =>
{
    o.WithOrigins("http://localhost:3000");
    o.AllowAnyHeader();
    o.AllowCredentials();
    o.AllowAnyMethod();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();