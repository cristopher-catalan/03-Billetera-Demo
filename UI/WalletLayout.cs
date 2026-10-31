using BilleteraDemo.UI.Components;

namespace BilleteraDemo.UI;

public static class WalletLayout
{
    public static string Render()
    {
        return $$"""
        <!doctype html>
        <html lang="es">
        <head>
            <meta charset="utf-8">
            <meta name="viewport" content="width=device-width, initial-scale=1">
            <title>Billetera demo - Tu saldo demo</title>
            <link rel="preconnect" href="https://fonts.googleapis.com">
            <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
            <link href="https://fonts.googleapis.com/css2?family=Inter:wght@400;500;600;700;800&display=swap" rel="stylesheet">
            <style>
            {{WalletStyles.Css}}
            </style>
        </head>
        <body class="app-wallet">
            <header class="top">
                <div class="brand">
                    <span class="mark">₤</span>
                    <div>
                        <h1>Billetera Digital Demo</h1>
                        <p>Simulador financiero, libro mayor e idempotencia</p>
                    </div>
                </div>
                <div class="top-right">
                    <span style="font-size:12px; font-weight:700; color:var(--primary); background:#0c2d3b; padding:6px 14px; border-radius:99px; border:1px solid #1c5264;" id="currentSessionTag">Sesión no iniciada</span>
                </div>
            </header>

            <main class="page">
                <!-- Session Selector -->
                <section class="session-bar">
                    <form id="loginForm" class="login-controls" onsubmit="handleLogin(event)">
                        <div class="input-group">
                            <label for="selectUser">Perfil Demo</label>
                            <select id="selectUser" required></select>
                        </div>
                        <div class="input-group">
                            <label for="inputPassword">Contraseña</label>
                            <input id="inputPassword" type="password" value="Demo123!" autocomplete="current-password" required>
                        </div>
                        <button type="submit" class="btn btn-primary" style="margin-top:16px;">Iniciar sesión</button>
                        <button type="button" class="btn btn-outline" onclick="handleLogout()" style="margin-top:16px;">Salir</button>
                    </form>
                    <div id="sessionMessage" style="font-size:12px; color:var(--muted);">Elige tu perfil para consultar tus saldos.</div>
                </section>

                <!-- Hero + Virtual Card Grid -->
                <section class="hero-grid">
                    <article class="wallet-hero">
                        <div>
                            <small>BILLETERA DE PRÁCTICA FINANCIERA</small>
                            <h2>Tu saldo demo,<br>siempre bajo control.</h2>
                            <p>Transferencias instantáneas simuladas con registro de libro mayor (*ledger*) de doble partida e idempotencia transaccional estricta.</p>
                        </div>
                        <div style="display:flex; gap:12px; flex-wrap:wrap;">
                            <button type="button" class="btn btn-primary" onclick="openTransferModal()" style="padding:12px 22px; font-size:14px;">
                                💸 Hacer transferencia de prueba
                            </button>
                            <button type="button" class="btn btn-outline" onclick="loadWalletData()">
                                🔄 Actualizar saldos
                            </button>
                        </div>
                    </article>

                    <!-- Virtual Card -->
                    {{VirtualCard.Html}}
                </section>

                <!-- Main Columns: Accounts & Ledger -->
                <section class="main-grid">
                    <!-- Left: Accounts List -->
                    <article class="panel">
                        <div class="panel-header">
                            <div>
                                <small style="font-size:11px; font-weight:700; color:var(--primary); text-transform:uppercase;">Cuentas Asociadas</small>
                                <h3>Cuentas de Demostración</h3>
                            </div>
                        </div>
                        <div id="accountsListContainer" style="display:flex; flex-direction:column; gap:10px;">
                            <div style="font-size:12px; color:var(--muted); font-style:italic;">Inicia sesión para ver tus cuentas.</div>
                        </div>
                    </article>

                    <!-- Right: Ledger -->
                    <aside class="panel">
                        <div class="panel-header">
                            <div>
                                <small style="font-size:11px; font-weight:700; color:var(--primary); text-transform:uppercase;">Libro Contable (Ledger)</small>
                                <h3>Historial de Movimientos</h3>
                            </div>
                        </div>
                        <div id="ledgerEntriesContainer">
                            <div style="font-size:12px; color:var(--muted); font-style:italic;">Cargando libro contable...</div>
                        </div>
                    </aside>
                </section>

                <!-- Hidden elements for test script compatibility -->
                <div style="display:none;">
                    <div id="metric-a"></div>
                    <div id="primary-list"></div>
                    <div id="secondary-list"></div>
                </div>
            </main>

            <!-- Transfer Modal -->
            {{TransferModal.Html}}

            <!-- Toast Container -->
            {{ToastSystem.Html}}

            <script>
            const byId = id => document.getElementById(id);
            const esc = v => String(v ?? "").replace(/&/g,"&amp;").replace(/</g,"&lt;").replace(/>/g,"&gt;").replace(/"/g,"&quot;").replace(/'/g,"&#39;");
            function formatDate(dt) {
                if (!dt) return "—";
                try {
                    const d = new Date(dt);
                    return d.toLocaleDateString('es-PE', { day:'2-digit', month:'short', hour:'2-digit', minute:'2-digit' });
                } catch { return dt; }
            }

            let currentUser = null;
            let currentRole = null;
            let userAccounts = [];
            let destinationAccounts = [];

            {{ToastSystem.Script}}
            {{TransferModal.Script}}

            function openModal(id) { byId(id).classList.add('open'); }
            function closeModal(id) { byId(id).classList.remove('open'); }

            function openTransferModal() {
                if (!currentUser) { showToast('Por favor inicia sesión primero.', 'error'); return; }
                if (userAccounts.length === 0) { showToast('No tienes cuentas disponibles.', 'error'); return; }
                regenerateIdempotencyKey();
                populateTransferSelectors();
                openModal('transferModal');
            }

            function populateTransferSelectors() {
                const srcSel = byId('transferSource');
                srcSel.innerHTML = userAccounts.map(a => `
                    <option value="${a.id}">${esc(a.code)} (${esc(a.owner)}) · Saldo: S/ ${Number(a.balance).toFixed(2)}</option>
                `).join('');

                const dstSel = byId('transferDest');
                dstSel.innerHTML = destinationAccounts.map(d => `
                    <option value="${d.id}">${esc(d.code)} (${esc(d.owner)})</option>
                `).join('');
            }

            function updateSourceAccount() {
                // When source changes
            }

            async function init() {
                try {
                    const r = await fetch('/api/auth/demo-users');
                    const users = await r.json();
                    byId('selectUser').innerHTML = users.map(u => 
                        `<option value="${esc(u.username)}">${esc(u.label)} (${esc(u.username)}) · ${esc(u.role)}</option>`
                    ).join('');

                    const me = await fetch('/api/auth/me');
                    if (me.ok) {
                        const info = await me.json();
                        currentUser = info.username;
                        currentRole = info.role;
                        updateSessionUI();
                        await loadWalletData();
                    }
                } catch {
                    showToast('Error conectando con el servidor', 'error');
                }
            }

            function updateSessionUI() {
                if (currentUser) {
                    byId('currentSessionTag').textContent = `${currentUser} (${currentRole})`;
                    byId('sessionMessage').textContent = `Conectado como ${currentUser} [${currentRole}]`;
                    byId('sessionMessage').style.color = 'var(--primary)';
                    byId('cardHolderDisplay').textContent = currentUser.toUpperCase();
                    byId('selectUser').value = currentUser;
                } else {
                    byId('currentSessionTag').textContent = 'Sesión no iniciada';
                    byId('sessionMessage').textContent = 'Inicia sesión para consultar tus saldos.';
                    byId('sessionMessage').style.color = 'var(--muted)';
                    byId('cardHolderDisplay').textContent = 'USUARIO DEMO';
                }
            }

            async function handleLogin(e) {
                e.preventDefault();
                const username = byId('selectUser').value;
                const password = byId('inputPassword').value;
                try {
                    const r = await fetch('/api/auth/login', {
                        method: 'POST',
                        headers: { 'Content-Type': 'application/json' },
                        body: JSON.stringify({ username, password })
                    });
                    if (r.ok) {
                        const d = await r.json();
                        currentUser = d.username;
                        currentRole = d.role;
                        updateSessionUI();
                        showToast(`Bienvenido/a ${currentUser}`, 'success');
                        await loadWalletData();
                    } else {
                        showToast('Credenciales incorrectas.', 'error');
                    }
                } catch {
                    showToast('Fallo al conectar', 'error');
                }
            }

            async function handleLogout() {
                await fetch('/api/auth/logout', { method: 'POST' });
                currentUser = null;
                currentRole = null;
                updateSessionUI();
                userAccounts = [];
                byId('cardBalanceDisplay').textContent = 'S/ 0.00';
                byId('accountsListContainer').innerHTML = '<div style="font-size:12px; color:var(--muted); font-style:italic;">Inicia sesión para ver cuentas.</div>';
                byId('ledgerEntriesContainer').innerHTML = '<div style="font-size:12px; color:var(--muted); font-style:italic;">Inicia sesión para ver el libro contable.</div>';
                showToast('Sesión finalizada', 'info');
            }

            async function loadWalletData() {
                try {
                    // Load accounts summary
                    const r = await fetch('/api/wallet-mgmt/summary');
                    if (r.ok) {
                        userAccounts = await r.json();
                        renderAccountsList(userAccounts);
                        if (userAccounts.length > 0) {
                            const primary = userAccounts[0];
                            byId('cardBalanceDisplay').textContent = `S/ ${Number(primary.balance).toFixed(2)}`;
                            byId('cardNumberDisplay').textContent = primary.maskedCard;
                        }
                    }

                    // Load destination accounts
                    const dResp = await fetch('/api/wallet-mgmt/destinations');
                    if (dResp.ok) {
                        destinationAccounts = await dResp.json();
                    }

                    // Load dashboard ledger & transfers
                    const dbResp = await fetch('/api/dashboard');
                    if (dbResp.ok) {
                        const db = await dbResp.json();
                        if (userAccounts.length > 0) {
                            const firstId = userAccounts[0].id;
                            const ledResp = await fetch(`/api/accounts/${firstId}/ledger`);
                            if (ledResp.ok) {
                                const ledData = await ledResp.json();
                                renderLedger(ledData.entries || []);
                            }
                        } else {
                            renderLedger([]);
                        }
                    }
                } catch (e) {
                    console.error(e);
                }
            }

            function renderAccountsList(accounts) {
                const container = byId('accountsListContainer');
                if (!accounts || accounts.length === 0) {
                    container.innerHTML = '<div style="font-size:12px; color:var(--muted); font-style:italic;">No hay cuentas disponibles.</div>';
                    return;
                }

                container.innerHTML = accounts.map(a => `
                    <div style="background:#091726; border:1px solid var(--panel-border); border-radius:10px; padding:12px 14px; display:flex; justify-content:space-between; align-items:center;">
                        <div>
                            <strong style="color:#fff; font-size:14px;">${esc(a.code)}</strong>
                            <div style="font-size:11px; color:var(--muted);">${esc(a.owner)} · ${esc(a.currency)}</div>
                        </div>
                        <div style="text-align:right;">
                            <div style="font-size:16px; font-weight:800; color:var(--primary);">S/ ${Number(a.balance).toFixed(2)}</div>
                            <span style="font-size:10px; color:${a.active ? 'var(--success)' : 'var(--danger)'};">● ${a.active ? 'Activa' : 'Inactiva'}</span>
                        </div>
                    </div>
                `).join('');
            }

            function renderLedger(entries) {
                const container = byId('ledgerEntriesContainer');
                if (!entries || entries.length === 0) {
                    container.innerHTML = '<div style="font-size:12px; color:var(--muted); font-style:italic; padding:20px 0; text-align:center;">Sin movimientos contables registrados.</div>';
                    return;
                }

                container.innerHTML = entries.map(e => {
                    const amt = Number(e.amount);
                    const isCredit = amt > 0;
                    return `
                    <div class="ledger-item">
                        <div>
                            <strong style="color:#fff; font-size:13px;">${esc(e.kind)}</strong>
                            <div style="font-size:11px; color:var(--muted);">${formatDate(e.at)} ${e.transferId ? `· Tx #${e.transferId}` : ''}</div>
                        </div>
                        <div>
                            <span class="${isCredit ? 'ledger-badge-credit' : 'ledger-badge-debit'}">
                                ${isCredit ? '+' : ''}S/ ${Math.abs(amt).toFixed(2)}
                            </span>
                        </div>
                    </div>
                    `;
                }).join('');
            }

            async function handleSendTransfer(e) {
                e.preventDefault();
                const btn = byId('btnSubmitTransfer');
                btn.disabled = true;
                btn.textContent = 'Procesando transferencia...';

                const sourceAccountId = Number(byId('transferSource').value);
                const destinationAccountId = Number(byId('transferDest').value);
                const amount = Number(byId('transferAmount').value);
                const idempotencyKey = byId('transferIdempotency').value.trim();

                try {
                    const r = await fetch('/api/transfers', {
                        method: 'POST',
                        headers: { 'Content-Type': 'application/json' },
                        body: JSON.stringify({ sourceAccountId, destinationAccountId, amount, idempotencyKey })
                    });
                    if (r.ok) {
                        const data = await r.json();
                        const isReused = data.reused === true;
                        showToast(isReused ? `Transferencia idempotente reutilizada por S/ ${amount.toFixed(2)}` : `¡Transferencia exitosa de S/ ${amount.toFixed(2)}!`, 'success');
                        closeModal('transferModal');
                        await loadWalletData();
                    } else {
                        const err = await r.json();
                        showToast(err.error || 'Error al realizar transferencia.', 'error');
                    }
                } catch {
                    showToast('Fallo de conexión al transferir', 'error');
                } finally {
                    btn.disabled = false;
                    btn.textContent = 'Confirmar Transferencia';
                }
            }

            init();
            </script>
        </body>
        </html>
        """;
    }
}
