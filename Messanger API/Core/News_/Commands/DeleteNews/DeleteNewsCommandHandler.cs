using MediatR;
using Messanger_API.Core.Models;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Domain.CustomExceptions;
using SocialNetwork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.News_.Commands.DeleteNews
{
    public class DeleteNewsCommandHandler : IRequestHandler<DeleteNewsCommand>
    {
        private readonly INewsDbContext _context;
        public DeleteNewsCommandHandler(INewsDbContext context) => 
            _context = context;

        public async Task Handle(DeleteNewsCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.News
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(News), request.Id);
            }

            _context.News.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
