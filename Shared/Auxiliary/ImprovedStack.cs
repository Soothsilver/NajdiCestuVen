﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Nsnbc.PostSharp;

namespace Nsnbc.Auxiliary
{
    /// <summary>
    /// Basically an improved list that provides access to the entire array, but provides methods for Push, Pop and Peek. However, stack pop operation is O(n). Use this structure only for small collections.
    /// </summary>
    /// <typeparam name="T">The type of elements on the stack.</typeparam>
    [PublicAPI]
    [Trace]

    public class ImprovedStack<T>: List<T> where T : class
    {
        /// <summary>
        /// Returns the top of the stack, without removing it.
        /// </summary>
        [Trace(AttributeExclude = true)]
        [return: MaybeNull]
        public T Peek()
        {
            if (Count > 0) return this[Count - 1];
            return default!;
        }
        /// <summary>
        /// Pushes the item to the top of the stack.
        /// </summary>
        /// <param name="t">Item to push on top of stack.</param>
        public void Push(T t)
        {
            Add(t);
        }

        /// <summary>
        /// Returns the item at the top of the stack and removes it, or returns null or throws an exception if the stack is empty.
        /// </summary>
        /// <returns></returns>
        public T Pop()
        {
            if (Count > 0)
            {
                T t = this[Count - 1];
                Remove(t);
                return t;
            }

            throw new InvalidOperationException("The Improved Stack is empty, yet its Pop() method was called.");
        }
    }
}
