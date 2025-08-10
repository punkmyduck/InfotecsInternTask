namespace InfotecsInternTask.InfrastructureLayer.Interfaces
{
    public interface ICsvParser<T>
    {
        List<T> Parse(Stream stream);
    }
}
