# Práctico Experimental N.° 02

## Sistema de asignación de 30 asientos

Este proyecto fue desarrollado en lenguaje C# como parte de la asignatura Estructura de Datos. El sistema simula una línea de espera para una atracción de un parque de diversiones con una capacidad máxima de 30 personas.

La aplicación registra a los visitantes en el orden en que llegan y les asigna los asientos respetando el principio FIFO. Esto significa que la primera persona en ingresar a la cola es la primera en recibir un asiento.

## Objetivo

Aplicar la teoría de colas mediante una solución informática funcional desarrollada con Programación Orientada a Objetos en C#.

## Problema resuelto

La atracción dispone de 30 asientos. Los visitantes deben ser atendidos según su orden de llegada, evitando que una persona que llegó después reciba un asiento antes que otra que ya se encontraba esperando.

El sistema permite controlar el registro de visitantes, la cola de espera, la asignación de asientos, las consultas y los reportes generales.

## Estructura de datos utilizada

Se utilizó la estructura:

```csharp
Queue<Persona>
```

La cola trabaja bajo el principio FIFO:

**First In, First Out: primero en entrar, primero en salir.**

Para registrar visitantes se utiliza el método:

```csharp
Enqueue()
```

Para retirar y atender a la primera persona de la cola se utiliza:

```csharp
Dequeue()
```

## Programación Orientada a Objetos

El proyecto se organizó mediante clases con responsabilidades específicas:

- `Persona`: almacena la identificación, el nombre, el número de llegada y la hora de registro.
- `Asiento`: representa cada asiento y guarda la información de su ocupante.
- `SistemaAtraccion`: administra la cola, los registros, los asientos, las búsquedas y los reportes.
- `AplicacionConsola`: presenta el menú y permite la interacción con el usuario.
- `Program`: inicia la ejecución del sistema.

## Funcionalidades

El programa permite:

- Registrar visitantes.
- Controlar una capacidad máxima de 30 personas.
- Evitar el registro de identificaciones repetidas.
- Asignar un asiento al siguiente visitante.
- Asignar automáticamente asientos a todas las personas de la cola.
- Mostrar la cola de espera.
- Mostrar los asientos asignados.
- Buscar visitantes por identificación.
- Consultar el estado de cada visitante.
- Mostrar un resumen general del sistema.
- Medir el tiempo de ejecución.
- Mostrar la complejidad de las operaciones.

## Menú principal

```text
1. Registrar visitante
2. Asignar asiento al siguiente visitante
3. Asignar asientos a toda la cola
4. Mostrar cola de espera
5. Mostrar asientos asignados
6. Buscar visitante por identificación
7. Mostrar resumen general
8. Medir tiempo de ejecución
0. Salir
```

## Organización del proyecto

```text
Practica02Atraccion
├── Models
│   ├── Asiento.cs
│   └── Persona.cs
├── Services
│   └── SistemaAtraccion.cs
├── UI
│   └── AplicacionConsola.cs
├── Program.cs
├── Practica02Atraccion.csproj
├── .gitignore
└── README.md
```

## Requisitos

Para ejecutar el proyecto se necesita:

- .NET SDK.
- Visual Studio Code.
- Extensión oficial C# Dev Kit.
- Git.

## Ejecución

Abra una terminal dentro de la carpeta principal del proyecto y ejecute:

```bash
dotnet build
```

Luego ejecute:

```bash
dotnet run
```

## Complejidad de las operaciones

- Registro con `Enqueue()`: O(1).
- Asignación con `Dequeue()`: O(1).
- Consulta del primer elemento: O(1).
- Búsqueda de visitantes: O(n).
- Generación de reportes: O(n).
- Espacio utilizado: O(n).

## Ventajas de la cola

- Respeta el orden de llegada.
- Evita preferencias injustificadas.
- Permite agregar personas rápidamente.
- Permite atender a la primera persona de manera eficiente.
- Representa de forma adecuada una línea de espera real.

## Desventajas de la cola

- Para buscar una persona es necesario recorrer la estructura.
- No permite retirar directamente un elemento ubicado en el centro.
- Los datos se mantienen solamente mientras el programa está en ejecución.
- No cuenta con almacenamiento permanente en una base de datos.

## Resultados

Durante las pruebas se comprobó que el sistema:

- Registró correctamente a los visitantes.
- Mantuvo el orden FIFO.
- Asignó los asientos según el orden de llegada.
- Evitó identificaciones repetidas.
- Permitió consultar visitantes.
- Generó reportes legibles.
- Mostró correctamente los asientos ocupados y disponibles.
- Midió el tiempo de ejecución de 30 registros y 30 asignaciones.

## Herramientas utilizadas

- Lenguaje C#.
- .NET.
- Visual Studio Code.
- Git.
- GitHub.

## Autor

Jorge Sanchez

## Asignatura

Estructura de Datos

## Institución

Universidad Estatal Amazónica

## Período académico

2026

