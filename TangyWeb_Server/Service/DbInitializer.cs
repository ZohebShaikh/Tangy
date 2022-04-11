using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tangy_Common;
using Tangy_DataAccess.Data;
using TangyWeb_Server.Service.IService;

namespace TangyWeb_Server.Service
{
  public class DbInitializer : IDbInitializer
  {
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDbContext _db;

    public DbInitializer(UserManager<IdentityUser> userManager,
      RoleManager<IdentityRole> roleManager,
      ApplicationDbContext db)
    {
      _db = db;
      _userManager = userManager; 
      _roleManager = roleManager;
    }
    public void Initialize()  
    {
      try
      {
        if(_db.Database.GetPendingMigrations().Count() > 0)
        {
          _db.Database.Migrate();
        }
        if (!_roleManager.RoleExistsAsync(StaticDetails.Role_Admin).GetAwaiter().GetResult())
        {
          _roleManager.CreateAsync(new IdentityRole(StaticDetails.Role_Admin)).GetAwaiter().GetResult();
          _roleManager.CreateAsync(new IdentityRole(StaticDetails.Role_Customer)).GetAwaiter().GetResult();
        }
        else
        {
          return;
        }

        IdentityUser user = new()
        {
          UserName = "admintest@test.com",
          Email = "admintest@test.com",
          EmailConfirmed = true
        };

        _userManager.CreateAsync(user,"Admin123*").GetAwaiter().GetResult();

        _userManager.AddToRoleAsync(user,StaticDetails.Role_Admin).GetAwaiter().GetResult();
      }
      catch (Exception ex)
      {

      }
    }
  }
}
