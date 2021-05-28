using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Budget.Resources.Auth;
using Budget.Data.Entities;
using CryptoHelper;
using Budget.Resources.Category;

namespace Budget.Mapping
{
    public class MappingProfile:Profile
    {
        private static string filePathUrl = "https://localhost:44305/uploads/";
        public MappingProfile()
        {
            CreateMap<RegisterResource, User>()
                .ForMember(d => d.AddedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(d => d.AddedBy, opt => opt.MapFrom(src => src.Fullname))
                .ForMember(d => d.Status, opt => opt.MapFrom(src => true))
                .ForMember(d => d.Password, opt => opt.MapFrom(src => CryptoHelper.Crypto.HashPassword(src.Password)));


            CreateMap<User, UserResource>()
                .ForMember(d => d.Token, opt => opt.MapFrom(src => src.Tokens.OrderByDescending(t => t.AddedDate).FirstOrDefault().Token)); //enson token-ni qaytarmaq ucun 

            #region Category

            CreateMap<Category, CategoryResource>()
                .ForMember(d => d.Icon, opt => opt.MapFrom(src => filePathUrl + src.Icon));


            CreateMap<CreateCategoryResource, Category>()
                .ForMember(d => d.AddedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(d => d.Status, opt => opt.MapFrom(src => true));  
            #endregion
        }
    }
}
