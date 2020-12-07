using System;
using System.Collections.Generic;
using Api.Helpers;

namespace Api.Communication.Response
{
    public class ApiResponseList<T>
    {

        public ApiReponseStatusCodes Status { get; set; }
        public string Message { get; set; }
        public List<T> Data { get; set; }
        
    }
}
