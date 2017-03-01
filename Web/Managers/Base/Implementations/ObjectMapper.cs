using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Managers.Base.Interfaces;

namespace Web.Managers.Base.Implementations
{
    public class ObjectMapper: IObjectMapper
    {
        public TTo Map<TFrom, TTo>(TFrom value)
        {
            return AutoMapper.Mapper.Map<TFrom, TTo>(value);
        }
    }
}