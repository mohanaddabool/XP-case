using XPAssignment.DbModels;

namespace XPAssignment.Repository.Interfaces;

public interface IShowRepository
{
    Task<List<Show>> GetAll();
    Task<Show?>? GetByName(string name);
    Task<Show?>? AddShow(Show show);
    Show? UpdateShow(Show show);
    Task<bool> DeleteShow(int id);
}