namespace API.Contracts.Caching
{
    public record CachedCodePublication(
        Guid Id,
        string Description,
        string Code,
        string Lang,
        decimal rating,
        DateTime PostedTime,
        CachedUser Creator,
        List<Guid> RatedUsers
    );
}