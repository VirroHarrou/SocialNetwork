using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.News_.Queries.GetNewsList
{
    public class NewsListVm
    {
        public IList<NewsLookupDto> News { get; set; }
    }
}
