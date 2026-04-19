using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Exceptions
{
    public class InternalServerException : AppException
    {
        public InternalServerException(string message):base(message, StatusCodes.Status500InternalServerError,
               "INTERNAL_SERVER_ERROR")
        {
            
        }
    }
}
