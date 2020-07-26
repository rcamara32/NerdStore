namespace NerdStore.Sales.Application.Queries.ViewModels
{
    public class CartPaymentViewModel
    {
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string CvvCode { get; set; }
    }

}
