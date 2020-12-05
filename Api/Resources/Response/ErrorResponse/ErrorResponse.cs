using System;
using System.Collections.Generic;
using Api.Helpers;

namespace Api.Resources.Response
{
    public class ErrorResponse
    { 
        public string Message { get; private set; }
        public ApiReponseStatusCodes Status { get; private set; }

        public List<string> Errors { get; private set; }

        public ErrorResponse(List<string> messages, ApiReponseStatusCodes Status)
        {
            this.Errors = messages ?? new List<string>();
            this.Status = Status;
        }

        public ErrorResponse(List<string> messages, string message, ApiReponseStatusCodes Status)
        {
            this.Status = Status;
            this.Errors = messages ?? new List<string>();
            this.Message = message;

        }

        public ErrorResponse(string message, ApiReponseStatusCodes Status)
        {
            this.Status = Status;
            this.Message = message;
        }
    }
}
