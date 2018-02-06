using System;

namespace electrocore.ViewModels
{
    public class ProyectoViewModel
    {
        public long Id{ get; set; }
        public string Nombre{ get; set; }
        public string Descripcion{ get; set; }
        public DateTime FechaInicio{ get; set; }
        public DateTime FechaFin{ get; set; }
        public string OrdenTrabajo{ get; set; }
        public bool IsActivo{ get; set; }

        //Porperties proyecto empresa
        public long Proyecto_Empresa_Id{ get; set; }
        public bool IsOperadora{ get; set; }
        public bool IsPropietaria{ get; set; }
        public bool IsInterventora{ get; set; }
        //Relaciones
        public long Empresa_Id{ get; set; }
        public long Proyecto_Id{ get; set; }
        
        //Relaciones
        public long Ciudad_Id{ get; set; }

        

    }
}