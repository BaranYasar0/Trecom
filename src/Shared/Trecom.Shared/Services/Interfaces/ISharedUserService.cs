﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trecom.Shared.Services.Interfaces;

public interface ISharedUserService
{
    public Task<string> GetUserIdAsync();

}