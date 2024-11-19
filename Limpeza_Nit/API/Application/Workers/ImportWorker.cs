
using System.Threading.Channels;
using API.Domain.Model;
using API.Domain.Service.Interface;

namespace API.Application.Workers
{
    public class ImportWorker(ILogger<ImportWorker> logger, 
                              Channel<ExcelInfo> channel,
                              IExcelImportService service) : BackgroundService
    {
        private readonly ILogger<ImportWorker> _logger = logger;
        private readonly Channel<ExcelInfo> _channel = channel;
        private readonly IExcelImportService _service = service;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker iniciado para processar planilhas.");

            await foreach (var excelInfo in _channel.Reader.ReadAllAsync(stoppingToken))
            {
                _logger.LogInformation("Iniciando o processamento...");

                _service.ProcessExcelFile(excelInfo.Stream, excelInfo.FileName);

                _logger.LogInformation("Processamento da planilha concluído.");
            }
        }
    }
}
