using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Configuration;
using NUnit.Framework;

namespace Sandbox
{
    public interface IFilter<T>
    {
        Func<T, bool> CompiledExpression { get; }
        
        Expression<Func<T, bool>> Expression { get; }
    }

    public abstract class AbstractFilter<T> : IFilter<T>
    {
        private Func<T, bool> compiledExpression;

        protected AbstractFilter()
        {
            CreateExpression();
        }

        public Func<T, bool> CompiledExpression
        {
            get
            {
                if (compiledExpression == null)
                    compiledExpression = Expression.Compile();

                return compiledExpression;
            }
        }

        public Expression<Func<T, bool>> Expression { get; protected set; }
        
        protected abstract void CreateExpression();
    }

    public class GreaterThan : AbstractFilter<int>
    {
        private readonly int value;

        public GreaterThan(int value)
        {
            this.value = value;
            
        }

        protected override void CreateExpression()
        {
            Expression = i => i > value;
        }
    }

    public static class FilterExtensions
    {
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> collection, IFilter<T> filter)
        {
            return collection.Where(filter.CompiledExpression);
        }

        public static IQueryable<T> Filter<T>(this IQueryable<T> collection, IFilter<T> filter)
        {
            return collection.Where(filter.Expression);
        }
    }

    [TestFixture]
    public class Tester
    {
        [Test]
        public void can_apply_filter()
        {
            var intArray = new [] { 1, 2, 3, 4, 5, 6, 7, 8 };
            var selectedValues = intArray.Filter(new GreaterThan(4));

            CollectionAssert.AreEquivalent(new [] { 5, 6, 7, 8 }, selectedValues);
        }
    }
}
