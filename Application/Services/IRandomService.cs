namespace Application.Services
{
    public interface IRandomService
    {
        Task<string> RandomSevenNumberCode();
        Task<string> RandomSixNumberCode();
    }
}
