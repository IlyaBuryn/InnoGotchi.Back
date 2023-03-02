using AutoFixture;
using FluentAssertions;
using InnoGotchi.DataAccess.Data;
using InnoGotchi.DataAccess.Models;
using InnoGotchi.DataAccess.Repositories;

namespace InnoGotchi.Tests.PetTests
{
    public class PetRepositoryTests
    {
        private Fixture _fixture;
        private InnoGotchiRepository<Pet> _petRepository;
        private InnoGotchiDbContext _context;

        public PetRepositoryTests()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _context = ContextGenerator.Generate();

            _petRepository = new InnoGotchiRepository<Pet>(_context);
        }

        [Fact]
        public async Task CreatePetAsync_ValidPet_ReturnPetId()
        {
            // Arrange
            var pet = _fixture.Create<Pet>();

            // Act
            var response = await _petRepository.AddAsync(pet);

            // Assert
            response.Should().Be(pet.Id);
            _context.Pets.Count().Should().Be(1);
            _context.Pets.First().Should().Be(pet);
        }

        [Fact]
        public async Task GetOnePetAsync_ValidExpression_ReturnPet()
        {
            // Arrange
            var id = _fixture.Create<int>();
            var pet = _fixture.Create<Pet>();
            _context.Pets.Add(pet);
            _context.SaveChanges();

            // Act
            var result = await _petRepository.GetOneAsync(x => x.Id == pet.Id);

            // Assert
            result.Should().Be(pet);
        }

        [Fact]
        public async Task GetOnePetByIdAsync_ValidId_ReturnPet()
        {
            // Arrange
            var pet = _fixture.Create<Pet>();
            _context.Pets.Add(pet);
            _context.SaveChanges();

            // Act
            var result = await _petRepository.GetOneByIdAsync(pet.Id);

            // Assert
            result.Should().Be(pet);
        }

        [Fact]
        public async Task Remove_PetById_ReturnTrue()
        {
            // Arrange
            var pet = _fixture.Create<Pet>();
            var itemsCount = _fixture.Create<int>();
            _context.Pets.Add(pet);
            TestPetsCollection(_context, itemsCount);

            // Act
            var result = await _petRepository.RemoveAsync(pet.Id);

            // Assert
            result.Should().BeTrue();
            _context.Pets.Count().Should().Be(itemsCount);
        }

        [Fact]
        public async Task UpdatePetAsync_ValidPet_ReturnTrue()
        {
            // Arrange
            var pet = _fixture.Create<Pet>();
            _context.Pets.Add(pet);
            _context.SaveChanges();

            var message = "updatedMessage";
            pet.Name = message;

            // Act
            var result = await _petRepository.UpdateAsync(pet);

            // Assert
            result.Should().BeTrue();
            _context.Pets.First().Should().Be(pet);
        }

        private void TestPetsCollection(InnoGotchiDbContext context, int itemsCount)
        {
            var pets = _fixture.CreateMany<Pet>(itemsCount);
            foreach (var item in pets)
            {
                context.Pets.Add(item);
            }
            context.SaveChanges();
        }
    }

}
