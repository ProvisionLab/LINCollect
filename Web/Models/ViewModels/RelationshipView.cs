using System.Collections.Generic;
using Web.Models.DTO;

namespace Web.Models.ViewModels
{
    public class RelationshipView : Entity
    {
        public int SurveyId { get; set; }

        public List<RelationshipItemModel> RelationshipItems { get; set; }
        public RelationshipItemModel SelectedItem { get; set; }
    }
}