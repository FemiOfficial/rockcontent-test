using System;

using AutoMapper;
using DataAccess.Domain.Models;
using Api.Resources.Request.LikesRequest;
using DataAccess.Domain.Queries;

namespace Api.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<LikeRequestDto, Likes>();
            CreateMap<LikesQueryByPostRequestDto, LikesQuery>();
            CreateMap<LikesQueryByClientReferenceIdRequestDto, LikesQuery>();
        }
    }
}
