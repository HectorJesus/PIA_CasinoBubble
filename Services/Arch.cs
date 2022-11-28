//namespace CasinoBubble.Services
//{
//    public class Arch : IHostedService
//    {
//        private readonly IWebHostEnvironment env;
//        private readonly string nombreArchivo = "ArchivoInicio.txt";
//        private Timer timer;
//        public Arch(IWebHostEnvironment env)
//        {
//            this.env = env;
//        }
//        public Task StartAsync(CancellationToken cancellationToken)
//        {
//            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
//            Escribir("Proceso Iniciado");
//            return Task.CompletedTask;
//        }

//        public Task StopAsync(CancellationToken cancellationToken)
//        {
//            timer.Dispose();
//            Escribir("Proceso Finalizado");
//            return Task.CompletedTask;
//        }

//        private void DoWork(object state)
//        {
//            Escribir("Proceso en ejecucion: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
//        }
//        private void Escribir(string msg)
//        {
//            var ruta = $@"{env.ContentRootPath}\wwwroot\{nombreArchivo}";
//            using (StreamWriter writer = new StreamWriter(ruta, append: true)) { writer.WriteLine(msg); }
//        }
//    }
//}