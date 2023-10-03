using DataEncapsulation.HalfLayer.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IUserService, UserService>().AddRouting(options => options.LowercaseUrls = true).AddControllers();
builder.Services.AddEndpointsApiExplorer().AddSwaggerGen();

var app = builder.Build();

app.UseSwagger().UseSwaggerUI();
app.MapControllers();

app.Run();