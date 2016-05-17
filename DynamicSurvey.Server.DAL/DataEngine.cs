﻿using DynamicSurvey.Server.DAL.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSurvey.Server.DAL
{

	public class DataEngine
	{
		private static DataEngine engine;
		public static DataEngine Engine
		{
			get 
			{ 
				if (engine == null)
					engine = new DataEngine();
				return engine;
			}
		}

		public static readonly string sp_add_language_to_user = "sp_add_language_to_user";
		public static readonly string sp_add_survey_template = "sp_add_survey_template";
		public static readonly string sp_add_survey_template_field = "sp_add_survey_template_field";
		public static readonly string sp_add_survey_template_page = "sp_add_survey_template_page";
		public static readonly string sp_add_user = "sp_add_user";
		public static readonly string sp_add_field_default_value = "sp_add_field_default_value";

		public static readonly string sp_remove_field_default_value = "sp_remove_field_default_value";
		public static readonly string sp_remove_survey_template_page = "sp_remove_survey_template_page";
		public static readonly string sp_remove_survey_template_field = "sp_remove_survey_template_field";
		public static readonly string sp_remove_survey_template = "sp_remove_survey_template";

		public static readonly string sp_update_field_default_value = "sp_update_field_default_value";
		public static readonly string sp_update_survey_template = "sp_update_survey_template";
		public static readonly string sp_update_survey_template_field = "sp_update_survey_template_field";
		public static readonly string sp_update_survey_template_page = "sp_update_survey_template_page";
		public static readonly string sp_update_user = "sp_update_user";

		public static readonly string vw_field_type_view = "vw_field_type_view";
		public static readonly string vw_survey_template = "vw_survey_template";
		public static readonly string vw_survey_template_field_default_values = "vw_survey_template_field_default_values";
		public static readonly string vw_survey_template_fields = "vw_survey_template_fields";
		public static readonly string vw_survey_template_pages = "vw_survey_template_pages";
		public static readonly string vw_user = "vw_user";

#if LOCAL_DB
		private readonly string connectionString = "server=localhost;user id=CodeClient;password=In0Dd~uc!A55;persistsecurityinfo=True;database=dbsurveys;";
#else
		// gear.host deny access to create user
		private readonly string connectionString = "server=mysql2.gear.host;user id=dbsurveys;password=In0Dd~uc!A55;persistsecurityinfo=True;database=dbsurveys;";
#endif


		private readonly MySqlParameter resultIdParameter;
		private readonly MySqlParameter errorCodeParameter;

		public DataEngine()
		{
			resultIdParameter = new MySqlParameter("result_id", MySqlDbType.Int64)
			{
				Direction = ParameterDirection.Output
			};

			errorCodeParameter = new MySqlParameter("error_code", MySqlDbType.Int64)
			{
				Direction = ParameterDirection.Output
			};
		}

		private void ThrowIfError()
		{
			if (errorCodeParameter.Value.GetType() != typeof(DBNull))
			{
				var val = Convert.ToInt32(errorCodeParameter.Value);
				if (val != 0)
				{
					throw new InvalidOperationException(string.Format("Failed to perform operation. Error code = {0}", val));
				}
			}
		}

		private long GetLastInsertId()
		{
			if (errorCodeParameter.Value.GetType() != typeof(DBNull))
			{
				return Convert.ToInt64(resultIdParameter.Value);
			}
			else
			{
				return 0;
			}
		}

		public long ExecuteStoredProcedure(string procedureName, Action<MySqlCommand> fillParamsAction)
		{
			using (var connection = new MySqlConnection(connectionString))
			using (var cmd = new MySqlCommand(procedureName, connection))
			{
				cmd.CommandType = CommandType.StoredProcedure;

				fillParamsAction(cmd);
				cmd.Parameters.Add(resultIdParameter);
				cmd.Parameters.Add(errorCodeParameter);

				connection.Open();
				cmd.ExecuteNonQuery();
				connection.Close();

				ThrowIfError();
				return GetLastInsertId();
			}
		}

		public void SelectFromView(string viewName, Action<DataRow> processRowAction, string selectFilter = "*", string whereClause = "", Action<MySqlCommand> fillCommandAction = null)
		{
			using (var connection = new MySqlConnection(connectionString))
			{
				var query = new StringBuilder();
				query.AppendFormat("select {0} from {1}", selectFilter, viewName);
				if (!string.IsNullOrEmpty(whereClause))
				{
					if (fillCommandAction == null)
					{
						throw new InvalidOperationException(string.Format("invalid usage. you provided where clause = {0}, but fillCommandAction == null", whereClause));
					}
					query.Append(" ");
					query.Append(whereClause);
				}
				query.Append(";");

				try
				{
					using (var command = new MySqlCommand(query.ToString(), connection))
					{
						if (fillCommandAction != null)
						{
							fillCommandAction(command);
						}

						MySqlDataAdapter myAdapter = new MySqlDataAdapter();
						myAdapter.SelectCommand = command;

						DataSet myDataSet = new DataSet();
						myAdapter.Fill(myDataSet);

						var table = myDataSet.Tables[0];
						foreach (DataRow row in table.Rows)
						{
							processRowAction(row);
						}

						table.Dispose();
						myDataSet.Dispose();
						myAdapter.Dispose();

					}
				}
				catch(Exception ex)
				{

				}
			}
		}
	}
}