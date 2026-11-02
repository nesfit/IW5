using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CookBook.IdentityProvider.DAL.Entities;

[Index(nameof(Subject), IsUnique = true)]
public class AppUserEntity : IdentityUser<Guid>
{
    public bool Active { get; set; }
    [MaxLength(450)]
    public string Subject { get; set; }
}
