namespace CarService.Services.Data.RepairServices
{
    using System.Threading.Tasks;

    public interface IVoteService
    {
        Task VoteAsync(string repairId, string userId, bool isUpVote);

        int GetVotes(string repairId);
    }
}
