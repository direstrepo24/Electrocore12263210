namespace electrocore.ViewModels
{
    public class ResponseListCiudadEmpresaViewModel
    {
        public long Ciudad_Empresa_Id{ get; set; }
        //Relaciones
        public long Ciudad_Id {get; set; }
        public  string Nombre_Ciudad{ get; set; }
        public long Empresa_Id{ get; set; }
        public  string Nombre_Empresa{ get; set; }
        public  string Descripcion_Empresa{ get; set; }
        public string Direccion{ get; set; }
        public string Telefono{ get; set; }
        public string Nit{ get; set; }

        public bool Is_Operadora{ get; set; }

    }
}