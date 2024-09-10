using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Filters;
using be.Repositories.ModRepository;
using be.Models;
using be.Services.ModService;
using be.Services.PostService;
using be.Services.PostcommentService;
using be.Repositories.UserRepository;
using be.Services.UserService;
using be.Repositories.TestDetailRepository;
using be.Services.TestDetailService;
using be.Services.SubjectService;
using be.Repositories.PostRepository;
using be.Repositories.PostcommentRepository;
using be.Repositories.SubjectRepository;
using be.Services.TopicService;
using be.Repositories.TopicRepository;
using be.Repositories.QuestionRepository;
using be.Services.QuestionService;
using be.Repositories.StatictisRepository;
using be.Services.StatictisService;
using be.Repositories.NewsRepository;
using be.Services.NewsService;
using be.Repositories.QuestionTestRepository;
using be.Services.QuestionTestService;
using be.Repositories.NewFolder;
using be.Repositories.SuperAdminRepository;
using be.Services.SuperAdminService;
using be.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); ;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddDbContext<SwtDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SwtDbContext")));
builder.Services.AddCors();

var services = builder.Services;
services.AddHttpContextAccessor();

services.AddScoped<ICouseCharterRepository, CouseCharterRepository>();

services.AddScoped<IModRepository, ModRepository>();
services.AddScoped<IModService, ModService>();

services.AddScoped<IPostRepository, PostRepository>();
services.AddScoped<IPostService, PostService>();

services.AddScoped<IPostcommentRepository, PostcommentRepository>();
services.AddScoped<IPostcommentService, PostcommentService>();

services.AddScoped<ISubjectRepository, SubjectRepository>();
services.AddScoped<ISubjectService, SubjectService>();

services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IUserService, UserService>();

services.AddScoped<ITopicRepository, TopicRepository>();
services.AddScoped<ITopicService, TopicService>();

services.AddScoped<IQuestionRepository, QuestionRepository>();
services.AddScoped<IQuestionService, QuestionService>();

services.AddScoped<ITestDetailRepository, TestDetailRepository>();
services.AddScoped<ITestDetailService, TestDetailService>();

services.AddScoped<IStatictisRepository, StatictisRepository>();
services.AddScoped<IStatictisService, StatictisService>();

services.AddScoped<INewsRepository, NewsRepository>();
services.AddScoped<INewsService, NewsService>();

services.AddScoped<IQuestionTestRepository, QuestionTestRepository>();
services.AddScoped<IQuestionTestService, QuestionTestService>();

services.AddScoped<ISuperAdminRepository, SuperAdminRepository>();
services.AddScoped<ISuperAdminService, SuperAdminService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.Run();
