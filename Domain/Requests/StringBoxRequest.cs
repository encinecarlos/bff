namespace POC.Bff.Web.Domain.Requests
{
    public class StringBoxRequest : ComponentBaseRequest
    {
        public int Inputs { get; set; }
        public int Outputs { get; set; }
        public int? Fuses { get; set; }
    }
}