﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSurvey.Server.DAL.Filters
{
	public class SurveyReportFilter
	{

		public long? SurveyId { get; set; }
		//long? SurveyTemplateId { get; set; }

		//long? RespondentId { get; set; }
		//long? EnumeratorId { get; set; }

		
		//string UserAnswerLike { get; set; } // like '%pattern%'
		//string CompanyNameLike { get; set; }
		//string CountryNameLike { get; set; }
		//string CityLike { get; set; }

		
		public long? PageId { get; set; }
		//long? GroupId { get; set; }
		public long? FieldId { get; set; }


		// hack:
		public string ToWhereClause()
		{
			if (IsAnyNotNull())
			{
				var clausesList = new List<string>();

				if (SurveyId.HasValue)
					clausesList.Add("SurveyId=@SurveyId");

				if (PageId.HasValue)
					clausesList.Add("PageId=@SurveyId");

				if (FieldId.HasValue)
					clausesList.Add("FieldId=@FieldId");


				var sb = new StringBuilder();
				sb.Append("WHERE ");
				sb.Append(clausesList.First());

				foreach (var clause in clausesList.Skip(1))
				{
					sb.AppendFormat("AND ", clause);
				}
			}
			return "";
		}

		public void FillCommand(MySqlCommand command)
		{
			if (IsAnyNotNull())
			{
				if (SurveyId.HasValue)
					command.Parameters.Add("@SurveyId", SurveyId.Value);

				if (PageId.HasValue)
					command.Parameters.Add("@SurveyId", PageId.Value);

				if (FieldId.HasValue)
					command.Parameters.Add("@FieldId", FieldId.Value);
			}
		}

		public bool IsAnyNotNull()
		{
			// TODO: reflection
			return SurveyId.HasValue;
		}
	}
}
