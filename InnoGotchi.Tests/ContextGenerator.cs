using InnoGotchi.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.Tests
{
    public static class ContextGenerator
    {
        public static InnoGotchiDbContext Generate()
        {
            var optionsBuilder = new DbContextOptionsBuilder<InnoGotchiDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            return new InnoGotchiDbContext(optionsBuilder.Options);
        }
    }
}
