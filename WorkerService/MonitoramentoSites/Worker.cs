using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.Versioning;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MonitoramentoSites
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ServiceConfigurations _serviceConfigurations;
        private readonly JsonSerializerOptions _jsonOptions;
        private static readonly string _versaoFramework;

        static Worker()
        {
            _versaoFramework = Assembly
               .GetEntryAssembly()?
               .GetCustomAttribute<TargetFrameworkAttribute>()?
               .FrameworkName;
        }

        public Worker(ILogger<Worker> logger,
            IConfiguration configuration)
        {
            _logger = logger;

            _serviceConfigurations = new ServiceConfigurations();
            new ConfigureFromConfigurationOptions<ServiceConfigurations>(
                configuration.GetSection("ServiceConfigurations"))
                    .Configure(_serviceConfigurations);

            _jsonOptions = new JsonSerializerOptions()
            {
                IgnoreNullValues = true
            };
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                _logger.LogInformation("Process Id: {id} - Worker executando em: {time}",
                    Process.GetCurrentProcess().Id,
                    DateTimeOffset.Now);

                foreach (string host in _serviceConfigurations.Hosts)
                {
                    _logger.LogInformation(
                        $"Verificando a disponibilidade do host {host}");

                    var resultado = new ResultadoMonitoramento();
                    resultado.Framework = _versaoFramework;
                    resultado.Horario =
                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    resultado.Host = host;

                    // Verifica a disponibilidade efetuando um ping
                    // no host que foi configurado em appsettings.json
                    try
                    {
                        using (Ping p = new Ping())
                        {
                            var resposta = p.Send(host);
                            resultado.Status = resposta.Status.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        resultado.Status = "Exception";
                        resultado.Exception = ex.Message;
                    }

                    string jsonResultado =
                        JsonSerializer.Serialize(resultado, _jsonOptions);

                    if (resultado.Exception == null)
                        _logger.LogInformation(jsonResultado);
                    else
                        _logger.LogError(jsonResultado);
                }

                await Task.Delay(
                    _serviceConfigurations.Intervalo, stoppingToken);
            }
        }
    }
}