

using Microsoft.EntityFrameworkCore;
using WEB_153551_BOHDAN.API.Data;
using WEB_153551_BOHDAN.API.Services;
using WEB_153551_BOHDAN.UI.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ��������� �������� ���� ������
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("WW_XXX.API") // ��������� ������ ��� ��������
    ));

// ��������� CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowAnyOrigin();
    });
});

var app = builder.Build();

// ��������� �������� � ��������� ��������� ������
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
        // ��������� ��������� ������
        if (!context.Categories.Any())
        {
            context.Categories.AddRange(
                new Category { Name = "������ �����", NormalizedName = "first-courses" },
                new Category { Name = "������", NormalizedName = "salads" },
                new Category { Name = "�������", NormalizedName = "drinks" }
            );
            context.SaveChanges();
        }

        if (!context.Dishes.Any())
        {
            var category = context.Categories.First();
            context.Dishes.AddRange(
                new Dish
                {
                    Name = "����",
                    Description = "������������ ����",
                    Calories = 350,
                    CategoryId = category.Id
                },
                new Dish
                {
                    Name = "���-�����",
                    Description = "������ ���",
                    Calories = 400,
                    CategoryId = category.Id
                }
            );
            context.SaveChanges();
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "��������� ������ ��� �������� ���� ������");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();
