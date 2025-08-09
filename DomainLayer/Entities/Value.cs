namespace InfotecsInternTask.DomainLayer.Entities;

public partial class Value
{
    public long Valueid { get; set; }

    public DateTime Date { get; set; }

    public int Executiontime { get; set; }

    public double Value1 { get; set; }

    public int? Resultid { get; set; }

    public virtual Result? Result { get; set; }
}
