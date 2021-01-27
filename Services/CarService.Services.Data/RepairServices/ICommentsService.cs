namespace CarService.Services.Data.RepairServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using global::CarService.Data.Models.CarRepair;

    public interface ICommentsService
    {
        Task Create(string repairId, string userId, string content, string parentId = null);

        bool IsInRepairId(string commentId, string repairId);

        IEnumerable<RepairComment> CommentsByRepairId(string repairId);
    }
}
