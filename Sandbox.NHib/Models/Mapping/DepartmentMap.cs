using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Sandbox.NHib.Models.Mapping
{
    public class DepartmentMap : ClassMapping<Department>
    {
        public DepartmentMap()
        {
            Id(x => x.Id, map =>
            {
                map.Generator(Generators.HighLow, gen =>
                {
                    gen.Params(new
                    {
                        table = "nh_hilo",
                        max_lo = 10,
                        where = "table_name = 'department'"
                    });
                });
            });
            Property(x => x.Name, map =>
            {
                map.Column("name");
            });
        }
    }
}
