namespace electrocore.ViewModels
{
    public class Ciudad_EmpresaViewModel
    {
        public long Id{ get; set; }
        //Relaciones
        public long Ciudad_Id {get; set; }
        public long Empresa_Id{ get; set; }
    }
}