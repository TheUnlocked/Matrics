using System;
using Matrics.Exceptions;

namespace Matrics
{
    public static class MatrixExtentions{
        /// <summary>
        /// Standard matrix addtion
        /// </summary>
        /// <param name="mat1">Addend matrix</param> 
        /// <param name="mat2">Addend matrix</param>
        /// <returns>Matrix sum</returns>
        public static T[,] Add<T>(this T[,] mat1, T[,] mat2){
            if (mat1.GetDimensions().Equals(mat2.GetDimensions())){
                return mat1.Map((v, r, c) => v + (dynamic)mat2[r-1,c-1]);
            }
            else{
                throw new MatrixSizeException($"Dimensions {mat1.GetDimensions().ToString()} and {mat2.GetDimensions().ToString()} do not match.");
            }
            throw new UnknownMatrixException("An unknown error occured when adding two matricies");
        }

        /// <summary>
        /// Standard matrix subtraction
        /// </summary>
        /// <param name="mat1">Minuend matrix</param>
        /// <param name="mat2">Subtrahend matrix</param>
        /// <returns>Matrix difference</returns>
        public static T[,] Subtract<T>(this T[,] mat1, T[,] mat2){
            if (mat1.GetDimensions().Equals(mat2.GetDimensions())){
                return mat1.Map((v, r, c) => v - (dynamic)mat2[r-1,c-1]);
            }
            else{
                throw new MatrixSizeException($"Dimensions {mat1.GetDimensions().ToString()} and {mat2.GetDimensions().ToString()} do not match.");
            }
            throw new UnknownMatrixException("An unknown error occured when subtracting two matricies");
        }

        /// <summary>
        /// Matrix-scalar multiplication
        /// </summary>
        /// <param name="mat">Matrix factor</param>
        /// <param name="scalar">Scalar factor</param>
        /// <returns>Matrix-scalar product</returns>
        public static T[,] Multiply<T>(this T[,] mat, T scalar){
            return mat.Map((v, r, c) => v * (dynamic)scalar);
        }

        /// <summary>
        /// Matrix-scalar division
        /// </summary>
        /// <param name="mat">Matrix dividend</param>
        /// <param name="scalar">Scalar divisor</param>
        /// <returns>Matrix-scalar quotient</returns>
        public static T[,] Divide<T>(this T[,] mat, T scalar){
            return mat.Map((v, r, c) => v / (dynamic)scalar);
        }

        /// <summary>
        /// Matrix multiplication
        /// </summary>
        /// <param name="mat1">Left matrix</param>
        /// <param name="mat2">Right matrix</param>
        /// <returns>The product of the two matricies</returns>
        public static T[,] Multiply<T>(this T[,] mat1, T[,] mat2){
            MatrixDimensions d1 = mat1.GetDimensions();
            MatrixDimensions d2 = mat2.GetDimensions();
            if (d1.cols == d2.rows){
                T[,] productMatrix = new T[d1.rows, d2.cols];
                for (int r = 0; r < d1.rows; r++){
                    for (int c = 0; c < d2.cols; c++){
                        productMatrix[r,c] = mat1.GetVector(VectorDirection.Row, r + 1).DotProduct(mat2.GetVector(VectorDirection.Column, c + 1));
                    }
                }
                return productMatrix;
            }
            throw new MatrixSizeException($"A matrix of size {d1.ToString()} cannot be multiplied by a matrix of size {d2.ToString()}");
        }

        /// <summary>
        /// Calculates the sum of all elements in the matrix
        /// </summary>
        /// <param name="mat">The matrix to be summed</param>
        /// <returns>The sum of all elements in the matrix</returns>
        public static T Sum<T>(this T[,] mat){
            if (mat.Length == 0){
                throw new MatrixSizeException("A matrix cannot have no elements");
            }
            T sum = mat[0,0];
            MatrixDimensions d = mat.GetDimensions();
            for (int r = 0; r < d.rows; r++){
                for (int c = 0; c < d.cols; c++){
                    if (r != 0 || c != 0){
                        sum += (dynamic)mat[r,c];
                    }
                }
            }
            return sum;
        }

