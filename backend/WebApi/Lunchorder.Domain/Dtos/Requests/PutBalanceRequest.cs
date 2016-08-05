namespace Lunchorder.Domain.Dtos.Requests
{
    public class PutBalanceRequest
    {
        public string UserId { get; set; }
        public decimal Amount { get; set; }
    }
}