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
