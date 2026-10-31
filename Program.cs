using BilleteraDemo.Endpoints;
using BilleteraDemo.Services;
using BilleteraDemo.UI;
using Preparation.Shared.Web;

var info = new ProjectInfo("wallet", "Billetera digital simulada", "BilleteraDb", 5103, "Prueba transacciones y ledger en una simulación financiera.");
var builder = WebApplication.CreateBuilder(args);

// Register wallet transaction and security service
builder.Services.AddSingleton<IWalletTransactionService, WalletTransactionService>();

ProjectBootstrap.Configure(builder, info);
var app = builder.Build();
await ProjectBootstrap.InitializeAsync(app, info);
ProjectBootstrap.ConfigurePipeline(app, info);

// Map Wallet enhanced endpoints and register modern UI layout
WalletEndpoints.Map(app);
DashboardRenderer.RegisterCustomView(info.Key, WalletLayout.Render);
app.MapControllers();

await app.RunAsync();
