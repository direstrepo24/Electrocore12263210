using System.Collections.Generic;
using Electro.model.datatakemodel;
using Electro.model.Models.datatakemodel;
using electrocore.ViewModels;
//using Electrocore.Models.datatakemodel;
namespace Electrocore.ViewModels
{
    public class ResponseDataSyncModelView
    {
        public  List<TipoCable> tipocable{ get; set; }

        //incluir en la respuesta "Cable"
        public  List<DetalleTipoCableResponseViewModel> detalletipocable{ get; set; }//inlcuir el cable
        public  List<TipoNovedad> tiponovedad {get;set;}
        public  List<TipoEquipo> TipoEquipo{get;set;}
        public  List<DetalleTipoNovedad> detalletiponovedad{get;set;}
        public  List<Empresa> empresa{get;set;}
        public  List<Departamento> departCiudades{get;set;}
        public  List<Estado> estados{get;set;}
        public  List<NivelTensionElemento> nivelTensionElementos {get;set;}
        public  List<LongitudElemento> longitudElementos{get;set;}
        public  List<Material> materiales{get;set;}
        public  List<Tipo_Perdida> tipo_Perdidas{get;set;}
        public  List<ResponseListCiudadEmpresaViewModel> ciudad_empresas{get;set;}

    }
}