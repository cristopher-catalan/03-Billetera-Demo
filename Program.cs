using Preparation.Shared.Web;

var info = new ProjectInfo("wallet", "Billetera digital simulada", "BilleteraDemoDb", 5103, "Simula transferencias ficticias con libro contable e idempotencia.");
var builder = WebApplication.CreateBuilder(args);
ProjectBootstrap.Configure(builder, info);
var app = builder.Build();
await ProjectBootstrap.InitializeAsync(app, info);
ProjectBootstrap.ConfigurePipeline(app, info);
app.MapControllers();
await app.RunAsync();
