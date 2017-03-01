using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Managers.Base.Interfaces
{
    public interface IObjectMapper
    {
        TTo Map<TFrom, TTo>(TFrom value);
    }
}