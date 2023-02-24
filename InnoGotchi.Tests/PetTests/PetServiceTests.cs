using AutoFixture;
using AutoMapper;
using FluentAssertions;
using FluentValidation.Results;
using InnoGotchi.BusinessLogic.Exceptions;
using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.BusinessLogic.Services;
using InnoGotchi.Components.DtoModels;
using InnoGotchi.Components.Enums;
using InnoGotchi.DataAccess.Interfaces;
using InnoGotchi.DataAccess.Models;
using Moq;
using System.Linq.Expressions;

namespace InnoGotchi.Tests.PetTests
{
    public class PetServiceTests
    {
        private Mock<IRepository<Pet>> _petRepMock;
        private Mock<IRepository<Farm>> _farmRepMock;
        private Mock<IRepository<BodyPartPet>> _bodyPartPetRepMock;
        private Mock<IMapper> _mapperMock;
        private Fixture _fixture;
        private IPetService _petService;

        public PetServiceTests()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _petRepMock = new Mock<IRepository<Pet>>();
            _farmRepMock = new Mock<IRepository<Farm>>();
            _bodyPartPetRepMock = new Mock<IRepository<BodyPartPet>>();

            _mapperMock = new Mock<IMapper>();

            _petService = new PetService(_petRepMock.Object, _farmRepMock.Object, _bodyPartPetRepMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task CreatePeAsync_ValidPetAndFarmExist_ReturnPetId()
        {
            // Arrange
            var petDto = _fixture.Create<PetDto>();
            var farm = _fixture.Create<Farm>();
            var pet = _fixture.Create<Pet>();
            pet.BodyParts = null;
            var petId = _fixture.Create<int>();

            _petRepMock.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<Pet, bool>>>())).ReturnsAsync((Pet)null);
            _farmRepMock.Setup(x => x.GetOneByIdAsync(petDto.FarmId)).ReturnsAsync(farm);
            _mapperMock.Setup(x => x.Map<Pet>(petDto)).Returns(pet);
            _petRepMock.Setup(x => x.AddAsync(pet)).ReturnsAsync(petId);
            _mapperMock.Setup(x => x.Map<ICollection<BodyPart>>(petDto.BodyParts)).Returns(_fixture.Create<ICollection<BodyPart>>());

            // Act
            var result = await _petService.AddNewPetAsync(petDto);

            // Assert
            result.Should().Be(petId);
        }

