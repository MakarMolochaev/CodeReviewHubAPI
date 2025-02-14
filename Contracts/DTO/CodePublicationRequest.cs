namespace API.Contracts.DTO
{
    public record CodePublicationRequest(
        string Description,
        string Code,
        string Lang
    );
}