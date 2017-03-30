using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models.DTO;

namespace Web.Models.ViewModels
{
    public class PollResultView
    {
        public Guid UserLinkId { get; set; }
        public ResultRespondentModel AboutYouBefore { get; set; }
        public ResultRespondentModel AboutYouAfter { get; set; }
        public List<ResultRelationShipModel> Items { get; set; }
    }
}