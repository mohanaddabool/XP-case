using XPAssignment.ViewModel;

namespace XPAssignment.Helper.Response
{
    public class Response
    {
        public ResponseState State { get; set; }
        public string Message { get; set; } = null!;
        public Show? Show { get; set; }
        public List<Show>? Shows { get; set; }
        public List<ShowFromApi>? ShowsFromApi { get; set; }
    }
}
