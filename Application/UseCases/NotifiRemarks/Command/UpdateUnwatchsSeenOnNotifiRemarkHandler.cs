using Application.Common.Messaging;
using Application.UseCases.Requests.Commands.UpdateRequest;
using Domain.Entities.Accounts;
using Domain.Repositories;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.NotifiRemarks.Command
{
    public sealed class UpdateUnwatchsSeenOnNotifiRemarkHandler : ICommandHandler<UpdateUnwatchsSeenOnNotifiRemarkCommand>
    {
        private readonly IUnitOfWorkRepository _repo;

        public UpdateUnwatchsSeenOnNotifiRemarkHandler(IUnitOfWorkRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result> Handle(UpdateUnwatchsSeenOnNotifiRemarkCommand request, CancellationToken cancellationToken)
        {
            var oldNotification = await _repo.notificationRemarkRepo.GetNotifiRemarkById(request.Id);

            if (oldNotification == null)
            {
                return Result.Failure(new Error("Error", "No data exists"), "No Data");
            }

            if (oldNotification != null)
            {
                oldNotification.Unwatchs = 0;
            }

            _repo.notificationRemarkRepo.Update(oldNotification);

            try
            {
                await _repo.SaveChangesAsync(cancellationToken);
                return Result.Success("You have updated successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Error error = new("Error.UpdateCommandHandler", "There is an error saving data!");
                return Result.Failure(error, "Update request failed");
            }
        }
    }
}
