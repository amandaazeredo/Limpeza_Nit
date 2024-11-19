using VMA_API.Application.Exceptions;

namespace VMA_API.Application.Middleware
{
    public class ExceptionHandler(RequestDelegate next, ILogger<ExceptionHandler> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionHandler> _logger = logger;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Passa o request para o próximo middleware
                await _next(context);
            }
            catch (Exception ex)
            {
                // Log da exceção
                _logger.LogError($"Erro: {ex.Message}");

                // Tratamento da exceção
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;

            var response = new ErrorResponse()
            {
                StatusCode = context.Response.StatusCode,
                Message = "Ocorreu um erro interno no servidor.",
                DetailedMessage = exception.Message // Detalhe pode ser oculto em produção
            };

            // Retorna a resposta como JSON
            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
