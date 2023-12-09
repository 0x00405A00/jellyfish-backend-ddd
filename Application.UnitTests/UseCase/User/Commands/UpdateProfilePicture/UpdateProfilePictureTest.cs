﻿using Application.CQS.User.Commands.UpdateProfilePicture;
using Application.Mapper;
using AutoMapper;
using Domain.Entities.User;
using Infrastructure.Abstractions;
using Infrastructure.FileSys;
using MediatR;
using System.Linq.Expressions;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Application.UnitTests.UseCase.User.Commands.UpdateProfilePicture
{
    public class UpdateProfilePictureTest
    {
        private static readonly Guid UserId = Guid.NewGuid();
        private static readonly UploadProfilePictureCommand ValidCommand = new UploadProfilePictureCommand(
            Guid.NewGuid(),
            UserId,
            Convert.ToBase64String(Encoding.UTF8.GetBytes("base64encodedimage")),
            "image/jpeg");

        private readonly UploadProfilePictureCommandHandler _handler;

        private readonly IMediaService _mediaService;
        private readonly IMapper _mapperMock;
        private readonly IUserRepository _userRepositoryMock;
        private readonly IUserTypeRepository _userTypeRepositoryMock;
        private readonly IRoleRepository _roleRepositoryMock;
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly IMediator _mediatorMock;
        private readonly IConfiguration _configuration;

        private static readonly Domain.Entities.User.User UserInstance = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance(UserId);

        public UpdateProfilePictureTest()
        {
            _configuration = Substitute.For<IConfiguration>();

            // Configure the mock as needed
            _configuration["Infrastructure:ContentDelivery:DeliveryUrl"].Returns("https://jellyfish.net");

            var mappingProfile = new DomainModelToDTOMappingProfile(_configuration);
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(mappingProfile));
            _mapperMock = Substitute.For<IMapper>();
            _mapperMock.ConfigurationProvider.Returns(configuration);

            _userRepositoryMock = Substitute.For<IUserRepository>();
            _userTypeRepositoryMock = Substitute.For<IUserTypeRepository>();
            _roleRepositoryMock = Substitute.For<IRoleRepository>();
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _mediatorMock = Substitute.For<IMediator>();
            _mediaService = Substitute.For<IMediaService>();

            _handler = new UploadProfilePictureCommandHandler(
                _mapperMock,
                _mediatorMock,
                _userRepositoryMock,
                _mediaService,
                _unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ValidCommand_ReturnsSuccessResult()
        {
            // Arrange
            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>()).Returns(UserInstance);
            _mediaService.CreateProfilePicture(Arg.Any<Guid>(), Arg.Any<string>(), Arg.Any<byte[]>(), Arg.Any<System.Threading.CancellationToken>())
                .Returns("path/to/profile/picture.png");

            // Act
            var result = await _handler.Handle(ValidCommand, default);

            // Assert
            Assert.True(result.IsSuccess);

            // Additional assertions as needed
        }

    }
}