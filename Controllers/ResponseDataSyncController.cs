using System.Threading.Tasks;
//using Electrocore.Repository;
using Electrocore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Electro.model.Repository;
using electrocore.ViewModels;
using System.Collections.Generic;
using Electro.model.datatakemodel;
using AutoMapper;
using System;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http.Internal;
using Swashbuckle.AspNetCore.SwaggerGen;
using Electro.model.Models.datatakemodel;
using System.Runtime.Serialization.Json;
using Amazon.S3;
using Amazon.S3.Model;
using System.Threading;
using System.Drawing;

namespace Electrocore.Controllers
{
    [Route("api/[controller]")]
    public class ResponseDataSyncController:Controller
    {
        //ResponseDataSyncModelView
        // private readonly IUserRepository _IuserRepository;

        #region Atributes


       // private  IAmazonS3 S3Client { get; set; }
      //  private static string _bucketName = "datatakefiles";//this is my Amazon Bucket name
      
      
         private readonly ITipoCableRepository _ITipoCableRepository;
         private readonly IDetalleTipoCableRepository _IDetalleTipoCableRepository;
         private readonly ITipoNovedadRepository _ITipoNovedadRepository;   
        private readonly IDetalleTipoNovedadRepository _IDetalleTipoNovedadRepository;
        private readonly ITipoEquipoRepository _ITipoEquipoRepository;  
        private readonly IEmpresaRepository _IEmpresaRepository;
        private readonly IEstadoRepository _IEstadoRepository;
        private readonly ILongitudElementoRepository _ILongitudElementoRepository;
        private readonly IMaterialRepository _IMaterialRepository;
        private readonly INivelTensionElementoRepository _INivelTensionElementoRepository;
        private readonly ICiudadRepository _ICiudadRepository;
        private readonly ITipo_PerdidaRepository _ITipo_PerdidaRepository;
        private readonly IDepartamentoRepository _IDepartamentoRepository;

        private readonly IDispositivoRepository _dispositivoRepository;

        //Rquest DataAsyn
        private readonly IElementoRepository _elementoRepository;
        private readonly IElementoCableRepository _elementoCableRepository;
        private readonly IEquipoElementoRepository _equipoElementoRepository;
        private readonly INovedadRepository _novedadRepository;
        private readonly IFotoRepository _fotoRepository;
        private readonly ILocalizacionElementoRepository _localizacionElementoRepository;
        private readonly IPerdidaRepository _perdidaRepository;

        private readonly ICiudad_EmpresaRepository _ciudad_EmpresaRepository;


        private readonly IMapper _mapper;
        IHostingEnvironment  _hostingEnvironment;

        #endregion

        #region Constructor
        public ResponseDataSyncController(ITipoCableRepository tipoCableRepository,
         IDetalleTipoCableRepository detalleTipoCableRepository, 
         ITipoNovedadRepository tipoNovedadRepository, 
         ITipoEquipoRepository tipoEquipoRepository, 
         IDetalleTipoNovedadRepository detalleTipoNovedadRepository, 
         IEmpresaRepository empresaRepository, 
         ICiudadRepository ciudadRepository, 
         IDepartamentoRepository departamentoRepository,
         IElementoRepository elementoRepository,
         IElementoCableRepository elementoCableRepository,
         IEquipoElementoRepository equipoElementoRepository,
         INovedadRepository novedadRepository,
         IFotoRepository fotoRepository,
         ILocalizacionElementoRepository localizacionElementoRepository,
         IPerdidaRepository perdidaRepository,
         IEstadoRepository estadoRepository,
         ILongitudElementoRepository longitudElementoRepository,
         IMaterialRepository materialRepository,
         INivelTensionElementoRepository nivelTensionElementoRepository,
         ITipo_PerdidaRepository tipo_PerdidaRepository,
         ICiudad_EmpresaRepository ciudad_EmpresaRepository,
        IDispositivoRepository dispositivoRepository,
        //IAmazonS3 s3Client,
         IMapper mapper,
         IHostingEnvironment hostingEnvironment)
        {
        
            _ITipoCableRepository=tipoCableRepository;
            _IDetalleTipoCableRepository=detalleTipoCableRepository;
            _ITipoNovedadRepository=tipoNovedadRepository;
            _ITipoEquipoRepository=tipoEquipoRepository;
            _IDetalleTipoNovedadRepository=detalleTipoNovedadRepository;
            _IEmpresaRepository=empresaRepository;
            _ICiudadRepository=ciudadRepository;
            _IDepartamentoRepository=departamentoRepository;
            _IEstadoRepository= estadoRepository;
            _ILongitudElementoRepository= longitudElementoRepository;
            _IMaterialRepository= materialRepository;
            _INivelTensionElementoRepository= nivelTensionElementoRepository;
            _ITipo_PerdidaRepository= tipo_PerdidaRepository;
            _dispositivoRepository=dispositivoRepository;
            //Asyn Post
            _elementoRepository= elementoRepository;
            _elementoCableRepository=elementoCableRepository;
            _equipoElementoRepository=equipoElementoRepository;
            _novedadRepository=novedadRepository;
            _fotoRepository= fotoRepository;
            _localizacionElementoRepository=localizacionElementoRepository;
            _perdidaRepository= perdidaRepository;
            _ciudad_EmpresaRepository=ciudad_EmpresaRepository;

            //Services
            _mapper= mapper;
            _hostingEnvironment=hostingEnvironment; //service images

            //S3
            // this.S3Client = s3Client;
        }

