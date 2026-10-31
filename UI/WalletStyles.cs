namespace BilleteraDemo.UI;

public static class WalletStyles
{
    public const string Css = """
    :root {
        --primary: #55d8bd;
        --primary-glow: rgba(85, 216, 189, 0.35);
        --primary-hover: #3fc8ab;
        --bg: #091422;
        --panel: #0e2035;
        --panel-border: #1a3654;
        --ink: #f1f5f9;
        --muted: #8ca3ba;
        --success: #34d399;
        --danger: #f87171;
        --radius: 14px;
        --shadow: 0 4px 20px rgba(0, 0, 0, 0.4);
    }
    * { box-sizing: border-box; margin: 0; padding: 0; }
    body.app-wallet {
        font-family: 'Inter', -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, sans-serif;
        background: var(--bg); color: var(--ink); line-height: 1.5; min-height: 100vh;
    }
    .top {
        display: flex; justify-content: space-between; align-items: center;
        background: #08121e; border-bottom: 1px solid var(--panel-border);
        padding: 14px 28px; position: sticky; top: 0; z-index: 40;
    }
    .brand { display: flex; align-items: center; gap: 14px; }
    .mark {
        width: 42px; height: 42px; border-radius: 12px;
        background: linear-gradient(135deg, #207967, #16465b);
        color: var(--primary); font-weight: 800; display: grid; place-items: center;
        font-size: 20px; border: 1px solid var(--primary); box-shadow: 0 0 15px var(--primary-glow);
    }
    .brand h1 { font-size: 17px; font-weight: 700; color: #fff; }
    .brand p { font-size: 12px; color: var(--muted); }
    .page { max-width: 1360px; margin: 24px auto 60px; padding: 0 24px; }

    /* Session Bar */
    .session-bar {
        background: var(--panel); border: 1px solid var(--panel-border);
        border-radius: var(--radius); padding: 12px 18px;
        display: flex; align-items: center; justify-content: space-between;
        gap: 16px; margin-bottom: 24px; box-shadow: var(--shadow);
    }
    .login-controls { display: flex; align-items: center; gap: 10px; flex-wrap: wrap; }
    .input-group { display: flex; flex-direction: column; gap: 2px; }
    .input-group label { font-size: 10px; font-weight: 700; color: var(--muted); text-transform: uppercase; }
    .input-group select, .input-group input {
        padding: 7px 11px; border: 1px solid var(--panel-border); border-radius: 7px;
        font-size: 13px; background: #081422; color: #fff;
    }
    .btn {
        padding: 8px 16px; border-radius: 8px; font-weight: 700; font-size: 13px;
        border: 0; cursor: pointer; display: inline-flex; align-items: center; gap: 6px;
        transition: all 0.15s ease;
    }
    .btn-primary {
        background: var(--primary); color: #08121e;
        box-shadow: 0 2px 10px var(--primary-glow);
    }
    .btn-primary:hover { background: var(--primary-hover); }
    .btn-outline { background: transparent; border: 1px solid var(--panel-border); color: var(--muted); }
    .btn-outline:hover { background: #162d4a; color: #fff; }

    /* Hero & Card Grid */
    .hero-grid { display: grid; grid-template-columns: 1.3fr 1fr; gap: 20px; margin-bottom: 24px; }
    @media(max-width: 900px) { .hero-grid { grid-template-columns: 1fr; } }

    .wallet-hero {
        background: linear-gradient(135deg, #0f273d, #143854);
        border: 1px solid var(--panel-border); border-radius: var(--radius);
        padding: 28px 32px; display: flex; flex-direction: column; justify-content: space-between;
    }
    .wallet-hero small { font-size: 11px; font-weight: 800; letter-spacing: 0.12em; color: var(--primary); }
    .wallet-hero h2 { font-size: 30px; font-weight: 800; margin: 6px 0 10px; letter-spacing: -0.02em; color: #fff; }
    .wallet-hero p { font-size: 14px; color: var(--muted); max-width: 500px; margin-bottom: 20px; }

    /* Virtual Metallic Debit Card */
    .virtual-card {
        background: linear-gradient(135deg, #184857, #0c2d3b);
        border: 1px solid #367280; border-radius: 18px; padding: 22px 24px;
        box-shadow: 0 12px 30px rgba(0, 0, 0, 0.5), inset 0 1px 1px rgba(255, 255, 255, 0.2);
        display: flex; flex-direction: column; justify-content: space-between;
        min-height: 200px; position: relative; overflow: hidden;
    }
    .virtual-card::after {
        content: ''; position: absolute; right: -50px; bottom: -50px;
        width: 160px; height: 160px; border-radius: 50%;
        background: radial-gradient(circle, rgba(85, 216, 189, 0.15) 0%, transparent 70%);
        pointer-events: none;
    }
    .card-top { display: flex; justify-content: space-between; align-items: center; }
    .chip {
        width: 38px; height: 28px; border-radius: 5px;
        background: linear-gradient(135deg, #d4af37, #f3e5ab);
        border: 1px solid #b8972e; position: relative;
    }
    .card-balance-label { font-size: 11px; color: #a5d3cb; text-transform: uppercase; letter-spacing: 0.08em; }
    .card-balance-val { font-size: 28px; font-weight: 800; color: #fff; margin-top: 2px; }
    .card-number { font-family: 'Courier New', monospace; font-size: 17px; letter-spacing: 2px; color: #e2f1ee; margin: 16px 0; }
    .card-bottom { display: flex; justify-content: space-between; align-items: flex-end; font-size: 11px; }
    .card-holder { font-weight: 700; text-transform: uppercase; color: #fff; }

    /* Main Columns */
    .main-grid { display: grid; grid-template-columns: 1fr 1.6fr; gap: 20px; }
    @media(max-width: 960px) { .main-grid { grid-template-columns: 1fr; } }

    .panel {
        background: var(--panel); border: 1px solid var(--panel-border);
        border-radius: var(--radius); padding: 22px; box-shadow: var(--shadow);
    }
    .panel-header {
        display: flex; justify-content: space-between; align-items: center;
        margin-bottom: 16px; padding-bottom: 12px; border-bottom: 1px solid var(--panel-border);
    }
    .panel-header h3 { font-size: 16px; font-weight: 700; color: #fff; }

    /* Ledger Timeline */
    .ledger-item {
        display: flex; justify-content: space-between; align-items: center;
        padding: 12px 14px; background: #091726; border: 1px solid var(--panel-border);
        border-radius: 10px; margin-bottom: 8px; transition: transform 0.15s ease;
    }
    .ledger-item:hover { transform: translateX(3px); background: #0c1f33; }
    .ledger-badge-credit { background: rgba(52, 211, 153, 0.15); color: var(--success); font-weight: 800; padding: 4px 10px; border-radius: 99px; }
    .ledger-badge-debit { background: rgba(248, 113, 113, 0.15); color: var(--danger); font-weight: 800; padding: 4px 10px; border-radius: 99px; }

    /* Modals */
    .modal-overlay {
        position: fixed; inset: 0; background: rgba(0, 0, 0, 0.75);
        backdrop-filter: blur(6px); z-index: 120; opacity: 0; pointer-events: none;
        transition: opacity 0.2s ease; display: grid; place-items: center; padding: 20px;
    }
    .modal-overlay.open { opacity: 1; pointer-events: auto; }
    .modal {
        background: #0d1e30; border: 1px solid var(--panel-border); width: 100%;
        max-width: 480px; border-radius: var(--radius); box-shadow: var(--shadow); overflow: hidden;
    }
    .modal-header { padding: 18px 24px; border-bottom: 1px solid var(--panel-border); display: flex; justify-content: space-between; align-items: center; }
    .modal-body { padding: 20px 24px; display: flex; flex-direction: column; gap: 14px; }
    .modal-footer { padding: 14px 24px; border-top: 1px solid var(--panel-border); background: #081320; display: flex; justify-content: flex-end; gap: 10px; }
    .form-group { display: flex; flex-direction: column; gap: 4px; }
    .form-group label { font-size: 12px; font-weight: 700; color: var(--muted); }
    .form-group input, .form-group select {
        padding: 9px 12px; border: 1px solid var(--panel-border); border-radius: 8px; font-size: 13px; background: #081320; color: #fff;
    }

    /* Toasts */
    .toast-container { position: fixed; bottom: 20px; right: 20px; z-index: 200; display: flex; flex-direction: column; gap: 8px; }
    .toast {
        background: #0d1f33; color: #fff; padding: 12px 18px; border-radius: 8px;
        box-shadow: var(--shadow); font-size: 13px; font-weight: 600; border: 1px solid var(--panel-border);
        display: flex; align-items: center; gap: 10px;
    }
    .toast.success { border-left: 4px solid var(--primary); }
    .toast.error { border-left: 4px solid var(--danger); }
    """;
}
