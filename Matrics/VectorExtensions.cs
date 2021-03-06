using System;
using Matrics.Exceptions;

namespace Matrics
{
    /// <summary>
    /// An extensions class for vectors
    /// </summary>
    public static class VectorExtensions{
        /// <summary>
        /// Standard vector addtion
        /// </summary>
        /// <param name="vector1">Addend vector</param> 
        /// <param name="vector2">Addend vector</param>
        /// <returns>Vector sum</returns>
        public static T[] Add<T>(this T[] vector1, T[] vector2){
            if (vector1.Length == vector2.Length){
                return vector1.Apply((v, i) => v + (dynamic)vector2[i]);
            }
            else{
                throw new MatrixSizeException($"Dimensions {vector1.Length} and {vector2.Length} do not match.");
            }
            throw new UnknownMatrixException("An unknown error occured when adding two vectors");
        }

        /// <summary>
        /// Standard vector subtraction
        /// </summary>
        /// <param name="vector1">Minuend vector</param> 
        /// <param name="vector2">Subtrahend vector</param>
        /// <returns>Vector difference</returns>
        public static T[] Subtract<T>(this T[] vector1, T[] vector2){
            if (vector1.Length == vector2.Length){
                return vector1.Apply((v, i) => v - (dynamic)vector2[i]);
            }
            else{
                throw new MatrixSizeException($"Dimensions {vector1.Length} and {vector2.Length} do not match.");
            }
            throw new UnknownMatrixException("An unknown error occured when subtracting two vectors");
        }

        /// <summary>
        /// Vector-scalar multiplication
        /// </summary>
        /// <param name="vector">Vector factor</param>
        /// <param name="scalar">Scalar factor</param>
        /// <returns>Vector-scalar product</returns>
        public static T[] Multiply<T>(this T[] vector, T scalar){
            return vector.Apply((v, i) => v * (dynamic)scalar);
        }

        /// <summary>
        /// Vector-scalar division
        /// </summary>
        /// <param name="vector">Vector dividend</param>
        /// <param name="scalar">Scalar divisor</param>
        /// <returns>Vector-scalar quotient</returns>
        public static T[] Divide<T>(this T[] vector, T scalar){
            return vector.Apply((v, i) => v / (dynamic)scalar);
        }

        /// <summary>
        /// Find the dot product of two vectors
        /// </summary>
        /// <param name="vector1">Vector factor</param>
        /// <param name="vector2">Vector factor</param>
        /// <returns>The dot product of the two vectors</returns>
        public static T DotProduct<T>(this T[] vector1, T[] vector2){
            if (vector1.Length == 0)
                throw new VectorSizeException("A vector cannot have no elements");
            if (vector1.Length == vector2.Length){
                T sum = vector1[0] * (dynamic)vector2[0];
                for (int i = 1; i < vector1.Length; i++){
                    sum += vector1[i] * (dynamic)vector2[i];
                }
                return sum;
            }
            Console.WriteLine(vector1.ToVectorString());
            Console.WriteLine(vector2.ToVectorString());
            throw new VectorSizeException("Vectors of different sizes can't be multiplied.");
        }

        /// <summary>
        /// Calculates the sum of all elements in the vector
        /// </summary>
        /// <param name="vector">The vector to be summed</param>
        /// <returns>The sum of all elements in the vector</returns>
        public static T Sum<T>(this T[] vector){
            if (vector.Length == 0)
                throw new VectorSizeException("A vector cannot have no elements");
            T sum = vector[0];
            for (int i = 1; i < vector.Length; i++)
                sum += (dynamic)vector[i];
            return sum;
        }

        /// <summary>
        /// Gets the squared magnitude of a vector
        /// </summary>
        /// <param name="vector">The vector</param>
        /// <returns>The squared magnitude of the vector</returns>
        public static T MagnitudeSquared<T>(this T[] vector)
        {
            if (vector.Length == 0)
                throw new VectorSizeException("A vector cannot have no elements");
            return vector.Apply((v, i) => v * (dynamic)v).Sum();
        }

        /// <summary>
        /// Gets the magnitude of a vector.
        /// <para>The magnitude will be processed as a <see cref="double"/> through the <see cref="Math.Sqrt(double)"/> function. Use <see cref="MagnitudeSquared{T}(T[])"/> instead for incompatible types.</para>
        /// </summary>
        /// <param name="vector">The vector</param>
        /// <returns>The magnitude of the vector</returns>
        public static T Magnitude<T>(this T[] vector)
        {
            return (T)(dynamic)Math.Sqrt((dynamic)vector.MagnitudeSquared());
        }

        /// <summary>
        /// Gets the normalized (or unit) vector.
        /// <para>This uses <see cref="Magnitude{T}(T[])"/>, meaning any input will be processed as a <see cref="double"/> through <see cref="Math.Sqrt(double)"/>.</para>
        /// </summary>
        /// <param name="vector">The vector</param>
        /// <returns></returns>
        public static T[] Normalize<T>(this T[] vector)
        {
            return vector.Divide(vector.Magnitude());
        }

        /// <summary>
        /// Produces the scalar projection of one vector onto another vector
        /// <para>This uses <see cref="Magnitude{T}(T[])"/>, meaning any input will be processed as a <see cref="double"/> through <see cref="Math.Sqrt(double)"/>.</para>
        /// </summary>
        /// <param name="fromVector">The vector to project from</param>
        /// <param name="onVector">The vector to project onto</param>
        /// <returns>The scalar projection</returns>
        public static T ScalarProject<T>(this T[] fromVector, T[] onVector)
        {
            return fromVector.DotProduct(onVector) / (dynamic)onVector.Magnitude();
        }

