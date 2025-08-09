namespace InfotecsInternTask.DomainLayer.Entities;

public partial class Result
{
    public int Resultid { get; set; }

    public string Filename { get; set; } = null!;

    public int Deltatime { get; set; }

    public DateTime Mindate { get; set; }

    public int Averageexecutiontime { get; set; }

    public double Averagevalue { get; set; }

    public double Medianvalue { get; set; }

    public double Maxvalue { get; set; }

    public double Minvalue { get; set; }

    public virtual ICollection<Value> Values { get; set; } = new List<Value>();
}
