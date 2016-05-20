using System;
using System.Collections.Generic;
using System.Linq;
using DynamicSurvey.Core.SessionStorage;

namespace DynamicSurvey.Bootstrapper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Application is starting.");

                // No parameters. No jobs
                if (!args.Any())
                {
                    Console.WriteLine("Appliction started without parameters. End of work.");
                    Console.ReadLine();
                    return;
                }

                // Make from parameters dictionary
                var parameters = new Dictionary<string, string>();

                foreach (var arg in args)
                {
                    var pos = arg.IndexOf('=');
                    if (pos != -1)
                    {
                        var key = arg.Substring(0, pos);
                        var value = arg.Substring(pos + 1);

                        parameters[key] = value;
                    }
                }

                // Take values of parameters from dictionary
                string connectionString, createDatabase;

                parameters.TryGetValue("connection_string", out connectionString);
                parameters.TryGetValue("create_database", out createDatabase);

                if (!string.IsNullOrEmpty(connectionString))
                {
                    PersistenceContext.SetConnectionString(connectionString);
                }

                // Create database
                if (!string.IsNullOrEmpty(createDatabase))
                {
                    if (createDatabase.ToLower().Trim() == "true")
                    {
                        if (!string.IsNullOrEmpty(connectionString))
                        {
                            Console.WriteLine("Creation database with the connection string:\n {0}", connectionString);

                            var dbCreator = new DbCreator(PersistenceContext.GetNHibernateConfiguration());
                            dbCreator.CreateSchema();

                            Console.WriteLine("Database creation is finished.");
                        }
                        else
                        {
                            Console.WriteLine("Error. Connection string not specified for database creation.");
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error. Try again.\n {0}", exception);
            }
        }
    }
}
