using System;
using System.Collections.Generic;

namespace Entidades
{
    public partial class Area
    {
        public Area()
        {
            Empleado = new HashSet<Empleado>();
        }

        public int IdArea { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Empleado> Empleado { get; set; }
    }
}
