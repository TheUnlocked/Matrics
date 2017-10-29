namespace Matrics
{
    public class MatrixDimensions{
        public MatrixDimensions(int r, int c){
            this.rows = r;
            this.cols = c;
        }

        public int rows;
        public int cols;

        /// <summary>
        /// Returns the number of rows/cols in the specified direction
        /// </summary>
        /// <param name="direction">The direction</param>
        /// <returns>The number of rows/cols in the specified direction</returns>
        public int InDirection(VectorDirection direction){
            return direction == VectorDirection.Row ? cols : rows;
        }

        /// <summary>
        /// Returns a flipped copy of the dimensions
        /// </summary>
        /// <returns>A flipped copy of the dimensions</returns>
        public MatrixDimensions Flip(){
            return new MatrixDimensions(cols, rows);
        }

        public bool IsSquare(){
            return rows == cols;
        }

        /// <summary>
        /// Creates a new matrix based on the dimensions
        /// </summary>
        /// <returns>A matrix whose rows and columns are derived from the dimensions</returns>
        public T[,] CreateMatrix<T>(){
            return new T[rows, cols];
        }

        public override string ToString(){
            return $"({rows}, {cols})";
        }

        public override int GetHashCode(){
            return rows * 3 + cols * 5;
        }

        public override bool Equals(object obj){
            MatrixDimensions d = obj as MatrixDimensions;
            if (d != null){
                if (d.rows == rows && d.cols == cols){
                    return true;
                }
            }
            return false;
        }
    }
}