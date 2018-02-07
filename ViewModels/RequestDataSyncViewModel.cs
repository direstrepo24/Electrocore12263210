using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Electro.model.datatakemodel;

namespace electrocore.ViewModels
{
    public class RequestDataSyncViewModel
    {
        
        public long Elemento_Id;
        public string CodigoApoyo{ get; set; }
        public long NumeroApoyo { get; set; }
        public DateTime FechaLevantamiento{ get; set; }
        public string HoraInicio{ get; set; }
        public string HoraFin{ get; set; }
        public string ResistenciaMecanica{ get; set; }
        public long Retenidas{ get; set; }
        public double AlturaDisponible{ get; set; }

        public string Imei_Device{ get; set; }
        public string Token_Elemento{ get; set; }

         public bool Is_Enabled_Data {get; set;}

        //Relaciones
        [Required]
        public long Usuario_Id{ get; set; }
        [Required]
        public long Estado_id{ get; set; }
        [Required]
        public long Longitud_Elemento_Id{ get; set; }
        [Required]
        public long Material_Id{ get; set; }

        [Range(0, long.MaxValue, ErrorMessage = "El valor {0} no es válido para Proyecto_Id")]
        [Required]
        public long Proyecto_Id{ get; set; }
        [Required]
        public long Nivel_Tension_Id{ get; set; }
        [Required]
        public long Ciudad_Id{ get; set; }


        //Localizacion Elemento
        public string Coordenadas{ get; set; }
        public decimal Latitud{ get; set; }
        public decimal Longitud{ get; set; }
        public string Direccion{ get; set; }
        public string DireccionAproximadaGps{ get; set; }
        public string Barrio{ get; set; }
        public string Localidad{ get; set; }
        public string Sector{ get; set; }
        public string ReferenciaLocalizacion{ get; set; }
        
        
        //Detalles del elemento
        public List<ElementoCableViewModel> Cables {get; set;}
        public List<EquipoElementoViewModel> Equipos {get; set;}
        public List<PerdidaViewModel> Perdidas {get; set;}
        public List<NovedadViewModel> Novedades {get; set;}
        public List<FotoViewModel> Fotos {get; set;}

    }
}