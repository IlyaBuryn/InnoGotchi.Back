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
        }

        [Fact]
        public async Task Create_Pet_ReturnPetId()
        {
            // Arrange
            var pet = _fixture.Create<Pet>();
            _petRepository = new InnoGotchiRepository<Pet>(_context);

            // Act
            var response = await _petRepository.AddAsync(pet);

            // Assert
            response.Should().Be(pet.Id);
            _context.Pets.Count().Should().Be(1);
            _context.Pets.First().Should().Be(pet);
        }

        [Fact]
        public async Task GetOne_Pet_ReturnPet()
        {
            // Arrange
            var id = _fixture.Create<int>();
            var pet = _fixture.Create<Pet>();
            _petRepository = new InnoGotchiRepository<Pet>(_context);
            _context.Pets.Add(pet);
            _context.SaveChanges();

            // Act
            var result = await _petRepository.GetOneAsync(x => x.Id == pet.Id);

            // Assert
            result.Should().Be(pet);
        }

        [Fact]
        public async Task GetOne_PetById_ReturnPet()
        {
            // Arrange
            var pet = _fixture.Create<Pet>();
            _petRepository = new InnoGotchiRepository<Pet>(_context);
            _context.Pets.Add(pet);
            _context.SaveChanges();

            // Act
            var result = await _petRepository.GetByIdAsync(pet.Id);

            // Assert
            result.Should().Be(pet);
        }

        [Theory]
        [InlineData(5)]
        public async Task Remove_PetById_ReturnTrue(int itemsCount)
        {
            // Arrange
            var pet = _fixture.Create<Pet>();
            _petRepository = new InnoGotchiRepository<Pet>(_context);
            _context.Pets.Add(pet);
            TestPetsCollection(_context, itemsCount);

            // Act
            var result = await _petRepository.RemoveAsync(pet.Id);

            // Assert
            result.Should().Be(true);
            _context.Pets.Count().Should().Be(itemsCount);
        }

        [Theory]
        [InlineData(5)]
        public async Task Update_Pet_ReturnTrue(int itemsCount)
        {
            // Arrange
            _petRepository = new InnoGotchiRepository<Pet>(_context);
            var pet = _fixture.Create<Pet>();
            _context.Pets.Add(pet);
            _context.SaveChanges();

            var message = "updatedMessage";
            pet.Name = message;

            // Act
            var result = await _petRepository.UpdateAsync(pet);

            // Assert
            result.Should().Be(true);
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
