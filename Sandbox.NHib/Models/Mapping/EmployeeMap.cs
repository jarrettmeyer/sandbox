using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Sandbox.NHib.Models.Mapping
{
    public class EmployeeMap : ClassMapping<Employee>
    {
        public EmployeeMap()
        {
            Id(x => x.Id, map =>
            {
                map.Generator(Generators.HighLow, gen =>
                {
                    gen.Params(new
                    {
                        table = "nh_hilo",
                        max_lo = 10,
                        where = "table_name = 'employee'"
                    });
                });
            });
            Property(x => x.FirstName, map =>
            {
                map.Column("first_name");
            });
            Property(x => x.LastName, map =>
            {
                map.Column("last_name");
            });
            Property(x => x.DateOfHire, map =>
            {
                map.Column("date_of_hire");
            });
            Property(x => x.SSN, map =>
            {
                map.Column("ssn");
            });
            ManyToOne(x => x.CurrentDepartment, map =>
            {
                map.Column("current_department_id");
            });
        }
    }
}
