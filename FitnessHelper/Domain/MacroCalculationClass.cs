namespace FitnessHelper.Domain
{
    public class MacroCalculationClass
    {
        public int BMR { get; set; } = 0;

        public string Goal { get; set; } = string.Empty;

        public double GoalBMR { get; set; } = 0;

        public double ProtGram { get; set; } = 0;

        public double CarbGram { get; set; } = 0;

        public double FatGram { get; set; } = 0;

        //public double ProtCal { get; set; } = 0;

        //public double CarbCal { get; set; } = 0;

        //public double FatCal { get; set; } = 0;

        public MacroCalculationClass()
        {
            
        }

        public MacroCalculationClass(int bmr, string goal, double goalBMR, double protGram, double carbGram, double fatGram)
        {
            BMR = bmr;
            Goal = goal;
            GoalBMR = goalBMR;
            ProtGram = protGram;
            CarbGram = carbGram;
            FatGram = fatGram;
        }
    }
}
