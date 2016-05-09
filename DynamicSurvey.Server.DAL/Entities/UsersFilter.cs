using DynamicSurvey.Server.DAL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSurvey.Server.DAL.Entities
{
    public class UsersFilter
    {
        public string UsernameSearchPattern { get; private set; }
        public AccessRight TargetAccessRight { get; private set; }

        public IPager Pager { get; private set; }

        public UsersFilter()
        {
            Pager = new Pager();
            Pager.PageSize = int.MaxValue;
            TargetAccessRight = new AccessRight();
            UsernameSearchPattern = "";
        }

        public UsersFilter(int pageIndex, int pageSize) : this()
        {
            Pager.CurrentPage = pageIndex;
            Pager.PageSize = pageSize;
        }
    }
}
