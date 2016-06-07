using System;
using System.Collections.Generic;
using DynamicSurvey.Server.DAL.Entities;
using DynamicSurvey.Server.DAL.Repositories;

namespace DynamicSurvey.Server.DAL.Fakes
{
	public class FakeSurveysFactory
	{

		private static readonly IFieldTypeRepository FieldType = new FieldTypeRepository();
		public static Survey[] CreateSurveyList()
		{
			return new Survey[]
			{
                EnglishSurvey.CreateEnglishSurvey(),
                MatrixSurvey.CreateMatrixSurvey()
			};
		}

	   
    }
}