        /// <summary>
        /// Add a row to another row
        /// </summary>
        /// <param name="mat">The matrix</param>
        /// <param name="addTo">The row to be added to</param>
        /// <param name="addFrom">The row to add</param>
        /// <returns>A copy of the matrix after row addition has taken place</returns>
        public static T[,] RowAdd<T>(this T[,] mat, int addTo, int addFrom){
            return mat.Map((v, r, c) => {
                if (r == addTo){
                    return v + (dynamic)mat[addFrom - 1, c-1];
                }
                return v;
            });
        }

        /// <summary>
        /// Subtract a row from another row
        /// </summary>
        /// <param name="mat">The matrix</param>
        /// <param name="subTo">The row to be subtracted from</param>
        /// <param name="subFrom">The row to subtract with</param>
        /// <returns>A copy of the matrix after row subtraction has taken place</returns>
        public static T[,] RowSubtract<T>(this T[,] mat, int subTo, int subFrom){
            return mat.Map((v, r, c) => {
                if (r == subTo){
                    return v - (dynamic)mat[subFrom - 1, c-1];
                }
                return v;
            });
        }

        /// <summary>
        /// Multiply a row by a scalar
        /// </summary>
        /// <param name="mat">The matrix</param>
        /// <param name="row">The row to be multiplied</param>
        /// <param name="scalar">The scalar to multiply by</param>
        /// <returns>A copy of the matrix after row multiplication has taken place</returns>
        public static T[,] RowMultiply<T>(this T[,] mat, int row, T scalar){
            if (scalar.GetHashCode() == 0){
                throw new IllegalOperationException("Multiplying by zero isn't a legal row operation.");
            }
            return mat.Map((v, r, c) => {
                if (r == row){
                    return v * (dynamic)scalar;
                }
                return v;
            });
        }

        /// <summary>
        /// Divide a row by a scalar
        /// </summary>
        /// <param name="mat">The matrix</param>
        /// <param name="row">The row to be divided</param>
        /// <param name="scalar">The scalar to divide by</param>
        /// <returns>A copy of the matrix after row division has taken place</returns>
        public static T[,] RowDivide<T>(this T[,] mat, int row, T scalar){
            return mat.Map((v, r, c) => {
                if (r == row){
                    return v / (dynamic)scalar;
                }
                return v;
            });
        }

        /// <summary>
        /// Swaps two rows in a matrix
        /// </summary>
        /// <param name="mat">The matrix</param>
        /// <param name="row1">One of the rows to swap</param>
        /// <param name="row2">The other row to swap</param>
        /// <returns>A copy of the matrix with the two rows swapped</returns>
        public static T[,] RowSwap<T>(this T[,] mat, int row1, int row2){
            return mat.Map((v, r, c) => {
                if (r == row1)
                    return mat[row2-1, c-1];
                if (r == row2)
                    return mat[row1-1, c-1];
                return v;
            });
        }

        /// <summary>
        /// Transposes a matrix
        /// </summary>
        /// <param name="mat">The matrix to transpose</param>
        /// <returns>The transposed matrix</returns>
        public static T[,] Transpose<T>(this T[,] mat){
            MatrixDimensions d = mat.GetDimensions().Flip();
            T[,] newMat = d.CreateMatrix<T>();
            for (int r = 0; r < d.rows; r++){
                for (int c = 0; c < d.cols; c++){
                    newMat[r,c] = mat[c,r];
                }
            }
            return newMat;
        }

