namespace Services.Tags.Contracts;

public interface ITagRepository
{
    Task<bool> TagsValidation(HashSet<Guid> tags ,
        CancellationToken cancellationToken);
}