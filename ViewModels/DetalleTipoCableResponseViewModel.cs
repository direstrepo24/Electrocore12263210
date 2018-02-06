namespace electrocore.ViewModels
{
    public class DetalleTipoCableResponseViewModel
    {
        public long Id{ get; set; }
        //Relaciones
        public long Tipocable_Id {get; set; }
        public long Cable_Id{ get; set; }

        //Cables
        public string NombreCable{ get; set; }
        public string SiglaCable{ get; set; }

        //TipoCable
        public string NombreTipoCable{ get; set; }


    }
}