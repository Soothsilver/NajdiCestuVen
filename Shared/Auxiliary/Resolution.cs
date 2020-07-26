using System;

namespace Auxiliary
{
   
    /// <summary>
    /// Represents a display resolution.
    /// </summary>
    [Serializable]
    public class Resolution : IComparable<Resolution>
    {
        /// <summary>
        /// Gets or sets the resolution height.
        /// </summary>
        public int Width { get; }
        /// <summary>
        /// Gets or sets the resolution height.
        /// </summary>
        public int Height { get; }
        /// <summary>
        /// Creates a new resolution class instance.
        /// </summary>
        /// <param name="width">Resolution width.</param>
        /// <param name="height">Resolution height.</param>
        public Resolution(int width, int height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// A resolution is less than another resolution if its width is less, or if its width is identical and its height is less.
        /// </summary>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other"/> parameter.Zero This object is equal to <paramref name="other"/>. Greater than zero This object is greater than <paramref name="other"/>. 
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public int CompareTo(Resolution? other)
        {
            if (other == null)
            {
                return 1;
            }
            if (Width < other.Width)
            {
                return -1;
            }

            if (Width == other.Width)
            {
                if (Height < other.Height)
                {
                    return -1;
                }

                if (Height == other.Height)
                {
                    return 0;
                }

                return 1;
            }

            return 1;
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>. </param><filterpriority>2</filterpriority>
        public override bool Equals(object? obj)
        {
            if (obj is Resolution r)
            {
                return Width == r.Width && Height == r.Height;
            }

            return false;
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            return Width * 10000 + Height;
        }

        /// <summary>
        /// Returns the string "(width)x(height)". For example, 1024x768.
        /// </summary>
        public override string ToString()
        {
            return Width + "x" + Height;
        }
    }
}