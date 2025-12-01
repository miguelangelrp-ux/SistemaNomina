# Sistema de Nómina - Servicios Corporativos Caribe SRL

Este proyecto es un **sistema de nómina mensual** en consola desarrollado en **C#** y utilizando **SQLite** para persistencia de datos.

Servicios Corporativos Caribe SRL es una empresa dominicana dedicada a servicios administrativos. Ese proyecto permite poder gestionar el proceso de nomina mensual y guardar los datos en una base de datos.

El programa corre en consola.

## Funcionalidades

- Agregar, consultar, editar y eliminar empleados.
- Generar nómina mensual con cálculo de deducciones:
  - AFP 2.87%
  - ARS 3.04%
- Mostrar reporte mensual de pagos por empleado y totales.
- Persistencia de datos mediante SQLite (`empresa.db`).