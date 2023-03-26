using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.News_.Commands.CreateNews
{
    public class CreateNewsCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public string Topic { get; set; }
        public string MainText { get; set; }
        public string ImageUrl { get; set; }
    }
}
