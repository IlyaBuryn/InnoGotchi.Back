using AutoFixture;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using InnoGotchi.API.Controllers;
using InnoGotchi.BusinessLogic.Exceptions;
using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.Components.DtoModels;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace InnoGotchi.Tests.PetTests
{
    public class PetControllerTests
    {
        private Mock<IPetService> _petServiceMock;
        private Mock<IValidator<PetDto>> _petValidatorMock;
        private Fixture _fixture;
        private PetsController _controller;

        public PetControllerTests()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _petServiceMock = new Mock<IPetService>();

            _petValidatorMock = new Mock<IValidator<PetDto>>();
            var validationResult = new Mock<ValidationResult>();
            validationResult.Setup(x => x.IsValid).Returns(true);
            _petValidatorMock.Setup(v => v.ValidateAsync(It.IsAny<PetDto>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationResult.Object);
        }

        [Fact]
        public void Get_PetById_ReturnOk()
        {
            // Arrange
            var pet = _fixture.Create<PetDto>();
            _petServiceMock.Setup(srv => srv.GetPetById(It.IsAny<int>())).Returns(pet);
            _controller = new PetsController(_petServiceMock.Object, _petValidatorMock.Object);

            // Act
            var result = _controller.GetPetById(It.IsAny<int>());
            var obj = result as ObjectResult;

            // Assert
            obj.StatusCode.Should().Be(200);
        }

        [Fact]
        public void Get_PetById_ThrowNotFoundException()
        {
            // Arrange 
            _petServiceMock.Setup(srv => srv.GetPetById(It.IsAny<int>())).Throws(new NotFoundException());
            _controller = new PetsController(_petServiceMock.Object, _petValidatorMock.Object);

            // Act
            var result = _controller.GetPetById(It.IsAny<int>());
            var obj = result as ObjectResult;

            // Assert
            obj.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task CreateAsync_Pet_ReturnOkAndSameId()
        {
            // Arrange
            var pet = _fixture.Create<PetDto>();
            _petServiceMock.Setup(srv => srv.AddNewPetAsync(It.IsAny<PetDto>())).ReturnsAsync(pet.Id);
            _controller = new PetsController(_petServiceMock.Object, _petValidatorMock.Object);

            // Act
            var result = await _controller.AddPetAsync(pet);
            var obj = result as ObjectResult;

            // Assert
            obj.StatusCode.Should().Be(201);
            obj.Value.Should().Be(pet.Id);
        }

        [Fact]
        public async Task CreateAsync_Pet_ThrowExceptions()
        {
            // Arrange
            var pet = _fixture.Create<PetDto>();
            _petServiceMock.Setup(srv => srv.AddNewPetAsync(It.IsAny<PetDto>())).Throws(new DataValidationException());
            _controller = new PetsController(_petServiceMock.Object, _petValidatorMock.Object);

            // Act
            var result = await _controller.AddPetAsync(pet);
            var obj = result as ObjectResult;

            // Assert
            obj.StatusCode.Should().BeOneOf(400);
        }

        [Fact]
        public async Task UpdateAsync_Pet_ReturnOkAndTrue()
        {
            // Arrange
            _petServiceMock.Setup(srv => srv.UpdatePetAsync(It.IsAny<PetDto>())).ReturnsAsync(true);
            _controller = new PetsController(_petServiceMock.Object, _petValidatorMock.Object);

            // Act
            var result = await _controller.UpdatePetAsync(_fixture.Create<PetDto>());
            var obj = result as ObjectResult;

            // Assert
            obj.StatusCode.Should().Be(200);
            obj.Value.Should().Be(true);
        }

        [Fact]
        public async Task UpdateAsync_Pet_ThrowExceptions()
        {
            // Arrange
            _petServiceMock.Setup(srv => srv.UpdatePetAsync(It.IsAny<PetDto>())).Throws(new DataValidationException());
            _controller = new PetsController(_petServiceMock.Object, _petValidatorMock.Object);

            // Act
            var result = await _controller.UpdatePetAsync(_fixture.Create<PetDto>());
            var obj = result as ObjectResult;

            // Assert
            obj.StatusCode.Should().BeOneOf(400);
        }

        [Fact]
        public async Task DeleteAsync_PetById_ReturnOkAndTrue()
        {
            // Arrange
            _petServiceMock.Setup(srv => srv.RemovePetAsync(It.IsAny<int>())).ReturnsAsync(true);
            _controller = new PetsController(_petServiceMock.Object, _petValidatorMock.Object);

            // Act
            var result = await _controller.DeletePetAsync(It.IsAny<int>());
            var obj = result as ObjectResult;

            // Assert
            obj.StatusCode.Should().BeOneOf(200, 204);
            obj.Value.Should().Be(true);
        }

        [Fact]
        public async Task DeleteAsync_PetById_ThrowNotFoundException()
        {
            // Arrange
            _petServiceMock.Setup(srv => srv.RemovePetAsync(It.IsAny<int>())).Throws(new NotFoundException());
            _controller = new PetsController(_petServiceMock.Object, _petValidatorMock.Object);

            // Act
            var result = await _controller.DeletePetAsync(It.IsAny<int>());
            var obj = result as ObjectResult;

            // Assert
            obj.StatusCode.Should().BeOneOf(404);
        }

        [Fact]
        public void Get_PetsById_ReturnOk()
        {
            // Arrange
            var pets = _fixture.CreateMany<PetDto>(5).ToList();
            _petServiceMock.Setup(srv => srv.GetPetsByFarmId(It.IsAny<int>())).Returns(pets);
            _controller = new PetsController(_petServiceMock.Object, _petValidatorMock.Object);

            // Act
            var result = _controller.GetPetByFarmId(It.IsAny<int>());
            var obj = result as ObjectResult;

            // Assert
            obj.StatusCode.Should().BeOneOf(200);
        }

        [Fact]
        public void Get_PetsById_ThorwException()
        {
            // Arrange
            _petServiceMock.Setup(srv => srv.GetPetsByFarmId(It.IsAny<int>())).Throws(new NotFoundException());
            _controller = new PetsController(_petServiceMock.Object, _petValidatorMock.Object);

            // Act
            var result = _controller.GetPetByFarmId(It.IsAny<int>());
            var obj = result as ObjectResult;

            // Assert
            obj.StatusCode.Should().BeOneOf(404);
        }
    }
}
