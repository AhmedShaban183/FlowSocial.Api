﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowSocial.Application.DTOs.Response
{
    public record LoginResponse(bool flag = false, string message = null!, string Token = null!, string RefreshToken = null!);
}
