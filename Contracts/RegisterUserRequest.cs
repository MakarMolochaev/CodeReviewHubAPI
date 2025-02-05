namespace API.Contracts
{
    public record RegisterUserRequest(
        string Username,
        string Email,
        string Password
    );
}