        #endregion

        #region Methods GET

        /* 
        [HttpGet]
        [Route("GetCables")] 
        [SwaggerResponse(200, typeof(MaterialViewModel))]
        public async Task<ActionResult> GetCables()
        {
                //https://stackoverflow.com/questions/40275195/how-to-setup-automapper-in-asp-net-core
           //var cables= await _elementoCableRepository.AllIncludingAsync(a=>a.Elemento_Id==2);//AllIncludingAsync(c=>c.Medidor);//GetAllAsync();
            var cables= await _elementoCableRepository.AllIncludingAsyncWhere(a=>a.Elemento_Id==2);
            return Ok(cables);
        }*/


        [HttpGet]
       
        public async Task<ResponseDataSyncModelView> Get()
        {
                
           var tipocable= await _ITipoCableRepository.GetAllAsync();//AllIncludingAsync(c=>c.Medidor);//GetAllAsync();
           var listdetalletipocable= await _IDetalleTipoCableRepository.AllIncludingAsync(a=>a.Cable,b=>b.TipoCable);//AllIncludingAsync(c=>c.Medidor);//GetAllAsync();
           var detalletipocable=ToListResponseDetalleCable(listdetalletipocable);

           var estados =await _IEstadoRepository .GetAllAsync();
           var longitudElemento =await _ILongitudElementoRepository .GetAllAsync();
           var nivelTensionElemento =await _INivelTensionElementoRepository .GetAllAsync();
           var material =await _IMaterialRepository .GetAllAsync();
           var tipo_perdidas= await _ITipo_PerdidaRepository.GetAllAsync();
           



           var tiponovedad =await _ITipoNovedadRepository .GetAllAsync();
           var tipoequipo=await  _ITipoEquipoRepository.GetAllAsync();
           var detalletiponovedad=await _IDetalleTipoNovedadRepository.GetAllAsync();
           var empresa=await _IEmpresaRepository.GetAllAsync();
           var dptociudades=await _IDepartamentoRepository.AllIncludingAsync(b=>b.ciudades);

        //Empresas
        var listEmpresas= await _ciudad_EmpresaRepository.AllIncludingAsync( b=>b.Empresa,c=>c.Ciudad);
        var viewModel = _mapper.Map<IEnumerable<Ciudad_Empresa>, IEnumerable<ResponseCiudadEmpresaViewModel>>(listEmpresas);
        var list= ToListCiudadEmpresa(viewModel);




           
        ResponseDataSyncModelView mydatasync= new ResponseDataSyncModelView();
        mydatasync.departCiudades=dptociudades.ToList();
        mydatasync.detalletipocable=detalletipocable.ToList();
        mydatasync.detalletiponovedad=detalletiponovedad.ToList();
        mydatasync.tipocable=tipocable.ToList();
        mydatasync.TipoEquipo=tipoequipo.ToList();
        mydatasync.empresa=empresa.ToList();
        mydatasync.tiponovedad=tiponovedad.ToList();
        mydatasync.estados=estados.ToList();
        mydatasync.nivelTensionElementos=nivelTensionElemento.ToList();
        mydatasync.longitudElementos=longitudElemento.ToList();
        mydatasync.materiales=material.ToList();
        mydatasync.tipo_Perdidas=tipo_perdidas.ToList();
        mydatasync.ciudad_empresas=list;

          return mydatasync;
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
                    SiglaCable= item.Cable.Sigla,
                });
            }
            return listResponse;
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
                    Empresa_Id= item.Empresa_Id,
                    Nombre_Empresa= item.Empresa.Nombre,
                    Descripcion_Empresa= item.Empresa.Descripcion,
                    Direccion= item.Empresa.Direccion,
                    Telefono= item.Empresa.Telefono,
                    Nit= item.Empresa.Nit,
                    Is_Operadora= item.Empresa.Is_Operadora,
                });
            }
           return list;
        }

        #endregion

        #region Methods Post

        [HttpPost]
        [Route("PostDataSync")] 
        public async Task<ActionResult> PostDataSync([FromBody]RequestDataSyncViewModel viewModel)
        {
            var response= new ResponseViewModel();
            try{       
                if(!ModelState.IsValid){
    
               // return BadRequest(ModelState);
                response.IsSuccess=false;
                response.Result=viewModel;
                response.Message="Modelo Invalido";
                return Ok(response);
            }

            if(viewModel.Estado_id==0 && viewModel.Longitud_Elemento_Id==0 && viewModel.Nivel_Tension_Id==0 && viewModel.Material_Id==0){
                viewModel.Estado_id=1;
                viewModel.Longitud_Elemento_Id=1;
                viewModel.Nivel_Tension_Id=1;
                viewModel.Material_Id=1;
                viewModel.CodigoApoyo=string.Format("{0}-{1}",viewModel.CodigoApoyo,"RevisarDatos");
            }

    
            var vericateAvailableDevice= await _dispositivoRepository.GetSingleAsync(a=>a.UsuarioId==viewModel.Usuario_Id && a.Imei==viewModel.Imei_Device);
            if(vericateAvailableDevice != null){

                if(!vericateAvailableDevice.Estado){
                    response.IsSuccess=false;
                    response.Result=viewModel;
                    response.Message="Dispositivo no se encuentra habilitado, para este proceso";
                    return Ok(response);
                }

            }else{
                    response.IsSuccess=false;
                    response.Result=viewModel;
                    response.Message="No se encuentra el dispositivo registrado para este proceso";
                    return Ok(response);
            }

            var verificateExistElement=await _elementoRepository.GetSingleAsync(a=>a.Usuario_Id==viewModel.Usuario_Id && a.NumeroApoyo==viewModel.NumeroApoyo && a.Imei_Device==viewModel.Imei_Device && a.Token_Elemento==viewModel.Token_Elemento);
            //Si existe lo actualiza si no lo registra
            //ELEMENTO
            /*-----------------------------------------------------------------------------------------------*/

            var hour_sync = string.Format("{0:HH:mm}", DateTime.Now);
            var date_sync=DateTime.Now;
            long Elemento_Id=0;
            if(verificateExistElement!=null){
                var model = _mapper.Map<RequestDataSyncViewModel, Elemento>(viewModel);
                verificateExistElement.CodigoApoyo=model.CodigoApoyo;
                verificateExistElement.NumeroApoyo=model.NumeroApoyo;
                verificateExistElement.FechaLevantamiento=model.FechaLevantamiento;
                verificateExistElement.HoraInicio=model.HoraInicio;
                verificateExistElement.HoraFin=model.HoraFin;
                verificateExistElement.ResistenciaMecanica=model.ResistenciaMecanica;
                verificateExistElement.Retenidas=model.Retenidas;
                verificateExistElement.AlturaDisponible=model.AlturaDisponible;
                verificateExistElement.Usuario_Id=model.Usuario_Id;
                verificateExistElement.Estado_id=model.Estado_id;
                verificateExistElement.Longitud_Elemento_Id=model.Longitud_Elemento_Id;
                verificateExistElement.Material_Id=model.Material_Id;
                verificateExistElement.Proyecto_Id=model.Proyecto_Id;
                verificateExistElement.Nivel_Tension_Id=model.Nivel_Tension_Id;
                verificateExistElement.Ciudad_Id=model.Ciudad_Id;
                verificateExistElement.Imei_Device=model.Imei_Device;
                verificateExistElement.Fecha_Sincronizacion=date_sync;
                verificateExistElement.Hora_Sincronizacion=hour_sync;
                   verificateExistElement.Is_Enabled_Data=true;
                await this._elementoRepository.EditAsync(verificateExistElement);

                Elemento_Id=verificateExistElement.Id;

            }else{
                var modelElemento = _mapper.Map<RequestDataSyncViewModel, Elemento>(viewModel);
                modelElemento.Fecha_Sincronizacion=date_sync;
                modelElemento.Hora_Sincronizacion=hour_sync;
                 modelElemento.Is_Enabled_Data=true;
                await this._elementoRepository.AddAsync(modelElemento);
                await _elementoRepository.Commit();
                Elemento_Id=modelElemento.Id;
            }
             
            //CABLES

            var cables=viewModel.Cables;//AllIncludingAsync(c=>c.Medidor);//GetAllAsync();
            var listCablesOld= await _elementoCableRepository.AllIncludingAsyncWhere(a=>a.Elemento_Id==Elemento_Id);
            foreach (var cableOld in listCablesOld)
            {
                this._elementoCableRepository.Delete(cableOld);
            }


            var listCablesNew = _mapper.Map<IEnumerable<ElementoCableViewModel>, IEnumerable<ElementoCable>>(cables);
            foreach(var cableNew in listCablesNew){
                cableNew.Elemento_Id= Elemento_Id;
                cableNew.Is_Enabled_Data= true;
                await this._elementoCableRepository.AddAsync(cableNew);
                await _elementoRepository.Commit();
            }

            //EQUIPOS
            var listEquiposOld=await this._equipoElementoRepository.AllIncludingAsyncWhere(a=>a.Elemento_Id==Elemento_Id);
            foreach (var equipoOld in listEquiposOld)
            {
                this._equipoElementoRepository.Delete(equipoOld);
            }

            var equipos=viewModel.Equipos;//AllIncludingAsync(c=>c.Medidor);//GetAllAsync();
            var listEquiposNew = _mapper.Map<IEnumerable<EquipoElementoViewModel>, IEnumerable<EquipoElemento>>(equipos);
            foreach(var equipoNew in listEquiposNew){
                equipoNew.Elemento_Id= Elemento_Id;
                await this._equipoElementoRepository.AddAsync(equipoNew);
                await _equipoElementoRepository.Commit();
            }


            //PERDIDAS
           
            var listPerdidasOld=await this._perdidaRepository.AllIncludingAsyncWhere(a=>a.Elemento_Id==Elemento_Id);
            foreach (var perdidaOld in listPerdidasOld)
            {
                this._perdidaRepository.Delete(perdidaOld);
            }

            var perdidas=viewModel.Perdidas;//AllIncludingAsync(c=>c.Medidor);//GetAllAsync();
            var listPerdidasNew = _mapper.Map<IEnumerable<PerdidaViewModel>, IEnumerable<Perdida>>(perdidas);
            foreach(var perdidaNew in listPerdidasNew){
                 perdidaNew.Elemento_Id= Elemento_Id;
                await this._perdidaRepository.AddAsync(perdidaNew);
                await _perdidaRepository.Commit();
            }


            //NOVEDADES
            var listNovedadesOld=await this._novedadRepository.AllIncludingAsyncWhere(a=>a.Elemento_Id==Elemento_Id);
            foreach (var novedadOld in listNovedadesOld)
            {
                this._novedadRepository.Delete(novedadOld);
                var listFotosNovedadesOld=await this._fotoRepository.AllIncludingAsyncWhere(a=>a.Novedad_Id==novedadOld.Id);
                foreach (var fotoitemOld in listFotosNovedadesOld)
                {
                    this._fotoRepository.Delete(fotoitemOld);
                    var rutaFoto= fotoitemOld.Ruta;
                    /*if(!fotoitemOld.Ruta.ToUpper().Contains("Foto Nula".ToUpper())){
                        string replaceFoto =fotoitemOld.Ruta.Replace("/Fotos/", "Fotos/");
                        await DeletingAnObject(_bucketName,replaceFoto);


                    }*/
                    //Local
                    string foto= _hostingEnvironment.WebRootPath+fotoitemOld.Ruta;
                    if (System.IO.File.Exists(foto))
                        System.IO.File.Delete(foto);
                       // if (System.IO.File.Exists(@"C:\test.txt"))
                       // System.IO.File.Delete(@"C:\test.txt"));
                }
            }

            var novedades=viewModel.Novedades;//AllIncludingAsync(c=>c.Medidor);//GetAllAsync();
            
            //register fotos novedad
            foreach (var novedadNew in novedades)
            {
                novedadNew.Elemento_Id= Elemento_Id;
                var novedad= _mapper.Map<NovedadViewModel, Novedad>(novedadNew);
                await this._novedadRepository.AddAsync(novedad);
                await _novedadRepository.Commit();

                long Novedad_Id= novedad.Id;
                var tipoNovedad= await _IDetalleTipoNovedadRepository.GetSingleAsync(a=>a.Id==novedadNew.Detalle_Tipo_Novedad_Id,b=>b.TipoNovedad);
                

                string fotoNovedadRuta=string.Empty;
                if(novedadNew.ImageArray!=null){
                    //Local
                    fotoNovedadRuta = await postUploadImage(novedadNew.ImageArray);

                    //S3
                    //var responseUploadNovedadFoto=await UploadFileAwsS3(novedadNew.ImageArray);
                   /* if(responseUploadNovedadFoto.IsSuccess){
                        fotoNovedadRuta=responseUploadNovedadFoto.Message;
                    }else{
                        response.IsSuccess=false;
                        response.Result=viewModel;
                        response.Message=responseUploadNovedadFoto.Message;
                        return Ok(response);
                    }*/
                }else{
                    fotoNovedadRuta = "Foto Nula";
                }


                var foto= new Foto{
                    FechaCreacion=novedadNew.FechaCreacion,
                    Hora=novedadNew.Hora,
                    Titulo=tipoNovedad.Nombre,
                    Ruta=fotoNovedadRuta,
                    Novedad_Id=Novedad_Id,
                    Elemento_Id=Elemento_Id,
                    Descripcion=novedadNew.Descripcion,
                };

                await this._fotoRepository.AddAsync(foto);
                await _fotoRepository.Commit();
                
            }

            //FOTOS
            var listFotosOld=await this._fotoRepository.AllIncludingAsyncWhere(a=>a.Elemento_Id==Elemento_Id && a.Novedad_Id==null || a.Elemento_Id==Elemento_Id && a.Novedad_Id==0);
            foreach (var fotoOld in listFotosOld)
            {
                this._fotoRepository.Delete(fotoOld);
                //REMOTE
                /*if(!fotoOld.Ruta.ToUpper().Contains("Foto Nula".ToUpper())){
                    string replaceFoto =fotoOld.Ruta.Replace("/Fotos/", "Fotos/");
                    await DeletingAnObject(_bucketName,replaceFoto);
                }*/
                //Local
                 
                string foto= _hostingEnvironment.WebRootPath+fotoOld.Ruta;
                if (System.IO.File.Exists(foto))
                        System.IO.File.Delete(foto);
            }

            var fotos=viewModel.Fotos;//AllIncludingAsync(c=>c.Medidor);//GetAllAsync();
            foreach(var fotoNew in fotos){
                fotoNew.Elemento_Id= Elemento_Id;
                if(fotoNew.ImageArray!=null){
                   
                    string rutaFoto = await postUploadImage(fotoNew.ImageArray);
                    fotoNew.Ruta=rutaFoto;
                   /* var responseUploadFoto=await UploadFileAwsS3(fotoNew.ImageArray);
                    if(responseUploadFoto.IsSuccess){
                        fotoNew.Ruta=responseUploadFoto.Message;
                    }else{
                        response.IsSuccess=false;
                        response.Result=viewModel;
                        response.Message=responseUploadFoto.Message;
                        return Ok(response);
                    }*/
                }else{
                    fotoNew.Ruta="Foto nula";
                }
               
                var foto= _mapper.Map<FotoViewModel, Foto>(fotoNew);
                await this._fotoRepository.AddAsync(foto);
                await _fotoRepository.Commit();
            }


            //LOCALIZACION ELEMENTO
            var localizacionElementoExist= await _localizacionElementoRepository.GetSingleAsync(a=>a.Element_Id==Elemento_Id);
            if(localizacionElementoExist!=null){

                 var model = _mapper.Map<RequestDataSyncViewModel, LocalizacionElemento>(viewModel);
                localizacionElementoExist.Coordenadas=model.Coordenadas;
                localizacionElementoExist.Latitud=model.Latitud;
                localizacionElementoExist.Longitud=model.Longitud;
                localizacionElementoExist.Direccion=model.Direccion;
                localizacionElementoExist.DireccionAproximadaGps=model.DireccionAproximadaGps;
                localizacionElementoExist.Barrio=model.Barrio;
                localizacionElementoExist.Localidad=model.Localidad;
                localizacionElementoExist.Sector=model.Sector;
                localizacionElementoExist.ReferenciaLocalizacion=model.ReferenciaLocalizacion;
               
                await this._localizacionElementoRepository.EditAsync(localizacionElementoExist);


            }else{
                var modelLocalizacionElemento = _mapper.Map<RequestDataSyncViewModel, LocalizacionElemento>(viewModel);
                modelLocalizacionElemento.Element_Id=Elemento_Id;
                await this._localizacionElementoRepository.AddAsync(modelLocalizacionElemento);
                await _localizacionElementoRepository.Commit();
            }
            

            response.IsSuccess=true;
            response.Result=viewModel;
            response.Message="Informacion Sincronizada";
            return Ok(response);

        
            }catch(Exception ex){
                response.Result=viewModel;
                response.IsSuccess=false;
                response.Message=ex.Message.ToString()+ " EXCEPTION";
                 Console.WriteLine(ex.Message.ToString()+ " EXCEPTION");

              ///   MemoryStream stream1 = new MemoryStream();
              
              //7  var request = JsonConvert.SerializeObject(materialView);
                /// var content = new StringContent(request, Encoding.UTF8, "application/json");


                /*
                

                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(RequestDataSyncViewModel));  
                ser.WriteObject(stream1, viewModel);  
                stream1.Position = 0;  
                StreamReader sr = new StreamReader(stream1);  }
             
                Console.Write("JSON form of Person object: ");  
                Console.WriteLine(sr.ReadToEnd());      */
                
                return Ok(response);
            }
        }

        private async Task<string> postUploadImage(byte[] imageArray)
        {
            string rutaFoto=string.Empty;
            if (imageArray != null && imageArray.Length > 0)
            {
                var streamRemote = new MemoryStream(imageArray);
                var guid = Guid.NewGuid().ToString();
                var file = string.Format("{0}.jpg", guid);
                var folder = "/Fotos";
              
                //rutaFoto= string.Format("{0}/{1}",folder,file);
               /// var folder_fotos = "/Fotos";
                rutaFoto= string.Format("{0}/{1}",folder,file);

                string pathExist= _hostingEnvironment.WebRootPath+folder;// _hostingEnvironment.WebRootPath es Igual a "wwwroot"
                if(!Directory.Exists(pathExist)){
                   Directory.CreateDirectory(pathExist);
                  //string message="No existe el directorio, se creo";
                }else{
                  //string message="Si existe el directorio"; 
                }
                var formFile = new FormFile(streamRemote , 0, streamRemote.Length, "name", file);
                if (formFile == null || formFile.Length == 0)
                    return "file not selected";

                var path = Path.Combine(
                        Directory.GetCurrentDirectory(), pathExist, 
                        Path.GetFileName(formFile.FileName));
                //Ruta foto
                //var fullPath = string.Format("{0}", path);
                //foto.Ruta= rutaFoto;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
            }

            return rutaFoto;
        }
        #endregion


        /*
        [HttpGet]
        [Route("GetVehicleByUser/{id}")]  
        public async Task<EmpresaViewModel> Get(int id)
        {
          //var empresa=  await this._IEmpresaRespository.GetSingleAsync(id);
            //  var model = _mapper.Map<Empresa, EmpresaViewModel>(empresa);
       
            //return model;//.GetProduct(id);
        } 
        */

