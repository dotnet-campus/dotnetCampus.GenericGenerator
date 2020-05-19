using System;

namespace dotnetCampus.Runtime.CompilerServices
{
    /// <summary>
    /// Mark this type as a generic template type, and many other generic types will be generated from this template.
    /// <para />
    /// Tips: Type parameter name MUST be T. All other names will be treated as NO CHANGE during the compilation.
    /// </summary>
    // We should mark this attribute allowing multiple because users may mark this on a partial class and this will cause a multiple attribute.
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct, Inherited = false, AllowMultiple = true)]
    internal sealed class GenerateGenericFromThisAttribute : Attribute
    {
        /// <summary>
        /// Mark this type as a generic template type, and many other generic types will be generated from this template.
        /// <para />
        /// Tips: Type parameter name MUST be T. All other names will be treated as NO CHANGE during the compilation.
        /// </summary>
        /// <param name="from">Generic parameter minimun number. Defaults 2.</param>
        /// <param name="to">Generic parameter maximun number. Defaults 8.</param>
        public GenerateGenericFromThisAttribute(int from = 2, int to = 8)
        {
            From = from;
            To = to;
        }

        /// <summary>
        /// During the compilation, some generic types will be generated from this type as template. This is the minimun parameter number.
        /// <para />
        /// From Foo&lt;T&gt;, we generate Foo&lt;T1, T2&gt;. Then From should be set to 2.
        /// </summary>
        public int From { get; set; }

        /// <summary>
        /// During the compilation, some generic types will be generated from this type as template. This is the maximun parameter number.
        /// <para />
        /// From Foo&lt;T&gt;, we generate Foo&lt;T1, T2, T3, T4, T5, T6, T7, T8&gt;. Then From should be set to 8.
        /// </summary>
        public int To { get; set; }
    }
}
