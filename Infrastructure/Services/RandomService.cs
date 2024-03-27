using Application.Services;

namespace Infrastructure.Services
{
    public class RandomService : IRandomService
    {
        public Task<string> RandomSevenNumberCode()
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 9999999);
            string formattedNumber = randomNumber.ToString("D7");
            return Task.FromResult(formattedNumber);
        }
        
        public Task<string> RandomSixNumberCode()
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 999999);
            string formattedNumber = randomNumber.ToString("D6");
            return Task.FromResult(formattedNumber);
        }
    }
}
