using System;

namespace Electrocore12263210.ViewModels
{
    public class ElementoViewModelSearch
    {
         public long Id{ get; set; }
        public string CodigoApoyo{ get; set; }
        public long NumeroApoyo{ get; set; }
        public DateTime FechaLevantamiento{ get; set; }
        public string HoraInicio{ get; set; }
        public string HoraFin{ get; set; }
        public string ResistenciaMecanica{ get; set; }
        public long Retenidas{ get; set; }
        public double AlturaDisponible{ get; set; }

        //Relaciones
        public long Usuario_Id{ get; set; }
        public long Estado_id{ get; set; }
        public long Longitud_Elemento_Id{ get; set; }
        public long Material_Id{ get; set; }
        public long Proyecto_Id{ get; set; }
        public long Nivel_Tension_Id{ get; set; }
        public long Ciudad_Id{ get; set; }

        public string Imei_Device{ get; set; }
        public string Token_Elemento{ get; set; }

         public bool Is_Enabled_Data {get; set;}

        //Ubicacion de 1
        public string Coordenadas{ get; set; }
        public decimal Latitud{ get; set; }
        public decimal Longitud{ get; set; }
        public string Direccion{ get; set; }
        public string DireccionAproximadaGps{ get; set; }
        public string Barrio{ get; set; }
        public string Localidad{ get; set; }
        public string Sector{ get; set; }
        public string ReferenciaLocalizacion{ get; set; }
        
    }
}