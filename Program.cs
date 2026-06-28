using System.Text;
using Practica02Atraccion.Services;
using Practica02Atraccion.UI;

Console.OutputEncoding = Encoding.UTF8;

SistemaAtraccion sistema =
    new SistemaAtraccion();

AplicacionConsola aplicacion =
    new AplicacionConsola(sistema);

aplicacion.Ejecutar();