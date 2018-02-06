using System;
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
    public class ProyectoController:Controller
    {

        #region Atributes
        private readonly IProyectoRepository _proyectoRepository;
        private readonly IProyectoEmpresaRepository _proyectoEmpresaRepository;
        private readonly IProyectoUsuarioRepository _proyectoUsuarioRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        #endregion
        
        #region Constructor
        public ProyectoController(IProyectoRepository proyectoRepository,
        IProyectoEmpresaRepository proyectoEmpresaRepository,
        IProyectoUsuarioRepository proyectoUsuarioRepository,
         IMapper mapper,
         IUsuarioRepository usuarioRepository){
            _proyectoRepository=proyectoRepository;
            _proyectoEmpresaRepository=proyectoEmpresaRepository;
            _proyectoUsuarioRepository=proyectoUsuarioRepository;
            _usuarioRepository= usuarioRepository;
            _mapper= mapper;
        }
        #endregion

         #region Methods


        //METHODS ADVANCE EMPRESA
        /*------------------------------------------------------------------------------------------------*/

        //Agregar usuarios al proyectos
        [HttpPost]
        [Route("PostAddUsuarioToProyecto")]  
        public async Task<ActionResult> PostAddUsuarioProyecto([FromBody]ProyectoUsuarioViewModel  viewModel)
        {
            if(!ModelState.IsValid){

                return BadRequest(ModelState);
            }

             var response = new ResponseViewModel();
             var verficateProyectoExist=await _proyectoRepository.GetSingleAsync(a=>a.Id==viewModel.Proyecto_Id);
             if(verficateProyectoExist==null){
                 response.IsSuccess=false;
                 response.Message="Datos Invalidos, el proyecto no existe";
                 return Ok(response);                
             }

             var vericateProyectoUsuarioExist= await _proyectoUsuarioRepository.GetSingleAsync(a=>a.Usuario_Id==viewModel.Usuario_Id && a.Proyecto_Id==viewModel.Proyecto_Id);
             if(vericateProyectoUsuarioExist!=null){
                 response.IsSuccess=false;
                 response.Message="Ya se encuentra asignado el usuario al proyecto"; 
                 return Ok(response);                
             }
            
             var proyectoUsuario = _mapper.Map<ProyectoUsuarioViewModel, ProyectoUsuario>(viewModel);
             await this._proyectoUsuarioRepository.AddAsync(proyectoUsuario);
             await _proyectoUsuarioRepository.Commit();

             var proyectoUsuarioNew = _mapper.Map<ProyectoUsuario, ProyectoUsuarioViewModel>(proyectoUsuario);


             response.IsSuccess=true;
             response.Message="Ok";
             response.Result=proyectoUsuarioNew;
             return Ok(response);
        }

        //Agregar empresas a un proyecto
        [HttpPost]
        [Route("PostAddEmpresaToProyecto")]  
        public async Task<ActionResult> PostAddEmpresaByProyecto([FromBody]ProyectoViewModel viewModel)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

             var response = new ResponseViewModel();

             var verficateProyectoExist=await _proyectoRepository.GetSingleAsync(a=>a.Id==viewModel.Proyecto_Id);
             if(verficateProyectoExist==null){
                 response.IsSuccess=false;
                 response.Message="Datos Invalidos, el proyecto no existe";
                 return Ok(response);                
             }

             var vericateProyectoEmpresaExist= await _proyectoEmpresaRepository.GetSingleAsync(a=>a.Empresa_Id==viewModel.Empresa_Id && a.Proyecto_Id==viewModel.Proyecto_Id);
             if(vericateProyectoEmpresaExist!=null){
                 response.IsSuccess=false;
                 response.Message="Ya se encuentra asignada la empresa al proyecto";
                 return Ok(response);                
             }

             var proyectoEmpresa = _mapper.Map<ProyectoViewModel, Proyecto_Empresa>(viewModel);
             await this._proyectoEmpresaRepository.AddAsync(proyectoEmpresa);
             await _proyectoEmpresaRepository.Commit();

             response.IsSuccess=true;
             response.Message="Ok";
             response.Result=viewModel;
             return Ok(response);

        }
        
        [HttpGet]
        [Route("GetProyectosWithUsuarios")]   
        public async Task<ActionResult> GetProyectosWithUsuarios()
        {

            var proyectos= await _proyectoRepository.AllIncludingAsync(b=>b.Ciudad, c=>c.ProyectoUsuarios);

            var listProyectosViewModel= _mapper.Map<IEnumerable<Proyecto>, IEnumerable<ResponseProyectosUsuarioViewModel>>(proyectos);

            var listProyects= await ToListProyectsWithUsers(listProyectosViewModel);
            
            return Ok(listProyects);
        }

        private async Task<List<ResponseProyectosUsuarioViewModel>> ToListProyectsWithUsers(IEnumerable<ResponseProyectosUsuarioViewModel> listProyectosViewModel)
        {
            var listProyects= new List<ResponseProyectosUsuarioViewModel>();
            foreach (var item in listProyectosViewModel)
            {
                listProyects.Add(new ResponseProyectosUsuarioViewModel{
                    Id=item.Id,
                    Nombre=item.Nombre,
                    Descripcion= item.Descripcion,
                    FechaInicio= item.FechaInicio,
                    FechaFin= item.FechaFin,
                    OrdenTrabajo= item.OrdenTrabajo,
                    IsActivo= item.IsActivo,
                    Ciudad_Id= item.Ciudad_Id,
                    Ciudad= item.Ciudad,
                    Usuarios= await ToViewUsuarios(item.ProyectoUsuarios)
                });
                
            }
            return listProyects;
        }
        private async Task<List<Usuario>> ToViewUsuarios(List<ProyectoUsuario> proyectoUsuarios)
        {
            var usuarios= new List<Usuario>();
            foreach (var item in proyectoUsuarios)
            {
                var usuario= await _usuarioRepository.GetSingleAsync(a=>a.Id==item.Usuario_Id);
                usuarios.Add(new Usuario{
                    Id= usuario.Id,
                    Nombre= usuario.Nombre,
                    Apellido= usuario.Apellido,
                    Cedula= usuario.Cedula,
                    CorreoElectronico= usuario.CorreoElectronico,
                    Passsword= usuario.Passsword
                });
            }
            return usuarios;
        }

        //METHODS CRUD BASIC EMPRESA
        /*------------------------------------------------------------------------------------------------*/

        //Registrar un proyecto y asignarlo a una empresa
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]ProyectoViewModel viewModel)
        {
            if(!ModelState.IsValid){

                return BadRequest(ModelState);
            }
             
            var response = new ResponseViewModel();
            var vericateProyectoEmpresaExist= await _proyectoEmpresaRepository.GetSingleAsync(a=>a.Empresa_Id==viewModel.Empresa_Id && a.Proyecto.OrdenTrabajo==viewModel.OrdenTrabajo, b=>b.Proyecto);
             if(vericateProyectoEmpresaExist!=null){
                 response.IsSuccess=false;
                 response.Message=string.Format("Ya se encuentra registrado un proyecto con orden de trabajo {0} y asigando a la misma empresa",viewModel.OrdenTrabajo);
                 return Ok(response);                
             }

             var proyecto = _mapper.Map<ProyectoViewModel, Proyecto>(viewModel);
             await this._proyectoRepository.AddAsync(proyecto);
             await _proyectoRepository.Commit();
             long Proyecto_Id= proyecto.Id;
             viewModel.Proyecto_Id=Proyecto_Id;

             var proyectoEmpresa = _mapper.Map<ProyectoViewModel, Proyecto_Empresa>(viewModel);
             await this._proyectoEmpresaRepository.AddAsync(proyectoEmpresa);
             await _proyectoEmpresaRepository.Commit();
             viewModel.Proyecto_Empresa_Id=proyectoEmpresa.Id;
             
            response.IsSuccess=true;
            response.Message="Ok";
            response.Result=viewModel;  
            return Ok(response);
        }

        //[Produces(typeof(Lectura))] 
         //  [HttpGet]
        //[Route("GetVehicleByUser/{cedula}")]      
        /*[HttpGet]
        [Route("GetProyectoEmpresa")]  
        [SwaggerResponse(200, typeof(ProyectoViewModel))]
        public async Task<ActionResult> GetProyectoEmpresa()
        {
           //https://stackoverflow.com/questions/40275195/how-to-setup-automapper-in-asp-net-core
           var list= await _proyectoEmpresaRepository.GetAllAsync();//AllIncludingAsync(c=>c.Medidor);//GetAllAsync();
            return Ok(list);
        }*/

        [HttpGet]
        [SwaggerResponse(200, typeof(ProyectoViewModel))]
        public async Task<ActionResult> Get()
        {
                //https://stackoverflow.com/questions/40275195/how-to-setup-automapper-in-asp-net-core
           var list= await _proyectoRepository.GetAllAsync();//AllIncludingAsync(c=>c.Medidor);//GetAllAsync();
            var modelView = _mapper.Map<IEnumerable<Proyecto>, IEnumerable<ProyectoViewModel>>(list);
            return Ok(modelView);
        }

        [HttpPut("{id}")]
        public async Task<ProyectoViewModel> Put(long id, [FromBody]ProyectoViewModel viewModel)
        {
            var modeledit= await _proyectoRepository.GetSingleAsync(m=>m.Id==id);
            
             var model = _mapper.Map<ProyectoViewModel, Proyecto>(viewModel);
        
            if(modeledit!=null){
            
                modeledit.Nombre=model.Nombre;
                modeledit.Descripcion=model.Descripcion;
                modeledit.FechaInicio=model.FechaInicio;
                modeledit.FechaFin=model.FechaFin;
                modeledit.OrdenTrabajo=model.OrdenTrabajo;
                modeledit.IsActivo=model.IsActivo;
                modeledit.Ciudad_Id=model.Ciudad_Id;

                await this._proyectoRepository.EditAsync(modeledit);
            }

            return viewModel;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
          var deleteEntity= await _proyectoRepository.GetSingleAsync(id);
          if(deleteEntity!=null){

                 //Eliminar de Entidad Proyecto Empresa
                 var deleteProyectoEmpresa= await _proyectoEmpresaRepository.GetSingleAsync(a=>a.Proyecto_Id==deleteEntity.Id);
                 if(deleteProyectoEmpresa!=null){
                    this._proyectoEmpresaRepository.Delete(deleteProyectoEmpresa);
                    await _proyectoEmpresaRepository.Commit();
                 }

                 //Eliminar de Proyecto
                 this._proyectoRepository.Delete(deleteEntity);
                 await _proyectoRepository.Commit();
          }
         var viewModel = _mapper.Map<Proyecto, ProyectoViewModel>(deleteEntity);
         return Ok(viewModel);

        }
        #endregion
    }
}