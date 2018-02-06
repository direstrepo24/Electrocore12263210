using System.Collections.Generic;
using Electro.model.datatakemodel;
using Electrocore.ViewModels;

namespace electrocore.ViewModels
{
    public class ResponseLoginViewModel
    {
        public long  Id{get;set;}
        public string Nombre {get;set;}
        public string Apellido {get;set;}
        public string Cedula {get;set;}
        public string Telefono {get;set;}
        public string Direccion{get;set;}
        public string CorreoElectronico{get;set;}
        public string Passsword{get;set;}

        public bool Device_Available{get;set;}

        //Relaciones
        public int  Tipo_Usuario_Id{get;set;}

        public long  Empresa_Id{get;set;}

        //
        public  TipoUsuarioViewModel Tipo_Usuario {get;set;}
        public  EmpresaViewModel Empresa {get;set;}
        public  DispositivoViewModel Dispositivo {get;set;}

        public List<ProyectoViewModel> Proyectos {get; set;}
    }
}