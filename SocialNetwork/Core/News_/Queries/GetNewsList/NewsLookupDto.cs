using AutoMapper;
using SocialNetwork.Core.Common.Mappings;
using SocialNetwork.Domain.Models;

namespace SocialNetwork.Core.News_.Queries.GetNewsList
{
    public class NewsLookupDto : IMapWith<News>
    {
        public Guid Id { get; set; }
        public string Topic { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<News, NewsLookupDto>()
                .ForMember(newsDto => newsDto.Id,
                opt => opt.MapFrom(news => news.Id))
                .ForMember(newsDto => newsDto.Topic,
                opt => opt.MapFrom(news => news.Topic));
        }
    }
}
