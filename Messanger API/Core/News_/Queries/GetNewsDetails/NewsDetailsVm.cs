using AutoMapper;
using SocialNetwork.Common.Mappings;
using SocialNetwork.Domain.Models;

namespace SocialNetwork.Core.News_.Queries.GetNewsDetails
{
    public class NewsDetailsVm : IMapWith<News>
    {
        public Guid Id { get; set; }
        public string Topic { get; set; }
        public string MainText { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<News, NewsDetailsVm>()
                .ForMember(newsVm => newsVm.Topic,
                opt => opt.MapFrom(news => news.Topic))
                .ForMember(newsVm => newsVm.MainText,
                opt => opt.MapFrom(news => news.MainText))
                .ForMember(newsVm => newsVm.ImageUrl,
                opt => opt.MapFrom(news => news.ImageUrl))
                .ForMember(newsVm => newsVm.CreatedAt,
                opt => opt.MapFrom(news => news.CreatedAt))
                .ForMember(newsVm => newsVm.UpdatedAt,
                opt => opt.MapFrom(news => news.UpdatedAt))
                .ForMember(newsVm => newsVm.Id,
                opt => opt.MapFrom(news => news.Id));
        }
    }
}
