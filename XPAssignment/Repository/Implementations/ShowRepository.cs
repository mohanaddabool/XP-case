using Microsoft.EntityFrameworkCore;
using XPAssignment.Context;
using XPAssignment.DbModels;
using XPAssignment.Repository.Interfaces;

namespace XPAssignment.Repository.Implementations
{
    public class ShowRepository: IShowRepository
    {
        public XPAssignmentDbContext Context { get; }

        public ShowRepository(XPAssignmentDbContext context)
        {
            Context = context;
        }

        public Task<List<Show>> GetAll()
        {
            var shows = Context.Shows.Include(show => show.Genres).AsNoTracking().ToListAsync();
            return shows;
        }

        public async Task<Show?>? GetByName(string name)
        {
            var show = Context.Shows.Where(s => s.Name == name).Include(s => s.Genres).AsNoTracking().FirstOrDefaultAsync();
            if (show?.Result == null) return null;
            return await show;
        }

        public async Task<Show?>? AddShow(Show show)
        {
            await Context.Shows.AddAsync(show);
            await Context.SaveChangesAsync();
            return show;
        }

        public Show? UpdateShow(Show show)
        {
            var existingShow = Context.Shows.Where(s => s.Id == show.Id).Include(s => s.Genres).FirstOrDefault();
            if (existingShow != null)
            {
                Context.Entry(existingShow).CurrentValues.SetValues(show);
                if (existingShow.Genres != null)
                {
                    Context.RemoveRange(existingShow.Genres);
                }

                if (show.Genres != null) Context.Genres.AddRange(show.Genres);
            }
            else
                return null;

            Context.SaveChanges();
            return show;
        }

        public async Task<bool> DeleteShow(int id)
        {
            var show = Context.Shows.Where(s => s.Id == id).Include(s => s.Genres).FirstOrDefaultAsync();
            if (show.Result == null) return await Task.FromResult(false);
            Context.Remove(show);
            await Context.SaveChangesAsync();
            return await Task.FromResult(true);
        }
    }
}
