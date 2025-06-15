using Serilog;
using System.Diagnostics;
using System.Text;
using ILogger = Serilog.ILogger;

namespace OrderManagement.Api.Middlewares
{
    public class LogHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public LogHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
            _logger = Log.ForContext<LogHandlingMiddleware>();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            var request = context.Request;

            var requestBody = await ReadRequestBodyAsync(request);
            _logger.Information("HTTP {Method} {Path} | Body: {Body}",
                request.Method, request.Path, requestBody);

            try
            {
                var originalBodyStream = context.Response.Body;
                using var responseBody = new MemoryStream();
                context.Response.Body = responseBody;

                await _next(context);

                stopwatch.Stop();

                var responseText = await ReadResponseBodyAsync(context.Response);
                _logger.Information("Response {StatusCode} in {Elapsed}ms | Body: {Body}",
                    context.Response.StatusCode, stopwatch.ElapsedMilliseconds, responseText);

                context.Response.Body = originalBodyStream;
                responseBody.Seek(0, SeekOrigin.Begin);
                await responseBody.CopyToAsync(originalBodyStream);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();

                _logger.Error(ex, "Unhandled exception after {Elapsed}ms", stopwatch.ElapsedMilliseconds);

                throw;
            }
        }

        private async Task<string> ReadRequestBodyAsync(HttpRequest request)
        {
            request.EnableBuffering();
            using var reader = new StreamReader(
                request.Body,
                Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false,
                leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            request.Body.Position = 0;
            return body;
        }

        private async Task<string> ReadResponseBodyAsync(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return text;
        }
    }
}