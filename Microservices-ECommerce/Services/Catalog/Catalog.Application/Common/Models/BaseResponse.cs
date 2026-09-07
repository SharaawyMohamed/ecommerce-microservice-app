using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.Common.Models
{
	public class BaseResponse<T>
	{
		public bool IsSuccess { get; init; }
		public string? Message { get; init; }
		public T? Data { get; init; }
		public IReadOnlyList<string> Errors { get; init; } = [];

		private BaseResponse()
		{
		}

		public static BaseResponse<T> Success(T data, string? message = null)
		{
			return new BaseResponse<T>
			{
				IsSuccess = true,
				Data = data,
				Message = message
			};
		}

		public static BaseResponse<T> Failure(string message, IEnumerable<string>? errors = null)
		{
			return new BaseResponse<T>
			{
				IsSuccess = false,
				Message = message,
				Errors = errors?.ToList() ?? []
			};
		}
	}
}
