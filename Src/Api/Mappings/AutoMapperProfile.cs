using System;

using AutoMapper;
using DataAccess.Domain.Models;
using Api.Communication.Request;
using DataAccess.Domain.Queries;

namespace Api.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<LikeRequestDto, LikeDto>();
            CreateMap<LikeDto, Likes>();

            CreateMap<Likes, LikesQuery>();
            CreateMap<Likes, LikeRequestDto>();
            CreateMap<LikesQueryByPostRequestDto, LikesQuery>();
            CreateMap<LikesQueryByClientReferenceIdRequestDto, LikesQuery>();
        }
    }
}
