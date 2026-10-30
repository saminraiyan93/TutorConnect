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
    public class StudentRequestService
    {
        StudentRequestRepo repo;
        IMapper mapper;

        public StudentRequestService(StudentRequestRepo repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        public List<StudentRequestInfoModel> GetStudentRequestsByUserIdWithInfo(int userId)
        {
            var data = repo.GetStudentRequestsByUserIdWithInfo(userId);
            return mapper.Map<List<StudentRequestInfoModel>>(data);
        }

        public int ConsumeApprovalNotices(int userId)
        {
            return repo.ConsumeApprovalNotices(userId);
        }

        public StudentRequestInfoModel GetStudentRequestWithInfoById(int id)
        {
            var data = repo.GetStudentRequestWithInfoById(id);
            var mappedObj = mapper.Map<StudentRequestInfoModel>(data);
            return mappedObj;
        }

        public List<StudentRequestInfoModel> GetAllStudentRequestsWithInfo()
        {
            var data = repo.GetAllStudentRequestsWithInfo();
            var mappedObj = mapper.Map<List<StudentRequestInfoModel>>(data);
            return mappedObj;
        }

        public List<StudentRequestModel> GetAllStudentRequests()
        {
            var data = repo.GetAllStudentRequests();
            var mappedObj = mapper.Map<List<StudentRequestModel>>(data);
            return mappedObj;
        }

        public StudentRequestModel GetStudentRequestById(int id)
        {
            var data = repo.GetStudentRequestById(id);
            var mappedObj = mapper.Map<StudentRequestModel>(data);
            return mappedObj;
        }

        public bool CreateStudentRequest(StudentRequestModel obj)
        {
            var mappedObj = mapper.Map<StudentRequest>(obj);
            var data = repo.CreateStudentRequest(mappedObj);
            return data;
        }

        public bool UpdateStudentRequest(StudentRequestModel obj)
        {
            var mappedObj = mapper.Map<StudentRequest>(obj);
            var data = repo.UpdateStudentRequest(mappedObj);
            return data;
        }

        public bool UpdateStudentRequest(StudentRequestModel obj, int userId)
        {
            var mappedObj = mapper.Map<StudentRequest>(obj);
            return repo.UpdateStudentRequest(mappedObj, userId);
        }

        public bool DeleteStudentRequest(int id, int userId)
        {
            return repo.DeleteStudentRequest(id, userId);
        }

        public bool DeleteStudentRequest(int id)
        {
            return repo.DeleteStudentRequest(id);
        }
    }
}
