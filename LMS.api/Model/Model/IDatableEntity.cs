namespace LMS.api.Model
{
    public interface IDatableEntity : IEntity<int>
    {
        DateTime LastModified { get; set; }
        DateTime Starts { get; set; }
        DateTime Ends { get; set; }
    }
}
