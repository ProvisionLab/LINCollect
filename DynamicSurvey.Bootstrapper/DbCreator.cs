using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace DynamicSurvey.Bootstrapper
{
    public class DbCreator
    {
        private readonly Configuration _nhConfiguration;

        public DbCreator(Configuration nhConfiguration)
        {
            _nhConfiguration = nhConfiguration;
        }

        public void CreateSchema()
        {
            var schemaExport = new SchemaExport(_nhConfiguration);
            schemaExport.Execute(false, true, false);
        }
    }
}
