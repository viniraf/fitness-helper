namespace WebAPI.Models;

public class MacronutrientCalculationModel
{
    public int BasalMetabolicRate { get; set; } = 0;

    public string Goal { get; set; } = string.Empty;

    public double GoalBasalMetabolicRate { get; set; } = 0;

    public double ProtGram { get; set; } = 0;

    public double CarbGram { get; set; } = 0;

    public double FatGram { get; set; } = 0;

    //public double ProtCal { get; set; } = 0;

    //public double CarbCal { get; set; } = 0;

    //public double FatCal { get; set; } = 0;

    public MacronutrientCalculationModel()
    {

    }

    public MacronutrientCalculationModel(int basalMetabolicRate, string goal, double goalBasalMetabolicRate, double protGram, double carbGram, double fatGram)
    {
        BasalMetabolicRate = basalMetabolicRate;
        Goal = goal;
        GoalBasalMetabolicRate = goalBasalMetabolicRate;
        ProtGram = protGram;
        CarbGram = carbGram;
        FatGram = fatGram;
    }
}
