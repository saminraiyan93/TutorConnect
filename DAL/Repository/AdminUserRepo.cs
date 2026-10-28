using DAL.EF;
using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repository;

/// <summary>
/// Provides database operations used by administrators to view and manage user accounts.
/// </summary>
public class AdminUserRepo
{
    private readonly TutorConnectContext db;

    public AdminUserRepo(TutorConnectContext db)
    {
        this.db = db;
    }

    public List<User> GetAllUsers()
    {
        return db.Users
            .OrderBy(u => u.UserId)
            .ToList();
    }

    public User? GetUserById(int id)
    {
        return db.Users.Find(id);
    }

    public bool UpdateAccountStatus(int id, string status, DateTime? restrictedUntil)
    {
        var user = db.Users.Find(id);

        if (user == null || user.Role != "User")
        {
            return false;
        }

        user.AccountStatus = status;
        user.RestrictedUntil = restrictedUntil;
        user.UpdatedAt = DateTime.UtcNow;

        return db.SaveChanges() > 0;
    }
}
