namespace LMS.api.Model
{
    public interface IEntity
    {
        int Id { get; set; }
        DateTime Created { get; set; }
        string SearchableString { get; }
    }

    public interface IEntity<TKey> where TKey : notnull, IEquatable<TKey>
    {
        TKey Id { get; set; }
        DateTime Created { get; set; }
        string SearchableString { get; }
    }
}
