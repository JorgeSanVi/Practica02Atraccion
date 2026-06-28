using System;
using System.Collections.Generic;
using System.Linq;
using Practica02Atraccion.Models;

namespace Practica02Atraccion.Services;

public class SistemaAtraccion
{
    public const int CapacidadMaxima = 30;

    private readonly Queue<Persona> _colaEspera;
    private readonly List<Asiento> _asientos;
    private readonly HashSet<string> _identificacionesRegistradas;

    private int _contadorLlegadas;

    public int PersonasEnEspera => _colaEspera.Count;

    public int AsientosOcupados =>
        _asientos.Count(asiento => asiento.EstaOcupado);

    public int AsientosDisponibles =>
        CapacidadMaxima - AsientosOcupados;

    public int TotalPersonasRegistradas =>
        PersonasEnEspera + AsientosOcupados;

    public int CuposDisponibles =>
        CapacidadMaxima - TotalPersonasRegistradas;

    public SistemaAtraccion()
    {
        _colaEspera = new Queue<Persona>();
        _asientos = new List<Asiento>();

        _identificacionesRegistradas =
            new HashSet<string>(
                StringComparer.OrdinalIgnoreCase);

        for (int numero = 1;
             numero <= CapacidadMaxima;
             numero++)
        {
            _asientos.Add(new Asiento(numero));
        }
    }

    public bool RegistrarPersona(
        string identificacion,
        string nombreCompleto,
        out string mensaje)
    {
        identificacion = identificacion.Trim();
        nombreCompleto = nombreCompleto.Trim();

        if (string.IsNullOrWhiteSpace(identificacion))
        {
            mensaje =
                "La identificación no puede estar vacía.";

            return false;
        }

        if (string.IsNullOrWhiteSpace(nombreCompleto))
        {
            mensaje =
                "El nombre no puede estar vacío.";

            return false;
        }

        if (_identificacionesRegistradas.Contains(
            identificacion))
        {
            mensaje =
                "Ya existe una persona registrada " +
                "con esa identificación.";

            return false;
        }

        if (TotalPersonasRegistradas >= CapacidadMaxima)
        {
            mensaje =
                "No se pueden registrar más personas. " +
                "Los 30 cupos están completos.";

            return false;
        }

        _contadorLlegadas++;

        Persona nuevaPersona = new Persona(
            identificacion,
            nombreCompleto,
            _contadorLlegadas);

        _colaEspera.Enqueue(nuevaPersona);

        _identificacionesRegistradas.Add(
            identificacion);

        mensaje =
            "Registro realizado correctamente. " +
            $"Posición en la cola: {_colaEspera.Count}.";

        return true;
    }

    public bool AsignarSiguienteAsiento(
        out Asiento? asientoAsignado,
        out string mensaje)
    {
        asientoAsignado = null;

        if (_colaEspera.Count == 0)
        {
            mensaje =
                "No existen personas esperando en la cola.";

            return false;
        }

        Asiento? asientoDisponible =
            _asientos.FirstOrDefault(
                asiento => !asiento.EstaOcupado);

        if (asientoDisponible is null)
        {
            mensaje =
                "No existen asientos disponibles.";

            return false;
        }

        Persona siguientePersona =
            _colaEspera.Dequeue();

        bool asignacionCorrecta =
            asientoDisponible.Asignar(
                siguientePersona);

        if (!asignacionCorrecta)
        {
            mensaje =
                "No fue posible realizar la asignación.";

            return false;
        }

        asientoAsignado = asientoDisponible;

        mensaje =
            $"Asiento {asientoDisponible.Numero} " +
            $"asignado a {siguientePersona.NombreCompleto}.";

        return true;
    }

    public int AsignarTodosLosAsientos(
        out string mensaje)
    {
        int totalAsignados = 0;

        while (_colaEspera.Count > 0)
        {
            bool asignado =
                AsignarSiguienteAsiento(
                    out _,
                    out _);

            if (!asignado)
            {
                break;
            }

            totalAsignados++;
        }

        if (totalAsignados == 0)
        {
            mensaje =
                "No existen personas esperando " +
                "para asignar asientos.";

            return 0;
        }

        mensaje =
            $"Se asignaron correctamente " +
            $"{totalAsignados} asiento(s).";

        return totalAsignados;
    }

    public bool BuscarPersona(
        string identificacion,
        out Persona? personaEncontrada,
        out string ubicacion)
    {
        personaEncontrada = null;
        identificacion = identificacion.Trim();

        if (string.IsNullOrWhiteSpace(
            identificacion))
        {
            ubicacion =
                "Debe ingresar una identificación.";

            return false;
        }

        int posicion = 1;

        foreach (Persona persona in _colaEspera)
        {
            bool coincide = string.Equals(
                persona.Identificacion,
                identificacion,
                StringComparison.OrdinalIgnoreCase);

            if (coincide)
            {
                personaEncontrada = persona;

                ubicacion =
                    "Se encuentra en espera. " +
                    $"Posición en la cola: {posicion}.";

                return true;
            }

            posicion++;
        }

        foreach (Asiento asiento in _asientos)
        {
            if (asiento.Ocupante is null)
            {
                continue;
            }

            bool coincide = string.Equals(
                asiento.Ocupante.Identificacion,
                identificacion,
                StringComparison.OrdinalIgnoreCase);

            if (coincide)
            {
                personaEncontrada =
                    asiento.Ocupante;

                ubicacion =
                    "Ya tiene asignado el asiento " +
                    $"{asiento.Numero}.";

                return true;
            }
        }

        ubicacion =
            "No existe un visitante registrado " +
            "con esa identificación.";

        return false;
    }

    public IReadOnlyCollection<Persona>
        ObtenerColaEspera()
    {
        return _colaEspera.ToArray();
    }

    public IReadOnlyCollection<Asiento>
        ObtenerAsientos()
    {
        return _asientos.AsReadOnly();
    }
}