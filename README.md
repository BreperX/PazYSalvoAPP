PazYSalvoAPP
PazYSalvoAPP es una aplicación web para la gestión de facturas y otros recursos relacionados con clientes, pagos, estados, roles y servicios. La aplicación está construida con ASP.NET Core y utiliza Entity Framework Core para la manipulación de datos.

Instalación
Clona el repositorio en tu máquina local:
bash
Copiar código
git clone https://github.com/tuusuario/PazYSalvoAPP.git
Configura la cadena de conexión a la base de datos en el archivo appsettings.json.
Configuración
Base de Datos
Si aún no tienes una base de datos configurada, puedes crear una nueva base de datos SQL Server y aplicar las migraciones de Entity Framework Core para generar las tablas necesarias.

Abre el archivo appsettings.json y configura la cadena de conexión:
json
Copiar código
{
    "ConnectionStrings": {
        "DefaultConnection": "Server=tu_servidor;Database=PazYSalvoDB;User Id=tu_usuario;Password=tu_contraseña;"
    }
}
Aplica las migraciones para crear las tablas en la base de datos:
bash
Copiar código
dotnet ef database update
Estructura del Proyecto
La estructura del proyecto es la siguiente:

PazYSalvoAPP.WebApp: Proyecto principal de la aplicación web.
Controllers: Controladores de MVC para manejar las solicitudes HTTP.
Models: Modelos de datos y vistas.
Views: Vistas de Razor para la interfaz de usuario.
PazYSalvoAPP.Data: Proyecto de acceso a datos.
Context: Contexto de la base de datos y configuración de Entity Framework Core.
Repositories: Implementaciones de los repositorios para acceder a los datos.
PazYSalvoAPP.Business: Proyecto de lógica de negocio.
Services: Servicios que contienen la lógica de negocio.
Repositorios
El repositorio FacturaRepository implementa la interfaz IGenericRepository<Factura> para realizar operaciones CRUD sobre las facturas.

Código del Repositorio de Facturas
csharp
Copiar código
// Definición de la clase FacturaRepository que implementa la interfaz IGenericRepository<Factura>
public class FacturaRepository : IGenericRepository<Factura>
{
    // Declaración de un campo privado readonly llamado _context de tipo PazSalvoContext
    private readonly PazSalvoContext _context;

    // Constructor de la clase que recibe una instancia de PazSalvoContext
    public FacturaRepository(PazSalvoContext context)
    {
        _context = context; // Asignación de la instancia de PazSalvoContext al campo _context
    }

    // Método para actualizar una factura en la base de datos
    public async Task<bool> Actualizar(Factura model)
    {
        bool result = default(bool); // Inicialización de una variable booleana llamada result

        try
        {
            _context.Facturas.Update(model); // Actualización de la factura en el contexto
            await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos

            return !result; // Devolver el valor inverso de result (true si se actualizó correctamente, false si no)
        }
        catch (Exception ex) // Captura de excepciones
        {
            return result; // Devolver el valor por defecto de result (false)
        }
    }

    // Método para eliminar una factura de la base de datos por su ID
    public async Task<bool> Eliminar(int id)
    {
        bool result = default(bool); // Inicialización de una variable booleana llamada result

        if (id == default(int)) return result;

        var factura = _context.Facturas.FirstOrDefault(f => f.Id == id); // Buscar la factura por su ID

        if (factura == null) return result; // Si la factura no se encontró, devolver el valor por defecto de result (false)

        try
        {
            _context.Facturas.Remove(factura); // Eliminar la factura del contexto
            await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos

            return !result; // Devolver el valor inverso de result (true si se eliminó correctamente, false si no)
        }
        catch (Exception ex) // Captura de excepciones
        {
            return result; // Devolver el valor por defecto de result (false)
        }
    }

    // Método para insertar una nueva factura en la base de datos
    public async Task<bool> Insertar(Factura model)
    {
        bool result = default(bool); // Inicialización de una variable booleana llamada result

        try
        {
            _context.Facturas.Add(model); // Agregar la factura al contexto
            await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos

            return !result; // Devolver el valor inverso de result (true si se insertó correctamente, false si no)
        }
        catch (Exception ex) // Captura de excepciones
        {
            return result; // Devolver el valor por defecto de result (false)
        }
    }

    // Método para leer una factura de la base de datos por su ID
    public async Task<Factura> Leer(int id)
    {
        if (id == default(int)) return null; // Verificar si el ID es cero, si es así, devolver null

        var factura = _context.Facturas.FirstAsync(f => f.Id == id); // Buscar la factura por su ID

        if (factura == null) return null; // Si la factura no se encontró, devolver null

        return await factura; // Devolver la factura encontrada
    }

