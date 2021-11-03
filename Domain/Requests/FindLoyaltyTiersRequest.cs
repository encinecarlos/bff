namespace POC.Bff.Web.Domain.Requests
{
    public class FindLoyaltyTiersRequest
    {
        public FindLoyaltyTiersRequest(bool validateLoyaltyPermission)
        {
            ValidateLoyaltyPermission = validateLoyaltyPermission;
        }

        public bool ValidateLoyaltyPermission { get; }
    }
}