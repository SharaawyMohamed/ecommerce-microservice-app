using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;

namespace Discount.API.Middlewares
{
	public class ValidationExceptionMiddleware
	{
		private readonly RequestDelegate _next;

		public ValidationExceptionMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (ValidationException ex)
			{
				context.Response.ContentType = "application/json";
				context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

				var errors = ex.Errors.Select(e => new
				{
					Field = e.PropertyName,
					Error = e.ErrorMessage
				});

				var response = new
				{
					StatusCode = (int)HttpStatusCode.BadRequest,
					Message = "Validation Failed",
					Errors = errors
				};

				var jsonResponse = JsonSerializer.Serialize(response);
				await context.Response.WriteAsync(jsonResponse);
			}
		}
	}
}