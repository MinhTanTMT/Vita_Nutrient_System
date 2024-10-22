using AutoMapper;
using SEP490_G87_Vita_Nutrient_System_API.Domain.ResponseModels;
using SEP490_G87_Vita_Nutrient_System_API.Models;

namespace SEP490_G87_Vita_Nutrient_System_API.Mapper
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            //From User to CommonUserResponse
            CreateMap<User, CommonUserResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => new UserRole
                {
                    RoleId = src.Role,
                    RoleName = src.RoleNavigation != null ? src.RoleNavigation.RoleName : string.Empty
                }));

            //From User to UserDetailResponse
            bool? nullableBool = null;
            int? nullableInt = null;
            short? nullableShort = null;
            double? nullableDouble = null;

            CreateMap<User, UserDetailResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Urlimage, opt => opt.MapFrom(src => src.Urlimage))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Dob, opt => opt.MapFrom(src => src.Dob))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
            .ForMember(dest => dest.Account, opt => opt.MapFrom(src => src.Account))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => new UserRole
            {
                RoleId = src.Role,
                RoleName = src.RoleNavigation != null ? src.RoleNavigation.RoleName : string.Empty
            }))
            .ForMember(dest => dest.DetailsInformation, opt => opt.MapFrom(src => src.UserDetail != null
                ? new UserDetailResponse.UserDetail
                {
                    Description = src.UserDetail.DescribeYourself ?? string.Empty,
                    Height = src.UserDetail.Height ?? nullableShort,
                    Weight = src.UserDetail.Weight ?? nullableShort,
                    Age = src.UserDetail.Age ?? nullableShort,
                    WantImprove = src.UserDetail.WantImprove ?? string.Empty,
                    UnderlyingDisease = src.UserDetail.UnderlyingDisease ?? string.Empty,
                    InforConfirmGood = src.UserDetail.InforConfirmGood ?? string.Empty,
                    InforConfirmBad = src.UserDetail.InforConfirmBad ?? string.Empty,
                    IsPremium = src.UserDetail.IsPremium ?? nullableBool
                }
                : new UserDetailResponse.UserDetail
                {
                    Description = string.Empty,
                    Height = nullableShort,
                    Weight = nullableShort,
                    Age = nullableShort,
                    WantImprove = string.Empty,
                    UnderlyingDisease = string.Empty,
                    InforConfirmGood = string.Empty,
                    InforConfirmBad = string.Empty,
                    IsPremium = nullableBool
                }));

            //From User to NutritionistDetailResponse
            CreateMap<User, NutritionistDetailResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Urlimage, opt => opt.MapFrom(src => src.Urlimage))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Dob, opt => opt.MapFrom(src => src.Dob))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
            .ForMember(dest => dest.Account, opt => opt.MapFrom(src => src.Account))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => new UserRole
            {
                RoleId = src.Role,
                RoleName = src.RoleNavigation != null ? src.RoleNavigation.RoleName : string.Empty
            }))
            .ForMember(dest => dest.DetailsInformation, opt => opt.MapFrom(src => src.NutritionistDetail != null
                ? new NutritionistDetailResponse.NutritionistDetail
                {
                    Id = src.NutritionistDetail.Id,
                    Description = src.NutritionistDetail.DescribeYourself ?? string.Empty,
                    Height = src.NutritionistDetail.Height ?? null,
                    Weight = src.NutritionistDetail.Weight ?? null,
                    Age = src.NutritionistDetail.Age ?? null,
                    Rate = src.NutritionistDetail.Rate ?? null,
                    NumberRate = src.NutritionistDetail.NumberRate ?? null
                }
                : new NutritionistDetailResponse.NutritionistDetail
                {
                    Id = 0,
                    Description = string.Empty,
                    Height = null,
                    Weight = null,
                    Age = null,
                    Rate = null,
                    NumberRate = null
                }));

            //From ExpertPackage to ExpertPackageResponse
            CreateMap<ExpertPackage, ExpertPackageResponse>();
        }

    }
}
