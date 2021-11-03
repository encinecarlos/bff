namespace POC.Bff.Web.Domain.Requests
{
    public class VoltageRequest
    {
        public decimal PhaseNeutral { get; set; }
        public decimal PhasePhase { get; set; }

    }
}