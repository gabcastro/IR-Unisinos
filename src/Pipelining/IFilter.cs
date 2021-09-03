namespace InformationRetrieval.Pipelining
{
    public interface IFilter
    {
        dynamic Execute(dynamic inputData);
    }
}