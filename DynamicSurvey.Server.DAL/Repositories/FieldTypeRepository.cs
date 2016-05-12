using System.Linq;
using System.Collections.Generic;
using System;

namespace DynamicSurvey.Server.DAL.Entities
{
	public interface IFieldTypeRepository
	{
		string TextBox {get;}
		string Email {get;}
		string Checkbox {get;}
		string List {get;}
		string Button {get;}
		string RadioButton {get;}
		string GroupBox {get;}
	}

	public class FieldTypeRepository : IFieldTypeRepository
	{
		private readonly Dictionary<string, string> fieldDict = new Dictionary<string, string>();

		public string TextBox { get { return GetFieldType("TextBox"); } }
		public string Email { get { return GetFieldType("Email"); } }
		public string Checkbox { get { return GetFieldType("Checkbox"); } }
		public string List { get { return GetFieldType("List"); } }
		public string Button { get { return GetFieldType("Button"); } }
		public string RadioButton { get { return GetFieldType("RadioButton"); } }
		public string GroupBox { get { return GetFieldType("GroupBox"); } }

		private string GetFieldType(string key)
		{
			if (!fieldDict.ContainsKey(key))
			{
				using (var dbContext = new DbSurveysContext())
				{
					var fields = dbContext.survey_field_type.ToArray();

					Action<string> addKey = (s) =>
					{
						fieldDict.Add(s, fields.Single(f => f.field_type.Equals(s, StringComparison.InvariantCultureIgnoreCase)).field_type);
					};

					addKey("TextBox");
					addKey("Email");
					addKey("Checkbox");
					addKey("List");
					addKey("Button");
					addKey("RadioButton");
					addKey("GroupBox");
					addKey("DatePicker");
					addKey("DropdownList");
				}
			}

			return fieldDict[key];
		}

	}
}
