﻿using System.Collections.Generic;
using System.Linq;

namespace DynamicSurvey.Server.DAL.Repositories
{
	public  interface IFieldTypeRepository
	{
		string TextBox {get;}
		string Email {get;}
		string Checkbox {get;}
		string Button {get;}
		string RadioButton {get;}
		string GroupBox {get;}

		string DatePicker { get; }
		string DropdownList { get; }

		ulong GetIdOf(string value);
	}

	public class FieldTypeRepository : IFieldTypeRepository
	{
		private readonly Dictionary<ulong, string> fieldDict = new Dictionary<ulong, string>();

		public string TextBox { get { return GetFieldType("TextBox"); } }
		public string Email { get { return GetFieldType("Email"); } }
		public string Checkbox { get { return GetFieldType("Checkbox"); } }
		public string List { get { return GetFieldType("List"); } }
		public string Button { get { return GetFieldType("Button"); } }
		public string RadioButton { get { return GetFieldType("RadioButton"); } }
		public string GroupBox { get { return GetFieldType("GroupBox"); } }

		public string DatePicker { get { return GetFieldType("DatePicker"); } }
		public string DropdownList { get { return GetFieldType("DropdownList"); } }

		public FieldTypeRepository()
		{
			var fields = new List<string>();

			DataEngine.Engine.SelectFromView(DataEngine.vw_field_type_view, (r) =>
			{
				var id = (ulong)(r["Id"]);
				var fieldType = (string)r["FieldType"];

				fieldDict.Add(id, fieldType);
			});
		}
		private string GetFieldType(string value)
		{
			return fieldDict
				.Select(d => d.Value).Where(d => d.Equals(value))
				.Single();
		}

		public ulong GetIdOf(string value)
		{
			return fieldDict
				.Where(d => d.Value.Equals(value))
				.Single().Key;
		}

	}
}
