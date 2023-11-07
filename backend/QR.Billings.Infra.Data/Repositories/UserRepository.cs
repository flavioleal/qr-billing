using QR.Billings.Business.Entities;
using QR.Billings.Business.Interfaces.Repositories;

namespace QR.Billings.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        public async Task<User?> GetAsync(string username, string password)
        {
            var users = new List<User>();
            users.Add(new User { Id = Guid.Parse("e4ae8020-92a3-4d26-8c41-2aaaa3604f0d"), Username = "admin", Password = "admin", Role = "admin" });
            users.Add(new User { Id = Guid.Parse("c2a04e21-961f-4f60-b339-9fe2596c0c62"), Username = "lojista1", Password = "lojista1", Role = "lojista" });
            users.Add(new User { Id = Guid.Parse("8f8e562c-ba10-45b7-8972-c0a6550ea45f"), Username = "lojista2", Password = "lojista2", Role = "lojista" });
            users.Add(new User { Id = Guid.Parse("52434933-3e23-48cb-a87e-51740a899d3a"), Username = "lojista3", Password = "lojista3", Role = "lojista" });
            var user = users.FirstOrDefault(x => x.Username.ToLower() == username.ToLower() && x.Password == password);

            return await Task.Run(() => user);
        }
    }
}
