using System;

namespace electrocore.ViewModels
{
    public class NovedadViewModel
    {
        public long Id { get; set; }
        public string Descripcion { get; set; }
      
        public long Detalle_Tipo_Novedad_Id { get; set; }   
        public long Elemento_Id { get; set; } 

        //Image
        public byte[] ImageArray { get; set; }
        public DateTime FechaCreacion{ get; set; }
        public string Hora{ get; set; }

    }
}