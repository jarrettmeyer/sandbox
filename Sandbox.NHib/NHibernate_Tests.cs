using System.Linq;
using FluentAssertions;
using NHibernate;
using NUnit.Framework;
using Sandbox.NHib.Models;

namespace Sandbox.NHib
{
    [TestFixture]
    public class NHibernate_Tests
    {
        private SampleConfiguration sampleConfiguration;
        private ISession session;
        private ISessionFactory sessionFactory;

        [SetUp]
        public void before_each_test()
        {
            sampleConfiguration = new SampleConfiguration();
            sampleConfiguration.Configure();
            sessionFactory = sampleConfiguration.SessionFactory;
            session = sessionFactory.OpenSession();
        }

        [TearDown]
        public void after_each_test()
        {
            if (session != null)
                session.Dispose();

            sessionFactory.Statistics.Clear();
        }

        [TestFixtureTearDown]
        public void after_all_tests()
        {
            using (session = sampleConfiguration.SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    DeleteAllEmployees();
                    DeleteAllDepartments();
                    transaction.Commit();
                }
            }

            sampleConfiguration.Reset();
        }

        [Test]
        public void can_create_instance()
        {
            var config = new SampleConfiguration();
            config.Should().BeAssignableTo<SampleConfiguration>();
        }

        [Test]
        public void can_create_configuration_instance()
        {
            sampleConfiguration.Configuration.Should().BeAssignableTo<NHibernate.Cfg.Configuration>();
        }

        [Test]
        [TestCase("Sandbox.NHib.Models.Department", Result = true)]
        [TestCase("Sandbox.NHib.Models.Employee", Result = true)]
        public bool should_include_mapped_types(string className)
        {
            var persistentClasses = sampleConfiguration.Configuration.ClassMappings;
            bool isClassMapped = persistentClasses.Any(pc => pc.EntityName == className);
            isClassMapped.Should().BeTrue();
            return isClassMapped;
        }

        [Test]
        public void can_open_a_new_session()
        {
            session = sessionFactory.OpenSession();
            session.Should().BeAssignableTo<ISession>();
        }

        [Test]
        public void adding_a_department_should_set_the_id_on_save()
        {
            using (var transaction = session.BeginTransaction())
            {
                var department = new Department { Name = "Human Resources" };
                session.Save(department);

                // The ID has been set, but the record hasn't actually been saved.
                // This is the HiLo in action!
                department.Id.Should().BeGreaterThan(0);
                sessionFactory.Statistics.EntityInsertCount.Should().Be(0);

                transaction.Rollback();
            }            
        }

        [Test]
        public void entity_is_actually_saved_after_a_commit()
        {
            using (var transaction = session.BeginTransaction())
            {
                var department = new Department { Name = "Human Resources" };
                session.Save(department);
                transaction.Commit();                
                sessionFactory.Statistics.EntityInsertCount.Should().Be(1);
            } 
        }

        private void DeleteAllDepartments()
        {
            var departments = session.All<Department>();
            foreach (var department in departments)
            {
                session.Delete(department);
            }
        }

        private void DeleteAllEmployees()
        {
            var employees = session.All<Employee>();
            foreach (var employee in employees)
            {
                session.Delete(employee);
            }
        }
    }
}
