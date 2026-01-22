using MES.Domain.Entities;

namespace MES.Application.Interfaces;

public interface ICommandService
{
    Task<List<EquipCommand>> GetPendingAsync(int equipId);
    Task HandleAckAsync(int commandId, bool success);
}