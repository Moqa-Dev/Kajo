using Kajo.Models.Exceptions;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Kajo.Models.Dtos
{
    /// <summary>
    /// Represents Errors
    /// </summary>
    public class ErrorDto
    {
        /// <summary>
        /// Error Code
        /// </summary>
        [Key] //key added for ODATA edm models
        public int ErrorCode { get; set; }

        /// <summary>
        /// Custom Error Message
        /// </summary>
        public string Message { get; set; }

        public ErrorDto(ApiException exception)
        {
            this.Message = exception.Message;
            this.ErrorCode = exception.StatusCode;
        }

        public ErrorDto(string Message = "Unknown Error", int ErrorCode = 500)
        {
            this.Message = Message;
            this.ErrorCode = ErrorCode;
        }

        public override string ToString()
        {
            var modelString = JsonConvert.SerializeObject(this);
            return modelString;
        }
    }
}
