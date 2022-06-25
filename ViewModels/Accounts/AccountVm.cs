using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels.Accounts
{
    public class AccountVm
    {[Required]
        public string name { get; set; }
        [Required,DataType(DataType.EmailAddress)]
        public string email { get; set; }
        [Required, DataType(DataType.Password)]
        public string password { get; set; }
        [Required, DataType(DataType.Password),Compare(nameof(password))]
        public string checkpassword { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}
