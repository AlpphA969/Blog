namespace Services.Tags.Contracts;

public interface ITagRepository
{
    Task<bool> TagsValidation(List<Guid> tags ,
        CancellationToken cancellationToken);
}