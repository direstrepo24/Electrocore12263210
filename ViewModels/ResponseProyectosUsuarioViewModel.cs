using System;
using System.Collections.Generic;
using Electro.model.datatakemodel;

namespace electrocore.ViewModels
{
    public class ResponseProyectosUsuarioViewModel
    {
        public long Id{ get; set; }
        public string Nombre{ get; set; }
        public string Descripcion{ get; set; }
        public DateTime FechaInicio{ get; set; }
        public DateTime FechaFin{ get; set; }
        public string OrdenTrabajo{ get; set; }
        public bool IsActivo{ get; set; }
        
        //Relaciones
        public long Ciudad_Id{ get; set; }
        public  Ciudad Ciudad{ get; set; }

        public List<ProyectoUsuario> ProyectoUsuarios {get; set;}
        public List<Usuario> Usuarios {get; set;}
    }
}