using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trecom.Api.Services.Order.Application.Constants;
using Trecom.Api.Services.Order.Application.Features.Dtos;
using Trecom.Shared.Models;
using Trecom.Shared.Pipelines.Authorization;
using Trecom.Shared.Pipelines.Logging;

namespace Trecom.Api.Services.Order.Application.Features.Commands;

public record CreateOrderCommand(CreateOrderDto CreateOrderDto) : CommandBase<OrderResponseDto>,ISecuredRequest
{
    public string[] Roles => new[] { BusinessRoleConstants.Admin, BusinessRoleConstants.User,BusinessRoleConstants.Moderator };
}