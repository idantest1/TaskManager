using Microsoft.EntityFrameworkCore;
using TaskManager.Logic.Exceptions;

namespace TaskManager.API.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BadRequestException ex)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(new { error = ex.Message });
            }
            catch (NotFoundException ex)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsJsonAsync(new { error = ex.Message });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "Concurrency issue");
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new { error = "A concurrency error occurred while accessing the database." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled error");
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new { error = "An unexpected error occurred." });
            }
        }
    }
}
