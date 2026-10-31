namespace BilleteraDemo.UI.Components;

public static class TransferModal
{
    public const string Html = """
    <div id="transferModal" class="modal-overlay">
        <div class="modal">
            <div class="modal-header">
                <h3 style="color:#fff;">Transferencia Ficticia Inmediata</h3>
                <button type="button" class="btn btn-outline" onclick="closeModal('transferModal')">✕</button>
            </div>
            <form id="transferForm" onsubmit="handleSendTransfer(event)">
                <div class="modal-body">
                    <div class="form-group">
                        <label for="transferSource">Cuenta de Origen</label>
                        <select id="transferSource" name="sourceAccountId" required onchange="updateSourceAccount()"></select>
                    </div>
                    <div class="form-group">
                        <label for="transferDest">Cuenta de Destino (Beneficiario)</label>
                        <select id="transferDest" name="destinationAccountId" required></select>
                    </div>
                    <div class="form-group">
                        <label for="transferAmount">Importe a Transferir (S/) *</label>
                        <input id="transferAmount" name="amount" type="number" step="0.5" min="1" value="25.00" required>
                    </div>
                    <div style="display:flex; gap:8px;">
                        <button type="button" class="btn btn-outline" onclick="setQuickAmount(10)" style="font-size:11px;">+ S/ 10</button>
                        <button type="button" class="btn btn-outline" onclick="setQuickAmount(25)" style="font-size:11px;">+ S/ 25</button>
                        <button type="button" class="btn btn-outline" onclick="setQuickAmount(50)" style="font-size:11px;">+ S/ 50</button>
                        <button type="button" class="btn btn-outline" onclick="setQuickAmount(100)" style="font-size:11px;">+ S/ 100</button>
                    </div>
                    <div class="form-group" style="margin-top:6px;">
                        <div style="display:flex; justify-content:space-between; align-items:center;">
                            <label for="transferIdempotency">Clave de Idempotencia (Garantía Anti-Duplicado)</label>
                            <button type="button" onclick="regenerateIdempotencyKey()" style="background:none; border:none; color:var(--primary); font-size:11px; cursor:pointer;">↻ Nueva clave</button>
                        </div>
                        <input id="transferIdempotency" name="idempotencyKey" readonly style="font-family:monospace; font-size:11px; color:#8ca3ba;">
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-outline" onclick="closeModal('transferModal')">Cancelar</button>
                    <button type="submit" class="btn btn-primary" id="btnSubmitTransfer">Confirmar Transferencia</button>
                </div>
            </form>
        </div>
    </div>
    """;

    public const string Script = """
    function setQuickAmount(amt) {
        byId('transferAmount').value = Number(amt).toFixed(2);
    }

    function regenerateIdempotencyKey() {
        byId('transferIdempotency').value = 'tx-' + Math.random().toString(36).substring(2, 10) + '-' + Date.now().toString(36);
    }
    """;
}
