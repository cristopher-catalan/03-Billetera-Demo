using System.Security.Claims;
using BilleteraDemo.Services;
using Microsoft.EntityFrameworkCore;
using Preparation.Shared.Data;

namespace BilleteraDemo.Endpoints;

public static class WalletEndpoints
{
    public static void Map(WebApplication app)
    {
        var group = app.MapGroup("/api/wallet-mgmt").RequireAuthorization();

        group.MapGet("/destinations", async (AppDbContext db, ClaimsPrincipal user) =>
        {
            var myUsername = user.Identity?.Name ?? "";
            var destinations = await db.DemoAccounts
                .Where(x => x.Active && (user.IsInRole("Administrador") || x.Owner != myUsername))
                .Select(x => new
                {
                    x.Id,
                    x.Code,
                    x.Owner,
                    x.Currency
                })
                .ToListAsync();

            return Results.Ok(destinations);
        });

        group.MapGet("/summary", async (AppDbContext db, ClaimsPrincipal user, IWalletTransactionService service) =>
        {
            var myUsername = user.Identity?.Name ?? "";
            var query = db.DemoAccounts.AsQueryable();
            if (user.IsInRole("Usuario"))
                query = query.Where(x => x.Owner == myUsername);

            var accounts = await query.ToListAsync();
            var result = new List<object>();

            foreach (var a in accounts)
            {
                var bal = await db.LedgerEntries.Where(x => x.AccountId == a.Id).SumAsync(x => (decimal?)x.Amount) ?? 0m;
                result.Add(new
                {
                    a.Id,
                    a.Code,
                    a.Owner,
                    a.Currency,
                    Balance = bal,
                    MaskedCard = service.GenerateMaskedCardNumber(a.Code),
                    a.Active
                });
            }

            return Results.Ok(result);
        });
    }
}
