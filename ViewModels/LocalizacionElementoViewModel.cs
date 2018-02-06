namespace electrocore.ViewModels
{
    public class LocalizacionElementoViewModel
    {
        public long Id{ get; set; }
        public string Coordenadas{ get; set; }
        public decimal Latitud{ get; set; }
        public decimal Longitud{ get; set; }
        public string Direccion{ get; set; }
        public string DireccionAproximadaGps{ get; set; }
        public string Barrio{ get; set; }
        public string Localidad{ get; set; }
        public string Sector{ get; set; }
        public string ReferenciaLocalizacion{ get; set; }
        
        //Referencias
        public long Element_Id{ get; set; }
    }
}