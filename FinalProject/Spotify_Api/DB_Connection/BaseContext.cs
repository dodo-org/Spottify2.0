using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Spotify_Api.DB_Connection
{
    public class BaseContext : DbContext
    {
        public DbSet<UserEntity> User {  get; set; }    


    }
}
