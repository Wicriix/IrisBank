using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iris.Shared.InputModels
{
    public class UserInputModel
    {
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
    }
}
