using Newtonsoft.Json;
using XPAssignment.Context;
using XPAssignment.DbModels;
using XPAssignment.Import.model;

namespace XPAssignment.Import
{
    public class Import: IImport
    {
        public XPAssignmentDbContext Context { get; }
        public Import(XPAssignmentDbContext context)
        {
            Context = context;
        }

        public void ReadAndInsert()
        {
            using StreamReader r = new StreamReader("query.json");
            string json = r.ReadToEnd();
            List<ImportModel>? items = JsonConvert.DeserializeObject<List<model.ImportModel>>(json);
            // AddToShowTable(items);
            AddToGenreTable(items, Context.Shows.ToList());
            //if (items != null)
            //{
            //    for (var index = 0; index < items.Count; index++)
            //    {
            //        var item = items[index];
            //        if (item.Genres == null) continue;
            //        foreach (var genere in item.Genres)
            //        {
            //            var showId = Context.Shows.Where(x => x.Name == item.Name).Select(x => x.Id)
            //                .FirstOrDefault();
            //            var genre = new Genre
            //            {
            //                Name = genere,
            //                ShowId = showId,
            //            };
            //            Context.Genres.Add(genre);
            //            Context.SaveChanges();
            //        }
            //    }
            //}
        }

        private void AddToShowTable(List<ImportModel>? model)
        {
            if (model == null) return;
            foreach (var item in model)
            {
                if (item.Premiered.Year >= 2014)
                {
                    var show = new Show
                    {
                        Name = item.Name,
                        Language = item.Language,
                        Premiered = item.Premiered,
                        Summary = item.Summary,
                    };
                    Context.Shows.Add(show);
                    Context.SaveChanges();
                }
                
            }
        }

        private void AddToGenreTable(List<ImportModel>? model, List<Show> shows)
        {
            if(model == null) return;
            for (var index = 0; index < shows.Count; index++)
            {
                var item = model.Where(x => x.Name == shows[index].Name)?.FirstOrDefault();
                if (item?.Genres == null) continue;
                foreach (var genre in item.Genres)
                {
                    var item1 = item;
                    var showId = Context.Shows.Where(x => x.Name == item1.Name).Select(x => x.Id)
                        .FirstOrDefault();
                    var genreDb = new Genre
                    {
                        Name = genre,
                        ShowId = showId,
                    };
                    Context.Genres.Add(genreDb);
                    Context.SaveChanges();
                }
            }
        }
    }
}
