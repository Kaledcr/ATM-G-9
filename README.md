#  Simulador de Cajero Automático (ATM)

**Curso:** SC-250 Paradigmas de Programación  
**Universidad:** Fidélitas  
**Integrantes:** Darian González Rojas · Teresa Pineda Molina  
**Profesor:** Elberth Adrián Garro Sánchez  
**Cuatrimestre:** I — 2026

## Descripción
 
Aplicación web que simula el funcionamiento de un cajero automático bancario, 
desarrollada en **C# con ASP.NET MVC**. El sistema permite iniciar sesión, consultar saldo, 
retirar dinero, depositar fondos y ver el historial de transacciones. El proyecto aplica dos 
paradigmas de programación de forma explícita y diferenciada: **Programación Orientada a Objetos 
(POO)** y **Programación Procedimental**.

## Paradigmas de programación
 
### Programación Orientada a Objetos (POO)
Aplicada en la capa de modelos (`/Models`). Las clases representan entidades del mundo real con sus propios datos y comportamientos:
 
- **Encapsulamiento:** el saldo en `Account` es privado y solo se modifica mediante `Deposit()` y `Withdraw()`.
- **Composición:** `User` contiene una instancia de `Account`; `Account` contiene una lista de `Transaccion`.
- **Abstracción:** `Transaccion` encapsula cualquier movimiento financiero en un objeto con tipo, monto y fecha.
### Programación Procedimental
Aplicada en `ServiceATM.cs`. Cada método define una secuencia explícita y numerada de pasos:
 
```
1. Validar precondiciones
2. Ejecutar la operación
3. Retornar el resultado
```
 
La clase es `static`, no mantiene estado propio y actúa como coordinadora del flujo de operaciones.
