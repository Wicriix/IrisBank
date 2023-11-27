using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iris.Shared.OutputModel
{
    public class UserAuthOutputModel
    {
        public string FirtsName { get; set; } = null!;
        public string? LastName { get; set; }
        public string UserName { get; set; } = null!;
        public string? Email { get; set; }
        public string? Token { get; set; }

    }
}
