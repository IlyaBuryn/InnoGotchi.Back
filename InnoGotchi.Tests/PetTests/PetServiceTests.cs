using AutoFixture;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
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
        private Mock<IRepository<BodyPartPet>> _relationRepMock;
        private Mock<IValidator<PetDto>> _petValidatorMock;
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
            _relationRepMock = new Mock<IRepository<BodyPartPet>>();
            _petValidatorMock = new Mock<IValidator<PetDto>>();
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public async Task CreateAsync_Pet_ReturnPetId()
        {
            // Arrange
            var pet = _fixture.Create<PetDto>();
            int? petId = pet.Id;

            var validationResult = new Mock<ValidationResult>();
            validationResult.Setup(x => x.IsValid).Returns(true);
            _petValidatorMock.Setup(v => v.ValidateAsync(It.IsAny<PetDto>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationResult.Object);

            Pet nullableGetResult = null;
            _petRepMock.Setup(rep => rep.GetOneAsync(It.IsAny<Expression<Func<Pet, bool>>>())).ReturnsAsync(nullableGetResult);
            _farmRepMock.Setup(rep => rep.GetByIdAsync(pet.FarmId)).ReturnsAsync(_fixture.Create<Farm>());
            _petRepMock.Setup(rep => rep.AddAsync(It.IsAny<Pet>())).ReturnsAsync(1);
            _relationRepMock.Setup(rep => rep.AddAsync(It.IsAny<BodyPartPet>())).ReturnsAsync(1);
            _mapperMock.Setup(x => x.Map<ICollection<BodyPart>>(pet.BodyParts)).Returns(_fixture.Create<ICollection<BodyPart>>);
            _mapperMock.Setup(x => x.Map<Pet>(pet)).Returns(_fixture.Create<Pet>);

            _petService = new PetService(_petRepMock.Object, _farmRepMock.Object,
                _relationRepMock.Object, _petValidatorMock.Object, _mapperMock.Object);

            // Act
            var result = await _petService.AddNewPetAsync(pet);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task CreateAsync_Pet_ThrowDataValidationException()
        {
            // Arrange
            var validationResult = new Mock<ValidationResult>();
            validationResult.Setup(x => x.IsValid).Returns(true);
            _petValidatorMock.Setup(v => v.ValidateAsync(It.IsAny<PetDto>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationResult.Object);

            Pet getResult = _fixture.Create<Pet>();
            _petRepMock.Setup(rep => rep.GetOneAsync(It.IsAny<Expression<Func<Pet, bool>>>())).ReturnsAsync(getResult);

            _petService = new PetService(_petRepMock.Object, _farmRepMock.Object,
                _relationRepMock.Object, _petValidatorMock.Object, _mapperMock.Object);

            try
            {
                // Act
                var result = await _petService.AddNewPetAsync(_fixture.Create<PetDto>());
            }
            catch (Exception ex)
            {
                // Assert
                ex.Should().BeOfType<DataValidationException>();
            }
        }

        [Fact]
        public void Get_PetById_ReturnPet()
        {
            // Arrange
            var pets = _fixture.CreateMany<Pet>(5);
            var pet = _fixture.Create<PetDto>();

            _petRepMock.Setup(rep => rep.GetAll(It.IsAny<Expression<Func<Pet, bool>>>())).Returns(pets.AsQueryable());
            _mapperMock.Setup(x => x.Map<PetDto>(pets.First())).Returns(pet);

            _petService = new PetService(_petRepMock.Object, _farmRepMock.Object,
                _relationRepMock.Object, _petValidatorMock.Object, _mapperMock.Object);

            // Act
            var result = _petService.GetPetById(pets.First().Id);

            // Assert
            result.Should().Be(pet);
        }

        [Fact]
        public void Get_PetById_ThrowNotFoundException()
        {
            // Arrange
            var pets = _fixture.CreateMany<Pet>(0);
            _petRepMock.Setup(rep => rep.GetAll(It.IsAny<Expression<Func<Pet, bool>>>())).Returns(pets.AsQueryable());

            _petService = new PetService(_petRepMock.Object, _farmRepMock.Object,
                _relationRepMock.Object, _petValidatorMock.Object, _mapperMock.Object);

            try
            {
                // Act
                var result = _petService.GetPetById(0);
            }
            catch (Exception ex)
            {
                // Assert
                ex.Should().BeOfType<NotFoundException>();
            }
        }

        [Theory]
        [InlineData(0, 0, SortFilter.ByHappinessDays)]
        public async Task GetAsync_PetsPage_ThrowDataValidationException(int pageNumber, int pageSize, SortFilter sortFilter)
        {
            // Arrange
            _petService = new PetService(_petRepMock.Object, _farmRepMock.Object,
                _relationRepMock.Object, _petValidatorMock.Object, _mapperMock.Object);

            try
            {
                // Act
                var result = await _petService.GetPetsAsyncAsPageAsync(pageNumber, pageSize, sortFilter);
            }
            catch (Exception ex)
            {
                // Assert
                ex.Should().BeOfType<DataValidationException>();
            }
        }

        [Theory]
        [InlineData(5)]
        public void Get_PetsCount_ReturnCount(int petsCount)
        {
            // Arrange
            var pets = _fixture.CreateMany<Pet>(petsCount);
            _petRepMock.Setup(x => x.GetAll(It.IsAny<Expression<Func<Pet, bool>>>())).Returns(pets.AsQueryable());

            _petService = new PetService(_petRepMock.Object, _farmRepMock.Object,
                _relationRepMock.Object, _petValidatorMock.Object, _mapperMock.Object);

            // Act
            var result = _petService.GetAllPetsCount();

            // Assert
            result.Should().Be(petsCount);
        }

        [Fact]
        public void Get_PetByFarmId_ReturnPets()
        {
            // Arrange
            var pets = _fixture.CreateMany<Pet>(5);
            _petRepMock.Setup(rep => rep.GetAll(It.IsAny<Expression<Func<Pet, bool>>>())).Returns(pets.AsQueryable());
            _mapperMock.Setup(x => x.Map<List<PetDto>>(pets)).Returns(_fixture.CreateMany<PetDto>(pets.Count()).ToList());

            _petService = new PetService(_petRepMock.Object, _farmRepMock.Object,
                _relationRepMock.Object, _petValidatorMock.Object, _mapperMock.Object);

            // Act
            var result = _petService.GetPetsByFarmId(It.IsAny<int>());

            // Assert
            result.Count().Should().Be(pets.Count());
        }

        [Fact]
        public void Get_PetByFarmId_ThrowNotFoundException()
        {
            // Arrange
            var pets = _fixture.CreateMany<Pet>(0);
            _petRepMock.Setup(rep => rep.GetAll(It.IsAny<Expression<Func<Pet, bool>>>())).Returns(pets.AsQueryable());

            _petService = new PetService(_petRepMock.Object, _farmRepMock.Object,
                _relationRepMock.Object, _petValidatorMock.Object, _mapperMock.Object);

            try
            {
                // Act
                var result = _petService.GetPetsByFarmId(It.IsAny<int>());
            }
            catch (Exception ex)
            {
                // Assert
                ex.Should().BeOfType<NotFoundException>();
            }
        }

        [Fact]
        public async Task RemoveAsync_PetById_ReturnTrue()
        {
            // Arrange
            var pet = _fixture.Create<Pet>();
            _petRepMock.Setup(rep => rep.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(pet);
            _petRepMock.Setup(rep => rep.RemoveAsync(It.IsAny<int>())).ReturnsAsync(true);

            _petService = new PetService(_petRepMock.Object, _farmRepMock.Object,
                _relationRepMock.Object, _petValidatorMock.Object, _mapperMock.Object);

            // Act
            var result = await _petService.RemovePetAsync(It.IsAny<int>());

            // Assert
            result.Should().BeTrue();

        }

        [Fact]
        public async Task RemoveAsync_PetById_ThrowNotFoundException()
        {
            // Arrange
            Pet petNullable = null;
            _petRepMock.Setup(rep => rep.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(petNullable);

            _petService = new PetService(_petRepMock.Object, _farmRepMock.Object,
                _relationRepMock.Object, _petValidatorMock.Object, _mapperMock.Object);

            try
            {
                // Act
                var result = await _petService.RemovePetAsync(It.IsAny<int>());
            }
            catch (Exception ex)
            {
                // Assert
                ex.Should().BeOfType<NotFoundException>();
            }
        }

        [Fact]
        public async Task UpdateAsync_Pet_ReturnTrue()
        {
            // Arrange
            var pet = _fixture.Create<Pet>();

            var validationResult = new Mock<ValidationResult>();
            validationResult.Setup(x => x.IsValid).Returns(true);
            _petValidatorMock.Setup(v => v.ValidateAsync(It.IsAny<PetDto>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationResult.Object);
            
            Pet nullableGetResult = null;
            _petRepMock.Setup(rep => rep.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(pet);
            _petRepMock.Setup(rep => rep.GetOneAsync(It.IsAny<Expression<Func<Pet, bool>>>())).ReturnsAsync(nullableGetResult);
            _petRepMock.Setup(rep => rep.UpdateAsync(pet)).ReturnsAsync(true);

            _petService = new PetService(_petRepMock.Object, _farmRepMock.Object,
                _relationRepMock.Object, _petValidatorMock.Object, _mapperMock.Object);

            // Act
            var result = await _petService.UpdatePetAsync(_fixture.Create<PetDto>());

            // Assert
            result.Should().BeTrue();

        }

        [Fact]
        public async Task UpdateAsync_Pet_ThrowNotFoundException()
        {
            // Arrange
            var pet = _fixture.Create<Pet>();

            var validationResult = new Mock<ValidationResult>();
            validationResult.Setup(x => x.IsValid).Returns(true);
            _petValidatorMock.Setup(v => v.ValidateAsync(It.IsAny<PetDto>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationResult.Object);

            Pet petNullable = null;
            _petRepMock.Setup(rep => rep.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(petNullable);

            _petService = new PetService(_petRepMock.Object, _farmRepMock.Object,
                _relationRepMock.Object, _petValidatorMock.Object, _mapperMock.Object);

            try
            {
                // Act
                var result = await _petService.UpdatePetAsync(_fixture.Create<PetDto>());
            }
            catch (Exception ex)
            {
                // Assert
                ex.Should().BeOfType<NotFoundException>();
            }
        }
    }
}
