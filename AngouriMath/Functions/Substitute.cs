﻿
/* Copyright (c) 2019-2020 Angourisoft
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation
 * files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy,
 * modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software
 * is furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
 * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
 * LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
 * CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using GenericTensor.Core;
using PeterO.Numbers;
using AngouriMath.Core;
using static AngouriMath.Entity.Number;

namespace AngouriMath
{
    partial record Entity
    {
        #region Simple
        partial record Sumf
        {
            public override Entity Substitute(Entity x, Entity value)
                => this == x ? value : New(Augend.Substitute(x, value), Addend.Substitute(x, value));
        }

        partial record Minusf
        {
            public override Entity Substitute(Entity x, Entity value)
                => this == x ? value : New(Subtrahend.Substitute(x, value), Minuend.Substitute(x, value));
        }

        partial record Mulf
        {
            public override Entity Substitute(Entity x, Entity value)
                => this == x ? value : New(Multiplier.Substitute(x, value), Multiplicand.Substitute(x, value));
        }

        partial record Divf
        {
            public override Entity Substitute(Entity x, Entity value)
                => this == x ? value : New(Dividend.Substitute(x, value), Divisor.Substitute(x, value));
        }

        partial record Sinf
        {
            public override Entity Substitute(Entity x, Entity value)
                => this == x ? value : New(Argument.Substitute(x, value));
        }

        partial record Cosf
        {
            public override Entity Substitute(Entity x, Entity value)
                => this == x ? value : New(Argument.Substitute(x, value));
        }

        partial record Tanf
        {
            public override Entity Substitute(Entity x, Entity value)
                => this == x ? value : New(Argument.Substitute(x, value));
        }

        partial record Cotanf
        {
            public override Entity Substitute(Entity x, Entity value)
                => this == x ? value : New(Argument.Substitute(x, value));
        }

        partial record Logf
        {
            public override Entity Substitute(Entity x, Entity value)
                => this == x ? value : New(Base.Substitute(x, value), Antilogarithm.Substitute(x, value));
        }

        partial record Powf
        {
            public override Entity Substitute(Entity x, Entity value)
                => this == x ? value : New(Base.Substitute(x, value), Exponent.Substitute(x, value));
        }

        partial record Arcsinf
        {
            public override Entity Substitute(Entity x, Entity value)
                => this == x ? value : New(Argument.Substitute(x, value));
        }

        partial record Arccosf
        {
            public override Entity Substitute(Entity x, Entity value)
                => this == x ? value : New(Argument.Substitute(x, value));
        }

        partial record Arctanf
        {
            public override Entity Substitute(Entity x, Entity value)
                => this == x ? value : New(Argument.Substitute(x, value));
        }

        partial record Arccotanf
        {
            public override Entity Substitute(Entity x, Entity value)
                => this == x ? value : New(Argument.Substitute(x, value));
        }

        partial record Factorialf
        {
            public override Entity Substitute(Entity x, Entity value)
                => this == x ? value : New(Argument.Substitute(x, value));
        }
        #endregion

        #region Local variable preserved
        partial record Set
        {
            partial record ConditionalSet
            {
                // TODO: it might be optimized
                public override Entity Substitute(Entity x, Entity value)
                {
                    if (this == x)
                        return value;
                    var replacement = Variable.CreateUnique(x + value + Predicate + Var, "temp");

                    // { x | x > a } -> { temp_1 | temp_1 > a }
                    var tempSubstituted = Predicate.Substitute(Var, replacement);

                    // a = 0 -> { temp_1 | temp_1 > a } -> { temp_1 | temp_1 > 0 }
                    // x = 0 -> { temp_1 | temp_1 > a } -> { temp_1 | temp_1 > a }
                    var subs = tempSubstituted.Substitute(x, value);

                    // { temp_1 | temp_1 > a } -> { x | x > a }
                    var postSubs = subs.Substitute(replacement, Var);

                    return New(postSubs, Var);
                }
            }
        }

        partial record Integralf
        {
            // TODO: it might be optimized
            public override Entity Substitute(Entity x, Entity value)
            {
                if (this == x)
                    return value;
                var replacement = Variable.CreateUnique(x + value + Expression + Var, "temp");

                // integrate(x ^ 2 + a, x) -> integrate(temp_1 ^ 2 + a, temp_1)
                var tempSubstituted = Expression.Substitute(Var, replacement);

                // a = 0 -> integrate(temp_1 ^ 2 + a, temp_1) -> integrate(temp_1 ^ 2 + 0, temp_1)
                // x = 0 -> integrate(temp_1 ^ 2 + a, temp_1) -> integrate(temp_1 ^ 2 + a, temp_1)
                var subs = tempSubstituted.Substitute(x, value);

                // integrate(temp_1 ^ 2 + a, temp_1) -> integrate(x ^ 2 + a, temp_1)
                var postSubs = subs.Substitute(replacement, Var);

                return New(postSubs, Var);
            }
        }

        partial record Derivativef
        {
            // TODO: it might be optimized
            public override Entity Substitute(Entity x, Entity value)
            {
                if (this == x)
                    return value;
                var replacement = Variable.CreateUnique(x + value + Expression + Var, "temp");

                // derive(x ^ 2 + a, x) -> derive(temp_1 ^ 2 + a, temp_1)
                var tempSubstituted = Expression.Substitute(Var, replacement);

                // a = 0 -> derive(temp_1 ^ 2 + a, temp_1) -> derive(temp_1 ^ 2 + 0, temp_1)
                // x = 0 -> derive(temp_1 ^ 2 + a, temp_1) -> derive(temp_1 ^ 2 + a, temp_1)
                var subs = tempSubstituted.Substitute(x, value);

                // derive(temp_1 ^ 2 + a, temp_1) -> derive(x ^ 2 + a, temp_1)
                var postSubs = subs.Substitute(replacement, Var);

                return New(postSubs, Var);
            }
        }

        #endregion
    }
}
