using DAL.Repository;
using AutoMapper;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Service;

/// <summary>
/// Contains business logic for administrative user-account management.
/// </summary>
public class AdminUserService
{
    private readonly AdminUserRepo repo;
    private readonly IMapper mapper;

    public AdminUserService(AdminUserRepo repo, IMapper mapper)
    {
        this.repo = repo;
        this.mapper = mapper;
    }

    public List<UserModel> GetAllUsers()
    {
        var users = repo.GetAllUsers();
        return mapper.Map<List<UserModel>>(users);
    }

    public List<UserModel> GetAllAdmins()
    {
        var users = repo.GetAllUsers()
            .Where(user => user.Role == "Admin" || user.Role == "SuperAdmin")
            .ToList();

        return mapper.Map<List<UserModel>>(users);
    }

    public UserModel? GetUserById(int id)
    {
        var user = repo.GetUserById(id);

        if (user == null)
        {
            return null;
        }

        return mapper.Map<UserModel>(user);
    }

    public bool EmailExists(string email, int userId)
    {
        return repo.EmailExists(email, userId);
    }

    public bool CreateAdmin(AdminCreateModel model)
    {
        if (repo.EmailExists(model.Email, 0))
        {
            return false;
        }

        var user = new DAL.EF.Tables.User
        {
            UserName = model.UserName,
            Email = model.Email,
            Password = model.Password,
            Role = "Admin",
            AccountStatus = "Active",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        return repo.CreateAdmin(user);
    }

    public bool UpdateAdmin(AdminUpdateModel model)
    {
        var admin = repo.GetUserById(model.UserId);

        if (admin == null || (admin.Role != "Admin" && admin.Role != "SuperAdmin"))
        {
            return false;
        }

        if (repo.EmailExists(model.Email, model.UserId))
        {
            return false;
        }

        return repo.UpdateAdmin(model.UserId, model.UserName, model.Email);
    }

    public bool ActivateAdmin(int id)
    {
        return repo.UpdateAdminStatus(id, "Active");
    }

    public bool DisableAdmin(int id, int currentUserId)
    {
        if (id == currentUserId)
        {
            return false;
        }

        var admin = repo.GetUserById(id);

        if (admin == null || (admin.Role != "Admin" && admin.Role != "SuperAdmin"))
        {
            return false;
        }

        if (admin.Role == "SuperAdmin" &&
            admin.AccountStatus == "Active" &&
            repo.GetActiveSuperAdminCount() <= 1)
        {
            return false;
        }

        return repo.UpdateAdminStatus(id, "Disabled");
    }

    public bool ActivateUser(int id)
    {
        var data = repo.UpdateAccountStatus(id, "Active", null);
        return data;
    }

    public bool RestrictUser(int id, int durationDays)
    {
        if (durationDays != 1 && durationDays != 7 && durationDays != 30)
        {
            return false;
        }

        var data = repo.UpdateAccountStatus(id, "Restricted", DateTime.UtcNow.AddDays(durationDays));

        return data;
    }

    public bool BanUser(int id)
    {
        var data = repo.UpdateAccountStatus(id, "Banned", null);
        return data;
    }
}
