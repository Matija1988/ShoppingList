namespace App.DTO
{
    public sealed record UserReadResponse
    {
        public Guid Id { get; init; }
        public string Email { get; init; }
        public string Username { get; init; }
        public string Password { get; init; }
        public DateTime DateCreated { get; init; }
        public DateTime DateUpdated { get; init; }
        public bool IsActive { get; init; }
    }
}
