namespace Trecom.Shared.Events;

public class PaymentMessage
{
    public string CardNumber { get; set; }

    public PaymentMessage(string cardNumber)
    {
        CardNumber = cardNumber;
    }
}