using System;
using System.Diagnostics.CodeAnalysis;

namespace Nsnbc.Util
{
    public static class Require
    {
        /// <summary>
        /// Asserts that the argument is currently not null.
        /// </summary>
        /// <exception cref="InvalidOperationException">If the argument is null.</exception>
        public static void NotNull<T> ([NotNull] T? instance) where T : class
        {
            if (instance == null)
            {
                throw new InvalidOperationException("This object was not supposed to be null.");
            }
        }
    }
}