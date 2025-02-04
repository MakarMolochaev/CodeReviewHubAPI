namespace API.Contracts
{
    public record CodePublicationRequest(
        string Description,
        string Code,
        string Lang,
        decimal rating,
        Guid creatorId //Удалить !!! ну я уже написал почему
    );
}