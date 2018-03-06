using dsASPCAutoCAdmin.Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dsASPCAutoCAdmin.ViewModels
{
    public partial class SampleData
    {
        public static List<TipoVidrio> TiposVidrio = new List<TipoVidrio>
        {
            new TipoVidrio
            {
                IDTipoVidrio = 1,
                Descripcion = "Fuerte",
                Imagen = Guid.NewGuid().ToString(),
            },
            new TipoVidrio
            {
                IDTipoVidrio = 2,
                Descripcion = "Blando",
                Imagen = Guid.NewGuid().ToString(),
            },
            new TipoVidrio
            {
                IDTipoVidrio = 3,
                Descripcion = "Se rompe con mirarlo",
                Imagen = Guid.NewGuid().ToString(),
            },
        };
    }
}
