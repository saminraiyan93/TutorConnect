using DAL.Repository;
using AutoMapper;
using BLL.Models;
using System;
using System.Collections.Generic;

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

    public UserModel? GetUserById(int id)
    {
        var user = repo.GetUserById(id);

        if (user == null)
        {
            return null;
        }

        return mapper.Map<UserModel>(user);
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
