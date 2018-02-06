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
    public class TipoUsuarioController:Controller
    {
        #region Atributes
        private readonly ITipoUsuarioRepository _tipoUsuarioRepository;
        private readonly IMapper _mapper;

        #endregion
        
        #region Constructor
        public TipoUsuarioController(ITipoUsuarioRepository tipoUsuarioRepository, IMapper mapper){
            _tipoUsuarioRepository=tipoUsuarioRepository;
            _mapper= mapper;
        }
        #endregion

        
        #region Methods

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]TipoUsuarioViewModel viewModel)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
             var model = _mapper.Map<TipoUsuarioViewModel, Tipo_Usuario>(viewModel);
             await this._tipoUsuarioRepository.AddAsync(model);
             await _tipoUsuarioRepository.Commit();
             return Ok(model);
        }


        //[Produces(typeof(Lectura))] 
         //  [HttpGet]
        //[Route("GetVehicleByUser/{cedula}")]      
        [HttpGet]
        [SwaggerResponse(200, typeof(TipoUsuarioViewModel))]
        public async Task<ActionResult> Get()
        {
                //https://stackoverflow.com/questions/40275195/how-to-setup-automapper-in-asp-net-core
           var list= await _tipoUsuarioRepository.GetAllAsync();//AllIncludingAsync(c=>c.Medidor);//GetAllAsync();
            var modelView = _mapper.Map<IEnumerable<Tipo_Usuario>, IEnumerable<TipoUsuarioViewModel>>(list);
            return Ok(modelView);
        }


        [HttpPut("{id}")]
        public async Task<TipoUsuarioViewModel> Put(long id, [FromBody]TipoUsuarioViewModel viewModel)
        {
            var modeledit= await _tipoUsuarioRepository.GetSingleAsync(m=>m.Id==id);
            
             var model = _mapper.Map<TipoUsuarioViewModel, Tipo_Usuario>(viewModel);
        
            if(modeledit!=null){
            
                modeledit.Nombre=model.Nombre;
                await this._tipoUsuarioRepository.EditAsync(modeledit);
            }

            return viewModel;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
          var deleteEntity= await _tipoUsuarioRepository.GetSingleAsync(id);
          if(deleteEntity!=null){
                 this._tipoUsuarioRepository.Delete(deleteEntity);
                 await _tipoUsuarioRepository.Commit();

          }

         var viewModel = _mapper.Map<Tipo_Usuario, TipoUsuarioViewModel>(deleteEntity);
         return Ok(viewModel);
           
        }
        #endregion

    }
}