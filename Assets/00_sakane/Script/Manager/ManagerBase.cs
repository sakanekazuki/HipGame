// マネージャーのベース
public abstract class ManagerBase<T> : Singleton<T> where T : Singleton<T>
{
}