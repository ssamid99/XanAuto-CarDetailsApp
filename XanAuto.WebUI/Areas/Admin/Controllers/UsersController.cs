using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XanAuto.Application.AppCode.Extensions;
using XanAuto.Domain.Models.DbContexts;
using System;
using System.Linq;
using System.Threading.Tasks;
using XanAuto.Domain.Models.Entities.Membership;

namespace XanAuto.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly XanAutoDbContext db;

        public UsersController(XanAutoDbContext db)
        {
            this.db = db;
        }
        [Authorize("admin.users.index")]
        public async Task<IActionResult> Index()
        {
            var data = await db.Users.ToListAsync();
            return View(data);
        }
        [Authorize("admin.users.details")]
        public async Task<IActionResult> Details(int id)
        {
            var user = await db.Users.FirstOrDefaultAsync(user => user.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            ViewBag.Roles = await (from r in db.Roles
                                   join ur in db.UserRoles on new { RoleId = r.Id, UserId = user.Id } equals new { ur.RoleId, ur.UserId } into lJoin
                                   from lj in lJoin.DefaultIfEmpty()
                                   select Tuple.Create(r.Id, r.Name, lj != null)).ToListAsync();

            ViewBag.Principals = (from p in Extension.policies
                                  join uc in db.UserClaims on new { ClaimValue = "1", ClaimType = p, UserId = user.Id } equals new { uc.ClaimValue, uc.ClaimType, uc.UserId } into lJoin
                                  from lj in lJoin.DefaultIfEmpty()
                                  select Tuple.Create(p, lj != null)).ToList();

            return View(user);
        }

        [HttpPost]
        [Route("/user-set-role")]
        [Authorize("admin.users.setrole")]
        public async Task<IActionResult> SetRole(int userId, int roleId, bool selected)
        {
            #region Check user and role
            var user = await db.Users.FirstOrDefaultAsync(user => user.Id == userId);

            if (user == null)
            {
                return Json(new
                {
                    error = true,
                    message = "Invalid operation!"
                });
            }

            if (userId == User.GetCurrentUserId())
            {
                return Json(new
                {
                    error = true,
                    message = "You can not give role to yourself!"
                });
            }

            var role = await db.Roles.FirstOrDefaultAsync(role => role.Id == roleId);

            if (role == null)
            {
                return Json(new
                {
                    error = true,
                    message = "Invalid operation!"
                });
            }
            #endregion

            if (selected)
            {
                if (await db.UserRoles.AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId))
                {
                    return Json(new
                    {
                        error = true,
                        message = $"'{user.Name} {user.Surname}' has already been assigned to '{role.Name}'!"
                    });
                }
                else
                {
                    db.UserRoles.Add(new XanAutoUserRole
                    {
                        UserId = userId,
                        RoleId = roleId,
                    });

                    await db.SaveChangesAsync();

                    return Json(new
                    {
                        error = false,
                        message = $"'{user.Name} {user.Surname}' is assigned to '{role.Name}'!"
                    });
                }

            }
            else
            {
                var userRole = await db.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);

                if (userRole == null)
                {
                    return Json(new
                    {
                        error = true,
                        message = $"'{user.Name} {user.Surname}' does not have this role: {role.Name}!"
                    });
                }
                else
                {
                    db.UserRoles.Remove(userRole);

                    await db.SaveChangesAsync();

                    return Json(new
                    {
                        error = false,
                        message = $"'{user.Name} {user.Surname}' has cancelled from '{role.Name}'!"
                    });
                }
            }
        }

        [HttpPost]
        [Route("/user-set-principal")]
        [Authorize("admin.users.setprincipal")]
        public async Task<IActionResult> SetPrincipal(int userId, string principalName, bool selected)
        {
            #region Check user and principal
            var user = await db.Users.FirstOrDefaultAsync(user => user.Id == userId);

            if (userId == User.GetCurrentUserId())
            {
                return Json(new
                {
                    error = true,
                    message = "You can not give principal to yourself!"
                });
            }


            if (user == null)
            {
                return Json(new
                {
                    error = true,
                    message = "Invalid operation!"
                });
            }

            var hasPrincipal = Extension.policies.Contains(principalName);

            if (!hasPrincipal)
            {
                return Json(new
                {
                    error = true,
                    message = "Invalid operation!"
                });
            }
            #endregion

            if (selected)
            {
                if (await db.UserClaims.AnyAsync(uc => uc.UserId == userId && uc.ClaimType.Equals(principalName) && uc.ClaimValue.Equals("1")))
                {
                    return Json(new
                    {
                        error = true,
                        message = $"'{user.Name} {user.Surname}' has already been assigned to '{principalName}'!"
                    });
                }
                else
                {
                    db.UserClaims.Add(new XanAutoUserClaim
                    {
                        UserId = userId,
                        ClaimType = principalName,
                        ClaimValue = "1"
                    });

                    await db.SaveChangesAsync();

                    return Json(new
                    {
                        error = false,
                        message = $"'{principalName}' is assigned to '{user.Name} {user.Surname}'!"
                    });
                }
            }
            else
            {
                var userClaim = await db.UserClaims.FirstOrDefaultAsync(uc => uc.UserId == userId && uc.ClaimType.Equals(principalName) && uc.ClaimValue.Equals("1"));
                if (userClaim == null)
                {
                    return Json(new
                    {
                        error = true,
                        message = $"'{user.Name} {user.Surname}' does not have this principal: {principalName}!"
                    });
                }
                else
                {
                    db.UserClaims.Remove(userClaim);

                    await db.SaveChangesAsync();

                    return Json(new
                    {
                        error = false,
                        message = $"'{user.Name} {user.Surname}' has cancelled from '{principalName}'!"
                    });
                }
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.users.delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var brand = await db.Users.FindAsync(id);

            if (brand == null)
            {
                return Json(new
                {
                    error = true,
                    message = "The user was not found!"
                });
            }

            db.Users.Remove(brand);
            await db.SaveChangesAsync();


            return Json(new
            {
                error = false,
                message = "The user was deleted!"
            });
        }
    }
}
