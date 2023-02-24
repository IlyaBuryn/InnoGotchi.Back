using AutoFixture;
using FluentAssertions;
using InnoGotchi.API.Controllers;
using InnoGotchi.BusinessLogic.Exceptions;
using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.BusinessLogic.Services;
using InnoGotchi.Components.DtoModels;
using InnoGotchi.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace InnoGotchi.Tests.PetTests
{
    public class PetControllerTests
    {
        private Mock<IPetService> _petServiceMock;
        private Fixture _fixture;
        private PetsController _controller;

        public PetControllerTests()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _petServiceMock = new Mock<IPetService>();

            _controller = new PetsController(_petServiceMock.Object);
        }

        [Fact]
        public async Task GetPetByIdAsync_WhenValidId_ReturnOk()
        {
            // Arrange
            var id = _fixture.Create<int>();
            var pet = _fixture.Create<PetDto>();
            _petServiceMock.Setup(srv => srv.GetPetByIdAsync(id)).ReturnsAsync(pet);

            // Act
            var result = await _controller.GetPetByIdAsync(id);
            var objResult = result as OkObjectResult;

            // Assert
            objResult.StatusCode.Should().Be(200);
            objResult.Value.Should().Be(pet);
        }

        [Fact]
        public async Task GetPetByIdAsync_WhenThrowsException_ThrowNotFoundException()
        {
            // Arrange 
            var id = _fixture.Create<int>();
            _petServiceMock.Setup(srv => srv.GetPetByIdAsync(id)).Throws<NotFoundException>();

            // Act & Assert
            await _controller.Invoking(x => x.GetPetByIdAsync(id))
                .Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task CreatePetAsync_WhenValidPet_ReturnOk()
        {
            // Arrange
            var id = _fixture.Create<int>();
            var pet = _fixture.Create<PetDto>();
            _petServiceMock.Setup(srv => srv.AddNewPetAsync(pet)).ReturnsAsync(id);

            // Act
            var result = await _controller.AddPetAsync(pet);
            var objResult = result as ObjectResult;

            // Assert
            objResult.StatusCode.Should().Be(201);
            objResult.Value.Should().Be(id);
        }

        [Fact]
        public async Task CreatePetAsync_WhenModelStateIsNotValid_ThrowDataValidationExceptions()
        {
            // Arrange
            var invalidPet = _fixture.Build<PetDto>()
                .With(p => p.Name, (string)null)
                .Create();
            _controller.ModelState.AddModelError("Name", "Pet name is required!");

            // Act & Assert
            await _controller.Invoking(c => c.AddPetAsync(invalidPet))
                .Should().ThrowAsync<DataValidationException>();
        }

        [Fact]
        public async Task UpdatePetAsync_WhenValidPet_ReturnOkAndTrue()
        {
            // Arrange
            var pet = _fixture.Create<PetDto>();
            _petServiceMock.Setup(srv => srv.UpdatePetAsync(pet)).ReturnsAsync(true);

            // Act
            var result = await _controller.UpdatePetAsync(pet);
            var objResult = result as OkObjectResult;

            // Assert
            objResult.StatusCode.Should().Be(200);
            var response = objResult.Value.Should().BeAssignableTo<bool>().Subject;
            response.Should().BeTrue();
        }

        [Fact]
        public async Task UpdatePetAsync_WhenInvalidPet_ThrowDataValidationExceptions()
        {
            // Arrange
            var invalidPet = _fixture.Build<PetDto>()
                .With(p => p.Name, (string)null)
                .Create();
            _controller.ModelState.AddModelError("Name", "Pet name is required!");

            // Act & Assert
            await _controller.Invoking(c => c.UpdatePetAsync(invalidPet))
                .Should().ThrowAsync<DataValidationException>();
        }

        [Fact]
        public async Task DeletePetByIdAsync_WhenValidId_ReturnOkAndTrue()
        {
            // Arrange
            var id = _fixture.Create<int>();
            _petServiceMock.Setup(srv => srv.RemovePetAsync(id)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeletePetAsync(id);
            var objResult = result as OkObjectResult;

            // Assert
            objResult.StatusCode.Should().Be(200);
            var response = objResult.Value.Should().BeAssignableTo<bool>().Subject;
            response.Should().BeTrue();
        }

        [Fact]
        public async Task DeletePetByIdAsync_InvalidId_ThrowNotFoundException()
        {
            // Arrange 
            var id = _fixture.Create<int>();
            _petServiceMock.Setup(srv => srv.RemovePetAsync(id)).Throws<NotFoundException>();

            // Act & Assert
            await _controller.Invoking(x => x.DeletePetAsync(id))
                .Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task GetPetsByIdAsync_WhenValidId_ReturnOk()
        {
            // Arrange
            var pets = _fixture.CreateMany<PetDto>(5).ToList();
            var pet = new List<PetDto> { pets.First() };
            _petServiceMock.Setup(srv => srv.GetPetsByFarmIdAsync(pets.First().Id)).ReturnsAsync(pet);

            // Act
            var result = await _controller.GetPetByFarmIdAsync(pets.First().Id);
            var objResult = result as OkObjectResult;

            // Assert
            objResult.StatusCode.Should().Be(200);
            objResult.Value.Should().BeEquivalentTo(pet);
        }

        [Fact]
        public async Task GetPetsByIdAsync_WhenInvalidId_ThorwNotFoundException()
        {
            // Arrange 
            var id = _fixture.Create<int>();
            _petServiceMock.Setup(srv => srv.GetPetsByFarmIdAsync(id)).Throws<NotFoundException>();

            // Act & Assert
            await _controller.Invoking(x => x.GetPetByFarmIdAsync(id))
                .Should().ThrowAsync<NotFoundException>();
        }
    }
}
