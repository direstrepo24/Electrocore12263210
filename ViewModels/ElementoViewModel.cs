using System;
using System.Collections.Generic;
using Electro.model.datatakemodel;

namespace electrocore.ViewModels
{
    public class ElementoViewModel
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

        //Relacion de 1
        public  Proyecto Proyecto{ get; set; }
        public  Material Material{ get; set; }
        public  LongitudElemento LongitudElemento{ get; set; }
        public  Estado Estado{ get; set; }
        public  NivelTensionElemento NivelTensionElemento{get;set;}
        public  List<LocalizacionElemento> LocalizacionElementos{get;set;}
    }
}