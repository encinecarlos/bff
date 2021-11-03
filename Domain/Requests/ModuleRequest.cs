using POC.Shared.Domain.Fixed;
using POC.Shared.Domain.ValueObjects;

namespace POC.Bff.Web.Domain.Requests
{
    public class ModuleRequest : ComponentBaseRequest
    {
        public decimal OpenCircuitVoltage { get; set; }
        public decimal ShortCircuitCurrent { get; set; }
        public decimal Efficiency { get; set; }
        public decimal CurrentAtMaxPower { get; set; }
        public decimal VoltageAtMaxPower { get; set; }
        public object Cell { get; set; }
        public Power Power { get; set; }
        public Voltage StringVoltage { get; set; }
        public object Temperature { get; set; }
        public object Dimension { get; set; }
        public int ConnectionType { get; set; }
        public OrientationType ForbiddenOrientation { get; set; }
    }
}