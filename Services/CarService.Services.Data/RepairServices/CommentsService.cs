using CarService.Data.Common.Repositories;
using CarService.Data.Models.CarRepair;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Services.Data.RepairServices
{
    public class CommentsService : ICommentsService
    {
        private readonly IDeletableEntityRepository<RepairComment> commentsRepository;

        public CommentsService(IDeletableEntityRepository<RepairComment> commentsRepository)
        {
            this.commentsRepository = commentsRepository;
        }

        public IEnumerable<RepairComment> CommentsByRepairId(string repairId)
        {
            var comments = this.commentsRepository.All().Where(x => x.RepairId == repairId).ToList();

            return comments;
        }

        public async Task Create(string repairId, string userId, string content, string parentId = null)
        {
            var newComment = new RepairComment
            {
                Content = content,
                ParentId = parentId,
                RepairId = repairId,
                UserId = userId,
            };

            await this.commentsRepository.AddAsync(newComment);

            await this.commentsRepository.SaveChangesAsync();
        }

        public bool IsInPostId(string commentId, string repairId)
        {
            var commentRepairId = this.commentsRepository
                .All()
                .Where(x => x.Id == commentId)
                .Select(x => x.RepairId)
                .FirstOrDefault();

            return commentRepairId == repairId;
        }
    }
}
