using Presentation;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

builder.Services.AddScoped<ISignUpService, SignUpService>();
builder.Services.AddGrpcClient<AccountGrpcService.AccountGrpcServiceClient>(x =>
{
    x.Address = new Uri(builder.Configuration["Providers:AccountServiceProvider"]!);
});

var app = builder.Build();


app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Ventixe AuthServiceProvider API");
    options.RoutePrefix = string.Empty;
});


//// Se till att controllers mapplas
//app.UseRouting();
// // Lägg till denna rad

app.UseHsts();
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
