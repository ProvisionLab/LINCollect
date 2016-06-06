using System;
using System.Collections.Generic;
using DynamicSurvey.Server.DAL.Entities;

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