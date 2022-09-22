using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEditor;
using UnityEngine;

namespace SoftwareCore.Specification
{
    public class Specification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Criteria { get; set; }

        public Expression<Func<T, bool>> OrderBy { get; set; }

        public Expression<Func<T, object>> GroupBy { get; set; }

        public bool UseMaxResults { get; set; }

        public int MaxResults { get; set; }

    }

}
