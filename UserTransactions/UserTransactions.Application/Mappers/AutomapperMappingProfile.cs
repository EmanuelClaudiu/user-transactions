using AutoMapper;
using UserTransactions.Application.DTOs;
using UserTransactions.Domain.Entities;

namespace UserTransactions.Application.Mappers
{
    public class AutomapperMappingProfile : Profile
    {
        public AutomapperMappingProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();

            CreateMap<UserUpsertDTO, UserDTO>();

            CreateMap<Transaction, TransactionDTO>();
            CreateMap<TransactionDTO, Transaction>();

            CreateMap<TransactionCreateDTO, TransactionDTO>();
        }
    }
}
