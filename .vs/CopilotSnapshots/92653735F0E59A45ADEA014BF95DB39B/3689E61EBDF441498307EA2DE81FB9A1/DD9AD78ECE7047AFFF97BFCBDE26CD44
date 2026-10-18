using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repository;
using BLL.Models;
using AutoMapper;
using DAL.EF.Tables;


namespace BLL.Service
{
    public class UserService
    {
        UserRepo repo;
        IMapper mapper;

        public UserService(UserRepo repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        public List<UserModel> GetAllUsers()
        {
            var data = repo.GetAllUsers();
            var mappedObj = mapper.Map<List<UserModel>>(data);
            return mappedObj;
        }

        public UserModel GetUserById(int id)
        {
            var data = repo.GetUserById(id);
            var mappedObj = mapper.Map<UserModel>(data);
            return mappedObj;
        }

        public bool CreateUser(UserModel obj)
        {
            var mappedObj = mapper.Map<User>(obj);
            var data = repo.CreateUser(mappedObj);
            return data;
        }

        public bool UpdateUser(UserModel obj)
        {
            var mappedObj = mapper.Map<User>(obj);
            var data = repo.UpdateUser(mappedObj);
            return data;
        }

        public bool DeleteUser(int id)
        {
            var data = repo.DeleteUser(id);
            return data;
        }

    }
}
