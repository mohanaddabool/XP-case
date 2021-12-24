using XPAssignment.Helper.Response;
using XPAssignment.PostData.Show;
using XPAssignment.ViewModel;

namespace XPAssignment.Service.Interfaces
{
    public interface IShowService
    {
        List<ShowFromApi>? GetFromTvMaze();
        Response GetAll();
        Response GetByName(string name);
        Response Add(AddShow show);
        Response Update(EditShow show);
        Response Delete(int id);
    }
}
