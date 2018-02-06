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
    public class DispositivoController:Controller
    {
         #region Atributes
        private readonly IDispositivoRepository _dispositivoRepository;
        private readonly IMapper _mapper;

        #endregion
        
        #region Constructor
        public DispositivoController(IDispositivoRepository dispositivoRepository, IMapper mapper){
            _dispositivoRepository=dispositivoRepository;
            _mapper= mapper;
        }
        #endregion

        
        #region Methods

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]DispositivoViewModel viewModel)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
             var model = _mapper.Map<DispositivoViewModel, Dispositivo>(viewModel);
             await this._dispositivoRepository.AddAsync(model);
             await _dispositivoRepository.Commit();
             return Ok(model);
        }


        //[Produces(typeof(Lectura))] 
         //  [HttpGet]
        //[Route("GetVehicleByUser/{cedula}")]      
        [HttpGet]
        [SwaggerResponse(200, typeof(DispositivoViewModel))]
        public async Task<ActionResult> Get()
        {
                //https://stackoverflow.com/questions/40275195/how-to-setup-automapper-in-asp-net-core
            var list= await _dispositivoRepository.GetAllAsync();//AllIncludingAsync(c=>c.Medidor);//GetAllAsync();
            var modelView = _mapper.Map<IEnumerable<Dispositivo>, IEnumerable<DispositivoViewModel>>(list);
            return Ok(modelView);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Put(long id, [FromBody]DispositivoViewModel viewModel)
        {
            var modeledit= await _dispositivoRepository.GetSingleAsync(m=>m.Id==id);
            
             var model = _mapper.Map<DispositivoViewModel, Dispositivo>(viewModel);
        
            if(modeledit!=null){
            
                modeledit.Imei=model.Imei;
                modeledit.Phone_Type_Device=model.Phone_Type_Device;
                modeledit.Android_Id=model.Android_Id;
                modeledit.Software_Version=model.Software_Version;
                modeledit.Local_Ip_Address=model.Local_Ip_Address;
                modeledit.Android_Version=model.Android_Version;
                modeledit.MacAddr=model.MacAddr;
                modeledit.Device_Name=model.Device_Name;
                modeledit.Direccion_Ip=model.Direccion_Ip;
                modeledit.Estado=model.Estado;
                modeledit.UsuarioId=model.UsuarioId;

                await this._dispositivoRepository.EditAsync(modeledit);

            }

            return Ok(viewModel);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
          var deleteEntity= await _dispositivoRepository.GetSingleAsync(id);
          if(deleteEntity!=null){
                 this._dispositivoRepository.Delete(deleteEntity);
                 await _dispositivoRepository.Commit();

          }

         var viewModel = _mapper.Map<Dispositivo, DispositivoViewModel>(deleteEntity);
         return Ok(viewModel);
           
        }
        #endregion
        
    }
}