        [Fact]
        public async Task CreatePetAsync_WhenPetExist_ThrowDataValidationException()
        {
            // Arrange
            var petDto = _fixture.Create<PetDto>();
            var pet = _fixture.Create<Pet>();

            _petRepMock.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<Pet, bool>>>()))
                .ReturnsAsync(pet);

            // Act & Assert
            await _petService.Invoking(c => c.AddNewPetAsync(petDto))
                .Should().ThrowAsync<DataValidationException>();
        }

        [Fact]
        public async Task CreatePetAsync_WhenFarmNotExist_ThrowNotFoundException()
        {
            // Arrange
            var petDto = _fixture.Create<PetDto>();
            var pet = _fixture.Create<Pet>();
            var farm = _fixture.Create<Farm>();

            _petRepMock.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<Pet, bool>>>())).ReturnsAsync((Pet)null);
            _farmRepMock.Setup(x => x.GetOneByIdAsync(petDto.FarmId)).ReturnsAsync((Farm)null);

            // Act & Assert
            await _petService.Invoking(c => c.AddNewPetAsync(petDto))
                .Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task GetPetByIdAsync_WhenPetExist_ReturnPet()
        {
            // Arrange
            var id = _fixture.Create<int>();
            var pet = _fixture.Create<Pet>();
            var petDto = _fixture.Create<PetDto>();

            _mapperMock.Setup(x => x.Map<PetDto>(pet)).Returns(petDto);
            _petRepMock.Setup(rep => rep.GetOneByIdAsync(id, It.IsAny<Expression<Func<Pet, object>>[]>())).ReturnsAsync(pet);

            // Act
            var result = await _petService.GetPetByIdAsync(id);

            // Assert
            result.Should().Be(petDto);
        }

        [Fact]
        public async Task GetPetByIdAsync_WhenPetNotExist_ThrowNotFoundException()
        {
            // Arrange
            var id = _fixture.Create<int>();
            _petRepMock.Setup(x => x.GetOneByIdAsync(id, It.IsAny<Expression<Func<Pet, object>>[]>()))
                   .ReturnsAsync((Pet)null);

            // Act & Assert
            await _petService.Invoking(c => c.GetPetByIdAsync(id))
                .Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task GetPetsPageAsync_WhenInvalidPageData_ThrowDataValidationException()
        {
            // Arrange
            var pageNumber = -1;
            var pageSize = 0;
            var sortFilter = _fixture.Create<SortFilter>();

            // Act & Assert
            await _petService.Invoking(c => c.GetPetsAsPageAsync(pageNumber, pageSize, sortFilter))
                .Should().ThrowAsync<DataValidationException>();
        }

        [Fact]
        public async Task GetPetsCountAsync_WhenValidPetsCount_ReturnCount()
        {
            // Arrange
            var petsCount = 5;
            var pets = _fixture.CreateMany<Pet>(petsCount);
            _petRepMock.Setup(x => x.GetCountAsync(It.IsAny<Expression<Func<Pet, bool>>>())).ReturnsAsync(petsCount);

            // Act
            var result = await _petService.GetAllPetsCountAsync();

            // Assert
            result.Should().Be(petsCount);
        }

        [Fact]
        public async Task GetPetsByFarmIdAsync_WhenPetsExist_ReturnPets()
        {
            // Arrange
            var petsCount = 3;
            var pets = _fixture.CreateMany<Pet>(petsCount).AsQueryable();
            var petsDto = _fixture.CreateMany<PetDto>(petsCount).ToList();
            var farmId = _fixture.Create<int>();
            foreach (var pet in pets)
            {
                pet.FarmId = farmId;
            }
            _petRepMock.Setup(rep => rep.GetManyAsync(
                It.IsAny<Expression<Func<Pet, bool>>>(),
                It.IsAny<Expression<Func<Pet, object>>[]>())).ReturnsAsync(pets);
            _mapperMock.Setup(x => x.Map<List<PetDto>>(pets)).Returns(petsDto);


            // Act
            var result = await _petService.GetPetsByFarmIdAsync(farmId);

            // Assert
            result.Should().BeEquivalentTo(petsDto);
        }

        [Fact]
        public async Task RemovePetByIdAsync_WhenPetExist_ReturnTrue()
        {
            // Arrange
            var pet = _fixture.Create<Pet>();
            var id = _fixture.Create<int>();
            _petRepMock.Setup(rep => rep.GetOneByIdAsync(id)).ReturnsAsync(pet);
            _petRepMock.Setup(rep => rep.RemoveAsync(id)).ReturnsAsync(true);

            // Act
            var result = await _petService.RemovePetAsync(id);

            // Assert
            result.Should().BeTrue();

        }

        [Fact]
        public async Task RemovePetByIdAsync_WhenPetNotFound_ThrowNotFoundException()
        {
            // Arrange
            var id = _fixture.Create<int>();
            _petRepMock.Setup(rep => rep.GetOneByIdAsync(id)).ReturnsAsync((Pet)null);

            // Act & Assert
            await _petService.Invoking(c => c.RemovePetAsync(id))
                .Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task UpdatePetAsync_WhenValidPet_ReturnTrue()
        {
            // Arrange
            var id = _fixture.Create<int>();
            var pet = _fixture.Create<Pet>();
            var petDto = _fixture.Create<PetDto>();

            _petRepMock.Setup(rep => rep.GetOneByIdAsync(petDto.Id)).ReturnsAsync(pet);
            _petRepMock.Setup(rep => rep.GetOneAsync(It.IsAny<Expression<Func<Pet, bool>>>())).ReturnsAsync((Pet)null);
            _petRepMock.Setup(rep => rep.UpdateAsync(pet)).ReturnsAsync(true);

            // Act
            var result = await _petService.UpdatePetAsync(petDto);

            // Assert
            result.Should().BeTrue();

        }

        [Fact]
        public async Task UpdatePetAsync_WhenPetNotFound_ThrowNotFoundException()
        {
            // Arrange
            var id = _fixture.Create<int>();
            var pet = _fixture.Create<Pet>();
            var petDto = _fixture.Create<PetDto>();

            _petRepMock.Setup(rep => rep.GetOneByIdAsync(id)).ReturnsAsync((Pet)null);

            // Act & Assert
            await _petService.Invoking(c => c.UpdatePetAsync(petDto))
                .Should().ThrowAsync<NotFoundException>();
        }
    }
}
