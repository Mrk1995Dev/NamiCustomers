using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NamiCustomers.Abstractions.Dtos.Account
{
    public class TwoFactorLoginDto
    {
        [Required]
        public string Code { get; set; }
        public bool IsPersistent { get; set; }
        public string Provider { get; set; }
    }
}
