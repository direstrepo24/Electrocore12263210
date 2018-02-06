using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Electro.model.datatakemodel;
using Electro.model.Models.datatakemodel;
using Electro.model.Repository;
using electrocore.ViewModels;
using Electrocore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace electrocore.Controllers
{
     [Route("api/[controller]")]
    public class MasterDataController:Controller
    {
        #region Atributes
        private readonly IEstadoRepository _estadoRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly ILongitudElementoRepository _longitudElementoRepository;
        private readonly INivelTensionElementoRepository _nivelTensionElementoRepository;
        private readonly ITipo_PerdidaRepository _tipo_PerdidaRepository;
        private readonly ITipoEquipoRepository _tipoEquipoRepository;
        private readonly ICableRepository _cableRepository;
        private readonly ITipoCableRepository _tipoCableRepository;
        private readonly IDetalleTipoCableRepository _detalleTipoCableRepository;
        private readonly ITipoNovedadRepository _tipoNovedadRepository;
        private readonly IDetalleTipoNovedadRepository _detalleTipoNovedadRepository;



        private readonly IMapper _mapper;

        #endregion

        #region Constructor
        public MasterDataController(
            IEstadoRepository estadoRepository,
            IMaterialRepository materialRepository,
            ILongitudElementoRepository longitudElementoRepository,
            INivelTensionElementoRepository nivelTensionElementoRepository,
            ITipo_PerdidaRepository tipo_PerdidaRepository,
            ITipoEquipoRepository tipoEquipoRepository,
            ICableRepository cableRepository,
            ITipoCableRepository tipoCableRepository,
            IDetalleTipoCableRepository detalleTipoCableRepository,
            ITipoNovedadRepository tipoNovedadRepository,
            IDetalleTipoNovedadRepository detalleTipoNovedadRepository,
            IMapper mapper){

            
            _materialRepository=materialRepository;
            _estadoRepository=estadoRepository;
            _longitudElementoRepository=longitudElementoRepository;
            _nivelTensionElementoRepository=nivelTensionElementoRepository;
            _tipo_PerdidaRepository=tipo_PerdidaRepository;
            _tipoEquipoRepository= tipoEquipoRepository;
            _cableRepository=cableRepository;
            _tipoCableRepository=tipoCableRepository;
            _detalleTipoCableRepository=detalleTipoCableRepository;
            _tipoNovedadRepository= tipoNovedadRepository;
            _detalleTipoNovedadRepository= detalleTipoNovedadRepository;

            _mapper= mapper;
        }
        #endregion

        #region Methods ESTADO
        [HttpPost]
        [Route("PostEstado")] 
        //Post
        public async Task<ActionResult> PostEstado([FromBody]EstadoViewModel estadoViewModel)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            // var model = _mapper.Map<LecturaViewModel, Lectura>(lectura);
             var model = _mapper.Map<EstadoViewModel, Estado>(estadoViewModel);
             await this._estadoRepository.AddAsync(model);
             await _estadoRepository.Commit();
             return Ok(model);
        }


        //[Produces(typeof(Lectura))] 
         //  [HttpGet]
        //[Route("GetVehicleByUser/{cedula}")]      
        [HttpGet]
        [Route("GetEstados")] 
        [SwaggerResponse(200, typeof(EstadoViewModel))]
        public async Task<ActionResult> GetEstados()
        {
           //https://stackoverflow.com/questions/40275195/how-to-setup-automapper-in-asp-net-core
           var estado= await _estadoRepository.GetAllAsync();//AllIncludingAsync(c=>c.Medidor);//GetAllAsync();
            var modelView = _mapper.Map<IEnumerable<Estado>, IEnumerable<EstadoViewModel>>(estado);
            return Ok(modelView);
        }



        [HttpPut]
        [Route("PutEstado/{Id}")] 
        public async Task<EstadoViewModel> PutEstado(long id, [FromBody]EstadoViewModel estadoViewModel)
        {
            var modeledit= await _estadoRepository.GetSingleAsync(m=>m.Id==id);
            var model = _mapper.Map<EstadoViewModel, Estado>(estadoViewModel);
            if(modeledit!=null){
             
                modeledit.Nombre=model.Nombre;
                modeledit.Sigla=model.Sigla;
                await this._estadoRepository.EditAsync(modeledit);
            }
            return estadoViewModel;
        }

        [HttpDelete]
        [Route("DeleteEstado/{Id}")] 
        public async Task<ActionResult> DeleteEstado(long id)
        {
          var deleteEntity= await _estadoRepository.GetSingleAsync(id);
          if(deleteEntity!=null){
                 this._estadoRepository.Delete(deleteEntity);
                 await _estadoRepository.Commit();
          }
         var estadoViewModel = _mapper.Map<Estado, EstadoViewModel>(deleteEntity);
         return Ok(estadoViewModel);
        }

        #endregion

        #region Methods MATERIAL
        
        [HttpPost]
        [Route("PostMaterial")] 
        public async Task<ActionResult> PostMaterial([FromBody]MaterialViewModel materialView)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
             var model = _mapper.Map<MaterialViewModel, Material>(materialView);
             await this._materialRepository.AddAsync(model);
             await _materialRepository.Commit();
             return Ok(model);
        }


        //[Produces(typeof(Lectura))] 
         //  [HttpGet]
        //[Route("GetVehicleByUser/{cedula}")]      
        [HttpGet]
        [Route("GetMaterial")] 
        [SwaggerResponse(200, typeof(MaterialViewModel))]
        public async Task<ActionResult> GetMaterial()
        {
                //https://stackoverflow.com/questions/40275195/how-to-setup-automapper-in-asp-net-core
           var material= await _materialRepository.GetAllAsync();//AllIncludingAsync(c=>c.Medidor);//GetAllAsync();
            var modelView = _mapper.Map<IEnumerable<Material>, IEnumerable<MaterialViewModel>>(material);
            return Ok(modelView);
        }


        [HttpPut]
        [Route("PutMaterial/{Id}")] 
        public async Task<MaterialViewModel> PutMaterial(long id, [FromBody]MaterialViewModel materialViewModel)
        {
            var modeledit= await _materialRepository.GetSingleAsync(m=>m.Id==id);
            
             var model = _mapper.Map<MaterialViewModel, Material>(materialViewModel);
        
            if(modeledit!=null){
             
                modeledit.Nombre=model.Nombre;
                modeledit.Sigla=model.Sigla;
                await this._materialRepository.EditAsync(modeledit);
            }

            return materialViewModel;
        }

        [HttpDelete]
        [Route("DeleteMaterial/{Id}")] 
        public async Task<ActionResult> DeleteMaterial(long id)
        {
          var deleteEntity= await _materialRepository.GetSingleAsync(id);
          if(deleteEntity!=null){
                 this._materialRepository.Delete(deleteEntity);
                 await _materialRepository.Commit();

          }

         var estadoViewModel = _mapper.Map<Material, MaterialViewModel>(deleteEntity);
         return Ok(estadoViewModel);
           
        }
        #endregion

        #region Methods NIVEL TENSION
        
        [HttpPost]
        [Route("PostNivelTension")] 
        public async Task<ActionResult> PostNivelTension([FromBody]NivelTensionElementoViewModel viewModel)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
             var model = _mapper.Map<NivelTensionElementoViewModel, NivelTensionElemento>(viewModel);
             await this._nivelTensionElementoRepository.AddAsync(model);
             await _nivelTensionElementoRepository.Commit();
             return Ok(model);
        }


        //[Produces(typeof(Lectura))] 
         //  [HttpGet]
        //[Route("GetVehicleByUser/{cedula}")]      
        [HttpGet]
        [Route("GetNivelTension")] 
        [SwaggerResponse(200, typeof(NivelTensionElementoViewModel))]
        public async Task<ActionResult> GetNivelTension()
        {
                //https://stackoverflow.com/questions/40275195/how-to-setup-automapper-in-asp-net-core
           var list= await _nivelTensionElementoRepository.GetAllAsync();//AllIncludingAsync(c=>c.Medidor);//GetAllAsync();
            var modelView = _mapper.Map<IEnumerable<NivelTensionElemento>, IEnumerable<NivelTensionElementoViewModel>>(list);
            return Ok(modelView);
        }


        [HttpPut]
        [Route("PutNivelTension/{Id}")] 
        public async Task<ActionResult> PutNivelTension(long id, [FromBody]NivelTensionElementoViewModel viewModel)
        {
            var modeledit= await _nivelTensionElementoRepository.GetSingleAsync(m=>m.Id==id);
            var model = _mapper.Map<NivelTensionElementoViewModel, NivelTensionElemento>(viewModel);
        
            if(modeledit!=null){
             
                modeledit.Nombre=model.Nombre;
                modeledit.Sigla=model.Sigla;
                await this._nivelTensionElementoRepository.EditAsync(modeledit);
            }
            return Ok(viewModel);
        }

        [HttpDelete]
        [Route("DeleteNivelTension/{Id}")] 
        public async Task<ActionResult> DeleteNivelTension(long id)
        {
          var deleteEntity= await _nivelTensionElementoRepository.GetSingleAsync(id);
          if(deleteEntity!=null){
                 this._nivelTensionElementoRepository.Delete(deleteEntity);
                 await _nivelTensionElementoRepository.Commit();

          }
         var viewModel = _mapper.Map<NivelTensionElemento, NivelTensionElementoViewModel>(deleteEntity);
         return Ok(viewModel);
           
        }

        #endregion
        
        #region Methods LONGITUD ELEMENTO
        [HttpPost]
        [Route("PostLongitudElemento")] 
        public async Task<ActionResult> PostLongitudElemento([FromBody]LongitudElementoViewModel viewModel)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
             var model = _mapper.Map<LongitudElementoViewModel, LongitudElemento>(viewModel);
             await this._longitudElementoRepository.AddAsync(model);
             await _longitudElementoRepository.Commit();
             return Ok(model);
        }


        //[Produces(typeof(Lectura))] 
         //  [HttpGet]
        //[Route("GetVehicleByUser/{cedula}")]      
        [HttpGet]
        [Route("GetLongitudElemento")] 
        [SwaggerResponse(200, typeof(LongitudElementoViewModel))]
        public async Task<ActionResult> GetLongitudElemento()
        {
                //https://stackoverflow.com/questions/40275195/how-to-setup-automapper-in-asp-net-core
            var list= await _longitudElementoRepository.GetAllAsync();//AllIncludingAsync(c=>c.Medidor);//GetAllAsync();
            var modelView = _mapper.Map<IEnumerable<LongitudElemento>, IEnumerable<LongitudElementoViewModel>>(list);
            return Ok(modelView);
        }


        [HttpPut]
        [Route("PutLongitudElemento/{Id}")] 
        public async Task<ActionResult> PutLongitudElemento(long id, [FromBody]LongitudElementoViewModel viewModel)
        {
            var modeledit= await _longitudElementoRepository.GetSingleAsync(m=>m.Id==id);
            var model = _mapper.Map<LongitudElementoViewModel, LongitudElemento>(viewModel);
        
            if(modeledit!=null){
             
                modeledit.Valor=model.Valor;
                modeledit.UnidadMedida=model.UnidadMedida;
                await this._longitudElementoRepository.EditAsync(modeledit);
            }
            return Ok(viewModel);
        }

        [HttpDelete]
        [Route("DeleteLongitudElemento/{Id}")] 
        public async Task<ActionResult> DeleteLongitudElemento(long id)
        {
          var deleteEntity= await _longitudElementoRepository.GetSingleAsync(id);
          if(deleteEntity!=null){
                 this._longitudElementoRepository.Delete(deleteEntity);
                 await _longitudElementoRepository.Commit();

          }
         var viewModel = _mapper.Map<LongitudElemento, LongitudElementoViewModel>(deleteEntity);
         return Ok(viewModel);
           
        }

        #endregion

        #region Methods TIPO PERDIDA
        [HttpPost]
        [Route("PostTipoPerdida")] 
        public async Task<ActionResult> PostTipoPerdida([FromBody]Tipo_PerdidaViewModel viewModel)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
             var model = _mapper.Map<Tipo_PerdidaViewModel, Tipo_Perdida>(viewModel);
             await this._tipo_PerdidaRepository.AddAsync(model);
             await _tipo_PerdidaRepository.Commit();
             return Ok(model);
        }


        //[Produces(typeof(Lectura))] 
         //  [HttpGet]
        //[Route("GetVehicleByUser/{cedula}")]      
        [HttpGet]
        [Route("GetTipoPerdida")] 
        [SwaggerResponse(200, typeof(Tipo_PerdidaViewModel))]
        public async Task<ActionResult> GetTipoPerdida()
        {
                //https://stackoverflow.com/questions/40275195/how-to-setup-automapper-in-asp-net-core
           var list= await _tipo_PerdidaRepository.GetAllAsync();//AllIncludingAsync(c=>c.Medidor);//GetAllAsync();
            var modelView = _mapper.Map<IEnumerable<Tipo_Perdida>, IEnumerable<Tipo_PerdidaViewModel>>(list);
            return Ok(modelView);
        }


        [HttpPut]
        [Route("PutTipoPerdida/{Id}")] 
        public async Task<ActionResult> PutTipoPerdida(long id, [FromBody]Tipo_PerdidaViewModel viewModel)
        {
            var modeledit= await _tipo_PerdidaRepository.GetSingleAsync(m=>m.Id==id);
            var model = _mapper.Map<Tipo_PerdidaViewModel, Tipo_Perdida>(viewModel);
            if(modeledit!=null){
                modeledit.Nombre=model.Nombre;
                await this._tipo_PerdidaRepository.EditAsync(modeledit);
            }
            return Ok(viewModel);
        }

        [HttpDelete]
        [Route("DeleteTipoPerdida/{Id}")] 
        public async Task<ActionResult> DeleteTipoPerdida(long id)
        {
          var deleteEntity= await _tipo_PerdidaRepository.GetSingleAsync(id);
          if(deleteEntity!=null){
                 this._tipo_PerdidaRepository.Delete(deleteEntity);
                 await _tipo_PerdidaRepository.Commit();

          }
         var viewModel = _mapper.Map<Tipo_Perdida, Tipo_PerdidaViewModel>(deleteEntity);
         return Ok(viewModel);
           
        }

        #endregion

        #region Methods TIPO EQUIPO
        [HttpPost]
        [Route("PostTipoEquipo")] 
        public async Task<ActionResult> PostTipoEquipo([FromBody]TipoEquipoViewModel viewModel)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
             var model = _mapper.Map<TipoEquipoViewModel, TipoEquipo>(viewModel);
             await this._tipoEquipoRepository.AddAsync(model);
             await _tipoEquipoRepository.Commit();
             return Ok(model);
        }


        //[Produces(typeof(Lectura))] 
         //  [HttpGet]
        //[Route("GetVehicleByUser/{cedula}")]      
        [HttpGet]
        [Route("GetTipoEquipo")] 
        [SwaggerResponse(200, typeof(TipoEquipoViewModel))]
        public async Task<ActionResult> GetTipoEquipo()
        {
                //https://stackoverflow.com/questions/40275195/how-to-setup-automapper-in-asp-net-core
           var list= await _tipoEquipoRepository.GetAllAsync();//AllIncludingAsync(c=>c.Medidor);//GetAllAsync();
            var modelView = _mapper.Map<IEnumerable<TipoEquipo>, IEnumerable<TipoEquipoViewModel>>(list);
            return Ok(modelView);
        }


        [HttpPut]
        [Route("PutTipoEquipo/{Id}")] 
        public async Task<ActionResult> PutTipoEquipo(long id, [FromBody]TipoEquipoViewModel viewModel)
        {
            var modeledit= await _tipoEquipoRepository.GetSingleAsync(m=>m.Id==id);
            var model = _mapper.Map<TipoEquipoViewModel, TipoEquipo>(viewModel);
            if(modeledit!=null){
                modeledit.Nombre=model.Nombre;
                await this._tipoEquipoRepository.EditAsync(modeledit);
            }
            return Ok(viewModel);
        }

        [HttpDelete]
        [Route("DeleteTipoEquipo/{Id}")] 
        public async Task<ActionResult> DeleteTipoEquipo(long id)
        {
          var deleteEntity= await _tipoEquipoRepository.GetSingleAsync(id);
          if(deleteEntity!=null){
                 this._tipoEquipoRepository.Delete(deleteEntity);
                 await _tipoEquipoRepository.Commit();

          }
         var viewModel = _mapper.Map<TipoEquipo, TipoEquipoViewModel>(deleteEntity);
         return Ok(viewModel);
           
        }

        #endregion

        #region Methods CABLES
        [HttpPost]
        [Route("PostCable")] 
        public async Task<ActionResult> PostCables([FromBody]CableViewModel viewModel)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
             var model = _mapper.Map<CableViewModel, Cable>(viewModel);
             await this._cableRepository.AddAsync(model);
             await _cableRepository.Commit();
             return Ok(model);
        }


        //[Produces(typeof(Lectura))] 
         //  [HttpGet]
        //[Route("GetVehicleByUser/{cedula}")]      
        [HttpGet]
        [Route("GetCables")] 
        [SwaggerResponse(200, typeof(CableViewModel))]
        public async Task<ActionResult> GetCables()
        {
                //https://stackoverflow.com/questions/40275195/how-to-setup-automapper-in-asp-net-core
           var list= await _cableRepository.GetAllAsync();//AllIncludingAsync(c=>c.Medidor);//GetAllAsync();
            var modelView = _mapper.Map<IEnumerable<Cable>, IEnumerable<CableViewModel>>(list);
            return Ok(modelView);
        }


        [HttpPut]
        [Route("PutCable/{Id}")] 
        public async Task<ActionResult> PutCable(long id, [FromBody]CableViewModel viewModel)
        {
            var modeledit= await _cableRepository.GetSingleAsync(m=>m.Id==id);
            var model = _mapper.Map<CableViewModel, Cable>(viewModel);
            if(modeledit!=null){
                modeledit.Nombre=model.Nombre;
                modeledit.Sigla=model.Sigla;
                await this._cableRepository.EditAsync(modeledit);
            }
            return Ok(viewModel);
        }

        [HttpDelete]
        [Route("DeleteCable/{Id}")] 
        public async Task<ActionResult> DeleteCable(long id)
        {
          var deleteEntity= await _cableRepository.GetSingleAsync(id);
          if(deleteEntity!=null){
                 this._cableRepository.Delete(deleteEntity);
                 await _cableRepository.Commit();

          }
         var viewModel = _mapper.Map<Cable, CableViewModel>(deleteEntity);
         return Ok(viewModel);
           
        }
        #endregion

        #region Methods TIPO CABLES
        [HttpPost]
        [Route("PostTipoCable")] 
        public async Task<ActionResult> PostTipoCable([FromBody]TipoCableViewModel viewModel)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
             var model = _mapper.Map<TipoCableViewModel, TipoCable>(viewModel);
             await this._tipoCableRepository.AddAsync(model);
             await _tipoCableRepository.Commit();
             return Ok(model);
        }


        //[Produces(typeof(Lectura))] 
         //  [HttpGet]
        //[Route("GetVehicleByUser/{cedula}")]      
        [HttpGet]
        [Route("GetTipoCables")] 
        [SwaggerResponse(200, typeof(CableViewModel))]
        public async Task<ActionResult> GetTipoCables()
        {
                //https://stackoverflow.com/questions/40275195/how-to-setup-automapper-in-asp-net-core
           var list= await _tipoCableRepository.GetAllAsync();//AllIncludingAsync(c=>c.Medidor);//GetAllAsync();
            var modelView = _mapper.Map<IEnumerable<TipoCable>, IEnumerable<TipoCableViewModel>>(list);
            return Ok(modelView);
        }


        [HttpPut]
        [Route("PutTipoCable/{Id}")] 
        public async Task<ActionResult> PutTipoCable(long id, [FromBody]TipoCableViewModel viewModel)
        {
            var modeledit= await _tipoCableRepository.GetSingleAsync(m=>m.Id==id);
            var model = _mapper.Map<TipoCableViewModel, TipoCable>(viewModel);
            if(modeledit!=null){
                modeledit.Nombre=model.Nombre;
                await this._tipoCableRepository.EditAsync(modeledit);
            }
            return Ok(viewModel);
        }

        [HttpDelete]
        [Route("DeleteTipoCable/{Id}")] 
        public async Task<ActionResult> DeleteTipoCable(long id)
        {
          var deleteEntity= await _tipoCableRepository.GetSingleAsync(id);
          if(deleteEntity!=null){
                 this._tipoCableRepository.Delete(deleteEntity);
                 await _tipoCableRepository.Commit();

          }
         var viewModel = _mapper.Map<TipoCable, TipoCableViewModel>(deleteEntity);
         return Ok(viewModel);
           
        }
        #endregion

        #region Methods DETALLE TIPO CABLES

        [HttpPost]
        [Route("PostDetalleTipoCable")] 
        public async Task<ActionResult> PostDetalleTipoCable([FromBody]DetalleTipoCableViewModel viewModel)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var response = new ResponseViewModel();
            var validacionCable= await _cableRepository.GetSingleAsync(a=>a.Id==viewModel.Cable_Id);
            var validacionTipoCable= await _tipoCableRepository.GetSingleAsync(a=>a.Id==viewModel.Tipocable_Id);

            var validacionExistDetalleCable= await _detalleTipoCableRepository.GetSingleAsync(a=>a.Id==viewModel.Tipocable_Id && a.Cable_Id==viewModel.Cable_Id);

            if(validacionCable==null){
                response.IsSuccess=false;
                response.Message="El cable no existe";
                return Ok(response);
            }

            if(validacionTipoCable==null){
                response.IsSuccess=false;
                response.Message="El Tipo de cable no existe";
                return Ok(response);
            }

            if(validacionExistDetalleCable!= null){
                response.IsSuccess=false;
                response.Message="Ya se encuentra registrado un detalle igual";
                response.Result= validacionExistDetalleCable;
                return Ok(response);
            }



             var model = _mapper.Map<DetalleTipoCableViewModel, DetalleTipoCable>(viewModel);
             await this._detalleTipoCableRepository.AddAsync(model);
             await _detalleTipoCableRepository.Commit();

             response.IsSuccess=true;
             response.Message="Ok";
             response.Result=model;
             return Ok(response);
        }


        //[Produces(typeof(Lectura))] 
         //  [HttpGet]
        //[Route("GetVehicleByUser/{cedula}")]      
        [HttpGet]
        [Route("GetDetalleTipoCable")] 
        [SwaggerResponse(200, typeof(DetalleTipoCable))]
        public async Task<ActionResult> GetDetalleTipoCable()
        {
           //https://stackoverflow.com/questions/40275195/how-to-setup-automapper-in-asp-net-core
           var list= await _detalleTipoCableRepository.AllIncludingAsync(a=>a.Cable,b=>b.TipoCable);//AllIncludingAsync(c=>c.Medidor);//GetAllAsync();
           ///var modelView = _mapper.Map<IEnumerable<DetalleTipoCable>, IEnumerable<DetalleTipoCableViewModel>>(list);
           var responseLis=ToListResponseDetalleCable(list);
           return Ok(responseLis);
        }

        private List<DetalleTipoCableResponseViewModel> ToListResponseDetalleCable(IEnumerable<DetalleTipoCable> modelView)
        {
            var listResponse= new List<DetalleTipoCableResponseViewModel>();
            foreach (var item in modelView)
            {
                listResponse.Add(new DetalleTipoCableResponseViewModel{
                    Id= item.Id,
                    Cable_Id= item.Cable_Id,
                    Tipocable_Id= item.Tipocable_Id,
                    NombreCable= item.Cable.Nombre,
                    NombreTipoCable= item.TipoCable.Nombre,
                    SiglaCable= item.Cable.Sigla
                });
            }
            return listResponse;
        }

        [HttpPut]
        [Route("PutDetalleTipoCable/{Id}")] 
        public async Task<ActionResult> PutDetalleTipoCable(long id, [FromBody]DetalleTipoCableViewModel viewModel)
        {
            var modeledit= await _detalleTipoCableRepository.GetSingleAsync(m=>m.Id==id);
            var model = _mapper.Map<DetalleTipoCableViewModel, DetalleTipoCable>(viewModel);
            if(modeledit!=null){

                modeledit.Tipocable_Id=model.Tipocable_Id;
                modeledit.Cable_Id=model.Cable_Id;
                await this._detalleTipoCableRepository.EditAsync(modeledit);
            }
            return Ok(viewModel);
        }

        [HttpDelete]
        [Route("DeleteDetalleTipoCable/{Id}")] 
        public async Task<ActionResult> DeleteDetalleTipoCable(long id)
        {
          var deleteEntity= await _detalleTipoCableRepository.GetSingleAsync(id);
          if(deleteEntity!=null){
                 this._detalleTipoCableRepository.Delete(deleteEntity);
                 await _detalleTipoCableRepository.Commit();

          }

         var viewModel = _mapper.Map<DetalleTipoCable, DetalleTipoCableViewModel>(deleteEntity);
         return Ok(viewModel);
        }
        #endregion

        #region Methods TIPO NOVEDAD
        [HttpPost]
        [Route("PostTipoNovedad")] 
        public async Task<ActionResult> PostTipoNovedad([FromBody]TipoNovedadViewModel viewModel)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var response= new ResponseViewModel();

            var verificateExist=await _tipoNovedadRepository.GetSingleAsync(a=>a.Nombre.ToUpper().Contains(viewModel.Nombre.ToUpper()));
            if(verificateExist!=null){
                response.IsSuccess=false;
                response.Message="La novedad ya se encuentra registrada";
                response.Result=verificateExist;
                return Ok(response);
            }
             var model = _mapper.Map<TipoNovedadViewModel, TipoNovedad>(viewModel);
             await this._tipoNovedadRepository.AddAsync(model);
             await _tipoNovedadRepository.Commit();


            response.IsSuccess=true;
            response.Message="Ok";
            response.Result=model;
            return Ok(response);
        }


        //[Produces(typeof(Lectura))] 
         //  [HttpGet]
        //[Route("GetVehicleByUser/{cedula}")]      
        [HttpGet]
        [Route("GetTipoNovedades")] 
        [SwaggerResponse(200, typeof(TipoNovedadViewModel))]
        public async Task<ActionResult> GetTipoNovedades()
        {
                //https://stackoverflow.com/questions/40275195/how-to-setup-automapper-in-asp-net-core
           var list= await _tipoNovedadRepository.GetAllAsync();//AllIncludingAsync(c=>c.Medidor);//GetAllAsync();
            var modelView = _mapper.Map<IEnumerable<TipoNovedad>, IEnumerable<TipoNovedadViewModel>>(list);
            return Ok(modelView);
        }


        [HttpPut]
        [Route("PutTipoNovedad/{Id}")] 
        public async Task<ActionResult> PutTipoNovedad(long id, [FromBody]TipoNovedadViewModel viewModel)
        {
            var modeledit= await _tipoNovedadRepository.GetSingleAsync(m=>m.Id==id);
            var model = _mapper.Map<TipoNovedadViewModel, TipoNovedad>(viewModel);
            if(modeledit!=null){
                modeledit.Nombre=model.Nombre;
                await this._tipoNovedadRepository.EditAsync(modeledit);
            }
            return Ok(viewModel);
        }

        [HttpDelete]
        [Route("DeleteTipoNovedad/{Id}")] 
        public async Task<ActionResult> DeleteTipoNovedad(long id)
        {
          var deleteEntity= await _tipoNovedadRepository.GetSingleAsync(id);
          if(deleteEntity!=null){
                 this._tipoNovedadRepository.Delete(deleteEntity);
                 await _tipoNovedadRepository.Commit();

          }
         var viewModel = _mapper.Map<TipoNovedad, TipoNovedadViewModel>(deleteEntity);
         return Ok(viewModel);
           
        }
        #endregion

        #region Methods DETALLE TIPO NOVEDAD
        [HttpPost]
        [Route("PostDetalleTipoNovedad")] 
        public async Task<ActionResult> PostDetalleTipoNovedad([FromBody]Detalle_TipoNovedadViewModel viewModel)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var response= new ResponseViewModel();

            var validarDates=await _tipoNovedadRepository.GetSingleAsync(a=>a.Id==viewModel.Tipo_Novedad_id);
            if(validarDates==null){
                response.IsSuccess=false;
                response.Message=string.Format("El tipo de novedad con id {0} no existe",viewModel.Tipo_Novedad_id);
                return Ok(response);
            }


            var verificateExist=await _detalleTipoNovedadRepository.GetSingleAsync(a=>a.Tipo_Novedad_id==viewModel.Tipo_Novedad_id && a.Nombre.ToUpper().Contains(viewModel.Nombre.ToUpper()));
            if(verificateExist!=null){
                response.IsSuccess=false;
                response.Message="El detalle de novedad ya se encuentra registrado";
                response.Result=verificateExist;
                return Ok(response);
            }


             var model = _mapper.Map<Detalle_TipoNovedadViewModel, DetalleTipoNovedad>(viewModel);
             await this._detalleTipoNovedadRepository.AddAsync(model);
             await _detalleTipoNovedadRepository.Commit();


            response.IsSuccess=true;
            response.Message="Ok";
            response.Result=model;
            return Ok(response);
             
        }


        //[Produces(typeof(Lectura))] 
         //  [HttpGet]
        //[Route("GetVehicleByUser/{cedula}")]      
        [HttpGet]
        [Route("GetDetalleTipoNovedades")] 
        [SwaggerResponse(200, typeof(TipoNovedadViewModel))]
        public async Task<ActionResult> GetDetalleTipoNovedades()
        {
                //https://stackoverflow.com/questions/40275195/how-to-setup-automapper-in-asp-net-core
           var list= await _detalleTipoNovedadRepository.GetAllAsync();//AllIncludingAsync(c=>c.Medidor);//GetAllAsync();
            var modelView = _mapper.Map<IEnumerable<DetalleTipoNovedad>, IEnumerable<Detalle_TipoNovedadViewModel>>(list);
            return Ok(modelView);
        }


        [HttpPut]
        [Route("PutDetalleTipoNovedad/{Id}")] 
        public async Task<ActionResult> PutDetalleTipoNovedad(long id, [FromBody]Detalle_TipoNovedadViewModel viewModel)
        {
            var modeledit= await _detalleTipoNovedadRepository.GetSingleAsync(m=>m.Id==id);
            var model = _mapper.Map<Detalle_TipoNovedadViewModel, DetalleTipoNovedad>(viewModel);
            if(modeledit!=null){
                modeledit.Nombre=model.Nombre;
                modeledit.Descripcion= model.Descripcion;
                modeledit.Tipo_Novedad_id= model.Tipo_Novedad_id;
                await this._detalleTipoNovedadRepository.EditAsync(modeledit);
            }
            return Ok(viewModel);

        }

        [HttpDelete]
        [Route("DeleteDetalleTipoNovedad/{Id}")] 
        public async Task<ActionResult> DeleteDetalleTipoNovedad(long id)
        {
          var deleteEntity= await _detalleTipoNovedadRepository.GetSingleAsync(id);
          if(deleteEntity!=null){
                 this._detalleTipoNovedadRepository.Delete(deleteEntity);
                 await _detalleTipoNovedadRepository.Commit();

          }
         var viewModel = _mapper.Map<DetalleTipoNovedad, Detalle_TipoNovedadViewModel>(deleteEntity);
         return Ok(viewModel);
           
        }
        #endregion
    }
}