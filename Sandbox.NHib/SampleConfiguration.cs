using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Context;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using Sandbox.NHib.Models.Mapping;

namespace Sandbox.NHib
{
    public class SampleConfiguration : IDisposable
    {
        private static Configuration configuration;
        private static string connectionString = @"Data Source=SampleDB.sdf";        
        private static ISessionFactory sessionFactory;

        public Configuration Configuration
        {
            get { return configuration; }
        }

        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }

        public ISessionFactory SessionFactory
        {
            get
            {
                if (sessionFactory == null)
                {
                    sessionFactory = configuration.BuildSessionFactory();                    
                }
                return sessionFactory;
            }
        }

        public void Configure()
        {
            if (configuration == null)
                BuildConfiguration();
        }

        public void Dispose()
        {
            if (sessionFactory != null)
                sessionFactory.Dispose();
        }

        /// <summary>
        /// Testing endpoint.
        /// </summary>
        public void Reset()
        {
            sessionFactory = null;
            configuration = null;
        }

        private static void BuildConfiguration()
        {
            configuration = new Configuration();
            configuration.DataBaseIntegration(db =>
            {
                db.ConnectionString = connectionString;
                db.Dialect<MsSqlCe40Dialect>();
                db.Driver<SqlServerCeDriver>();
                db.BatchSize = 50;
                db.Timeout = 20;

                // Incredibly useful for debugging.                
                db.LogFormattedSql = true;
                db.LogSqlInConsole = true;
                db.AutoCommentSql = true;
            });

            // Specific session factory configuration.
            var sessionFactoryConfiguration = configuration.SessionFactory();
            sessionFactoryConfiguration.GenerateStatistics();

            // Save the session in the thread.
            // This lets us use SessionFactory.GetCurrentSession() method.
            configuration.CurrentSessionContext<ThreadStaticSessionContext>();

            // Add our maps to the configuration
            var modelMapper = GetModelMapper();
            var mapping = GetHibernateMapping(modelMapper);
            configuration.AddMapping(mapping);

        }

        private static HbmMapping GetHibernateMapping(ModelMapper modelMapper)
        {
            var mapping = modelMapper.CompileMappingForAllExplicitlyAddedEntities();
            return mapping;
        }

        private static ModelMapper GetModelMapper()
        {
            var modelMapper = new ModelMapper();
            var exportedTypes = new List<Type>(typeof(DepartmentMap).Assembly.GetExportedTypes());
            modelMapper.AddMappings(exportedTypes);
            return modelMapper;
        }
    }
}
