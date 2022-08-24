using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Application.Common.Interface
{
    public interface ISessionUserService
    {
        Guid? UserId { get; }
        string? Username { get; }
    }
}