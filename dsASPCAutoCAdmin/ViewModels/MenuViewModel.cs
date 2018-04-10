using dsASPCAutoCAdmin.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dsASPCAutoCAdmin.ViewModels
{
    
    public class MenuViewModel
    {
        public List<ElementoMenu> menu;
        public MenuViewModel()
        {
            menu = new List<ElementoMenu>
            {
                new ElementoMenu { Key = "Marcas > Modelos", Value = "Marcas", Img = "~/images/logo.png" },
                new ElementoMenu { Key = "Tipos de Vehículo", Value = "TiposVehiculo", Img = "~/images/logo.png" },
                //new ElementoMenu { Key = "Modelo", Value = "Modelos" },
                new ElementoMenu { Key = "Carrocerias", Value = "Carrocerias", Img = "~/images/logo.png" },
                new ElementoMenu { Key = "Tipos de Vidrio", Value = "TiposVidrio", Img = "~/images/logo.png" },
                //new ElementoMenu { Key = "Vidrio", Value = "Vidrio" },
                new ElementoMenu { Key = "Articulos", Value = "Articulos", Img = "~/images/logo.png" },
                new ElementoMenu { Key = "Usuarios", Value = "Usuarios", Img = "~/images/logo.png" },
                new ElementoMenu { Key = "Configuración", Value = "Web", Img = "~/images/logo.png" },
                new ElementoMenu { Key = "Mensajes", Value = "Mensajes", Img = "~/images/logo.png" },
            };
        }
    }
}
