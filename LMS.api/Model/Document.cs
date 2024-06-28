
namespace LMS.api.Model
{
    public class Document : IEntity<int>
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }

        public string SearchableString => $"{Id} {Created}";
    }
}
