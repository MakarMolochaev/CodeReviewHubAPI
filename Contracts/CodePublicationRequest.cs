namespace API.Contracts
{
    public record CodePublicationRequest(
        string Description,
        string Code,
        string Lang
    );
}