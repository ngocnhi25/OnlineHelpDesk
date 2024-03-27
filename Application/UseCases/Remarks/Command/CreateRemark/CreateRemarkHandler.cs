using Application.Common.Messaging;
using Application.DTOs.Remarks;
using Application.DTOs.Requests;
using AutoMapper;
using Domain.Entities.Accounts;
using Domain.Entities.Requests;
using Domain.Repositories;

using SharedKernel;
using System.Collections.Generic;

namespace Application.UseCases.Remarks.Command.CreateRemark
{
    public sealed class CreateRemarkHandler : ICommandHandler<CreateRemarkCommand, RemarkDTO>
    {
        private readonly IUnitOfWorkRepository _repo;
        private readonly IMapper _mapper;
        public CreateRemarkHandler(IUnitOfWorkRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Result<RemarkDTO>> Handle(CreateRemarkCommand request, CancellationToken cancellationToken)
        {
            var listfacilityHeads = await _repo.accountRepo.GetAllFacilityHeads();
            var listAssigneeRalateToRequestId = await _repo.assigneesRepo.GetListByAssigneeHandleRequest(Guid.Parse(request.RequestId));
            var listNotifiRemarkRelateToRequestId = await _repo.notificationRemarkRepo.GetNotificationRemarkByRequestId(Guid.Parse(request.RequestId));
            var accountChatting = await _repo.accountRepo.GetByAccountId(request.AccountId);
            var requetRelateUserById = await _repo.requestRepo.GetRequestById(Guid.Parse(request.RequestId));
 
            var remarkData = new Remark
            {
                Id = new Guid(),
                AccountId = request.AccountId,
                RequestId = new Guid(request.RequestId),
                Comment = request.Comment,
                CreateAt = DateTime.Now,
                Enable = true
            };
            _repo.remarkRepo.Add(remarkData);

            if (accountChatting != null)
            {
                foreach (var head in listfacilityHeads)
                {
                    var foundNotifiRemark = listNotifiRemarkRelateToRequestId.
                                                FirstOrDefault(notifiRemark => notifiRemark.AccountId == head.AccountId);
                    if (head.AccountId != accountChatting.AccountId && foundNotifiRemark != null)
                    {
                        foundNotifiRemark.Unwatchs += 1;
                        foundNotifiRemark.IsSeen = false;
                        foundNotifiRemark.UpdatedAt = DateTime.Now;
                        _repo.notificationRemarkRepo.Update(foundNotifiRemark);
                    }
                }

                if (listAssigneeRalateToRequestId != null)
                {
                    foreach (var assignee in listAssigneeRalateToRequestId)
                    {
                        var foundNotifiRemark = listNotifiRemarkRelateToRequestId.
                                                FirstOrDefault(notifiRemark => notifiRemark.AccountId == assignee.AccountId);

                        if (assignee.AccountId != accountChatting.AccountId && foundNotifiRemark != null)
                        {
                            foundNotifiRemark.Unwatchs += 1;
                            foundNotifiRemark.IsSeen = false;
                            foundNotifiRemark.UpdatedAt = DateTime.Now;
                            _repo.notificationRemarkRepo.Update(foundNotifiRemark);
                        }
                    }
                }

                if (accountChatting != null &&
                   (accountChatting.Role?.RoleTypes?.Id == 2 ||
                    accountChatting.Role?.RoleTypes?.Id == 3))
                {

                    var foundNotifiRemarkOfUser = listNotifiRemarkRelateToRequestId.
                                                Where(noti => noti.Account!.Role!.RoleTypes!.Id == 1).           
                                                FirstOrDefault(notifiRemark => notifiRemark.RequestId == Guid.Parse(request.RequestId));
                    if (foundNotifiRemarkOfUser != null)
                    {
                        foundNotifiRemarkOfUser.Unwatchs += 1;
                        foundNotifiRemarkOfUser.IsSeen = false;
                        foundNotifiRemarkOfUser.UpdatedAt = DateTime.Now;
                        _repo.notificationRemarkRepo.Update(foundNotifiRemarkOfUser);
                    }
                }
            }

            if(requetRelateUserById != null)
            {
                requetRelateUserById.UpdateAt = DateTime.Now;
                _repo.requestRepo.Update(requetRelateUserById);
            }



            try
            {
                await _repo.SaveChangesAsync(cancellationToken);

                var latestRemark = await _repo.remarkRepo.GetLatestRemark(remarkData.Id.ToString().ToUpper());
                var latestRemarkMapper = _mapper.Map<RemarkDTO>(latestRemark);

                return Result.Success<RemarkDTO>(latestRemarkMapper, "Create remark successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Error error = new("Error.RemarkCommandHandler", "There is an error saving data!");
                return Result.Failure<RemarkDTO>(error, "Create Remark failed!");
            }
        }

        
    }
}
