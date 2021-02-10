using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Error
{
    public class ErrorDetails
    {
        public ErrorDetails()
        {

        }

        public ErrorDetails(int StatusCode, string Message)
        {
            this.StatusCode = StatusCode;
            this.Message = Message;
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
