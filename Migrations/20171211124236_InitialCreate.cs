using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Electrocore.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cable",
                columns: table => new
                {
                    Id = table.Column<long>(type: "int8", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nombre = table.Column<string>(type: "text", nullable: true),
                    Sigla = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departamento",
                columns: table => new
                {
                    Id = table.Column<long>(type: "int8", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nombre = table.Column<string>(type: "text", nullable: true),
                    codigodpto = table.Column<long>(type: "int8", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Empresa",
                columns: table => new
                {
                    Id = table.Column<long>(type: "int8", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Direccion = table.Column<string>(type: "text", nullable: true),
                    Is_Operadora = table.Column<bool>(type: "bool", nullable: false),
                    Nit = table.Column<string>(type: "text", nullable: true),
                    Nombre = table.Column<string>(type: "text", nullable: true),
                    Telefono = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estado",
                columns: table => new
                {
                    Id = table.Column<long>(type: "int8", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nombre = table.Column<string>(type: "text", nullable: true),
                    Sigla = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estado", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LongitudElemento",
                columns: table => new
                {
                    Id = table.Column<long>(type: "int8", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UnidadMedida = table.Column<string>(type: "text", nullable: true),
                    Valor = table.Column<double>(type: "float8", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LongitudElemento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Material",
                columns: table => new
                {
                    Id = table.Column<long>(type: "int8", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nombre = table.Column<string>(type: "text", nullable: true),
                    Sigla = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Material", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NivelTensionElemento",
                columns: table => new
                {
                    Id = table.Column<long>(type: "int8", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nombre = table.Column<string>(type: "text", nullable: true),
                    Sigla = table.Column<string>(type: "text", nullable: true),
                    Valor = table.Column<long>(type: "int8", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NivelTensionElemento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictApplications",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ClientId = table.Column<string>(type: "text", nullable: false),
                    ClientSecret = table.Column<string>(type: "text", nullable: true),
                    DisplayName = table.Column<string>(type: "text", nullable: true),
                    PostLogoutRedirectUris = table.Column<string>(type: "text", nullable: true),
                    RedirectUris = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictApplications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictScopes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictScopes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tipo_Perdida",
                columns: table => new
                {
                    Id = table.Column<long>(type: "int8", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nombre = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo_Perdida", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tipo_Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nombre = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoCable",
                columns: table => new
                {
                    Id = table.Column<long>(type: "int8", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nombre = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoCable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoEquipo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "int8", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nombre = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoEquipo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoNovedad",
                columns: table => new
                {
                    Id = table.Column<long>(type: "int8", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nombre = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoNovedad", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ciudad",
                columns: table => new
                {
                    Id = table.Column<long>(type: "int8", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nombre = table.Column<string>(type: "text", nullable: true),
                    departmentoId = table.Column<long>(type: "int8", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ciudad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ciudad_Departamento_departmentoId",
                        column: x => x.departmentoId,
                        principalTable: "Departamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictAuthorizations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ApplicationId = table.Column<string>(type: "text", nullable: true),
                    Scopes = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Subject = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictAuthorizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpenIddictAuthorizations_OpenIddictApplications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "OpenIddictApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<long>(type: "int8", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Apellido = table.Column<string>(type: "text", nullable: true),
                    Cedula = table.Column<string>(type: "text", nullable: true),
                    CorreoElectronico = table.Column<string>(type: "text", nullable: true),
                    Direccion = table.Column<string>(type: "text", nullable: true),
                    Empresa_Id = table.Column<long>(type: "int8", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: true),
                    Passsword = table.Column<string>(type: "text", nullable: true),
                    Telefono = table.Column<string>(type: "text", nullable: true),
                    Tipo_Usuario_Id = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_Empresa_Empresa_Id",
                        column: x => x.Empresa_Id,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Usuario_Tipo_Usuario_Tipo_Usuario_Id",
                        column: x => x.Tipo_Usuario_Id,
                        principalTable: "Tipo_Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalleTipoCable",
                columns: table => new
                {
                    Id = table.Column<long>(type: "int8", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Cable_Id = table.Column<long>(type: "int8", nullable: false),
                    Tipocable_Id = table.Column<long>(type: "int8", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleTipoCable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetalleTipoCable_Cable_Cable_Id",
                        column: x => x.Cable_Id,
                        principalTable: "Cable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleTipoCable_TipoCable_Tipocable_Id",
                        column: x => x.Tipocable_Id,
                        principalTable: "TipoCable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalleTipoNovedad",
                columns: table => new
                {
                    Id = table.Column<long>(type: "int8", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    Nombre = table.Column<string>(type: "text", nullable: true),
                    Tipo_Novedad_id = table.Column<long>(type: "int8", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleTipoNovedad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetalleTipoNovedad_TipoNovedad_Tipo_Novedad_id",
                        column: x => x.Tipo_Novedad_id,
                        principalTable: "TipoNovedad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ciudad_Empresa",
                columns: table => new
                {
                    Id = table.Column<long>(type: "int8", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Ciudad_Id = table.Column<long>(type: "int8", nullable: false),
                    Empresa_Id = table.Column<long>(type: "int8", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ciudad_Empresa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ciudad_Empresa_Ciudad_Ciudad_Id",
                        column: x => x.Ciudad_Id,
                        principalTable: "Ciudad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ciudad_Empresa_Empresa_Empresa_Id",
                        column: x => x.Empresa_Id,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Proyecto",
                columns: table => new
                {
                    Id = table.Column<long>(type: "int8", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Ciudad_Id = table.Column<long>(type: "int8", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    FechaFin = table.Column<DateTime>(type: "timestamp", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "timestamp", nullable: false),
                    IsActivo = table.Column<bool>(type: "bool", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: true),
                    OrdenTrabajo = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proyecto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proyecto_Ciudad_Ciudad_Id",
                        column: x => x.Ciudad_Id,
                        principalTable: "Ciudad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictTokens",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ApplicationId = table.Column<string>(type: "text", nullable: true),
                    AuthorizationId = table.Column<string>(type: "text", nullable: true),
                    Ciphertext = table.Column<string>(type: "text", nullable: true),
                    CreationDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: true),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: true),
                    Hash = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    Subject = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpenIddictTokens_OpenIddictApplications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "OpenIddictApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OpenIddictTokens_OpenIddictAuthorizations_AuthorizationId",
                        column: x => x.AuthorizationId,
                        principalTable: "OpenIddictAuthorizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Dispositivo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "int8", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Estado = table.Column<bool>(type: "bool", nullable: false),
                    Imei = table.Column<string>(type: "text", nullable: true),
                    UsuarioId = table.Column<long>(type: "int8", nullable: true),
                    userId = table.Column<long>(type: "int8", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dispositivo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dispositivo_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Elemento",
                columns: table => new
                {
                    Id = table.Column<long>(type: "int8", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AlturaDisponible = table.Column<double>(type: "float8", nullable: false),
                    Ciudad_Id = table.Column<long>(type: "int8", nullable: false),
                    CodigoApoyo = table.Column<string>(type: "text", nullable: true),
                    Estado_id = table.Column<long>(type: "int8", nullable: false),
                    FechaLevantamiento = table.Column<DateTime>(type: "timestamp", nullable: false),
                    HoraFin = table.Column<string>(type: "text", nullable: true),
                    HoraInicio = table.Column<string>(type: "text", nullable: true),
                    Longitud_Elemento_Id = table.Column<long>(type: "int8", nullable: false),
                    Material_Id = table.Column<long>(type: "int8", nullable: false),
                    Nivel_Tension_Id = table.Column<long>(type: "int8", nullable: false),
                    NumeroApoyo = table.Column<long>(type: "int8", nullable: false),
                    Proyecto_Id = table.Column<long>(type: "int8", nullable: false),
                    ResistenciaMecanica = table.Column<string>(type: "text", nullable: true),
                    Retenidas = table.Column<long>(type: "int8", nullable: false),
                    Usuario_Id = table.Column<long>(type: "int8", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elemento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Elemento_Estado_Estado_id",
                        column: x => x.Estado_id,
                        principalTable: "Estado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Elemento_LongitudElemento_Longitud_Elemento_Id",
                        column: x => x.Longitud_Elemento_Id,
                        principalTable: "LongitudElemento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Elemento_Material_Material_Id",
                        column: x => x.Material_Id,
                        principalTable: "Material",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Elemento_NivelTensionElemento_Nivel_Tension_Id",
                        column: x => x.Nivel_Tension_Id,
                        principalTable: "NivelTensionElemento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Elemento_Proyecto_Proyecto_Id",
                        column: x => x.Proyecto_Id,
                        principalTable: "Proyecto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Proyecto_Empresa",
                columns: table => new
                {
                    Id = table.Column<long>(type: "int8", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Empresa_Id = table.Column<long>(type: "int8", nullable: false),
                    IsInterventora = table.Column<bool>(type: "bool", nullable: false),
                    IsOperadora = table.Column<bool>(type: "bool", nullable: false),
                    IsPropietaria = table.Column<bool>(type: "bool", nullable: false),
                    Proyecto_Id = table.Column<long>(type: "int8", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proyecto_Empresa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proyecto_Empresa_Empresa_Empresa_Id",
                        column: x => x.Empresa_Id,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Proyecto_Empresa_Proyecto_Proyecto_Id",
                        column: x => x.Proyecto_Id,
                        principalTable: "Proyecto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProyectoUsuario",
                columns: table => new
                {
                    Id = table.Column<long>(type: "int8", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    IsActivo = table.Column<bool>(type: "bool", nullable: false),
                    Proyecto_Id = table.Column<long>(type: "int8", nullable: false),
                    Usuario_Id = table.Column<long>(type: "int8", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProyectoUsuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProyectoUsuario_Proyecto_Proyecto_Id",
                        column: x => x.Proyecto_Id,
                        principalTable: "Proyecto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProyectoUsuario_Usuario_Usuario_Id",
                        column: x => x.Usuario_Id,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ElementoCable",
                columns: table => new
                {
                    Id = table.Column<long>(type: "int8", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Cantidad = table.Column<long>(type: "int8", nullable: false),
                    Ciudad_Empresa_Id = table.Column<long>(type: "int8", nullable: true),
                    Ciudad_Id = table.Column<long>(type: "int8", nullable: false),
                    Codigo = table.Column<string>(type: "text", nullable: true),
                    DetalleTipocable_Id = table.Column<long>(type: "int8", nullable: false),
                    Elemento_Id = table.Column<long>(type: "int8", nullable: false),
                    Empresa_Id = table.Column<long>(type: "int8", nullable: false),
                    SobreRbt = table.Column<bool>(type: "bool", nullable: false),
                    Tiene_Marquilla = table.Column<bool>(type: "bool", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElementoCable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElementoCable_Ciudad_Empresa_Ciudad_Empresa_Id",
                        column: x => x.Ciudad_Empresa_Id,
                        principalTable: "Ciudad_Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ElementoCable_DetalleTipoCable_DetalleTipocable_Id",
                        column: x => x.DetalleTipocable_Id,
                        principalTable: "DetalleTipoCable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ElementoCable_Elemento_Elemento_Id",
                        column: x => x.Elemento_Id,
                        principalTable: "Elemento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquipoElemento",
                columns: table => new
                {
                    Id = table.Column<long>(type: "int8", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Cantidad = table.Column<long>(type: "int8", nullable: false),
                    Ciudad_Empresa_Id = table.Column<long>(type: "int8", nullable: true),
                    Ciudad_Id = table.Column<long>(type: "int8", nullable: false),
                    Codigo = table.Column<string>(type: "text", nullable: true),
                    ConectadoRbt = table.Column<bool>(type: "bool", nullable: false),
                    Consumo = table.Column<long>(type: "int8", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    Elemento_Id = table.Column<long>(type: "int8", nullable: false),
                    EmpresaId = table.Column<long>(type: "int8", nullable: false),
                    MedidorBt = table.Column<bool>(type: "bool", nullable: false),
                    TipoEquipo_Id = table.Column<long>(type: "int8", nullable: false),
                    UnidadMedida = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipoElemento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipoElemento_Ciudad_Empresa_Ciudad_Empresa_Id",
                        column: x => x.Ciudad_Empresa_Id,
                        principalTable: "Ciudad_Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EquipoElemento_Elemento_Elemento_Id",
                        column: x => x.Elemento_Id,
                        principalTable: "Elemento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipoElemento_TipoEquipo_TipoEquipo_Id",
                        column: x => x.TipoEquipo_Id,
                        principalTable: "TipoEquipo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Novedad",
                columns: table => new
                {
                    Id = table.Column<long>(type: "int8", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    Detalle_Tipo_Novedad_Id = table.Column<long>(type: "int8", nullable: false),
                    Elemento_Id = table.Column<long>(type: "int8", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Novedad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Novedad_DetalleTipoNovedad_Detalle_Tipo_Novedad_Id",
                        column: x => x.Detalle_Tipo_Novedad_Id,
                        principalTable: "DetalleTipoNovedad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Novedad_Elemento_Elemento_Id",
                        column: x => x.Elemento_Id,
                        principalTable: "Elemento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Perdida",
                columns: table => new
                {
                    Id = table.Column<long>(type: "int8", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Cantidad = table.Column<long>(type: "int8", nullable: false),
                    Concepto = table.Column<string>(type: "text", nullable: true),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    Elemento_Id = table.Column<long>(type: "int8", nullable: false),
                    Response_Checked = table.Column<bool>(type: "bool", nullable: false),
                    Tipo_Perdida_Id = table.Column<long>(type: "int8", nullable: false),
                    Valor = table.Column<double>(type: "float8", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perdida", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Perdida_Elemento_Elemento_Id",
                        column: x => x.Elemento_Id,
                        principalTable: "Elemento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Perdida_Tipo_Perdida_Tipo_Perdida_Id",
                        column: x => x.Tipo_Perdida_Id,
                        principalTable: "Tipo_Perdida",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Project_locationelement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "int8", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Barrio = table.Column<string>(type: "text", nullable: true),
                    Coordenadas = table.Column<string>(type: "text", nullable: true),
                    Direccion = table.Column<string>(type: "text", nullable: true),
                    DireccionAproximadaGps = table.Column<string>(type: "text", nullable: true),
                    Element_Id = table.Column<long>(type: "int8", nullable: false),
                    Latitud = table.Column<decimal>(type: "numeric", nullable: false),
                    Localidad = table.Column<string>(type: "text", nullable: true),
                    Longitud = table.Column<decimal>(type: "numeric", nullable: false),
                    ReferenciaLocalizacion = table.Column<string>(type: "text", nullable: true),
                    Sector = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project_locationelement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Project_locationelement_Elemento_Element_Id",
                        column: x => x.Element_Id,
                        principalTable: "Elemento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Project_locationcable",
                columns: table => new
                {
                    Id = table.Column<long>(type: "int8", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Altitud = table.Column<string>(type: "text", nullable: true),
                    Color = table.Column<string>(type: "text", nullable: true),
                    ElementoCable_Id = table.Column<long>(type: "int8", nullable: false),
                    LineaCable = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project_locationcable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Project_locationcable_ElementoCable_ElementoCable_Id",
                        column: x => x.ElementoCable_Id,
                        principalTable: "ElementoCable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Foto",
                columns: table => new
                {
                    Id = table.Column<long>(type: "int8", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    Elemento_Id = table.Column<long>(type: "int8", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Hora = table.Column<string>(type: "text", nullable: true),
                    NovedadId = table.Column<long>(type: "int8", nullable: true),
                    Novedad_Id = table.Column<long>(type: "int8", nullable: true),
                    Ruta = table.Column<string>(type: "text", nullable: true),
                    Titulo = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Foto_Elemento_Elemento_Id",
                        column: x => x.Elemento_Id,
                        principalTable: "Elemento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Foto_Novedad_NovedadId",
                        column: x => x.NovedadId,
                        principalTable: "Novedad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ciudad_departmentoId",
                table: "Ciudad",
                column: "departmentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Ciudad_Empresa_Ciudad_Id",
                table: "Ciudad_Empresa",
                column: "Ciudad_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ciudad_Empresa_Empresa_Id",
                table: "Ciudad_Empresa",
                column: "Empresa_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleTipoCable_Cable_Id",
                table: "DetalleTipoCable",
                column: "Cable_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleTipoCable_Tipocable_Id",
                table: "DetalleTipoCable",
                column: "Tipocable_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleTipoNovedad_Tipo_Novedad_id",
                table: "DetalleTipoNovedad",
                column: "Tipo_Novedad_id");

            migrationBuilder.CreateIndex(
                name: "IX_Dispositivo_UsuarioId",
                table: "Dispositivo",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Elemento_Estado_id",
                table: "Elemento",
                column: "Estado_id");

            migrationBuilder.CreateIndex(
                name: "IX_Elemento_Longitud_Elemento_Id",
                table: "Elemento",
                column: "Longitud_Elemento_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Elemento_Material_Id",
                table: "Elemento",
                column: "Material_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Elemento_Nivel_Tension_Id",
                table: "Elemento",
                column: "Nivel_Tension_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Elemento_Proyecto_Id",
                table: "Elemento",
                column: "Proyecto_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ElementoCable_Ciudad_Empresa_Id",
                table: "ElementoCable",
                column: "Ciudad_Empresa_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ElementoCable_DetalleTipocable_Id",
                table: "ElementoCable",
                column: "DetalleTipocable_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ElementoCable_Elemento_Id",
                table: "ElementoCable",
                column: "Elemento_Id");

            migrationBuilder.CreateIndex(
                name: "IX_EquipoElemento_Ciudad_Empresa_Id",
                table: "EquipoElemento",
                column: "Ciudad_Empresa_Id");

            migrationBuilder.CreateIndex(
                name: "IX_EquipoElemento_Elemento_Id",
                table: "EquipoElemento",
                column: "Elemento_Id");

            migrationBuilder.CreateIndex(
                name: "IX_EquipoElemento_TipoEquipo_Id",
                table: "EquipoElemento",
                column: "TipoEquipo_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Foto_Elemento_Id",
                table: "Foto",
                column: "Elemento_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Foto_NovedadId",
                table: "Foto",
                column: "NovedadId");

            migrationBuilder.CreateIndex(
                name: "IX_Novedad_Detalle_Tipo_Novedad_Id",
                table: "Novedad",
                column: "Detalle_Tipo_Novedad_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Novedad_Elemento_Id",
                table: "Novedad",
                column: "Elemento_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictApplications_ClientId",
                table: "OpenIddictApplications",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictAuthorizations_ApplicationId",
                table: "OpenIddictAuthorizations",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictTokens_ApplicationId",
                table: "OpenIddictTokens",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictTokens_AuthorizationId",
                table: "OpenIddictTokens",
                column: "AuthorizationId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictTokens_Hash",
                table: "OpenIddictTokens",
                column: "Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Perdida_Elemento_Id",
                table: "Perdida",
                column: "Elemento_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Perdida_Tipo_Perdida_Id",
                table: "Perdida",
                column: "Tipo_Perdida_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Project_locationcable_ElementoCable_Id",
                table: "Project_locationcable",
                column: "ElementoCable_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Project_locationelement_Element_Id",
                table: "Project_locationelement",
                column: "Element_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Proyecto_Ciudad_Id",
                table: "Proyecto",
                column: "Ciudad_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Proyecto_Empresa_Empresa_Id",
                table: "Proyecto_Empresa",
                column: "Empresa_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Proyecto_Empresa_Proyecto_Id",
                table: "Proyecto_Empresa",
                column: "Proyecto_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProyectoUsuario_Proyecto_Id",
                table: "ProyectoUsuario",
                column: "Proyecto_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProyectoUsuario_Usuario_Id",
                table: "ProyectoUsuario",
                column: "Usuario_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Empresa_Id",
                table: "Usuario",
                column: "Empresa_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Tipo_Usuario_Id",
                table: "Usuario",
                column: "Tipo_Usuario_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dispositivo");

            migrationBuilder.DropTable(
                name: "EquipoElemento");

            migrationBuilder.DropTable(
                name: "Foto");

            migrationBuilder.DropTable(
                name: "OpenIddictScopes");

            migrationBuilder.DropTable(
                name: "OpenIddictTokens");

            migrationBuilder.DropTable(
                name: "Perdida");

            migrationBuilder.DropTable(
                name: "Project_locationcable");

            migrationBuilder.DropTable(
                name: "Project_locationelement");

            migrationBuilder.DropTable(
                name: "Proyecto_Empresa");

            migrationBuilder.DropTable(
                name: "ProyectoUsuario");

            migrationBuilder.DropTable(
                name: "TipoEquipo");

            migrationBuilder.DropTable(
                name: "Novedad");

            migrationBuilder.DropTable(
                name: "OpenIddictAuthorizations");

            migrationBuilder.DropTable(
                name: "Tipo_Perdida");

            migrationBuilder.DropTable(
                name: "ElementoCable");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "DetalleTipoNovedad");

            migrationBuilder.DropTable(
                name: "OpenIddictApplications");

            migrationBuilder.DropTable(
                name: "Ciudad_Empresa");

            migrationBuilder.DropTable(
                name: "DetalleTipoCable");

            migrationBuilder.DropTable(
                name: "Elemento");

            migrationBuilder.DropTable(
                name: "Tipo_Usuario");

            migrationBuilder.DropTable(
                name: "TipoNovedad");

            migrationBuilder.DropTable(
                name: "Empresa");

            migrationBuilder.DropTable(
                name: "Cable");

            migrationBuilder.DropTable(
                name: "TipoCable");

            migrationBuilder.DropTable(
                name: "Estado");

            migrationBuilder.DropTable(
                name: "LongitudElemento");

            migrationBuilder.DropTable(
                name: "Material");

            migrationBuilder.DropTable(
                name: "NivelTensionElemento");

            migrationBuilder.DropTable(
                name: "Proyecto");

            migrationBuilder.DropTable(
                name: "Ciudad");

            migrationBuilder.DropTable(
                name: "Departamento");
        }
    }
}
