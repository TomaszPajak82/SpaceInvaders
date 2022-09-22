using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEditor;
using UnityEngine;

namespace SoftwareCore.Specification
{
    public class MaxResultsSpecification<T> : Specification<T> {

        public MaxResultsSpecification(int maxResults) {
            this.UseMaxResults = true;
            this.MaxResults = maxResults;
        }

    }

}