using DAL.EF;
using DAL.EF.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class StudentRequestRepo
    {
        TutorConnectContext db;
        public StudentRequestRepo(TutorConnectContext db)
        {
            this.db = db;
        }

        public List<StudentRequest> GetStudentRequestsByUserIdWithInfo(int userId)
        {
            return db.StudentRequests
                .Include(sr => sr.User)
                .Include(sr => sr.Course)
                    .ThenInclude(c => c.Department)
                .Where(sr => sr.UserId == userId)
                .OrderByDescending(sr => sr.CreatedAt)
                .ToList();
        }

        public StudentRequest GetStudentRequestWithInfoById(int id)
        {
            var data = db.StudentRequests
                .Include(sr => sr.User)
                .Include(sr => sr.Course)
                    .ThenInclude(c => c.Department)
                .FirstOrDefault(sr => sr.StudentRequestId == id);

            return data;
        }

        public List<StudentRequest> GetAllStudentRequestsWithInfo()
        {
            var data = (from sr in db.StudentRequests
                        .Include(sr => sr.User)
                       .Include(sr => sr.Course)
                       .ThenInclude(c => c.Department)
                       .OrderByDescending(sr => sr.CreatedAt)
                        select sr).ToList();
            return data;
        }

        public List<StudentRequest> GetAllStudentRequests()
        {
            return db.StudentRequests.ToList();
        }

        public StudentRequest GetStudentRequestById(int id)
        {
            var data = db.StudentRequests.Find(id);
            return data;
        }

        public bool CreateStudentRequest(StudentRequest StudentRequest)
        {
            StudentRequest.CreatedAt = DateTime.Now;
            var data = db.StudentRequests.Add(StudentRequest);
            return db.SaveChanges() > 0;
        }

        public bool UpdateStudentRequest(StudentRequest StudentRequest)
        {
            var exStudentRequest = db.StudentRequests.Find(StudentRequest.StudentRequestId);

            if (exStudentRequest == null)
            {
                return false;
            }

            exStudentRequest.UserId = StudentRequest.UserId;
            exStudentRequest.CourseId = StudentRequest.CourseId;
            exStudentRequest.CoverageType = StudentRequest.CoverageType;
            exStudentRequest.TopicDescription = StudentRequest.TopicDescription;
            exStudentRequest.PostTitle = StudentRequest.PostTitle;
            exStudentRequest.TeachingMode = StudentRequest.TeachingMode;
            exStudentRequest.BudgetType = StudentRequest.BudgetType;
            exStudentRequest.BudgetPerLecture = StudentRequest.BudgetPerLecture;
            exStudentRequest.Availability = StudentRequest.Availability;
            exStudentRequest.ConnectVia = StudentRequest.ConnectVia;
            exStudentRequest.ConnectValue = StudentRequest.ConnectValue;
            exStudentRequest.UpdatedAt = StudentRequest.UpdatedAt;
            exStudentRequest.PostStatus = StudentRequest.PostStatus;
            return db.SaveChanges() > 0;
        }

        public bool UpdateStudentRequest(StudentRequest studentRequest, int userId)
        {
            var existingRequest = db.StudentRequests.FirstOrDefault(sr =>
                sr.StudentRequestId == studentRequest.StudentRequestId &&
                sr.UserId == userId);

            if (existingRequest == null)
            {
                return false;
            }

            existingRequest.CourseId = studentRequest.CourseId;
            existingRequest.CoverageType = studentRequest.CoverageType;
            existingRequest.TopicDescription = studentRequest.TopicDescription;
            existingRequest.PostTitle = studentRequest.PostTitle;
            existingRequest.TeachingMode = studentRequest.TeachingMode;
            existingRequest.BudgetType = studentRequest.BudgetType;
            existingRequest.BudgetPerLecture = studentRequest.BudgetPerLecture;
            existingRequest.Availability = studentRequest.Availability;
            existingRequest.ConnectVia = studentRequest.ConnectVia;
            existingRequest.ConnectValue = studentRequest.ConnectValue;
            existingRequest.UpdatedAt = DateTime.Now;

            return db.SaveChanges() > 0;
        }

        public bool DeleteStudentRequest(int id, int userId)
        {
            var request = db.StudentRequests.FirstOrDefault(sr =>
                sr.StudentRequestId == id &&
                sr.UserId == userId);

            if (request == null)
            {
                return false;
            }

            db.StudentRequests.Remove(request);
            return db.SaveChanges() > 0;
        }

        public bool DeleteStudentRequest(int id)
        {
            var request = db.StudentRequests.Find(id);

            if (request == null)
            {
                return false;
            }

            db.StudentRequests.Remove(request);
            return db.SaveChanges() > 0;
        }
    }
}
