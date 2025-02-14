namespace API.Contracts.DTO
{
    public record CodePublicationResponse(
        Guid Id,
        string Description,
        string Code,
        string Lang,
        decimal rating,
        DateTime PostedTime,
        UserResponse Creator,
        List<Guid> RatedUsers
    );
}