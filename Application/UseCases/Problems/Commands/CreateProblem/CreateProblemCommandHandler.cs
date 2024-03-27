using Application.Common.Messaging;
using Domain.Entities.Requests;
using Domain.Repositories;
using SharedKernel;

namespace Application.UseCases.Problems.Commands.CreateProblem
{
    public sealed class CreateProblemCommandHandler : ICommandHandler<CreateProblemCommand>
    {
        private readonly IUnitOfWorkRepository _repo;

        public CreateProblemCommandHandler(IUnitOfWorkRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result> Handle(CreateProblemCommand request, CancellationToken cancellationToken)
        {
            var newProblem = new Problem
            {
                Title = request.Title,
                IsDisplay = true
            };
            _repo.problemRepo.Add(newProblem);

            try
            {
                await _repo.SaveChangesAsync(cancellationToken);

                return Result.Success("Create problem successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                Error error = new("Error.RegisterCommandHandler", "There is an error saving data!");
                return Result.Failure(error, "Register failed!");
            }
        }
    }
}
