namespace Battleship.BL.Logic.Interface
{
    public interface IServiceLocator
    {
        T Resolve<T>(string name);
    }
}
