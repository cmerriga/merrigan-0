using System;
using System.Collections.Generic;
using System.Diagnostics;
using Merrigan0.Internal.DotNet.Extensions;

namespace Merrigan0 {
    [WhatItIs("an attribute that marks a variable's value as being required to be a particular type")]
    [Untested]
    public class IsAttribute : ConstraintAttribute {
        [MayBeNull] ////(When = "!Null(typeNml)")]
        private Type type;

        [MayBeNull]////(When = "!Null(type)")]
        private string typeNml;

        public IsAttribute(Type type) {
            this.type = type;
        }

        public IsAttribute(string typeNml) {
            this.typeNml = typeNml;
        }

        public IsAttribute(Type subjectType, Type type)
            : base(subjectType.ToExpression()) {
            this.type = type;
        }

        public IsAttribute(Type subjectType, string typeNml)
            : base(subjectType.ToExpression()) {
            this.typeNml = typeNml;
        }

        public IsAttribute(string subjectNml, Type type)
            : base(subjectNml) {
            this.type = type;
        }

        public IsAttribute(string subjectNml, string typeNml)
            : base(subjectNml) {
                this.typeNml = typeNml;
        }

        protected override Expression CreateConstraint() {
            Expression newExpression = new IsExpression(
                SubjectExpression,
                type == null ? Nml.Expression(typeNml) : type.ToExpression());
            return newExpression;
        }
    }
}