		/// <summary>
		/// Produces the determinant of a square matrix
		/// </summary>
		/// <param name="mat">The matrix</param>
		/// <returns>The determinant of the matrix</returns>
        public static T Determinant<T>(this T[,] mat){
            MatrixDimensions d = mat.GetDimensions();
            if (d.IsSquare()){
                switch (d.rows)
                {
                    case 0:
                        throw new MatrixSizeException("A matrix cannot have no elements.");
                    case 1:
                        return mat[0,0];
                    case 2:
                        return mat[0,0] * (dynamic)mat[1,1] - mat[0,1] * (dynamic)mat[1,0];
                    default:
                        T sum = mat[0, 0] * (dynamic)(mat.RemoveVector(VectorDirection.Row, 1).RemoveVector(VectorDirection.Column, 1).Determinant());
                        for (int i = 1; i < d.rows; i++){
                            if (i % 2 == 0)
                                sum += mat[0, i] * (dynamic)(mat.RemoveVector(VectorDirection.Row, 1).RemoveVector(VectorDirection.Column, i + 1).Determinant());
                            else
                                sum -= mat[0, i] * (dynamic)(mat.RemoveVector(VectorDirection.Row, 1).RemoveVector(VectorDirection.Column, i + 1).Determinant());
                        }
                        return sum;
                }
            }
            throw new IllegalOperationException("Only squre matricies have determinants.");
        }

        /// <summary>
        /// Maps a scalar function over a matrix
        /// </summary>
        /// <param name="mat">The matrix to be acted on</param>
        /// <param name="f">The function to be applied to each element of the matrix.
        /// Its arguments are the row, col, and the value for each element in the matrix</param>
        /// <returns>A copy of the old matrix with a scalar function mapped over it</returns>
        public static T[,] Map<T>(this T[,] mat, Func<T, int, int, T> f){
            T[,] newMat = new T[mat.GetLength(0),mat.GetLength(1)];
            MatrixDimensions dim = mat.GetDimensions();
            for(int r = 0; r < dim.rows; r++){
                for (int c = 0; c < dim.cols; c++){
                    newMat[r,c] = f.Invoke(mat[r,c], r + 1, c + 1);
                }
            }
            return newMat;
        }

        /// <summary>
        /// Inserts a row or column vector into a matrix
        /// </summary>
        /// <param name="mat">The matrix</param>
        /// <param name="direction">The type of vector to insert (row or column)</param>
        /// <param name="insertIndex">The vector index to insert at</param>
        /// <returns>The matrix with an additional row/column at the specified index</returns>
        public static T[,] InsertVector<T>(this T[,] mat, VectorDirection direction, int insertIndex){
            MatrixDimensions d = mat.GetDimensions();
            T[,] newMat;
            if (direction == VectorDirection.Column){
                if (insertIndex > d.cols + 1 || insertIndex < 1){
                    throw new IndexOutOfRangeException("That column index does not and will not exist in the new matrix.");
                }
                newMat = new T[d.rows, d.cols + 1];
                return newMat.Map((v, r, c) => {
                    if (c < insertIndex){
                        return mat[r-1,c-1];
                    }
                    else if (c > insertIndex){
                         return mat[r-1,c-2];
                    }
                    return v;
                });
            }
            else{
                if (insertIndex > d.rows + 1 || insertIndex < 1){
                    throw new IndexOutOfRangeException("That row index does not and will not exist in the new matrix.");
                }
                newMat = new T[d.rows + 1, d.cols];
                return newMat.Map((v, r, c) => {
                    if (r < insertIndex){
                        return mat[r-1,c-1];
                    }
                    else if (r > insertIndex){
                         return mat[r-2,c-1];
                    }
                    return v;
                });
            }
        }

