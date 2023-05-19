using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyChat.Models;

public class ChatContext : IdentityDbContext<User>
{
    public DbSet<Message> Messages { get; set; }
    public ChatContext (DbContextOptions<ChatContext> options) : base(options){}
}