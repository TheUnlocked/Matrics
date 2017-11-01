namespace Matrics
{
    /// <summary>
    /// A wrapper struct for matricies
    /// </summary>
    public struct Matrix<T>{
        private T[,] innerMatrix;

        /// <summary>
        /// Wrapper constructor
        /// </summary>
        public Matrix(T[,] innerMatrix){
            this.innerMatrix = innerMatrix;
        }

        /// <summary>
        /// Produces the wrapped matrix
        /// </summary>
        public T[,] M(){
            return innerMatrix;
        }

        /// <summary>
        /// Adds the two matricies through <seealso cref="MatrixExtentions.Add{T}(T[,], T[,])"/>
        /// </summary>
        public static Matrix<T> operator +(Matrix<T> arg1, Matrix<T> arg2){
            return arg1.M().Add(arg2);
        }
        /// <summary>
        /// Subtracts the two matricies through <seealso cref="MatrixExtentions.Subtract{T}(T[,], T[,])"/>
        /// </summary>
        public static Matrix<T> operator -(Matrix<T> arg1, Matrix<T> arg2){
            return arg1.M().Subtract(arg2);
        }
        /// <summary>
        /// Scalar multiplies the two matricies through <seealso cref="MatrixExtentions.Multiply{T}(T[,], T)"/>
        /// </summary>
        public static Matrix<T> operator *(Matrix<T> arg1, T scalar){
            return arg1.M().Multiply(scalar);
        }
        /// <summary>
        /// Scalar divides the two matricies through <seealso cref="MatrixExtentions.Divide{T}(T[,], T)"/>
        /// </summary>
        public static Matrix<T> operator /(Matrix<T> arg1, T scalar){
            return arg1.M().Divide(scalar);
        }
        /// <summary>
        /// Multiplies the two matricies through <seealso cref="MatrixExtentions.Multiply{T}(T[,], T[,])"/>
        /// </summary>
        public static Matrix<T> operator *(Matrix<T> arg1, Matrix<T> arg2){
            return arg1.M().Multiply(arg2);
        }

        /// <summary>
        /// Implicit wrapper cast
        /// </summary>
        public static implicit operator T[,](Matrix<T> wrapper){
            return wrapper.M();
        }
        /// <summary>
        /// Implicit wrapper cast
        /// </summary>
        public static implicit operator Matrix<T>(T[,] matrix){
            return new Matrix<T>(matrix);
        }
    }
}