using AzFuncTableOperation.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;




var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(s=>s.AddScoped<ITableOperations, TableOperations>())
    .Build();

host.Run();