/* 
        #region METHODS AWS S3
         
        private async Task<ResponseViewModel> UploadFileAwsS3(byte[] imageArray){
            var response= new ResponseViewModel();
            try
            {

            MemoryStream fotoArray = new MemoryStream(imageArray);
            Image originalImage = Image.FromStream(fotoArray);
            
            //Orientation Image
            if (Array.IndexOf(originalImage.PropertyIdList, 274) > -1)
            {
                        var orientation = (int)originalImage.GetPropertyItem(274).Value[0];
                        switch (orientation)
                        {
                            case 1:
                                // No rotation required.
                                break;
                            case 2:
                                originalImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
                                break;
                            case 3:
                                originalImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
                                break;
                            case 4:
                                originalImage.RotateFlip(RotateFlipType.Rotate180FlipX);
                                break;
                            case 5:
                                originalImage.RotateFlip(RotateFlipType.Rotate90FlipX);
                                break;
                            case 6:
                                originalImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                break;
                            case 7:
                                originalImage.RotateFlip(RotateFlipType.Rotate270FlipX);
                                break;
                            case 8:
                                originalImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                break;
                        }
                        // This EXIF data is now invalid and should be removed.
                        originalImage.RemovePropertyItem(274);
            }
                    
            var imageByte= ImageToByteArray(originalImage);
          //  Stream fotoArrayNewDelete = new MemoryStream(imageByte);

            Stream fotoArrayNew = new MemoryStream(imageByte);
         
            var guid1 = Guid.NewGuid().ToString();
            var guid2 = Guid.NewGuid().ToString();
            var file = string.Format("{0}{1}.jpg", guid1,guid2);
            var folder = "Fotos";
            var rutaFoto= string.Format("{0}/{1}",folder,file);
            var rutaFotoBD= string.Format("/{0}",rutaFoto);
            var request = new PutObjectRequest()
            {
                BucketName = _bucketName,
                ContentType = "image/jpg",
                //CannedACL = S3CannedACL.PublicRead,//PERMISSION TO FILE PUBLIC ACCESIBLE
                Key = rutaFoto,
                InputStream = fotoArrayNew,//SEND THE FILE STREAM,                            
            };
          
                        
            //await S3Client.PutObjectAsync(request);
            var cancellationTokenFoto = new CancellationToken();
            var responseUploadFoto = await S3Client.PutObjectAsync(request, cancellationTokenFoto);

            response.IsSuccess=true;
            response.Result=responseUploadFoto;
            response.Message=rutaFotoBD;
            return response;


            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    Console.WriteLine("Please check the provided AWS Credentials.");
                    Console.WriteLine("If you haven't signed up for Amazon S3, please visit http://aws.amazon.com/s3");

                    response.IsSuccess=false;
                    response.Message=string.Format("Please check the provided AWS Credentials.");
                    return response;
                }
                else
                {
                    Console.WriteLine("An error occurred with the message '{0}' when writing an object", amazonS3Exception.Message);
                     response.IsSuccess=false;
                     response.Message=string.Format("An error occurred with the message {0} when writing an object",amazonS3Exception.Message);
                      return response;
                }
            }
        }

        private async  Task<ResponseViewModel> DeletingAnObject(string bucketName,string keyName)
        {
            var response= new ResponseViewModel();
            try
            {
                DeleteObjectRequest request = new DeleteObjectRequest()
                {
                    BucketName = bucketName,
                    Key = keyName
                };

              await S3Client.DeleteObjectAsync(request);

                response.IsSuccess=true;
                response.Message="file delete";
                return response;
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    Console.WriteLine("Please check the provided AWS Credentials.");
                    Console.WriteLine("If you haven't signed up for Amazon S3, please visit http://aws.amazon.com/s3");

                    response.IsSuccess=false;
                    response.Message=string.Format("Please check the provided AWS Credentials.");
                    return response;
                }
                else
                {
                    Console.WriteLine("An error occurred with the message '{0}' when deleting an object", amazonS3Exception.Message);
                     response.IsSuccess=false;
                     response.Message=string.Format("An error occurred with the message {0} when deleting an object",amazonS3Exception.Message);
                      return response;
                }
            }
        }

        private  byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
        using (var ms = new MemoryStream())
        {
            imageIn.Save(ms,System.Drawing.Imaging.ImageFormat.Jpeg);
            return  ms.ToArray();
        }
        }



        #endregion */
    }
}