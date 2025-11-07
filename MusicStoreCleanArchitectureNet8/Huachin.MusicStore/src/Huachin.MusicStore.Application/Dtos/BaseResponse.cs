namespace Huachin.MusicStore.Application.Dtos
{
	public class BaseResponse<T>
	{
		public bool IsSuccess { get; set; }
		public string ErrorMessage { get; set; } = string.Empty;
		public string Message { get; set; } = string.Empty;
		public string? ErrorCode { get; set; }
		public T? Result { get; set; }

		public static BaseResponse<T> Success(T data, string message = "Operation completed success")
		{
			return new BaseResponse<T>
			{
				IsSuccess = true,
				Message = message,
				Result = data,
			};
		}
		public static BaseResponse<T> Failure(string errorMessage, string? errorCode = null)
		{
			return new BaseResponse<T>
			{
				IsSuccess = false,
				ErrorCode = errorCode,
				ErrorMessage = errorMessage,
			};
		}
	}
}
