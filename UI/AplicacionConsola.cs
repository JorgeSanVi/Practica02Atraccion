using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Practica02Atraccion.Models;
using Practica02Atraccion.Services;

namespace Practica02Atraccion.UI;

public class AplicacionConsola
{
    private readonly SistemaAtraccion _sistema;

    public AplicacionConsola(
        SistemaAtraccion sistema)
    {
        _sistema = sistema;
    }

    public void Ejecutar()
    {
        bool continuar = true;

        while (continuar)
        {
            Console.Clear();
            MostrarMenu();

            Console.Write(
                "Seleccione una opción: ");

            string opcion =
                Console.ReadLine()?.Trim()
                ?? string.Empty;

            Console.Clear();

            switch (opcion)
            {
                case "1":
                    RegistrarVisitante();
                    break;

                case "2":
                    AsignarSiguienteAsiento();
                    break;

                case "3":
                    AsignarTodosLosAsientos();
                    break;

                case "4":
                    MostrarColaEspera();
                    break;

                case "5":
                    MostrarAsientosAsignados();
                    break;

                case "6":
                    BuscarVisitante();
                    break;

                case "7":
                    MostrarResumenGeneral();
                    break;

                case "8":
                    MedirRendimiento();
                    break;

                case "0":
                    continuar = false;
                    MostrarDespedida();
                    break;

                default:
                    MostrarMensaje(
                        "La opción ingresada no es válida.",
                        ConsoleColor.Yellow);
                    break;
            }

            if (continuar)
            {
                Pausar();
            }
        }
    }

    private static void MostrarMenu()
{
    Console.ForegroundColor = ConsoleColor.Cyan;

    Console.WriteLine("==================================================");
    Console.WriteLine("       SISTEMA DE ATRACCIÓN - 30 ASIENTOS");
    Console.WriteLine("==================================================");

    Console.ResetColor();

    Console.WriteLine("1. Registrar visitante");
    Console.WriteLine("2. Asignar asiento al siguiente visitante");
    Console.WriteLine("3. Asignar asientos a toda la cola");
    Console.WriteLine("4. Mostrar cola de espera");
    Console.WriteLine("5. Mostrar asientos asignados");
    Console.WriteLine("6. Buscar visitante por identificación");
    Console.WriteLine("7. Mostrar resumen general");
    Console.WriteLine("8. Medir tiempo de ejecución");
    Console.WriteLine("0. Salir");

    Console.WriteLine("==================================================");
}

    private void RegistrarVisitante()
    {
        Console.WriteLine(
            "REGISTRO DE VISITANTE");

        Console.WriteLine(
            "---------------------");

        Console.Write(
            "Ingrese la identificación: ");

        string identificacion =
            Console.ReadLine()?.Trim()
            ?? string.Empty;

        Console.Write(
            "Ingrese el nombre completo: ");

        string nombreCompleto =
            Console.ReadLine()?.Trim()
            ?? string.Empty;

        bool registrado =
            _sistema.RegistrarPersona(
                identificacion,
                nombreCompleto,
                out string mensaje);

        MostrarMensaje(
            mensaje,
            registrado
                ? ConsoleColor.Green
                : ConsoleColor.Red);
    }

    private void AsignarSiguienteAsiento()
    {
        Console.WriteLine(
            "ASIGNACIÓN DE ASIENTO");

        Console.WriteLine(
            "---------------------");

        bool asignado =
            _sistema.AsignarSiguienteAsiento(
                out Asiento? asientoAsignado,
                out string mensaje);

        MostrarMensaje(
            mensaje,
            asignado
                ? ConsoleColor.Green
                : ConsoleColor.Red);

        if (asignado &&
            asientoAsignado?.Ocupante is not null)
        {
            Console.WriteLine();

            Console.WriteLine(
                $"Visitante: " +
                $"{asientoAsignado.Ocupante.NombreCompleto}");

            Console.WriteLine(
                $"Identificación: " +
                $"{asientoAsignado.Ocupante.Identificacion}");

            Console.WriteLine(
                $"Número de asiento: " +
                $"{asientoAsignado.Numero}");
        }
    }

