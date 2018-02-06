using Electro.model.datatakemodel;

namespace electrocore.ViewModels
{
    public class DetalleTipoCableViewModel
    {
        public long Id{ get; set; }
        //Relaciones
        public long Tipocable_Id {get; set; }
        public long Cable_Id{ get; set; }

    }
}