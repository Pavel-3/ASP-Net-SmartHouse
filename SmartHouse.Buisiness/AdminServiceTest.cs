namespace SmartHouse.Buisiness
{
    using AutoMapper;
    using Moq;
    using SmartHouse.Abstractions.Data;
    using SmartHouse.Business;
    using SmartHouse.Core.DTOs;
    using SmartHouse.Data.Entities;

    public class AdminServiceTest
    {

        [Fact]
        public async void GetUsersByPageAsync_CorrectData_CorrectListOfUsers()
        {
            var uowMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMapper>();
            var user1 = new User();
            var user2 = new User();
            var users = new List<User> { user1, user2 };
            var userDTO1 = new UserDTO();
            var userDTO2 = new UserDTO();
            var userDTOs = new List<UserDTO> { userDTO1, userDTO2};

            uowMock.Setup(uow => uow.Users.GetUsersByPageAsync(2, 2)).ReturnsAsync(users);
            mapperMock.Setup(mapper => mapper.Map<UserDTO>(user1)).Returns(userDTO1);
            mapperMock.Setup(mapper => mapper.Map<UserDTO>(user2)).Returns(userDTO2);

            var adminService = new AdminService(uowMock.Object, mapperMock.Object);

            var result = await adminService.GetUsersByPageAsync(2, 2);

            Assert.Equal(userDTOs, result);
        }
    }
}