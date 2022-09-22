using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEditor;
using UnityEngine;

namespace SoftwareCore.Specification
{
    public interface ISpecification<T> {

        Expression<Func<T, bool>> Criteria { get; }

        Expression<Func<T, bool>> OrderBy { get; }

        Expression<Func<T, object>> GroupBy { get; }

        bool UseMaxResults { get; }

        int MaxResults { get; }

    }

}