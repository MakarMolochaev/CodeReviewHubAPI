namespace API.Contracts.DTO
{
    public record UserResponse(
        string Username,
        Guid id
    );
}