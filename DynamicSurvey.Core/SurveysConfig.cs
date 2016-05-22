using System.Configuration;

namespace DynamicSurvey.Core
{
    public static class SurveysConfig
    {
        public static string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["Surveys"].ConnectionString; }
        }
    }
}
