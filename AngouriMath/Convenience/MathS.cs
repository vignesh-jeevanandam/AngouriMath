﻿using AngouriMath.Core;
using AngouriMath.Core.FromString;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Linq.Expressions;
using AngouriMath.Core.FromLinq;
using AngouriMath.Core.TreeAnalysis;
using AngouriMath.Core.TreeAnalysis.Division.RationalDiv;
using AngouriMath.Functions.NumberSystem;

namespace AngouriMath
{
    /// <summary>
    /// Use functions from this class
    /// </summary>

    public static partial class MathS
    {
        /// <summary>
        /// https://en.wikipedia.org/wiki/Trigonometric_functions
        /// </summary>
        /// <param name="a"></param>
        /// <returns>
        /// A / C
        /// </returns>
        public static Entity Sin(Entity a) => Sinf.Hang(a);

        /// <summary>
        /// https://en.wikipedia.org/wiki/Trigonometric_functions
        /// </summary>
        /// <param name="a"></param>
        /// <returns>
        /// B / C
        /// </returns>
        public static Entity Cos(Entity a) => Cosf.Hang(a);

        /// <summary>
        /// https://en.wikipedia.org/wiki/Logarithm
        /// </summary>
        /// <param name="num"></param>
        /// <param name="base_"></param>
        /// <returns></returns>
        public static Entity Log(Entity num, Entity base_) => Logf.Hang(num, base_);

        /// <summary>
        /// https://en.wikipedia.org/wiki/Power_function
        /// </summary>
        /// <param name="base_"></param>
        /// <param name="power"></param>
        /// <returns></returns>
        public static Entity Pow(Entity base_, Entity power) => Powf.Hang(base_, power);

        /// <summary>
        /// Special case of power function
        /// </summary>
        /// <param name="a"></param>
        /// <returns>
        /// a ^ 0.5
        /// </returns>
        public static Entity Sqrt(Entity a) => Powf.Hang(a, 0.5);

        /// <summary>
        /// Special case of power function
        /// </summary>
        /// <param name="a"></param>
        /// <returns>
        /// a ^ 2
        /// </returns>
        public static Entity Sqr(Entity a) => Powf.Hang(a, 2);

        /// <summary>
        /// https://en.wikipedia.org/wiki/Trigonometric_functions
        /// </summary>
        /// <param name="a"></param>
        /// <returns>
        /// A / B
        /// </returns>
        public static Entity Tan(Entity a) => Tanf.Hang(a);

        /// <summary>
        /// https://en.wikipedia.org/wiki/Trigonometric_functions
        /// </summary>
        /// <param name="a"></param>
        /// <returns>
        /// B / A
        /// </returns>
        public static Entity Cotan(Entity a) => Cotanf.Hang(a);

        /// <summary>
        /// https://en.wikipedia.org/wiki/Trigonometric_functions
        /// </summary>
        /// <param name="a"></param>
        /// <returns>
        /// C / B
        /// </returns>
        public static Entity Sec(Entity a) => 1 / Cos(a);

        /// <summary>
        /// https://en.wikipedia.org/wiki/Trigonometric_functions
        /// </summary>
        /// <param name="a"></param>
        /// <returns>
        /// C / A
        /// </returns>
        public static Entity Cosec(Entity a) => 1 / Sin(a);

        /// <summary>
        /// This function is every interesting for ASC (https://asc-community.org)
        /// </summary>
        /// <param name="a"></param>
        /// <returns>
        /// x * sin(x)
        /// </returns>
        public static Entity B(Entity a) => a * Sin(a);

        /// <summary>
        /// This function is every interesting for ASC (https://asc-community.org)
        /// </summary>
        /// <param name="a"></param>
        /// <returns>
        /// x * cos(x)
        /// </returns>
        public static Entity TB(Entity a) => a * Cos(a);

