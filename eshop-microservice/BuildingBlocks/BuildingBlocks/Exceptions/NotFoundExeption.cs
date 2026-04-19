using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Exceptions
{
    public class NotFoundExeption : AppException
    {
        public NotFoundExeption(string resourceName, object key) : base($"{resourceName} with key '{key}' was not found.",
               StatusCodes.Status404NotFound,
               "NOT_FOUND")
        {            
        }
    }
}
