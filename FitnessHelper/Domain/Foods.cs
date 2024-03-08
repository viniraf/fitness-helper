namespace FitnessHelper.Domain;

public class Foods
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public double QtyProtPerGram { get; set; }

    public double QtyCarbPerGram { get; set; }

    public double QtyFatPerGram { get; set; }

    public double QtyCalPerGram { get; set; }

    public Foods()
    {
        
    }
}
