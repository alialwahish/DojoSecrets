using Microsoft.EntityFrameworkCore;

namespace DojoSecret{

public class MyContext : DbContext
{

    public MyContext(DbContextOptions<MyContext>options): base(options){}

    public DbSet<User> users {get;set;}
    public DbSet<Posts> posts {get;set;}

    public DbSet<Likes> likes {get;set;}
}



}