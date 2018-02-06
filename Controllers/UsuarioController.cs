using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Electro.model.datatakemodel;
using Electro.model.Repository;
using electrocore.ViewModels;
using Electrocore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace electrocore.Controllers
{
     [Route("api/[controller]")]
    public class UsuarioController:Controller
    {
        #region Atributes
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IProyectoUsuarioRepository _proyectoUsuarioRepository;
        private readonly IEmpresaRepository _empresaRepository;
        private readonly ITipoUsuarioRepository _tipoUsuarioRepository;

        private readonly IDispositivoRepository _dispositivoRepository;
        private readonly IMapper _mapper;

        #endregion
        
        #region Constructor
        public UsuarioController(IUsuarioRepository usuarioRepository,
        IProyectoUsuarioRepository proyectoUsuarioRepository, 
        IEmpresaRepository empresaRepository,
        ITipoUsuarioRepository tipoUsuarioRepository,
        IDispositivoRepository dispositivoRepository,
        IMapper mapper){

            _usuarioRepository=usuarioRepository;
            _proyectoUsuarioRepository= proyectoUsuarioRepository;
            _empresaRepository= empresaRepository;
            _tipoUsuarioRepository= tipoUsuarioRepository;
            _dispositivoRepository=dispositivoRepository;
            _mapper= mapper;
        }
        #endregion

        #region Methods

        //METHODS ADVANCE USUARIO
        /*------------------------------------------------------------------------------------------------*/
        [HttpGet]
        [Route("GetUsuariosByEmpresa/{Empresa_Id}")]   
        public async Task<ActionResult> GetUsuariosByEmpresa(long Empresa_Id)
        {
            var listModel=  await this._usuarioRepository.AllIncludingAsyncWhere(a=>a.Empresa_Id==Empresa_Id);
            var listViewModel = _mapper.Map<IEnumerable<Usuario>, IEnumerable<UsuarioViewModel>>(listModel);
            return Ok(listViewModel);
        }


        [HttpGet]
        [Route("GetLoginUser/{Cedula}/{Password}")]   
        public async Task<ActionResult> GetLoginUser(string Cedula, string Password)
        {
            var response= new ResponseViewModel();

            var user=  await this._usuarioRepository.GetSingleAsync(a=>a.Cedula==Cedula && a.Passsword==Password, b=>b.Empresa, c=>c.Tipo_Usuario);
            if(user== null){
                response.IsSuccess=false;
                response.Message="Identificacion o contraseña incorrectos";
                return Ok(response);
            }

            var verificateUserProyects= await _proyectoUsuarioRepository.AllIncludingAsyncWhere(a=>a.Usuario_Id==user.Id, b=>b.proyecto);
            if(verificateUserProyects.ToList().Count==0){
                response.IsSuccess=false;
                response.Message="El usuario no se encuentra asignado a ningun proyecto";
                return Ok(response);
            }

            var responseLoginViewModel= _mapper.Map<Usuario,ResponseLoginViewModel>(user);
            var listProyects= ToListProyects(verificateUserProyects);
            responseLoginViewModel.Proyectos=listProyects;

            response.Result=responseLoginViewModel;
            response.IsSuccess=true;
            response.Message="Login Ok";
            return Ok(response);
        }

        [HttpPost]
        [Route("PostLoginUser")]   
        public async Task<ActionResult> PostLoginUser([FromBody]RequestLoginViewModel viewModel)
        {
            var response= new ResponseViewModel();
            if(!ModelState.IsValid){
                response.IsSuccess=false;
                response.Message="Peticion Invalida";
                return Ok(response);
            }

            var user=  await this._usuarioRepository.GetSingleAsync(a=>a.Cedula==viewModel.Cedula && a.Passsword==viewModel.Passsword, b=>b.Empresa, c=>c.Tipo_Usuario);
            if(user== null){
                response.IsSuccess=false;
                response.Message="Identificacion o contraseña incorrectos";
                return Ok(response);
            }

            var verificateUserProyects= await _proyectoUsuarioRepository.AllIncludingAsyncWhere(a=>a.Usuario_Id==user.Id, b=>b.proyecto);
            if(verificateUserProyects.ToList().Count==0){
                response.IsSuccess=false;
                response.Message="El usuario no se encuentra asignado a ningun proyecto";
                return Ok(response);
            }

            var dispositivoEdit=await _dispositivoRepository.GetSingleAsync(a=>a.Imei==viewModel.Imei);
            var modelDispositivo =  _mapper.Map<RequestLoginViewModel, Dispositivo>(viewModel);
        
            if(dispositivoEdit!=null){
                dispositivoEdit.Imei=modelDispositivo.Imei;
                dispositivoEdit.Android_Id=modelDispositivo.Android_Id;
                dispositivoEdit.Software_Version=modelDispositivo.Software_Version;
                dispositivoEdit.Local_Ip_Address=modelDispositivo.Local_Ip_Address;
                dispositivoEdit.Android_Version=modelDispositivo.Android_Version;
                dispositivoEdit.MacAddr=modelDispositivo.MacAddr;
                dispositivoEdit.Device_Name=modelDispositivo.Device_Name;
                dispositivoEdit.Direccion_Ip=modelDispositivo.Direccion_Ip;
                dispositivoEdit.Estado=false;
                await this._dispositivoRepository.EditAsync(dispositivoEdit);
                modelDispositivo= dispositivoEdit;
            }else{
                modelDispositivo.Estado=false;
                await this._dispositivoRepository.AddAsync(modelDispositivo);
                await _dispositivoRepository.Commit();
            }

        
            modelDispositivo.Estado=true;
            modelDispositivo.UsuarioId=user.Id;
            await this._dispositivoRepository.EditAsync(modelDispositivo);


            var viewModelDispositivo =  _mapper.Map<Dispositivo, DispositivoViewModel>(modelDispositivo);
            var responseLoginViewModel= _mapper.Map<Usuario,ResponseLoginViewModel>(user);
            var listProyects= ToListProyects(verificateUserProyects);


            responseLoginViewModel.Proyectos=listProyects;
            responseLoginViewModel.Dispositivo=viewModelDispositivo;
            responseLoginViewModel.Device_Available=modelDispositivo.Estado;

            response.Result=responseLoginViewModel;
            response.IsSuccess=true;
            response.Message="Login Ok";

            return Ok(response);
        }

        private List<ProyectoViewModel> ToListProyects(IEnumerable<ProyectoUsuario> listProyects)
        {
            var listViewModelProyects= new List<ProyectoViewModel>();
            foreach (var item in listProyects)
            {
                listViewModelProyects.Add(new ProyectoViewModel{
                    Id= item.Proyecto_Id,
                    Nombre= item.proyecto.Nombre,
                    Descripcion= item.proyecto.Descripcion,
                    FechaInicio= item.proyecto.FechaInicio,
                    FechaFin= item.proyecto.FechaFin,
                    OrdenTrabajo= item.proyecto.OrdenTrabajo,
                    IsActivo=item.proyecto.IsActivo,
                    Ciudad_Id= item.proyecto.Ciudad_Id,
                    Proyecto_Id= item.Proyecto_Id
                });
            }

            return listViewModelProyects;
        }

        //METHODS CRUD BASIC USUARIO
        /*------------------------------------------------------------------------------------------------*/
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]UsuarioViewModel viewModel)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var response= new ResponseViewModel();

            var empresaExist= await _empresaRepository.GetSingleAsync(a=>a.Id==viewModel.Empresa_Id);
            if(empresaExist==null){
                response.IsSuccess=false;
                response.Message="La empresa no existe";
                return Ok(response);
            }


             var tipoUsuarioExist= await _tipoUsuarioRepository.GetSingleAsync(a=>a.Id==viewModel.Tipo_Usuario_Id);
            if(tipoUsuarioExist==null){
                response.IsSuccess=false;
                response.Message="Datos Invalidaos, Tipo de usuario no existe";
                return Ok(response);
            }

             var model = _mapper.Map<UsuarioViewModel, Usuario>(viewModel);
             await this._usuarioRepository.AddAsync(model);
             await _usuarioRepository.Commit();

             response.IsSuccess=true;
             response.Message="ok";
             response.Result=model;

             return Ok(model);
        }


        //[Produces(typeof(Lectura))] 
         //  [HttpGet]
        //[Route("GetVehicleByUser/{cedula}")]      
        [HttpGet]
        [SwaggerResponse(200, typeof(UsuarioViewModel))]
        public async Task<ActionResult> Get()
        {
                //https://stackoverflow.com/questions/40275195/how-to-setup-automapper-in-asp-net-core
           var list= await _usuarioRepository.GetAllAsync();//AllIncludingAsync(c=>c.Medidor);//GetAllAsync();
            var modelView = _mapper.Map<IEnumerable<Usuario>, IEnumerable<UsuarioViewModel>>(list);
            return Ok(modelView);
        }


        [HttpPut("{id}")]
        public async Task<UsuarioViewModel> Put(long id, [FromBody]UsuarioViewModel viewModel)
        {
            var modeledit= await _usuarioRepository.GetSingleAsync(m=>m.Id==id);
            
             var model = _mapper.Map<UsuarioViewModel, Usuario>(viewModel);
        
            if(modeledit!=null){
            
             
                modeledit.Nombre=model.Nombre;
                modeledit.Apellido=model.Apellido;
                modeledit.Cedula=model.Cedula;
                modeledit.Telefono=model.Telefono;
                modeledit.Direccion=model.Direccion;
                modeledit.CorreoElectronico=model.CorreoElectronico;
                modeledit.Passsword=model.Passsword;
                modeledit.Tipo_Usuario_Id=model.Tipo_Usuario_Id;
                modeledit.Empresa_Id=model.Empresa_Id;
                

                //modeledit=model;

                await this._usuarioRepository.EditAsync(modeledit);
            }

            return viewModel;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
          var deleteEntity= await _usuarioRepository.GetSingleAsync(id);
          if(deleteEntity!=null){
                 var deleteUsuarioProyecto=await _proyectoUsuarioRepository.GetSingleAsync(a=>a.Usuario_Id==deleteEntity.Id);
                  this._proyectoUsuarioRepository.Delete(deleteUsuarioProyecto);
                 await _proyectoUsuarioRepository.Commit();


                 this._usuarioRepository.Delete(deleteEntity);
                 await _usuarioRepository.Commit();
          }

         var viewModel = _mapper.Map<Usuario, UsuarioViewModel>(deleteEntity);
         return Ok(viewModel);
           
        }
        #endregion
    }
}