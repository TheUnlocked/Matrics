namespace Matrics
{
    public class MatrixWrapper<T>{
        private T[,] innerMatrix;
        public MatrixWrapper(T[,] innerMatrix){
            this.innerMatrix = innerMatrix;
        }

        public T[,] M(){
            return innerMatrix;
        }

        public static MatrixWrapper<T> operator +(MatrixWrapper<T> arg1, MatrixWrapper<T> arg2){
            return arg1.M().Add(arg2);
        }
        public static MatrixWrapper<T> operator -(MatrixWrapper<T> arg1, MatrixWrapper<T> arg2){
            return arg1.M().Subtract(arg2);
        }
        public static MatrixWrapper<T> operator *(MatrixWrapper<T> arg1, T scalar){
            return arg1.M().Multiply(scalar);
        }
        public static MatrixWrapper<T> operator /(MatrixWrapper<T> arg1, T scalar){
            return arg1.M().Divide(scalar);
        }
        public static MatrixWrapper<T> operator *(MatrixWrapper<T> arg1, MatrixWrapper<T> arg2){
            return arg1.M().Multiply(arg2);
        }
        public static T operator -(MatrixWrapper<T> arg1){
            return arg1.M().Determinant();
        }
        public static MatrixWrapper<T> operator ~(MatrixWrapper<T> arg1){
            return arg1.M().Transpose();
        }

        public static implicit operator T[,](MatrixWrapper<T> wrapper){
            return wrapper.M();
        }
        public static implicit operator MatrixWrapper<T>(T[,] matrix){
            return new MatrixWrapper<T>(matrix);
        }
    }
}