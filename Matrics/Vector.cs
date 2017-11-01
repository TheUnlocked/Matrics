namespace Matrics
{
    /// <summary>
    /// A wrapper struct for vectors
    /// </summary>
    public struct Vector<T>{
        private T[] innerVector;

        public Vector(T[] innerVector){
            this.innerVector = innerVector;
        }

        /// <summary>
        /// Produces the wrapped vector
        /// </summary>
        public T[] V(){
            return innerVector;
        }

        /// <summary>
        /// Adds the two vectors through <seealso cref="VectorExtensions.Add{T}(T[], T[])"/>
        /// </summary>
        public static Vector<T> operator +(Vector<T> arg1, Vector<T> arg2){
            return arg1.V().Add(arg2);
        }
        /// <summary>
        /// Subtracts the two vectors through <seealso cref="VectorExtensions.Subtract{T}(T[], T[])"/>
        /// </summary>
        public static Vector<T> operator -(Vector<T> arg1, Vector<T> arg2){
            return arg1.V().Subtract(arg2);
        }
        /// <summary>
        /// Multiplies the two vectors through <seealso cref="VectorExtensions.Multiply{T}(T[], T)"/>
        /// </summary>
        public static Vector<T> operator *(Vector<T> arg1, T scalar){
            return arg1.V().Multiply(scalar);
        }
        /// <summary>
        /// Divides the two vectors through <seealso cref="VectorExtensions.Divide{T}(T[], T)"/>
        /// </summary>
        public static Vector<T> operator /(Vector<T> arg1, T scalar){
            return arg1.V().Divide(scalar);
        }
        /// <summary>
        /// Dot products the two vectors through <seealso cref="VectorExtensions.DotProduct{T}(T[], T[])"/>
        /// </summary>
        public static T operator *(Vector<T> arg1, Vector<T> arg2){
            return arg1.V().DotProduct(arg2);
        }
        /// <summary>
        /// Negates the vector through <c>Apply((v, i) => -(dynamic)v);</c>
        /// </summary>
        public static Vector<T> operator -(Vector<T> arg1){
            return arg1.V().Apply((v, i) => -(dynamic)v);
        }

        /// <summary>
        /// Implicit wrapper cast
        /// </summary>
        public static implicit operator T[](Vector<T> wrapper){
            return wrapper.V();
        }
        /// <summary>
        /// Implicit wrapper cast
        /// </summary>
        public static implicit operator Vector<T>(T[] vector){
            return new Vector<T>(vector);
        }
    }
}