namespace BilleteraDemo.Domain;

public sealed record AccountCardDto(
    int Id,
    string Owner,
    string Code,
    string Currency,
    decimal Balance,
    bool Active,
    string MaskedCardNumber
);

public sealed record TransferReceiptDto(
    int Id,
    int SourceAccountId,
    string SourceOwner,
    int DestinationAccountId,
    string DestinationOwner,
    decimal Amount,
    string Currency,
    string IdempotencyKey,
    string State,
    DateTimeOffset CreatedAt,
    bool WasReused
);

public sealed record BeneficiaryDto(
    int Id,
    string Owner,
    string Alias,
    int DestinationAccountId,
    string DestinationCode
);
