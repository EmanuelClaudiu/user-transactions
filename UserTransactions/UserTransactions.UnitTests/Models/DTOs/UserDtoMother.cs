using UserTransactions.Application.DTOs;
using UserTransactions.UnitTests.Models.Common;

namespace UserTransactions.UnitTests.Models.DTOs
{
    public static class UserDtoMother
    {
        public static UserDTO? Simple()
        {
            return new UserDTO
            {
                UserId = GetRandom.RandomString(20),
                Name = GetRandom.RandomString(7),
                Adress = GetRandom.RandomString(20)
            };
        }

        public static UserDTO? WithUserId(this UserDTO source, string userId)
        {
            source.UserId = userId;
            return source;
        }
    }
}
