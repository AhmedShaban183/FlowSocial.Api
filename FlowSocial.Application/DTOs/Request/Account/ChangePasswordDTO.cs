﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowSocial.Application.Common.DTO.Request.Account
{
    public class ChangePasswordDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public string NewPassword { get; set; }
    }
}
