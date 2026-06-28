using System;

namespace Practica02Atraccion.Models;

public class Persona
{
    public string Identificacion { get; }

    public string NombreCompleto { get; }

    public int NumeroLlegada { get; }

    public DateTime HoraRegistro { get; }

    public Persona(
        string identificacion,
        string nombreCompleto,
        int numeroLlegada)
    {
        Identificacion = identificacion;
        NombreCompleto = nombreCompleto;
        NumeroLlegada = numeroLlegada;
        HoraRegistro = DateTime.Now;
    }

    public override string ToString()
    {
        return $"{NumeroLlegada,-6} | " +
               $"{Identificacion,-15} | " +
               $"{NombreCompleto,-25} | " +
               $"{HoraRegistro:HH:mm:ss}";
    }
}