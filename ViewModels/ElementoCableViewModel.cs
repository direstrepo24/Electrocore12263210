namespace electrocore.ViewModels
{
    public class ElementoCableViewModel
    {
        public long Elemento_Cable_Id{ get; set; }
        public string Codigo{ get; set; }
        public long Cantidad{ get; set; }
        public bool SobreRbt{ get; set; }
        public bool Tiene_Marquilla {get;set;}

        //Relaciones
        public long Empresa_Id{ get; set; }
        public long Ciudad_Id{ get; set; }
        public long? Ciudad_Empresa_Id{ get; set; }
        
        public long DetalleTipocable_Id{ get; set; }
        public long Elemento_Id{ get; set; }

         public bool Is_Enabled_Data {get; set;}
    }
}