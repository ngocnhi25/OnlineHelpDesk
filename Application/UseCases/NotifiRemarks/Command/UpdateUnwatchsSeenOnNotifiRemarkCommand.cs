using Application.Common.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.NotifiRemarks.Command
{
    public record class UpdateUnwatchsSeenOnNotifiRemarkCommand(Guid Id) : ICommand;

}
