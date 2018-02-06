using AutoMapper;
using Electro.model.datatakemodel;
using Electro.model.Models.datatakemodel;
using electrocore.ViewModels;

//using Electrocore.Models.datatakemodel;
using Electrocore.ViewModels;

public class MappingProfile : Profile {
    public MappingProfile() {
        // Add as many of these lines as you need to map your objects
       
        CreateMap<EmpresaViewModel, Empresa>();
        CreateMap<Empresa, EmpresaViewModel>();
        CreateMap<Estado, EstadoViewModel>();
        CreateMap<EstadoViewModel, Estado>();
        CreateMap<Material, MaterialViewModel>();
        CreateMap<MaterialViewModel, Material>();
        CreateMap<TipoUsuarioViewModel, Tipo_Usuario>();
        CreateMap<Tipo_Usuario, TipoUsuarioViewModel>();
        CreateMap<UsuarioViewModel, Usuario>();
        CreateMap<Usuario, UsuarioViewModel>();
        CreateMap<ProyectoViewModel, Proyecto>();
        CreateMap<Proyecto, ProyectoViewModel>();
        CreateMap<ProyectoViewModel, Proyecto_Empresa>();
        CreateMap<Proyecto_Empresa, ProyectoViewModel>();
        
        CreateMap<ProyectoUsuarioViewModel, ProyectoUsuario>();
        CreateMap<ProyectoUsuario, ProyectoUsuarioViewModel>();
        

        CreateMap<CableViewModel, Cable>();
        CreateMap<Cable, CableViewModel>();

        CreateMap<DetalleTipoCableViewModel, DetalleTipoCable>();
        CreateMap<DetalleTipoCable, DetalleTipoCableViewModel>();

        CreateMap<LongitudElementoViewModel, LongitudElemento>();
        CreateMap<LongitudElemento, LongitudElementoViewModel>();

        CreateMap<NivelTensionElementoViewModel, NivelTensionElemento>();
        CreateMap<NivelTensionElemento, NivelTensionElementoViewModel>();

        CreateMap<Tipo_PerdidaViewModel, Tipo_Perdida>();
        CreateMap<Tipo_Perdida, Tipo_PerdidaViewModel>();

        CreateMap<TipoCableViewModel, TipoCable>();
        CreateMap<TipoCable, TipoCableViewModel>();

        CreateMap<TipoEquipoViewModel, TipoEquipo>();
        CreateMap<TipoEquipo, TipoEquipoViewModel>();

        CreateMap<TipoNovedadViewModel, TipoNovedad>();
        CreateMap<TipoNovedad, TipoNovedadViewModel>();

        CreateMap<Detalle_TipoNovedadViewModel, DetalleTipoNovedad>();
        CreateMap<DetalleTipoNovedad, Detalle_TipoNovedadViewModel>();

        //Maping data postAsync
        CreateMap<ElementoViewModel, Elemento>();
        CreateMap<Elemento, ElementoViewModel>();

        CreateMap<RequestDataSyncViewModel, Elemento>();
        CreateMap<Elemento, RequestDataSyncViewModel>();

          CreateMap<RequestDataSyncViewModel, LocalizacionElemento>();
        CreateMap<LocalizacionElemento, RequestDataSyncViewModel>();


        CreateMap<NovedadViewModel, Novedad>();
        CreateMap<Novedad, NovedadViewModel>();

        CreateMap<ElementoCableViewModel, ElementoCable>();
        CreateMap<ElementoCable, ElementoCableViewModel>();

        CreateMap<RequestDataSyncViewModel, ElementoCable>();
        CreateMap<ElementoCable, RequestDataSyncViewModel>();

        CreateMap<EquipoElementoViewModel, EquipoElemento>();
        CreateMap<EquipoElemento, EquipoElementoViewModel>();

        CreateMap<FotoViewModel, Foto>();
        CreateMap<Foto, FotoViewModel>();

        CreateMap<PerdidaViewModel, Perdida>();
        CreateMap<Perdida, PerdidaViewModel>();

        CreateMap<LocalizacionElementoViewModel, LocalizacionElemento>();
        CreateMap<LocalizacionElemento, LocalizacionElementoViewModel>();

        //User Login
        
        CreateMap<ResponseLoginViewModel, Usuario>();
        CreateMap<Usuario, ResponseLoginViewModel>();

        CreateMap<ResponseLoginViewModel, Dispositivo>();
        CreateMap<Dispositivo, ResponseLoginViewModel>();

        CreateMap<DispositivoViewModel, Dispositivo>();
        CreateMap<Dispositivo, DispositivoViewModel>();

        //Proyectos
        CreateMap<ResponseProyectosUsuarioViewModel, Proyecto>();
        CreateMap<Proyecto, ResponseProyectosUsuarioViewModel>();

        //Ciudad Empresa
        CreateMap<Ciudad_EmpresaViewModel, Ciudad_Empresa>();
        CreateMap<Ciudad_Empresa, Ciudad_EmpresaViewModel>();

        CreateMap<ResponseCiudadEmpresaViewModel, Ciudad_Empresa>();
        CreateMap<Ciudad_Empresa, ResponseCiudadEmpresaViewModel>();

        CreateMap<CiudadViewModel, Ciudad>();
        CreateMap<Ciudad, CiudadViewModel>();

        //Login
        CreateMap<RequestLoginViewModel, Usuario>();
        CreateMap<Usuario, RequestLoginViewModel>();


        CreateMap<RequestLoginViewModel, Dispositivo>();
        CreateMap<Dispositivo, RequestLoginViewModel>();


       
    }
}