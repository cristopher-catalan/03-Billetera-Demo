namespace BilleteraDemo.UI.Components;

public static class VirtualCard
{
    public const string Html = """
    <article class="virtual-card">
        <div class="card-top">
            <div class="chip"></div>
            <span style="font-size:12px; font-weight:800; color:var(--primary); letter-spacing:1px;">DEMO DIGITAL</span>
        </div>
        <div>
            <div class="card-balance-label">Saldo Disponible Simulado</div>
            <div class="card-balance-val" id="cardBalanceDisplay">S/ 0.00</div>
            <div class="card-number" id="cardNumberDisplay">4557 •••• •••• 1001</div>
        </div>
        <div class="card-bottom">
            <div>
                <span style="color:#7899a8; font-size:9px; text-transform:uppercase;">Titular</span>
                <div class="card-holder" id="cardHolderDisplay">USUARIO DEMO</div>
            </div>
            <div style="text-align:right;">
                <span style="color:#7899a8; font-size:9px; text-transform:uppercase;">Válido Hasta</span>
                <div style="font-weight:700; color:#fff;">12/30</div>
            </div>
        </div>
    </article>
    """;
}
