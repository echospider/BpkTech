using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BpkTech.Core
{
    public class ApiResult<T>
    {
        public ApiResult(T result, List<ErrorList> errors, bool success = true, int statusCode = 0, int resultCount = 0, int draw = 1, int recordsTotal = 0, int recordsFiltered = 0)
        {
            Result = result;
            Success = success;
            StatusCode = statusCode;
            Errors = errors;
            ResultCount = resultCount;
            Draw = draw;
            RecordsTotal = recordsTotal;
            RecordsFiltered = recordsFiltered;
        }

        public bool Success { get; set; }
        public List<ErrorList> Errors { get; set; }

        public int StatusCode { get; set; }
        public T Result { get; set; }
        public int ResultCount { get; set; }
        public int Draw { get; set; }
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
    }
    public class ErrorList
    {
        public string ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
        public string ErrorMessage { get; set; }
    }
}
