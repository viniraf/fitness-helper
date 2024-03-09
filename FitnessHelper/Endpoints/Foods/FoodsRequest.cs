namespace FitnessHelper.Endpoints.Foods;

public class FoodsRequest
{
    public string Name { get; set; } = string.Empty;

    public double QtyProtPerGram { get; set; }

    public double QtyCarbPerGram { get; set; }

    public double QtyFatPerGram { get; set; }
}
