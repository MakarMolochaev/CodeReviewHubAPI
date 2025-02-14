namespace API.Contracts.DTO
{
    public record RegisterUserRequest(
        string Username,
        string Email,
        string Password
    );
}