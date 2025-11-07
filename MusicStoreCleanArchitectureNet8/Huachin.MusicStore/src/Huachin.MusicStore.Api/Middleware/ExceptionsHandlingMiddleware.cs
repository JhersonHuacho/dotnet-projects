using Huachin.MusicStore.Application.Dtos;
using Huachin.MusicStore.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace Huachin.MusicStore.Api.Middleware
{
	public class ExceptionsHandlingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionsHandlingMiddleware> _logger;

		public ExceptionsHandlingMiddleware(RequestDelegate next, ILogger<ExceptionsHandlingMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (DomainException ex)
			{
				_logger.LogWarning(ex, "A domain exception occurred while processing the request: {Message}", ex.Message);
				await HandleExceptionAsync(context, ex.Message, "DOMAIN_ERROR", HttpStatusCode.BadRequest);
			}
			catch (ApplicationException ex)
			{
				_logger.LogWarning(ex, "Application exception occurred while processing the request: {Message}", ex.Message);
				await HandleExceptionAsync(context, ex.Message, "APP_ERROR", HttpStatusCode.BadRequest);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An unhandled exception occurred while processing the request: {Message}", ex.Message);
				await HandleExceptionAsync(context, "An unexpected error occurred. Please try again later.", "SERVER_ERROR", HttpStatusCode.InternalServerError);
			}
		}

		public async Task HandleExceptionAsync(HttpContext context, string errorMessage, string errroCode, HttpStatusCode statusCode)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)statusCode;

			var response = BaseResponse<string>.Failure(errorMessage, errroCode);

			var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase
			});

			await context.Response.WriteAsync(json);
		}
	}

	public static class ExceptionsHandlingMiddlewareExtensions
	{
		public static IApplicationBuilder UseExceptionsHandlingMiddleware(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<ExceptionsHandlingMiddleware>();
		}
	}
}
