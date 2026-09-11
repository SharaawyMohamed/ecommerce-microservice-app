using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.Common.Models
{
	public class BaseResponse
	{
		public bool IsSuccess { get; init; }
		public string? Message { get; init; }
		public object? Data { get; init; }
		public IReadOnlyList<string> Errors { get; init; } = [];

		private BaseResponse()
		{
		}

		public static BaseResponse Success(object? data, string? message = null)
		{
			return new BaseResponse
			{
				IsSuccess = true,
				Data = data,
				Message = message
			};
		}

		public static BaseResponse Failure(string message, IEnumerable<string>? errors = null)
		{
			return new BaseResponse
			{
				IsSuccess = false,
				Message = message,
				Errors = errors?.ToList() ?? []
			};
		}
	}
}
