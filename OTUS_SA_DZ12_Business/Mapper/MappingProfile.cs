﻿using AutoMapper;
//using Catalog_Models.CatalogModels.Author;
//using Catalog_Models.CatalogModels.Book;
//using Catalog_Models.CatalogModels.BookInstance;
//using Catalog_Models.CatalogModels.BookToAuthor;
//using Catalog_Models.CatalogModels.Publisher;

namespace Catalog_Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            //CreateMap<Publisher, PublisherItemResponse>();
            //CreateMap<Publisher, PublisherItemCreateUpdateRequest>();

            //CreateMap<Author, AuthorItemResponse>();
            //CreateMap<Author, AuthorForBookRequest>();
            //CreateMap<Author, AuthorShortResponse>();

            //CreateMap<Book, BookItemResponse>();
            ////  .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => (src.Publisher == null) ? null : src.Publisher));
            //CreateMap<Book, BookShortResponse>();

            //CreateMap<BookToAuthor, AuthorItemResponse>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AuthorId))
            //    .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Author.FirstName))
            //    .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Author.LastName))
            //    .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.Author.MiddleName))
            //    .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Author.FirstName + " " + src.Author.LastName + (String.IsNullOrWhiteSpace(src.Author.MiddleName) ? "" : src.Author.MiddleName)))
            //    .ForMember(dest => dest.IsForeign, opt => opt.MapFrom(src => src.Author.IsForeign))
            //    .ForMember(dest => dest.AddUserId, opt => opt.MapFrom(src => src.Author.AddUserId))
            //    .ForMember(dest => dest.AddTime, opt => opt.MapFrom(src => src.Author.AddTime))
            //    .ForMember(dest => dest.IsArchive, opt => opt.MapFrom(src => src.Author.IsArchive));

            //CreateMap<BookToAuthor, BookToAuthorResponse>();

            //CreateMap<BookInstance, BookInstanceCreateUpdateRequest>().ReverseMap();
            //CreateMap<BookInstance, BookInstanceResponse>();
            //CreateMap<BookInstance, BookInstanceOnlyForReadingRoomResponse>();
            //CreateMap<BookInstance, BookInstanceOutMaxDaysResponse>();
        }
    }
}
