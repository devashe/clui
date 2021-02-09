using System;
using System.Threading.Tasks;

namespace DeVashe.CluiLib
{
    public class InputRequest
    {
        public InputRequest(string message, Func<string, Task<InputRequest>> responseHandler)
        {
            Message = message;
            ResponseHandler = responseHandler;
        }

        public string Message { get; set; }
        public Func<string, Task<InputRequest>> ResponseHandler { get; set; }
    }
}