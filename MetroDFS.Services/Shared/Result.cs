using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MetroDFS.Services.Shared
{
    public class Result<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }

        public static Result<T> Success(T Data)
        {
            return new Result<T>()
            {
                Data = Data,
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK
            };
        }

        public static Result<T>Failure(string message)
        {
            return new Result<T>()
            {
                Message = message,
                StatusCode = HttpStatusCode.BadRequest
            };
        }
    }
}
