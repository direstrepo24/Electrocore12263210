using System;
using System.ComponentModel.DataAnnotations;

namespace Electrocore.ViewModels
{
    public class LecturaViewModel
    {
    public long  Id{get;set;}
     public long Cuenta_id {get;set;}
     public int Ruta_id {get;set;} 
     [Required]
     public long Medidor_id{get;set;}
     public int Tipo_medida_id {get;set;}
     public int Servicio_id{get;set;}
     public DateTime  Fecha {get;set;}
     public int Periodo_id{get;set;}
     public int Lector_id{get;set;}
     [Required]
     public long lectura{get;set;}
    }
}