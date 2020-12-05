﻿using System;
using Api.Helpers;
namespace Api.Resources.Response
{
    public class ApiResponse<T>
    {
        public ApiReponseStatusCodes Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}