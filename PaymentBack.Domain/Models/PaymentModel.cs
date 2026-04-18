using PaymentBack.Domain.Enums;
using System.Text.Json.Serialization;


public class PaymentModel
{
    public long Id { get; set; }

    public required string WalletNumber { get; set; }

    public required long Account { get; set; }

    public required string Email { get; set; }

    public string? Phone { get; set; }

    public required decimal Amount { get; set; }

    public required string Currency { get; set; }

    public string? Comment { set; get; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Status Status { set; get; }

    public DateTime CreatedAt { set; get; }
}
