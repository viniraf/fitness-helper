namespace WebAPI.Models
{
    public class BasalMetabolicRateModel
    {

        public int RoundedBasalMetabolicRate { get; set; }

        public string Type { get; set; }

        public BasalMetabolicRateModel()
        {
            
        }

        public BasalMetabolicRateModel(int roundedBasalMetabolic, string type)
        {
            RoundedBasalMetabolicRate = roundedBasalMetabolic;
            Type = type;
        }
    }
}
