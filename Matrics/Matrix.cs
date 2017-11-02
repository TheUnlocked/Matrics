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
        /// Creates an identity matrix of a given size.
        /// <para>Identity matricies use 1 for 1 and the 0 for 0. T must support int casting or an error will be produced.</para>
        /// </summary>
        /// <param name="size">The length of the identity matrix</param>
        /// <returns>An identity matrix of the given size</returns>
        public static T[,] IdentityMatrix(int size)
        {
            return new MatrixDimensions(size, size).CreateMatrix<T>().Apply((v, r, c) => r == c ? (dynamic)1 : (dynamic)0);
        }

        /// <summary>
        /// Compares if another matrix is equal to this one
        /// </summary>
        /// <param name="obj">The other object</param>
        /// <returns>Whether or not the two matricies are equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Matrix<T>)
            {
                return M().MatrixEquals(((Matrix<T>)obj).M());
            }
            if (obj is T[,])
            {
                return M().MatrixEquals((T[,])obj);
            }
            return false;
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