        /// <summary>
        /// Removes a row or column vector in a matrix
        /// </summary>
        /// <param name="mat">The matrix</param>
        /// <param name="direction">The type of vector to remove (row or column)</param>
        /// <param name="removeIndex">The vector index to remove at</param>
        /// <returns>The matrix with an removed row/column at the specified index</returns>
        public static T[,] RemoveVector<T>(this T[,] mat, VectorDirection direction, int removeIndex){
            MatrixDimensions d = mat.GetDimensions();
            T[,] newMat;
            if (direction == VectorDirection.Column){
                if (removeIndex > d.cols || removeIndex < 1){
                    throw new IndexOutOfRangeException("That column index does not exist in the matrix.");
                }
                newMat = new T[d.rows, d.cols - 1];
                return newMat.Map((v, r, c) => {
                    if (c < removeIndex){
                        return mat[r-1,c-1];
                    }
                    else {
                         return mat[r-1,c];
                    }
                });
            }
            else{
                if (removeIndex > d.rows || removeIndex < 1){
                    throw new IndexOutOfRangeException("That row index does not exist in the matrix.");
                }
                newMat = new T[d.rows - 1, d.cols];
                return newMat.Map((v, r, c) => {
                    if (r < removeIndex){
                        return mat[r-1,c-1];
                    }
                    else {
                         return mat[r,c-1];
                    }
                });
            }
        }

        /// <summary>
        /// Get the dimensions of a matrix
        /// </summary>
        /// <param name="mat">The matrix</param>
        /// <returns>The dimensions of the matrix</returns>
        public static MatrixDimensions GetDimensions<T>(this T[,] mat){
            return new MatrixDimensions(mat.GetLength(0), mat.GetLength(1));
        }

        /// <summary>
        /// Produces a human-readable matrix
        /// </summary>
        /// <param name="mat">The matrix</param>
        /// <returns>A human-readable representation of the matrix</returns>
        public static string ToMatrixString<T>(this T[,] mat){
            MatrixDimensions d = mat.GetDimensions();
            string[] lines = new string[d.rows];
            for (int r = 0; r < d.rows; r++){
                string[] line = new string[d.cols];
                for (int c = 0; c < d.cols; c++){
                    line[c] = mat[r,c].ToString();
                }
                lines[r] = "[" + String.Join(" ", line) + "]";
            }
            return "[" + String.Join("\n", lines) + "]";
        }

        /// <summary>
        /// Produces a vector in the form [R(1), R(2), R(3), ..., R(n)]
        /// </summary>
        /// <param name="mat">The matrix to flatten</param>
        /// <returns>A vector resulting from the flattened matrix</returns>
        public static T[] FlattenToVector<T>(this T[,] mat){
            MatrixDimensions d = mat.GetDimensions();
            T[] vector = new T[d.rows * d.cols];
            for (int r = 0; r < d.rows; r++){
                for (int c = 0; c < d.cols; c++){
                    vector[r * d.cols + c] = mat[r,c];
                }
            }
            return vector;
        }

        /// <summary>
        /// Gets the vector R(n) or C(n) from the matrix
        /// </summary>
        /// <param name="mat">The matrix</param>
        /// <param name="direction">The direction to retrive the vector from (either rows or columns)</param>
        /// <param name="index">The index of the row or column</param>
        /// <returns>The vector at R(n) or C(n)</returns>
        public static T[] GetVector<T>(this T[,] mat, VectorDirection direction, int index){
            T[] vector = new T[mat.GetDimensions().InDirection(direction)];

            // if (vector.Length <= index){
            //     throw new IndexOutOfRangeException($"Index {index + 1} is larger than the size of the matrix axis {vector.Length}");
            // }
            if (index < 1){
                throw new IndexOutOfRangeException($"Index {index} is less than 1");
            }

            if (direction == VectorDirection.Row){
                for (int c = 0; c < vector.Length; c++){
                    vector[c] = mat[index-1, c];
                }
            }
            else{
                for (int r = 0; r < vector.Length; r++){
                    vector[r] = mat[r, index-1];
                }
            }

            return vector;
        }

        /// <summary>
        /// Wraps a matrix
        /// </summary>
        /// <param name="mat">The matrix</param>
        /// <returns>The wrapped matrix</returns>
        public static MatrixWrapper<T> W<T>(this T[,] mat){
            return new MatrixWrapper<T>(mat);
        }
    }
}