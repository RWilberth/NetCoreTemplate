using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Application.Common.Constants
{
    public class ErrorMessages
    {
        public readonly static string NOT_HANDLED = "Error interno en el servicio.";
        public readonly static string VALIDATION = "Error en la validación de datos.";
        public readonly static string UNAUTHORIZED = "Error las credenciales expiraron.";
        public readonly static string NOT_FOUND = "No se encontro el recurso.";
    }
}
