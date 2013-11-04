using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using Dapper;
using FluentAssertions;
using NUnit.Framework;

namespace Sandbox
{
    [TestFixture]
    public class DapperExamples
    {
        private IDbConnection connection;
        private IEnumerable<Employee> employees;
            
        [SetUp]
        public void Before_each_test()
        {
            connection = new SqlConnection(@"Data Source=.\SQLExpress;Initial Catalog=zJunk;Integrated Security=True;");
            connection.Open();
        }

        [TearDown]
        public void After_each_test()
        {
            if (connection != null)
            {
                if (connection.State != ConnectionState.Closed)
                    connection.Close();

                connection.Dispose();
            }
        }

        [Test]
        public void Can_execute_a_stored_procedure()
        {
            var sqlParams = new
            {
                LowerBound = 50000m,
                UpperBound = 100000m
            };
            employees = connection.Query<Employee>("dbo.GetEmployeesBySalaryRange", sqlParams, commandType: CommandType.StoredProcedure);
            int numberOfEmployees = employees.Count();
            Debug.WriteLine("Number of employees: {0}", numberOfEmployees);
            numberOfEmployees.Should().BeGreaterThan(0);
        }

        [Test]
        public void Can_run_queries_inside_transactions()
        {
            using (var transaction = connection.BeginTransaction())
            {
                const string selectSql = "SELECT COUNT(*) FROM Employees WHERE Salary > 100000";
                int originalCount = connection.Query<int>(selectSql, transaction: transaction).Single();
                Debug.WriteLine("Original count: {0}", originalCount);

                const string updateSql = "UPDATE Employees SET Salary = Salary * 1.10";
                int rowCount = connection.Execute(updateSql, transaction: transaction);
                Debug.WriteLine("Row count: {0}", rowCount);

                int updatedCount = connection.Query<int>(selectSql, transaction: transaction).Single();
                Debug.WriteLine("Updated count: {0}", updatedCount);
                
                transaction.Rollback();                
            }
        }

        [Test]
        public void Can_query_for_all_employees()
        {
            employees = connection.Query<Employee>("Select * from Employees");
            int numberOfEmployees = employees.Count();
            Debug.WriteLine("Number of employees: {0}", numberOfEmployees);
        }

        public class Employee
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public decimal Salary { get; set; }
        }
    }
}
