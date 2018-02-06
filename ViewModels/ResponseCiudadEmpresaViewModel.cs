using Electrocore.ViewModels;

namespace electrocore.ViewModels
{
    public class ResponseCiudadEmpresaViewModel
    {
        public long Id{ get; set; }
        //Relaciones
        public long Ciudad_Id {get; set; }
        public long Empresa_Id{ get; set; }

        public  CiudadViewModel Ciudad{ get; set; }
        
        public  EmpresaViewModel Empresa{ get; set; }
    }
}