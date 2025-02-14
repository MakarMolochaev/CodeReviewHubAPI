namespace API.Contracts.DTO
{
    public record LoginUserRequest(
        string Email,
        string Password
    );
}