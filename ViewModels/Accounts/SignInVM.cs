using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels.Accounts
{
    public class SignInVM
    {
        [Required, DataType(DataType.EmailAddress)]
        public string email { get; set; }
        [Required, DataType(DataType.Password)]
        public string password { get; set; }
        public bool IsPersistent { get; set; }
    }
}
