namespace LMS.api.Model
{
    public interface IDatableEntity : IEntity
    {
        DateTime LastModified { get; set; }
        DateTime Start { get; set; }
        DateTime End { get; set; }
    }
}
