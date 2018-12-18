using Project1.Models;

namespace Project1.Managers
{
    public interface ICaptainsManager
    {
        Captain Create(Captain captain);
        Captain GetCaptain(string id);
        Captain GetCurrent();
        Captain GetCurrent(string email, string password);
        void Unload();
        void Update(Captain captain);
    }
}