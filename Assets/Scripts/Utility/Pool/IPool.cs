

public interface IPool  {

     void ClearAll();

     void ClearKeepActive();

     void ReSize(int initSize, int maxSize);

     int ActivatedCount { get; }

     int NotActiveCount { get; }
}
