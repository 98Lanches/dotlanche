#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace DotLanches.Api.Dtos
{
    public class ClienteDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
    }
}