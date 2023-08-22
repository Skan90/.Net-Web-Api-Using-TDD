using CloudCostumers.API.Models;

namespace CloudCostumers.UnitTests.Fixtures;

public static class UsersFixture {
        public static List<User> GetTestUsers() => new() {
            new User {
                Id = 1,
                Name = "Cintia",
                Address = new Address()
                {
                    Street = "Main St",
                    City = "Porto Alegre",
                    ZipCode = "90000000"
                },
                Email = "cintia-valente@linkedin.com"
            },
            new User {
                Id = 2,
                Name = "Rafael",
                Address = new Address()
                {
                    Street = "Main St",
                    City = "Santos",
                    ZipCode = "11065200"
                },
                Email = "skan90@linkedin.com"
            },
            new User {
                Id = 3,
                Name = "John",
                Address = new Address()
                {
                    Street = "Main St",
                    City = "São Luiz",
                    ZipCode = "65010904"
                },
                Email = "jovicarvalho@linkedin.com"
            },
            new User {
                Id = 4,
                Name = "Matthew",
                Address = new Address()
                {
                    Street = "Main St",
                    City = "Salvador",
                    ZipCode = "41745000"
                },
                Email = "catureba@linkedin.com"
            }
        };
}
