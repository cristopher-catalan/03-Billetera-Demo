namespace BilleteraDemo.Services;

public interface IWalletTransactionService
{
    string GenerateIdempotencyKey(string prefix = "tx");
    string GenerateMaskedCardNumber(string accountCode);
    bool ValidateTransfer(decimal amount, decimal currentBalance, out string? errorMessage);
}

public sealed class WalletTransactionService : IWalletTransactionService
{
    public string GenerateIdempotencyKey(string prefix = "tx")
    {
        return $"{prefix}-{Guid.NewGuid():N}";
    }

    public string GenerateMaskedCardNumber(string accountCode)
    {
        var clean = accountCode.Replace("-", "").PadLeft(16, '0');
        var last4 = clean.Length >= 4 ? clean[^4..] : "0000";
        return $"4557 •••• •••• {last4}";
    }

    public bool ValidateTransfer(decimal amount, decimal currentBalance, out string? errorMessage)
    {
        if (amount <= 0)
        {
            errorMessage = "El monto debe ser estrictamente mayor a cero.";
            return false;
        }

        if (amount > currentBalance)
        {
            errorMessage = $"Saldo ficticio insuficiente (Disponible: S/ {currentBalance:0.00}).";
            return false;
        }

        errorMessage = null;
        return true;
    }
}
