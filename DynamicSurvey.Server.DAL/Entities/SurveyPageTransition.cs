using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSurvey.Server.DAL.Entities
{
	public class SurveyPageTransition
	{
        public int Id { get; set; }
		public int? NextPageId { get; set; }

		public List<ConditionBase> Conditions { get; set; }

		public SurveyPageTransition()
		{
			Conditions = new List<ConditionBase>();
		}

		public static List<SurveyPageTransition> CreateSimple(int id, int nextDefaultPageId)
		{
			return new List<SurveyPageTransition>()
			{
				new SurveyPageTransition()
				{
                    Id = id,
					NextPageId = nextDefaultPageId
				}
			};
		}

		public static List<SurveyPageTransition> CreateFinal(int id)
		{
			return new List<SurveyPageTransition>()
			{
				new SurveyPageTransition()
				{
                    Id= id,
					NextPageId = null
				}
			};
		} 
	}

}
