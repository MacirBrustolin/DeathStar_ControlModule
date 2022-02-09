namespace DeathStar_ControlModule.Models
{
    public class APIResultsModel<ViewModel>
    {
        public string Next { get; set; }
        public IReadOnlyList<ViewModel> Results { get; set; }
    }
}
