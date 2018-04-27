public interface IObjectPool :IPool {

	T Get<T>() where T:class;
    void Remove<T>(T obj);
}
