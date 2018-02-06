using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Electro.model.datatakemodel;
using Electro.model.Repository;
using electrocore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace electrocore.Controllers
{
    [Route("api/[controller]")]
    public class ElementoController:Controller
    {
        #region Atributes
        private readonly IElementoRepository _elementoRepository;
        private readonly IElementoCableRepository _elementoCableRepository;
        private readonly IEquipoElementoRepository _equipoElementoRepository;
        private readonly IPerdidaRepository _perdidaRepository;
        private readonly INovedadRepository _novedadRepository;
        private readonly IFotoRepository _fotoRepository;


        private readonly IMapper _mapper;

        #endregion

        #region Constructor
        public ElementoController(
            IElementoRepository elementoRepository, 
            IElementoCableRepository elementoCableRepository,
            IEquipoElementoRepository equipoElementoRepository,
            IPerdidaRepository perdidaRepository,
            INovedadRepository novedadRepository,
            IFotoRepository fotoRepository,
            IMapper mapper){


            _elementoRepository=elementoRepository;
            _elementoCableRepository= elementoCableRepository;
            _equipoElementoRepository= equipoElementoRepository;
            _perdidaRepository= perdidaRepository;
            _novedadRepository= novedadRepository;
            _fotoRepository= fotoRepository;
            _mapper= mapper;
        }
        #endregion


        #region Methods ADVANCE

        [HttpGet]
        [Route("GetCablesElemento")] 
        [SwaggerResponse(200, typeof(ElementoCableViewModel))]
        public async Task<ActionResult> GetCablesElemento()
        {
           //https://stackoverflow.com/questions/40275195/how-to-setup-automapper-in-asp-net-core
           var list= await _elementoCableRepository.GetAllAsync();//AllIncludingAsync(c=>c.Medidor);//GetAllAsync();
           ///var modelView = _mapper.Map<IEnumerable<DetalleTipoCable>, IEnumerable<DetalleTipoCableViewModel>>(list);
           return Ok(list);
        }


        [HttpGet]
        [Route("GetEquiposElemento")] 
        [SwaggerResponse(200, typeof(EquipoElementoViewModel))]
        public async Task<ActionResult> GetEquiposElemento()
        {
           //https://stackoverflow.com/questions/40275195/how-to-setup-automapper-in-asp-net-core
           var list= await _equipoElementoRepository.GetAllAsync();//AllIncludingAsync(c=>c.Medidor);//GetAllAsync();
           ///var modelView = _mapper.Map<IEnumerable<DetalleTipoCable>, IEnumerable<DetalleTipoCableViewModel>>(list);
           return Ok(list);
        }


        

        [HttpGet]
        [Route("GetPerdidasElemento")] 
        [SwaggerResponse(200, typeof(NovedadViewModel))]
        public async Task<ActionResult> GetPerdidasElemento()
        {
           //https://stackoverflow.com/questions/40275195/how-to-setup-automapper-in-asp-net-core
           var list= await _perdidaRepository.GetAllAsync();//AllIncludingAsync(c=>c.Medidor);//GetAllAsync();
           ///var modelView = _mapper.Map<IEnumerable<DetalleTipoCable>, IEnumerable<DetalleTipoCableViewModel>>(list);
           return Ok(list);
        }


        #endregion



        #region Methods

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]ElementoViewModel viewModel)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
             var model = _mapper.Map<ElementoViewModel, Elemento>(viewModel);
             await this._elementoRepository.AddAsync(model);
             await _elementoRepository.Commit();
             return Ok(model);
        }

        [HttpGet]
        [Route("GetNovedadesElemento")] 
        [SwaggerResponse(200, typeof(NovedadViewModel))]
        public async Task<ActionResult> GetNovedadesElemento()
        {
           //https://stackoverflow.com/questions/40275195/how-to-setup-automapper-in-asp-net-core
           var list= await _novedadRepository.GetAllAsync();//AllIncludingAsync(c=>c.Medidor);//GetAllAsync();
           ///var modelView = _mapper.Map<IEnumerable<DetalleTipoCable>, IEnumerable<DetalleTipoCableViewModel>>(list);
           return Ok(list);
        }

        [HttpGet]
        [Route("GetFotosElemento")] 
        [SwaggerResponse(200, typeof(FotoViewModel))]
        public async Task<ActionResult> GetFotosElemento()
        {
           //https://stackoverflow.com/questions/40275195/how-to-setup-automapper-in-asp-net-core
           var list= await _fotoRepository.GetAllAsync();//AllIncludingAsync(c=>c.Medidor);//GetAllAsync();
           ///var modelView = _mapper.Map<IEnumerable<DetalleTipoCable>, IEnumerable<DetalleTipoCableViewModel>>(list);
           return Ok(list);
        }



        //[Produces(typeof(Lectura))] 
         //  [HttpGet]
        //[Route("GetVehicleByUser/{cedula}")]      
        [HttpGet]
        [SwaggerResponse(200, typeof(ElementoViewModel))]
        public async Task<ActionResult> Get()
        {
                //https://stackoverflow.com/questions/40275195/how-to-setup-automapper-in-asp-net-core
           var list= await _elementoRepository.AllIncludingAsync(t=>t.LongitudElemento, a=>a.Estado,b=>b.NivelTensionElemento, p=>p.Proyecto,m=>m.Material, n=>n.LocalizacionElementos);//AllIncludingAsync(c=>c.Medidor);//GetAllAsync();
            var modelView = _mapper.Map<IEnumerable<Elemento>, IEnumerable<ElementoViewModel>>(list);
            return Ok(modelView);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Put(long id, [FromBody]ElementoViewModel viewModel)
        {
            var modeledit= await _elementoRepository.GetSingleAsync(m=>m.Id==id);
            
             var model = _mapper.Map<ElementoViewModel, Elemento>(viewModel);
        
            if(modeledit!=null){
            
                modeledit.CodigoApoyo=model.CodigoApoyo;
                modeledit.NumeroApoyo=model.NumeroApoyo;
                modeledit.FechaLevantamiento=model.FechaLevantamiento;
                modeledit.HoraInicio=model.HoraInicio;
                modeledit.HoraFin=model.HoraFin;
                modeledit.ResistenciaMecanica=model.ResistenciaMecanica;
                modeledit.Retenidas=model.Retenidas;
                modeledit.AlturaDisponible=model.AlturaDisponible;
                modeledit.Usuario_Id=model.Usuario_Id;
                modeledit.Estado_id=model.Estado_id;
                modeledit.Longitud_Elemento_Id=model.Longitud_Elemento_Id;
                modeledit.Material_Id=model.Material_Id;
                modeledit.Proyecto_Id=model.Proyecto_Id;
                modeledit.Nivel_Tension_Id=model.Nivel_Tension_Id;
                modeledit.Ciudad_Id=model.Ciudad_Id;

                await this._elementoRepository.EditAsync(modeledit);



            }

            return Ok(viewModel);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
          var deleteEntity= await _elementoRepository.GetSingleAsync(id);
          if(deleteEntity!=null){
                 this._elementoRepository.Delete(deleteEntity);
                 await _elementoRepository.Commit();

          }

         var viewModel = _mapper.Map<Elemento, ElementoViewModel>(deleteEntity);
         return Ok(viewModel);
           
        }
        #endregion

    }
}