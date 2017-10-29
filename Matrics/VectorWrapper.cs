namespace Matrics
{
    public class VectorWrapper<T>{
        private T[] innerVector;
        public VectorWrapper(T[] innerVector){
            this.innerVector = innerVector;
        }

        public T[] V(){
            return innerVector;
        }

        public static VectorWrapper<T> operator +(VectorWrapper<T> arg1, VectorWrapper<T> arg2){
            return arg1.V().Add(arg2);
        }
        public static VectorWrapper<T> operator -(VectorWrapper<T> arg1, VectorWrapper<T> arg2){
            return arg1.V().Subtract(arg2);
        }
        public static VectorWrapper<T> operator *(VectorWrapper<T> arg1, T scalar){
            return arg1.V().Multiply(scalar);
        }
        public static VectorWrapper<T> operator /(VectorWrapper<T> arg1, T scalar){
            return arg1.V().Divide(scalar);
        }
        public static T operator *(VectorWrapper<T> arg1, VectorWrapper<T> arg2){
            return arg1.V().DotProduct(arg2);
        }
        public static VectorWrapper<T> operator -(VectorWrapper<T> arg1){
            return arg1.V().Map((v, i) => -(dynamic)v);
        }

        public static implicit operator T[](VectorWrapper<T> wrapper){
            return wrapper.V();
        }
        public static implicit operator VectorWrapper<T>(T[] vector){
            return new VectorWrapper<T>(vector);
        }
    }
}