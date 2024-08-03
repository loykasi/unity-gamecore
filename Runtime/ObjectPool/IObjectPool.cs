namespace GameCore.ObjectPool
{
    public interface IObjectPool<T> {
        T Get();
        void Return(T item);
    }
}