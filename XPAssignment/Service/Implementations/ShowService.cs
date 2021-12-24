using System.Net;
using Newtonsoft.Json;
using XPAssignment.DbModels;
using XPAssignment.Helper.Response;
using XPAssignment.PostData.Show;
using XPAssignment.Repository.Interfaces;
using XPAssignment.Service.Interfaces;
using XPAssignment.ViewModel;
using static System.Net.WebRequest;
using Show = XPAssignment.DbModels.Show;

namespace XPAssignment.Service.Implementations
{
    public class ShowService : IShowService
    {
        public IShowRepository ShowRepository { get; }
        private readonly IConfiguration _configuration;

        public ShowService(IShowRepository showRepository, IConfiguration configuration)
        {
            ShowRepository = showRepository;
            _configuration = configuration;
        }

        public List<ShowFromApi>? GetFromTvMaze()
        {
            var apiLink = _configuration.GetValue<string>("TvMaze:apiLink");
            var request = Create(apiLink);
            var response = (HttpWebResponse)request.GetResponse();
            string? result = null;
            using (var stream = response.GetResponseStream())
            {
                var reader = new StreamReader(stream);
                result = reader.ReadToEnd();
                reader.Close();
            }
            var data = JsonConvert.DeserializeObject<List<ShowFromApi>>(result);
            return data?.OrderByDescending(show => show.Premiered).ThenByDescending(show => show.Rating?.Average).ToList();
        }

        public Response GetAll()
        {
            var data = ShowRepository.GetAll();
            var showViewModel = MapToShows(data);
            if (showViewModel == null)
            {
                return new Response
                {
                    Shows = null,
                    Show = null,
                    Message = "There are no result",
                    State = ResponseState.NotFound,
                };
            }

            return new Response
            {
                Shows = showViewModel,
                Show = null,
                Message = $"Result is : ",
                State = ResponseState.Found,
            };
        }

        public Response GetByName(string name)
        {
            var data = ShowRepository.GetByName(name);
            var result = MapToShow(data);
            if (result == null)
                return new Response
                {
                    Show = null,
                    Shows = null,
                    Message = $"Show with name {name} is not found please check name",
                    State = ResponseState.NotFound,
                };
            return new Response
            {
                Show = result,
                Shows = null,
                Message = $"Show with name {name} give this result: ",
                State = ResponseState.Found
            };
        }

        public Response Add(AddShow show)
        {
            var showDb = new DbModels.Show
            {
                Language = show.Language,
                Name = show.Name,
                Premiered = show.Premiered,
                Summary = show.Summary,
                Genres = MapGenresToAddModel(show?.Genres),
            };
            var checkName = ShowRepository.GetByName(showDb.Name);
            if (checkName?.Result != null)
                return new Response
                {
                    Shows = null,
                    Show = null,
                    Message = $"There are already show with name {showDb.Name}",
                    State = ResponseState.DuplicateName,
                };
            var data = ShowRepository.AddShow(showDb);
            var result = MapToShow(data);
            return new Response
            {
                Message = $"This show has been created",
                State = ResponseState.Created,
                Show = result,
                Shows = null,
            };
        }

        public Response Update(EditShow show)
        {
            var showDb = new Show
            {
                Id = show.Id,
                Language = show.Language,
                Name = show.Name,
                Premiered = show.Premiered,
                Summary = show.Summary,
                Genres = MapGenreToUpdateDataModel(show.Genres, show)
            };
            var data = ShowRepository.UpdateShow(showDb);
            if (data == null)
                return new Response
                {
                    Show = null,
                    Shows = null,
                    Message = $"Show with id {show.Id} is not found",
                    State = ResponseState.NotFound,
                };
            var result = MapToShowWithoutAsync(data);
            return new Response
            {
                Show = result,
                Message = $"Show with id {show.Id} is updated and this is the result",
                State = ResponseState.Edited,
                Shows = null,
            };
        }

        public Response Delete(int id)
        {
            var data = ShowRepository.DeleteShow(id);
            if (!data.Result)
                return new Response
                {
                    Show = null,
                    Shows = null,
                    Message = $"Show with id {id} is not found",
                    State = ResponseState.NotFound,
                };
            return new Response
            {
                Show = null,
                Message = $"Show with id {id} is deleted",
                Shows = null,
                State = ResponseState.Deleted,
            };
        }

        private List<ViewModel.Show>? MapToShows(Task<List<Show>>? shows)
        {
            return shows?.Result?.Select(show => new ViewModel.Show
            {
                Genres = MapToGenre(show.Genres?.ToList()),
                Id = show.Id,
                Language = show.Language,
                Name = show.Name,
                Summary = show.Summary,
                Premiered = show.Premiered,
            }).ToList();
        }

        private List<string>? MapToGenre(List<Genre>? genres)
        {
            return genres?.Select(genre => genre.Name).ToList();
        }

        private ViewModel.Show? MapToShow(Task<Show?>? show)
        {
            if (show?.Result == null) return null;
            return new ViewModel.Show
            {
                Genres = MapToGenre(show.Result.Genres?.ToList()),
                Id = show.Result.Id,
                Language = show.Result.Language,
                Name = show.Result.Name,
                Premiered = show.Result.Premiered,
                Summary = show.Result.Summary,
            };
        }

        private ViewModel.Show? MapToShowWithoutAsync(Show?show)
        {
            if (show == null) return null;
            return new ViewModel.Show
            {
                Genres = MapToGenre(show.Genres?.ToList()),
                Id = show.Id,
                Language = show.Language,
                Name = show.Name,
                Premiered = show.Premiered,
                Summary = show.Summary,
            };
        }

        private List<Genre>? MapGenreToUpdateDataModel(List<string>? genres, EditShow show)
        {
            return genres?.Select(genre => new Genre { Name = genre, ShowId = show.Id}).ToList();
        }

        private List<Genre>? MapGenresToAddModel(List<string>? genres)
        {
            return genres?.Select(genre => new Genre { Name = genre}).ToList();
        }
    }
}
