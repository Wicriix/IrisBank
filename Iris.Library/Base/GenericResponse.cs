using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iris.Commons.Library.Base
{
    public class GenericResponse
    {
        public bool OperationSuccess { get; set; }
        public object? ObjectResponse { get; set; }
        public string? SuccessMessage { get; set; }
        public string? ErrorMessage { get; set; }
        public DateTime OperationDate { get; set; }

        public GenericResponse()
        {
            OperationSuccess = true;
            SuccessMessage = string.Empty;
            ErrorMessage = string.Empty;
            OperationDate = DateTime.UtcNow;
        }

        public GenericResponse(object objectResponse)
        {
            ObjectResponse = objectResponse;
            OperationSuccess = true;
            SuccessMessage = string.Empty;
            ErrorMessage = string.Empty;
            OperationDate = DateTime.UtcNow;
        }
    }
}