    private void AsignarTodosLosAsientos()
    {
        Console.WriteLine(
            "ASIGNACIÓN AUTOMÁTICA DE ASIENTOS");

        Console.WriteLine(
            "--------------------------------");

        int totalAsignados =
            _sistema.AsignarTodosLosAsientos(
                out string mensaje);

        MostrarMensaje(
            mensaje,
            totalAsignados > 0
                ? ConsoleColor.Green
                : ConsoleColor.Yellow);
    }

    private void MostrarColaEspera()
    {
        Console.WriteLine(
            "REPORTE DE LA COLA DE ESPERA");

        Console.WriteLine(
            "----------------------------");

        IReadOnlyCollection<Persona> personas =
            _sistema.ObtenerColaEspera();

        if (personas.Count == 0)
        {
            MostrarMensaje(
                "Actualmente no existen " +
                "personas esperando.",
                ConsoleColor.Yellow);

            return;
        }

        Console.WriteLine(
            "{0,-6} | {1,-6} | {2,-15} | {3,-25} | {4,-8}",
            "POS.",
            "ORDEN",
            "IDENTIFICACIÓN",
            "NOMBRE COMPLETO",
            "HORA");

        Console.WriteLine(
            new string('-', 75));

        int posicion = 1;

        foreach (Persona persona in personas)
        {
            Console.WriteLine(
                $"{posicion,-6} | {persona}");

            posicion++;
        }

        Console.WriteLine(
            new string('-', 75));

        Console.WriteLine(
            $"Total de personas en espera: " +
            $"{personas.Count}");
    }

    private void MostrarAsientosAsignados()
    {
        Console.WriteLine(
            "REPORTE DE ASIENTOS ASIGNADOS");

        Console.WriteLine(
            "-----------------------------");

        List<Asiento> asientosOcupados =
            _sistema
                .ObtenerAsientos()
                .Where(
                    asiento =>
                        asiento.EstaOcupado)
                .ToList();

        if (asientosOcupados.Count == 0)
        {
            MostrarMensaje(
                "Todavía no existen " +
                "asientos asignados.",
                ConsoleColor.Yellow);

            return;
        }

        Console.WriteLine(
            "{0,-8} | {1,-15} | {2,-25}",
            "ASIENTO",
            "IDENTIFICACIÓN",
            "VISITANTE");

        Console.WriteLine(
            new string('-', 55));

        foreach (Asiento asiento
                 in asientosOcupados)
        {
            Console.WriteLine(asiento);
        }

        Console.WriteLine(
            new string('-', 55));

        Console.WriteLine(
            $"Total de asientos ocupados: " +
            $"{asientosOcupados.Count}");
    }

    private void BuscarVisitante()
    {
        Console.WriteLine(
            "CONSULTA DE VISITANTE");

        Console.WriteLine(
            "---------------------");

        Console.Write(
            "Ingrese la identificación: ");

        string identificacion =
            Console.ReadLine()?.Trim()
            ?? string.Empty;

        bool encontrado =
            _sistema.BuscarPersona(
                identificacion,
                out Persona? persona,
                out string ubicacion);

        if (!encontrado || persona is null)
        {
            MostrarMensaje(
                ubicacion,
                ConsoleColor.Yellow);

            return;
        }

        MostrarMensaje(
            "Visitante encontrado correctamente.",
            ConsoleColor.Green);

        Console.WriteLine(
            $"Identificación: " +
            $"{persona.Identificacion}");

        Console.WriteLine(
            $"Nombre completo: " +
            $"{persona.NombreCompleto}");

        Console.WriteLine(
            $"Número de llegada: " +
            $"{persona.NumeroLlegada}");

        Console.WriteLine(
            $"Hora de registro: " +
            $"{persona.HoraRegistro:HH:mm:ss}");

        Console.WriteLine(
            $"Estado actual: {ubicacion}");
    }

