namespace API.Contracts.Caching
{
    public record CachedUser(
        string Username,
        Guid id
    );
}