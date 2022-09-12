using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kajo.Models.Exceptions
{
    public class ApiException: Exception
    {
        public static readonly int INVALID_BODY_CODE = 400;
        public static readonly int ODATA_ERROR = 400;
        public static readonly int NOT_FOUND_CODE = 404;
        public static readonly int UNKNOWN_ERROR_CODE = 500;
        public static readonly int INTERNAL_SERVER_ERROR_CODE = 500;
        public static readonly int REGISTERATION_FAILED_ERROR_CODE = 500;
        public static readonly int UPDATE_ROLE_FAILED_ERROR_CODE = 500;

        public int StatusCode { get; set; }

        public ApiException(string message, int statusCode)
        : base(message)
        {
            this.StatusCode = statusCode;
        }

        public ApiException(string message, int statusCode, Exception inner)
            : base(message, inner)
        {
            this.StatusCode = statusCode;
        }
    }
}