    private void MostrarResumenGeneral()
    {
        double porcentajeOcupacion =
            _sistema.AsientosOcupados
            * 100.0
            / SistemaAtraccion.CapacidadMaxima;

        Console.WriteLine(
            "RESUMEN GENERAL DEL SISTEMA");

        Console.WriteLine(
            "---------------------------");

        Console.WriteLine(
            $"Capacidad máxima: " +
            $"{SistemaAtraccion.CapacidadMaxima}");

        Console.WriteLine(
            $"Total de personas registradas: " +
            $"{_sistema.TotalPersonasRegistradas}");

        Console.WriteLine(
            $"Personas en espera: " +
            $"{_sistema.PersonasEnEspera}");

        Console.WriteLine(
            $"Asientos ocupados: " +
            $"{_sistema.AsientosOcupados}");

        Console.WriteLine(
            $"Asientos todavía sin asignar: " +
            $"{_sistema.AsientosDisponibles}");

        Console.WriteLine(
            $"Cupos disponibles para registros: " +
            $"{_sistema.CuposDisponibles}");

        Console.WriteLine(
            $"Porcentaje de ocupación: " +
            $"{porcentajeOcupacion:F2}%");
    }

    private static void MedirRendimiento()
    {
        Console.WriteLine(
            "ANÁLISIS DEL TIEMPO DE EJECUCIÓN");

        Console.WriteLine(
            "-------------------------------");

        SistemaAtraccion sistemaPrueba =
            new SistemaAtraccion();

        Stopwatch tiempoRegistro =
            Stopwatch.StartNew();

        for (int numero = 1;
             numero <= SistemaAtraccion.CapacidadMaxima;
             numero++)
        {
            sistemaPrueba.RegistrarPersona(
                $"PRUEBA{numero:D3}",
                $"Visitante de prueba {numero}",
                out _);
        }

        tiempoRegistro.Stop();

        Stopwatch tiempoAsignacion =
            Stopwatch.StartNew();

        sistemaPrueba.AsignarTodosLosAsientos(
            out _);

        tiempoAsignacion.Stop();

        double microsegundosRegistro =
            tiempoRegistro.Elapsed.TotalMilliseconds
            * 1000.0;

        double microsegundosAsignacion =
            tiempoAsignacion.Elapsed.TotalMilliseconds
            * 1000.0;

        Console.WriteLine(
            "Cantidad de registros probados: 30");

        Console.WriteLine(
            $"Tiempo de registro: " +
            $"{microsegundosRegistro:F2} microsegundos");

        Console.WriteLine(
            $"Ticks de registro: " +
            $"{tiempoRegistro.ElapsedTicks}");

        Console.WriteLine();

        Console.WriteLine(
            "Cantidad de asignaciones probadas: 30");

        Console.WriteLine(
            $"Tiempo de asignación: " +
            $"{microsegundosAsignacion:F2} microsegundos");

        Console.WriteLine(
            $"Ticks de asignación: " +
            $"{tiempoAsignacion.ElapsedTicks}");

        Console.WriteLine();

        Console.WriteLine(
            "Complejidad de Enqueue: O(1)");

        Console.WriteLine(
            "Complejidad de Dequeue: O(1)");

        Console.WriteLine(
            "Complejidad de los reportes: O(n)");

        Console.WriteLine();

        Console.WriteLine(
            "Los tiempos pueden variar según " +
            "la computadora utilizada.");
    }

    private static void MostrarMensaje(
        string mensaje,
        ConsoleColor color)
    {
        Console.ForegroundColor = color;

        Console.WriteLine();
        Console.WriteLine(mensaje);

        Console.ResetColor();
    }

    private static void Pausar()
    {
        Console.WriteLine();

        Console.WriteLine(
            "Presione cualquier tecla " +
            "para regresar al menú...");

        Console.ReadKey(true);
    }

    private static void MostrarDespedida()
    {
        Console.ForegroundColor =
            ConsoleColor.Cyan;

        Console.WriteLine(
            "El sistema se cerró correctamente.");

        Console.ResetColor();
    }
}