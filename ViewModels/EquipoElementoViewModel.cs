namespace electrocore.ViewModels
{
    public class EquipoElementoViewModel
    {
        public long Perdida_Id{ get; set; }
        public string Codigo{ get; set; }
        public string Descripcion{ get; set; }
        public long Cantidad{ get; set; }
        public bool ConectadoRbt{ get; set; }
        public bool MedidorBt{ get; set; }
        public long Consumo{ get; set; }
        public string UnidadMedida{ get; set; }

        //Relaciones

        public long EmpresaId{ get; set; }
        public long Ciudad_Id{ get; set; }
        public long? Ciudad_Empresa_Id{ get; set; }
        public long TipoEquipo_Id{ get; set; }
        public long Elemento_Id {get; set;}
    }
}