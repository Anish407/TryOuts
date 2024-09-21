using System.Linq.Expressions;
using EFCoreQuerying.Context;
using EFCoreQuerying.EF;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContextPool<MyDbContext>(op =>
    op.UseSqlServer(builder.Configuration.GetConnectionString("EfCoreQueryingContext")));
WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/Demo", async (MyDbContext context) =>
{
    new List<string>().Where(i => true);
    IQueryable<Author> x = context.Authors.Where(i => i.Age != 3);
    return await x.ToListAsync();
});
app.Run();
/*
Query Question
Using Entity Framework Core, write a query that returns the titles of all books that have an average
review rating of 4.5 or higher and were published after 1950. The result should include the book title,
the author's name, the average rating, and the total number of sales for those books.
*/

// app.MapGet("/Question1", async (MyDbContext context) =>
//     {
//         var result = await GetTitlesWithRatingAndPublishAfter(4.5, 1950, context);
//         return result; 
//     })
//     .WithName("GetWeatherForecast")
//     .WithOpenApi();

public abstract class GenericRepo<T> where T : class
{
    public abstract string DBName { get; set; }
    private DbSet<T> _dbSet { get; }

    public GenericRepo(MyDbContext context)
    {
        _dbSet = context.Set<T>();
    }

    public async Task<IList<T>> Where(Expression<Func<T, bool>> exp)
    {
        return await _dbSet.Where(exp).ToListAsync();
    }
}

public abstract class Demo
{
    
}

public interface MyInterface
{
    void display(int x);
}

public interface MyInterface2
{
    void display(int x);
}

public class MyClass : MyInterface, MyInterface2
{
    void MyInterface.display(int x)
    {
    }

    void MyInterface2.display(int x)
    {
    
    }

    void sample()
    {
       var x =new MyClass();
       ((MyInterface)x).display(2);
       ((MyInterface2)x).display(2);

    }
}


public class StudentRepo : GenericRepo<Author>
{
    public override string DBName { get; set; }

    public StudentRepo(MyDbContext context) : base(context)
    {
        ((MyInterface)new MyClass()).display(3);
    }
}

public class StudentRepo2 : GenericRepo<Author>
{
    public StudentRepo2(MyDbContext context) : base(context)
    {
    }

    public override string DBName { get; set; }
}