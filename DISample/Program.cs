var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ScopedClass>();
builder.Services.AddTransient<TransientClass>();
builder.Services.AddSingleton<SingletonClass>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/DiSample", MyHandler).WithOpenApi();

app.Run();

static async Task MyHandler(
    ScopedClass scopedClass, 
    SingletonClass singletonClass,
    TransientClass transientClass,
    IServiceScopeFactory serviceScopeFactory)
{
    scopedClass.Name = "Anish";
    
    using (var scope = serviceScopeFactory.CreateScope())
    {
        var singletonClass1 = scope.ServiceProvider.GetRequiredService<SingletonClass>();
        singletonClass1._transientClass.Age = 50;
        var scopedObj1 = scope.ServiceProvider.GetRequiredService<ScopedClass>();
        scopedObj1.Name = "Test";
        scopedObj1._transientClass.Age = 30;
        var transientObj1 = scope.ServiceProvider.GetRequiredService<TransientClass>();
        transientObj1.Age = 20;
        
        var scopedObj2 = scope.ServiceProvider.GetRequiredService<ScopedClass>();
        var transientObj2 = scope.ServiceProvider.GetRequiredService<TransientClass>();
        var singletonClass2 = scope.ServiceProvider.GetRequiredService<SingletonClass>();
    }
    
    using (var scope = serviceScopeFactory.CreateScope())
    {
        var scopedObj1 = scope.ServiceProvider.GetRequiredService<ScopedClass>();
        var transientObj1 = scope.ServiceProvider.GetRequiredService<TransientClass>();
        var singletonClass1 = scope.ServiceProvider.GetRequiredService<SingletonClass>();
    }
}


public class SingletonClass
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    public readonly TransientClass _transientClass;

    public SingletonClass(IServiceScopeFactory serviceScopeFactory, TransientClass transientClass)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _transientClass = transientClass;
    }

    public async Task Demo()
    {
        CancellationTokenSource tokenSource = new CancellationTokenSource();
        CancellationToken token = tokenSource.Token;
        tokenSource.CancelAfter(2000);
        
        while (!token.IsCancellationRequested)
        {
            await Task.Delay(10000);
        }
        
        
        
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var myScopedClass = scope.ServiceProvider.GetRequiredService<ScopedClass>();
        }
    }
    
    
    
    public string Name { get; set; }
}




public class ScopedClass
{
    public readonly TransientClass _transientClass;

    public ScopedClass(TransientClass transientClass)
    {
        _transientClass = transientClass;
    }
    public string Name { get; set; }
}

public class TransientClass
{
    public int Age { get; set; }
}