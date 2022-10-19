// See https://aka.ms/new-console-template for more information

using MyECS;

Action<App> printEvensSystem = app =>
{
    var evens = app 
        .Query<int>()
        .Where(i => i % 2 == 0);
    
    foreach (int num in evens)
        Console.WriteLine(num);
};

new App()
    .AddEntity(2)
    .AddEntity(5)
    .AddEntity(3)
    .AddEntity(7)
    .AddEntity(4)
    .AddEntity(8)
    .AddSystem(printEvensSystem)
    .Run();