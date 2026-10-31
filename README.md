# 03 — Billetera digital simulada

**Estado:** MVP local funcional implementado para práctica; no es una aplicación de producción.  

**Ejecución:** desde la raíz de Preparacion-7-Proyectos-Web-DotNet, ejecuta dotnet run --project .\03-Billetera-Demo\BilleteraDemo.csproj y abre http://localhost:5103. Contraseña de demostración: Demo123!. La persistencia usa EF Core InMemory por defecto; los perfiles y roles están en /api/auth/demo-users.
**Advertencia de alcance:** es una simulación educativa sin dinero real, bancos, tarjetas ni proveedores de pago. No debe ofrecerse como producto financiero.

## Objetivo

Demostrar diseño de APIs, autenticación, consistencia transaccional, idempotencia, SQL y auditoría en un dominio sensible.

## Usuarios

- **Usuario:** consulta su perfil, cuentas de demostración y movimientos; transfiere fondos ficticios.
- **Soporte:** revisa incidentes sin modificar saldos manualmente.
- **Administrador:** crea cuentas de prueba, congela una cuenta de demostración y consulta auditoría.

## Alcance funcional

- Registro/inicio de sesión y verificación del perfil demo.
- Cuentas ficticias en una moneda de prueba única.
- Consulta de saldo y movimientos.
- Transferencia entre cuentas internas simuladas.
- Beneficiarios guardados opcionales.
- Historial, filtros y detalle de transferencia.
- Estado de transferencia: iniciada, completada o rechazada; no hay retiros ni conversión de monedas.
- Datos de demostración claramente marcados en la interfaz.

## Modelo de datos

- **DemoAccount:** usuario propietario, código, moneda fija, estado y fecha.
- **Transfer:** origen, destino, cantidad, referencia idempotente, estado y fechas.
- **LedgerEntry:** cuenta, transferencia, importe con signo, tipo y fecha.
- **Beneficiary:** dueño, alias y cuenta demo destino.
- **AuditEvent:** actor, acción, resultado y fecha.

Una transferencia completada crea exactamente dos asientos: débito y crédito por el mismo importe. La suma de los asientos de cada transferencia completada debe ser cero.

## API y páginas indicativas

Páginas: resumen de cuentas demo; crear transferencia; confirmación; actividad; detalle y beneficiarios.

Endpoints candidatos: GET /api/accounts; GET /api/accounts/{id}/ledger; POST /api/transfers; GET /api/transfers/{id}; POST /api/beneficiaries; GET /api/beneficiaries.

## Reglas críticas

- La transferencia completa, el débito, el crédito y el registro idempotente ocurren en una única transacción SQL.
- Rechazar importe menor o igual a cero, cuenta inactiva, origen y destino iguales, usuario que no es propietario y saldo insuficiente.
- Repetir la misma clave idempotente devuelve el resultado original sin duplicar cargos.
- Aplicar control de concurrencia para transferencias simultáneas.
- Nunca permitir que un endpoint de consulta acepte el ID de cuenta como única prueba de autorización.
- El saldo se deriva del libro contable o se reconcilia contra este; no editar el saldo desde el panel administrativo.
- Mostrar en todas las páginas que se trata de datos ficticios.

## Seguridad y pruebas previstas

Autenticación segura, autorización por recurso, antiforgery en formularios MVC, TLS al desplegar, secretos fuera de Git y rate limiting. Pruebas de integración deben cubrir saldo insuficiente, doble envío, concurrencia, transferencia entre usuarios y transacción fallida.

## Criterios de aceptación

Una transferencia válida cambia ambos saldos de forma atómica; una transferencia inválida no crea asientos; repetir el mismo envío no duplica movimientos; los dos asientos balancean; y un usuario no puede ver cuentas ajenas.

## Implementación local actual

MVP disponible: ASP.NET Core + EF Core, panel interactivo y endpoints JSON descritos en el README raíz. El almacenamiento predeterminado es InMemory; SQL Server es configurable. La especificación de este documento sirve como alcance ampliado y backlog.

