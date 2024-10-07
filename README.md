
# ServiceWorkerTestNetCore6
Ejemplo de implementación de un Worker Service en .NET Core 6.

## Tutoriales
1. [YouTube - Implementación del Servicio Worker](https://www.youtube.com/watch?v=8Sy69b6-nj0&t=617s)
2. [YouTube - Corrección de error en ejecución del servicio](https://www.youtube.com/watch?v=pxdIfRDqhL0)

## Instalación de Paquetes
Para trabajar con servicios en Windows, instala el siguiente paquete NuGet:

```bash
dotnet add package Microsoft.Extensions.Hosting.WindowsServices
```
- Luego, agrega esta línea en tu configuración de host:
``.UseWindowsService();
``

## Comandos para Servicios en Windows
### - Crear Servicio
`sc.exe create MyWorkerService start=auto binPath= "C:\Users\carloscc.CAINCO\Downloads\myworkerservicetest\MyWorkerService.exe"
`
### - Ejecutar Servicio
`sc start MyWorkerService`

### - Verificar Estado del Servicio
`sc query MyWorkerService`
**Opción 2:** Usar el Administrador de Tareas
-   Abre el Administrador de Tareas (`Ctrl + Shift + Esc`).
-   Ve a la pestaña **Servicios** y busca tu servicio por nombre.

### - Eliminar Servicio
`sc.exe delete MyWorkerService`


## Example Code

### Program.cs

    using MyWorkerService;
    
    IHost host = Host.CreateDefaultBuilder(args)
        .UseWindowsService()
        .ConfigureServices(services =>
        {
            services.AddHostedService<Worker>();
        })
        .Build();
    
    await host.RunAsync();

**

### Worker.cs

    namespace MyWorkerService
    {
        public class Worker : BackgroundService
        {
            private readonly ILogger<Worker> _logger;
    
            public Worker(ILogger<Worker> logger)
            {
                _logger = logger;
            }
    
            protected override async Task ExecuteAsync(CancellationToken stoppingToken)
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    
                    Console.WriteLine("Iniciando recorrido de Pagos");
                    await VerificarEstadoPago();
                    Console.WriteLine("Finalizado verificacion de Pagos");
                    Console.WriteLine("Iniciando recorrido de Credenciales");
                    await EnviarCredenciales();
                    Console.WriteLine("Finalizado verificacion de Credenciales");
    
                    await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                }
            }
    
            public static async Task VerificarEstadoPago()
            {
                try
                {
                    Console.WriteLine("1. Ejecuta VerificarEstadoPago");
                    // Lógica de Verificación
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en VerificarEstadoPago: {ex.Message}");
                }
            }
    
            public static async Task EnviarCredenciales()
            {
                try
                {
                    Console.WriteLine("2. Ejecuta EnviarCredenciales");
                    // Lógica de Enviar Credenciales
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en EnviarCredenciales: {ex.Message}");
                }
            }
    
    
        }
    }
