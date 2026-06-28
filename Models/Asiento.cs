namespace Practica02Atraccion.Models;

public class Asiento
{
    public int Numero { get; }

    public Persona? Ocupante { get; private set; }

    public bool EstaOcupado => Ocupante is not null;

    public Asiento(int numero)
    {
        Numero = numero;
    }

    public bool Asignar(Persona persona)
    {
        if (EstaOcupado)
        {
            return false;
        }

        Ocupante = persona;
        return true;
    }

    public override string ToString()
    {
        if (Ocupante is null)
        {
            return $"{Numero,-8} | DISPONIBLE";
        }

        return $"{Numero,-8} | " +
               $"{Ocupante.Identificacion,-15} | " +
               $"{Ocupante.NombreCompleto,-25}";
    }
}