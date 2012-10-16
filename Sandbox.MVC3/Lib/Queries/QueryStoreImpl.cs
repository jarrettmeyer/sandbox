using System.Collections.Generic;
using System.Linq;

namespace Sandbox.MVC.Lib.Queries
{
    public class QueryStoreImpl : IQueryStore
    {
        private readonly IEnumerable<IQuery> queries;

        public QueryStoreImpl(IEnumerable<IQuery> queries)
        {
            this.queries = queries ?? new List<IQuery>();
        }

        public TQuery GetQuery<TQuery>() where TQuery : IQuery
        {
            var query = queries.First(q => q is TQuery);
            return (TQuery)query;
        }
    }
}