        /// <summary>
        /// https://en.wikipedia.org/wiki/Inverse_trigonometric_functions
        /// </summary>
        /// <param name="a"></param>
        /// <returns>
        /// angle between A and C
        /// </returns>
        public static Entity Arcsin(Entity a) => Arcsinf.Hang(a);

        /// <summary>
        /// https://en.wikipedia.org/wiki/Inverse_trigonometric_functions
        /// </summary>
        /// <param name="a"></param>
        /// <returns>
        /// angle between B and C
        /// </returns>
        public static Entity Arccos(Entity a) => Arccosf.Hang(a);

        /// <summary>
        /// https://en.wikipedia.org/wiki/Inverse_trigonometric_functions
        /// </summary>
        /// <param name="a"></param>
        /// <returns>
        /// angle between A and C
        /// </returns>
        public static Entity Arctan(Entity a) => Arctanf.Hang(a);

        /// <summary>
        /// https://en.wikipedia.org/wiki/Inverse_trigonometric_functions
        /// </summary>
        /// <param name="a"></param>
        /// <returns>
        /// angle between B and C
        /// </returns>
        public static Entity Arccotan(Entity a) => Arccotanf.Hang(a);

        /// <summary>
        /// https://en.wikipedia.org/wiki/Natural_logarithm
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Entity Ln(Entity a) => Logf.Hang(a, e);

        /// <summary>
        /// Creates an instance of variable entity.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static VariableEntity Var(string name) => new VariableEntity(name);

        /// <summary>
        /// Creates a complex instance of Number (not NumberEntity!)
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Number Num(double a, double b) => new Number(a, b);

        /// <summary>
        /// Creates a real instance of Number (not NumberEntity!)
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Number Num(double a) => new Number(a);

        public static readonly VariableEntity e = "e";
        public static readonly Number i = new Number(0, 1);
        public static readonly VariableEntity pi = "pi";
        public static double EQUALITY_THRESHOLD { get; set; } = 1.0e-11;

        /// <summary>
        /// Converts an expression from a string
        /// </summary>
        /// <param name="expr">
        /// String expression, for example, "2 * x + 3 + sqrt(x)"
        /// </param>
        /// <returns></returns>
        public static Entity FromString(string expr) => FromString(expr, true);

        /// <summary>
        /// Converts an expression from a string
        /// </summary>
        /// <param name="expr">
        /// String expression, for example, "2 * x + 3 + sqrt(x)"
        /// </param>
        /// <param name="intelli">
        /// Bool parameter responsible for neat-syntax parsing, for example
        /// 2x will be parsed as 2 * x.
        /// </param>
        /// <returns></returns>
        public static Entity FromString(string expr, bool intelli)
        {
            var lexer = new Lexer(expr);
            if (intelli)
                lexer.AddOmittedOperators();
            var res = Parser.Parse(lexer);
            return SynonymFunctions.Synonymize(res);
        }

        /// <summary>
        /// Converts an exprssion from linq expression
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static Entity FromLinq(Expression expr)
        {
            var parser = new LinqParser(expr);
            return parser.Parse();
        }

        /// <summary>
        /// Returns list of unique variables, for example 
        /// it extracts `x`, `goose` from (x + 2 * goose) - pi * x
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static EntitySet GetUniqueVariables(Entity expr)
        {
            var res = new EntitySet();
            TreeAnalyzer.GetUniqueVariables(expr, res);
            return res;
        }

        /// <summary>
        /// Translates num10 into another number system
        /// </summary>
        /// <param name="num"></param>
        /// <param name="N"></param>
        /// <returns></returns>
        public static string ToBaseN(double num, int N) => NumberSystem.ToBaseN(num, N);

        /// <summary>
        /// Translates num into 10 number system
        /// </summary>
        /// <param name="num"></param>
        /// <param name="N"></param>
        /// <returns></returns>
        public static double FromBaseN(string num, int N) => NumberSystem.FromBaseN(num, N);
    }
}
