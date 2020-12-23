using CarService.Data.Common.Repositories;
using CarService.Data.Models.CarRepair;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Services.Data.RepairServices
{
    public class VoteService : IVoteService
    {
        private readonly IRepository<Vote> votesRepository;

        public VoteService(IRepository<Vote> votesRepository)
        {
            this.votesRepository = votesRepository;
        }

        public int GetVotes(string repairId)
        {
            var votes = this.votesRepository.All()
                .Where(x => x.RepairId == repairId).Sum(x => (int)x.Type);
            return votes;
        }

        public async Task VoteAsync(string repairId, string userId, bool isUpVote)
        {
            var vote = this.votesRepository.All()
                .FirstOrDefault(x => x.RepairId == repairId && x.UserId == userId);
            if (vote != null)
            {
                vote.Type = isUpVote ? VoteType.UpVote : VoteType.DownVote;
            }
            else
            {
                vote = new Vote
                {
                    RepairId = repairId,
                    UserId = userId,
                    Type = isUpVote ? VoteType.UpVote : VoteType.DownVote,
                };

                await this.votesRepository.AddAsync(vote);
            }

            await this.votesRepository.SaveChangesAsync();
        }
    }
}
