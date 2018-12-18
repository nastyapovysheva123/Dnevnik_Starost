using Project1.Models;

namespace Project1.Managers
{
    public interface IGroupsManager
    {
        Group Create(Group group);
        Group GetGroup(string id);
        void Update(Group group);
    }
}