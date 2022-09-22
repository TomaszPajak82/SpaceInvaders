
using System.Linq;

namespace SoftwareCore.Specification
{


    public static class SpecificationEvaluator
    {

        public static IQueryable<T> Evaluate<T>(IQueryable<T> querry, ISpecification<T> specification) {

            IQueryable<T> newQuery = querry;

            if (specification.Criteria != null) {
                newQuery = querry.Where(specification.Criteria);
            }

            if (specification.OrderBy != null) {
                newQuery = newQuery.OrderBy(specification.OrderBy);
            }

            if (specification.GroupBy != null) {
                newQuery = newQuery.GroupBy(specification.GroupBy).SelectMany(x => x);
            }

            if (specification.UseMaxResults) {
                newQuery = newQuery.Take(specification.MaxResults);
            }

            return newQuery;
        }

    }

}