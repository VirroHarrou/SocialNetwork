using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.News_.Queries.GetNewsList
{
    public class GetNewsListQuery : IRequest<NewsListVm>
    {
        public int Count { get; set; }
        public int Skip { get; set; }
    }
}
