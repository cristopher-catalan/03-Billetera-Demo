# 03 — Billetera digital simulada

**Estado:** Simulador financiero interactivo con tarjeta digital virtual, transferencias en tiempo real con garantía de idempotencia y libro mayor contable (*ledger*) auditable.

**Ejecución:** desde la raíz de Preparacion-7-Proyectos-Web-DotNet, ejecuta `dotnet run --project .\03-Billetera-Demo\BilleteraDemo.csproj` y abre `http://localhost:5103`. Contraseña de demostración: `Demo123!`.

---

## Características de la Interfaz Moderna y Flujos Dinámicos

1. **Tarjeta de Débito Virtual Interactiva:**
   - Visualización premium con gradiente metálico, chip EMV y tarjeta enmascarada (`4557 •••• •••• 1001`).
   - Saldo simulado visible en tiempo real sincronizado con el libro mayor contable.

2. **Modal de Transferencias Inmediatas:**
   - Selección ágil de cuenta destino y beneficiarios frecuentes.
   - Botones rápidos de importe (+S/ 10, +S/ 25, +S/ 50, +S/ 100).
   - Generación automática de clave de idempotencia (`tx-uuid`) con opción de regeneración.

3. **Garantía Transaccional e Idempotencia:**
   - Si una petición de transferencia se reenvía con la misma clave de idempotencia, el backend detecta la repetición y devuelve la operación original (`reused: true`), impidiendo cobros duplicados accidentales.

4. **Historial de Libro Contable (Ledger) con Badges en Tiempo Real:**
   - Desglose de asientos contables con badges diferenciados:
     - **Crédito (+ S/)** en color verde menta.
     - **Débito (- S/)** en color carmesí suave.

---

## Arquitectura de Código

```
03-Billetera-Demo/
├── Domain/
│   └── WalletModels.cs              # DTOs de cuenta, tarjeta y recibos
├── Services/
│   └── WalletTransactionService.cs  # Idempotencia, máscara de tarjeta y validaciones
├── Endpoints/
│   └── WalletEndpoints.cs           # Endpoints de destinos y resumen financiero
├── UI/
│   ├── WalletStyles.cs              # Tema oscuro fintech con glow neón
│   ├── WalletLayout.cs              # Dashboard interactivo y ledger
│   └── Components/
│       ├── VirtualCard.cs           # Componente de tarjeta digital animada
│       ├── TransferModal.cs         # Modal de transferencias e idempotencia
│       └── ToastSystem.cs           # Notificaciones flotantes animadas
└── Program.cs                       # Pipeline y servicios
```

---

## Verificación Funcional

Compilación limpia en .NET 10. Valida transferencias, ledger e idempotencia en `Review-FunctionalFlows.ps1`.
