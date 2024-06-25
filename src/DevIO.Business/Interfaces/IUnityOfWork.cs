namespace DevIO.Business.Interfaces
{
    public interface IUnityOfWork
    {
        Task<bool> Commit();
    }
}
