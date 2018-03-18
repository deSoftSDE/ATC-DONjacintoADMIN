﻿using dsASPCAutoCAdmin.Entidades;
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
                new ElementoMenu { Key = "Marcas > Modelos", Value = "Marcas" },
                new ElementoMenu { Key = "Tipos de Vehículo", Value = "TiposVehiculo" },
                //new ElementoMenu { Key = "Modelo", Value = "Modelos" },
                new ElementoMenu { Key = "Carrocerias", Value = "Carrocerias" },
                new ElementoMenu { Key = "Tipos de Vidrio", Value = "TiposVidrio" },
                //new ElementoMenu { Key = "Vidrio", Value = "Vidrio" },
                new ElementoMenu { Key = "Articulos", Value = "Articulos" },

            };
        }
    }
}
