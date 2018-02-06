namespace electrocore.ViewModels
{
    public class UsuarioViewModel
    {
        
 
        public long  Id{get;set;}
        public string Nombre {get;set;}
        public string Apellido {get;set;}
        public string Cedula {get;set;}
        public string Telefono {get;set;}
        public string Direccion{get;set;}
        public string CorreoElectronico{get;set;}
        public string Passsword{get;set;}

        //Relaciones
        public int  Tipo_Usuario_Id{get;set;}

        public long  Empresa_Id{get;set;}
    }
}