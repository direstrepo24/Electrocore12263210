using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Electro.model.datatakemodel;
using Electro.model.Models.datatakemodel;
using Electro.model.Repository;
using electrocore.ViewModels;
//using Electrocore.Models.datatakemodel;
//using Electrocore.Repository;
using Electrocore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Electrocore.Controllers
{
   
    [Route("api/[controller]")]
    public class EmpresaController:Controller
    {
        #region Atributes
        private readonly IEmpresaRepository _IEmpresaRespository;
        private readonly IProyectoRepository _proyectoRepository;
        private readonly IProyectoEmpresaRepository _proyectoEmpresaRepository;
        private readonly ICiudad_EmpresaRepository _ciudad_EmpresaRepository;

        private readonly ICiudadRepository _ciudadRepository;
        

        private readonly IMapper _mapper;

        #endregion

        #region Constructor 
        public EmpresaController(IMapper mapper,  
        IEmpresaRepository IEmpresaRespository,
        IProyectoRepository proyectoRepository,
        IProyectoEmpresaRepository proyectoEmpresaRepository,
        ICiudad_EmpresaRepository ciudad_EmpresaRepository,
         ICiudadRepository ciudadRepository)
        {
            //Mapper
            _mapper = mapper;
            //Inyecciones
            _IEmpresaRespository=IEmpresaRespository;
            _proyectoRepository=proyectoRepository;
            _proyectoEmpresaRepository=proyectoEmpresaRepository;
            _ciudad_EmpresaRepository= ciudad_EmpresaRepository;
            _ciudadRepository= ciudadRepository;

         }

         #endregion


        #region Methods


         //METHODS ADVANCE EMPRESA
        /*------------------------------------------------------------------------------------------------*/
        [HttpPost]
        [Route("PostAddEmpresaToCiudad")]   
        public async Task<ActionResult> PostCiudadEmpresa([FromBody]Ciudad_EmpresaViewModel viewModel)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            
            var response= new ResponseViewModel();
            var verifcateExistCiudad= await _ciudadRepository.GetSingleAsync(a=>a.Id==viewModel.Ciudad_Id);
            if(verifcateExistCiudad==null){
                response.Message=string.Format("La ciudad con Id {0}, no existe", viewModel.Ciudad_Id);
                response.IsSuccess=false;
                 return Ok(response);
            }

            var verifcateExistEmpresa= await _IEmpresaRespository.GetSingleAsync(a=>a.Id==viewModel.Empresa_Id);
            if(verifcateExistEmpresa==null){
                response.Message=string.Format("La empresa con Id {0}, no existe", viewModel.Empresa_Id);
                response.IsSuccess=false;
                 return Ok(response);
            }

            var verifcateExist= await _ciudad_EmpresaRepository.GetSingleAsync(a=>a.Empresa_Id==viewModel.Empresa_Id && a.Ciudad_Id==viewModel.Ciudad_Id);
            if(verifcateExist!=null){
                response.Message=string.Format("Ya existe empresa asignada a la ciudad id {0}, con id empresa {1}", viewModel.Ciudad_Id,viewModel.Empresa_Id);
                response.IsSuccess=false;
                 return Ok(response);
            }
            
             var model = _mapper.Map<Ciudad_EmpresaViewModel, Ciudad_Empresa>(viewModel);
        
             await this._ciudad_EmpresaRepository.AddAsync(model);
             await _ciudad_EmpresaRepository.Commit();

            //Response
            long Ciudad_Empresa_Id= model.Id;
            var modelRegister=await _ciudad_EmpresaRepository.GetSingleAsync(a=>a.Id==Ciudad_Empresa_Id, b=>b.Ciudad,c=>c.Empresa);
            var CiudadEmpresaViewModel = _mapper.Map<Ciudad_Empresa, ResponseCiudadEmpresaViewModel>(modelRegister);

            response.Message="Ok";
            response.IsSuccess=true;
            response.Result=CiudadEmpresaViewModel;
            return Ok(response);
        }



        [HttpGet]
        [Route("GetEmpresaByCiudad/{Ciudad_Id}")]   
        public async Task<ActionResult> GetEmpresaByCiudad(long Ciudad_Id)
        {
            var listEmpresas= await _ciudad_EmpresaRepository.AllIncludingAsyncWhere(a=>a.Ciudad_Id==Ciudad_Id, b=>b.Empresa,c=>c.Ciudad);
            var viewModel = _mapper.Map<IEnumerable<Ciudad_Empresa>, IEnumerable<ResponseCiudadEmpresaViewModel>>(listEmpresas);
             return Ok(viewModel);
        }

        [HttpGet]
        [Route("GetEmpresaByDepartamento/{Departamento_Id}")]   
        public async Task<ActionResult> GetEmpresaByDepartamento(long Departamento_Id)
        {
            var listEmpresas= await _ciudad_EmpresaRepository.AllIncludingAsyncWhere(a=>a.Ciudad.departmentoId==Departamento_Id, b=>b.Empresa,c=>c.Ciudad);
            var viewModel = _mapper.Map<IEnumerable<Ciudad_Empresa>, IEnumerable<ResponseCiudadEmpresaViewModel>>(listEmpresas);
             return Ok(viewModel);
        }

        [HttpGet]
        [Route("GetEmpresaCiudades")]   
        public async Task<ActionResult> GetEmpresaCiudades()
        {
            var listEmpresas= await _ciudad_EmpresaRepository.AllIncludingAsync( b=>b.Empresa,c=>c.Ciudad);
            var viewModel = _mapper.Map<IEnumerable<Ciudad_Empresa>, IEnumerable<ResponseCiudadEmpresaViewModel>>(listEmpresas);
            
            var list= ToListCiudadEmpresa(viewModel);
            return Ok(list);
        }

        private List<ResponseListCiudadEmpresaViewModel> ToListCiudadEmpresa(IEnumerable<ResponseCiudadEmpresaViewModel> viewModelList)
        {
            var list=new List<ResponseListCiudadEmpresaViewModel>();  
            foreach (var item in viewModelList)
            {
                list.Add(new ResponseListCiudadEmpresaViewModel{
                    Ciudad_Empresa_Id=item.Id,
                    Ciudad_Id= item.Ciudad_Id,
                    Nombre_Ciudad= item.Ciudad.Nombre,
                    Descripcion_Empresa=item.Empresa.Descripcion,
                    Empresa_Id= item.Empresa_Id,
                    Nombre_Empresa= item.Empresa.Nombre,
                    Direccion= item.Empresa.Direccion,
                    Telefono= item.Empresa.Telefono,
                    Nit= item.Empresa.Nit,
                    Is_Operadora= item.Empresa.Is_Operadora,
                    
                });
            }
           return list;
        }




        //METHODS CRUD BASIC EMPRESA
        /*------------------------------------------------------------------------------------------------*/
        /// <summary>Gets  elements contained in the Lectura.</summary>
        /// <returns>The elements contained in the Lectura.</returns>
        //[Produces(typeof(Lectura))] 
        //  [HttpGet]
        //[Route("GetVehicleByUser/{cedula}")]      
        [HttpGet]
        [SwaggerResponse(200, typeof(Empresa))]
        public async Task<ActionResult> Get()
        {
            //https://stackoverflow.com/questions/40275195/how-to-setup-automapper-in-asp-net-core
            var empresa= await _IEmpresaRespository.GetAllAsync();//AllIncludingAsync(c=>c.Medidor);//GetAllAsync();
            //var dispositivos= await _IDispositivoRespository.
            var model = _mapper.Map<IEnumerable<Empresa>, IEnumerable<EmpresaViewModel>>(empresa);
            return Ok(model);
        }

        
        [HttpGet("{id}")]
        public async Task<EmpresaViewModel> Get(int id)
        {
          var empresa=  await this._IEmpresaRespository.GetSingleAsync(id);
              var model = _mapper.Map<Empresa, EmpresaViewModel>(empresa);
       
            return model;//.GetProduct(id);
        }
        //[Produces("application/json", Type=typeof(value))]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]EmpresaViewModel viewModel)
        {
            if(!ModelState.IsValid){

                return BadRequest(ModelState);
            }


            var response= new ResponseViewModel();
            var verifcateExist= await _IEmpresaRespository.GetSingleAsync(a=>a.Nit.ToUpper().Contains(viewModel.Nit));
            if(verifcateExist!=null){
                response.Message=string.Format("Ya existe empresa registrada, con nit {0}", viewModel.Nit);
                response.IsSuccess=false;
                 return Ok(response);
            }
            

             var model = _mapper.Map<EmpresaViewModel, Empresa>(viewModel);
        
             await this._IEmpresaRespository.AddAsync(model);
             await _IEmpresaRespository.Commit();

             response.Message="Ok";
             response.IsSuccess=true;
             response.Result=model;
             return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<EmpresaViewModel> Put(long id, [FromBody]EmpresaViewModel empresa)
        {
            var modeledit= await _IEmpresaRespository.GetSingleAsync(m=>m.Id==id);
            
            var model = _mapper.Map<EmpresaViewModel, Empresa>(empresa);
            if(modeledit!=null){
             
                modeledit.Nit=empresa.Nit;
                modeledit.Nombre=empresa.Nombre;
                 modeledit.Descripcion=empresa.Descripcion;
                modeledit.Telefono=empresa.Telefono;
                modeledit.Direccion=empresa.Direccion;
                modeledit.Is_Operadora=empresa.Is_Operadora;

               // lecturaedit.Id=id;
                await this._IEmpresaRespository.EditAsync(modeledit);
                }
            return empresa;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
          var deleteEntity= await _IEmpresaRespository.GetSingleAsync(id);
          if(deleteEntity!=null){
                 this._IEmpresaRespository.Delete(deleteEntity);
                 await _IEmpresaRespository.Commit();

          }
         return Ok(deleteEntity);
           
        }
        #endregion

    }
    
    
}