    // Método para leer todas las facturas de la base de datos
    public async Task<IQueryable<Factura>> LeerTodos()
    {
        IQueryable<Factura> listaDeFacturas = _context.Facturas; // Obtener todas las facturas del contexto

        return listaDeFacturas; // Devolver la lista de facturas
    }
}
Controladores
El controlador FacturaController gestiona las operaciones relacionadas con las facturas. Otros controladores (Clientes, Estados, Medio de Pago, Pago, Persona, Roles, Servicios, Usuario) tienen una estructura similar, con acciones básicas como Index.

Código del Controlador de Facturas
csharp
Copiar código
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PazYSalvoAPP.Business.Services;
using PazYSalvoAPP.Data.Context;
using PazYSalvoAPP.WebApp.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PazYSalvoAPP.WebApp.Controllers
{
    public class FacturaController : Controller
    {
        // Mensaje de prueba para la vista
        public IActionResult Index()
        {
            ViewBag.Mensaje = "Hola Clase";
            return View();
        }
        private readonly IFacturaService _facturaService;

        // Constructor que recibe una instancia de IFacturaService
        public FacturaController(IFacturaService facturaService)
        {
            _facturaService = facturaService;
        }

        // Acción para mostrar la vista principal
        public IActionResult Index()
        {
            return View();
        }

        // Acción para listar las facturas
        [HttpGet]
        public async Task<IActionResult> ListarFacturas()
        {
            IQueryable<Factura>? consultaDeFacturas = await _facturaService.LeerTodos();

            // Mapeo de Factura a FacturaViewModel
            List<FacturaViewModel> listadoDeFacturas = consultaDeFacturas.Select(f => new FacturaViewModel
            {
                Id = f.Id,
                Saldo = f.Saldo,
                ClienteId = f.ClienteId,
                ServicioAdquiridoId = f.ServicioAdquiridoId,
                MedioDePagoId = f.MedioDePagoId,
                EstadoId = f.EstadoId,

            }).ToList();

            return PartialView("_ListadoDeFacturas", listadoDeFacturas);
        }

        // Acción para agregar una nueva factura
        [HttpPost]
        public async Task<IActionResult> AgregarFacturas([FromBody] FacturaViewModel model)
        {
            Factura factura = new Factura()
            {
                Saldo = model.Saldo,
                ClienteId = model.ClienteId,
                ServicioAdquiridoId = model.ServicioAdquiridoId,
                MedioDePagoId = model.MedioDePagoId,
                EstadoId = model.EstadoId,

            };

            bool response = await _facturaService.Insertar(factura);

            return StatusCode(StatusCodes.Status200OK, new { valor = response });
        }

        // Acción para actualizar una factura existente
        [HttpPut]
        public async Task<IActionResult> ActualizarFacturas([FromBody] FacturaViewModel model)
        {
            Factura factura = new Factura()
            {
                Id = model.Id,
                Saldo = model.Saldo,
                ClienteId = model.ClienteId,
                ServicioAdquiridoId = model.ServicioAdquiridoId,
                MedioDePagoId = model.MedioDePagoId,
                EstadoId = model.EstadoId,
            };

            bool response = await _facturaService.Actualizar(factura);

            return StatusCode(StatusCodes.Status200OK, new { valor = response });
        }
    }
}
Configuración de la Aplicación
El archivo Startup.cs configura los servicios y el pipeline de la aplicación.

Código de Startup.cs
csharp
Copiar código
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PazYSalvoAPP.Business.Services;
using PazYSalvoAPP.Data.Context;
using PazYSalvoAPP.Data.Repositories;
using System;

namespace PazYSalvoAPP.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Este método se llama en tiempo de ejecución. Usa este método para agregar servicios al contenedor.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<PazSalvoContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<IFacturaRepository, FacturaRepository>();
            services.AddTransient<IFacturaService, FacturaService>();
        }

        // Este método se llama en tiempo de ejecución. Usa este método para configurar el pipeline de solicitudes HTTP.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
Uso
Para iniciar la aplicación, usa el siguiente comando:

bash
Copiar código
dotnet run
Accede a la aplicación en tu navegador web en https://localhost:5001.

TEN EN CUENTA QUE TIENES QUE CAMBIAR ESTO POR EL QUE TENGAS CONFIGURADO EN TU MAQUINA
