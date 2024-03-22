using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FitnessHelper.Common;

public class Calculations
{
    public Calculations()
    {
        
    }

    public double MaleBasalMetabolicRate(double weight, double height, int age)
    {
        double maleBasalMetabolicRate = 0;

        maleBasalMetabolicRate = (10 * weight) + (6.25 * height) - (5 * age) + 5;

        return maleBasalMetabolicRate;
    }

    public double FemaleBasalMetabolicRate(double weight, double height, int age)
    {
        double femaleBasalMetabolicRate = 0;

        femaleBasalMetabolicRate = (10 * weight) + (6.25 * height) - (5 * age) + 161;

        return femaleBasalMetabolicRate;
    }



}
