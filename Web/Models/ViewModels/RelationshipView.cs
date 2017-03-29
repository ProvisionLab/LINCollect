using System.Collections.Generic;
using Web.Data;

namespace Web.Models.ViewModels
{
    public class RelationshipView : Entity
    {
        public int SurveyId { get; set; }

        public List<RelationshipItem> RelationshipItems { get; set; }
        public RelationshipItem SelectedItem { get; set; }
    }
}