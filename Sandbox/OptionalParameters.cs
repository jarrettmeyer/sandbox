using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using FluentAssertions;
using NUnit.Framework;

namespace Sandbox
{
    [TestFixture]
    public class OptionalParameters
    {
        private const string CONNECTION_STRING = @"Data Source=.\SQLExpress;Initial Catalog=zJunk;Integrated Security=true;";
        private const string SQL = @"SELECT * FROM Employees";

        private IDbCommand command;
        private IDbConnection connection;
        private IDataReader dataReader;
        private List<dynamic> objects;

        [Test]
        public void Can_have_a_default_optional_parameter()
        {
            objects = new List<dynamic>();

            using (connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                command = connection.CreateCommand();
                command.CommandText = SQL;
                command.CommandType = CommandType.Text;

                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    dynamic employee = new
                    {
                        Id = GetColumnValue<int>(dataReader, "ID", 0),
                        FirstName = GetColumnValue<string>(dataReader, "FirstName"),
                        LastName = GetColumnValue<string>(dataReader, "LastName")
                    };
                    objects.Add(employee);
                }

                connection.Close();
            }

            int count = objects.Count;
            Debug.WriteLine("Count: {0}", count);
            count.Should().BeGreaterThan(0);
        }

        public static T GetColumnValue<T>(IDataReader dataReader, string columnName, T valueIfDBNull = default(T))
        {
            object columnValue = dataReader[columnName];
            if (columnValue != DBNull.Value)
                return (T)columnValue;

            return valueIfDBNull;
        }
    }
}
