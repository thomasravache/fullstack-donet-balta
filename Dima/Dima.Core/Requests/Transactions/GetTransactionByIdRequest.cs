namespace Dima.Core.Requests.Transactions;

public class GetTransactionByIdRequest : PagedRequest   
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}