        /// <summary>
        /// Projects one vector onto another.
        /// <para>This uses <see cref="ScalarProject{T}(T[], T[])"/> and <see cref="Normalize{T}(T[])"/>, meaning any input will be processed as a <see cref="double"/> through <see cref="Math.Sqrt(double)"/>.</para>
        /// </summary>
        /// <param name="fromVector">The vector to project from</param>
        /// <param name="onVector">The vector to project onto</param>
        /// <returns>The vector projection</returns>
        public static T[] VectorProject<T>(this T[] fromVector, T[] onVector)
        {
            return onVector.Normalize().Multiply(fromVector.ScalarProject(onVector));
        }

        /// <summary>
        /// Rejects one vector onto another
        /// <para>This uses <see cref="VectorProject{T}(T[], T[])"/>, meaning any input will be processed as a <see cref="double"/> through <see cref="Math.Sqrt(double)"/>.</para>
        /// </summary>
        /// <param name="fromVector">The vector to reject from</param>
        /// <param name="onVector">The vector to reject onto</param>
        /// <returns>The vector rejection</returns>
        public static T[] VectorReject<T>(this T[] fromVector, T[] onVector)
        {
            return fromVector.Subtract(fromVector.VectorProject(onVector));
        }

        /// <summary>
        /// Maps a scalar function over a vector
        /// </summary>
        /// <param name="vector">The vector to be acted on</param>
        /// <param name="f">The function to be applied to each element of the vector.
        /// Its arguments are i and the value for each element in the vector</param>
        /// <returns>A copy of the old vector with a scalar function mapped over it</returns>
        public static T[] Apply<T>(this T[] vector, Func<T, int, T> f){
            T[] newVector = new T[vector.Length];
            for (int i = 0; i < vector.Length; i++){
                newVector[i] = f.Invoke(vector[i], i);
            }
            return newVector;
        }

        /// <summary>
        /// Inserts an element into a vector at a specified position
        /// </summary>
        /// <param name="vector">The vector to insert into</param>
        /// <param name="element">The element to insert</param>
        /// <param name="insertPosition">The position at which to insert the element</param>
        /// <returns>A copy of the vector with the element inserted</returns>
        public static T[] InsertElement<T>(this T[] vector, T element, int insertPosition){
            if (insertPosition > vector.Length + 1 || insertPosition < 0){
                throw new IndexOutOfRangeException($"The index {insertPosition} will not exist in the new vector");
            }
            T[] newVector = new T[vector.Length + 1];
            for (int i = 1; i < newVector.Length + 1; i++){
                if (i < insertPosition){
                    newVector[i-1] = vector[i-1];
                }
                else if(i == insertPosition){
                    newVector[i-1] = element;
                }
                else{
                    newVector[i-1] = vector[i-2];
                }
            }
            return newVector;
        }

        /// <summary>
        /// Removes an element from a vector
        /// </summary>
        /// <param name="vector">The vector</param>
        /// <param name="removePosition">The position of the element to remove</param>
        /// <returns>A copy of the vector with the element removed</returns>
        public static T[] RemoveElement<T>(this T[] vector, int removePosition){
            if (removePosition > vector.Length + 1 || removePosition < 0){
                throw new IndexOutOfRangeException($"The index {removePosition} does not exist");
            }
            T[] newVector = new T[vector.Length - 1];
            for (int i = 1; i < newVector.Length + 1; i++){
                if (i < removePosition){
                    newVector[i-1] = vector[i-1];
                }
                else{
                    newVector[i-1] = vector[i];
                }
            }
            return newVector;
        }

        /// <summary>
        /// Get a human-readable representation of a vector
        /// </summary>
        /// <param name="vector">The vector</param>
        /// <returns>A human-readable representation of the vector</returns>
        public static string ToVectorString<T>(this T[] vector){
            string[] elements = new string[vector.Length];
            for (int i = 0; i < vector.Length; i++){
                elements[i] = vector[i].ToString();
            }
            return "[" + String.Join(" ", elements) + "]";
        }

        /// <summary>
        /// Converts a vector to a single-row/column matrix
        /// </summary>
        /// <param name="vector">The vector</param>
        /// <param name="shape">The resulting shape of the matrix (either horizontal or vertical)</param>
        /// <returns>The single-row/column matrix derived from the vector</returns>
        public static T[,] ExpandToMatrix<T>(this T[] vector, VectorDirection shape=VectorDirection.Vertical){
            T[,] mat;
            if (shape == VectorDirection.Horizontal){
                mat = new T[1, vector.Length];
                for (int i = 0; i < vector.Length; i++)
                    mat[0, i] = vector[i];
            }
            else{
                mat = new T[vector.Length, 1];
                for (int i = 0; i < vector.Length; i++)
                    mat[i, 0] = vector[i];
            }
            return mat;
        }

        /// <summary>
        /// Wraps the vector.
        /// </summary>
        /// <param name="vector">The vector</param>
        /// <returns>The wrapped vector</returns>
        public static Vector<T> W<T>(this T[] vector){
            return new Vector<T>(vector);
        }
    }
}