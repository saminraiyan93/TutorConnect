using DAL.EF;
using DAL.EF.Tables;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repository;

/// <summary>
/// Provides database operations for administrators to manage departments and courses in the catalog.
/// </summary>
public class AdminCatalogRepo
{
    private readonly TutorConnectContext db;

    public AdminCatalogRepo(TutorConnectContext db)
    {
        this.db = db;
    }

    public List<Course> GetAllCourses()
    {
        return db.Courses
            .OrderBy(c => c.CourseName)
            .ToList();
    }

    public List<Department> GetAllDepartments()
    {
        return db.Departments
            .OrderBy(d => d.DepartmentName)
            .ToList();
    }

    public bool CreateCourse(Course course)
    {
        db.Courses.Add(course);
        return db.SaveChanges() > 0;
    }

    public bool UpdateCourse(Course course)
    {
        var existingCourse = db.Courses.Find(course.CourseId);

        if (existingCourse == null)
        {
            return false;
        }

        existingCourse.CourseCode = course.CourseCode;
        existingCourse.CourseName = course.CourseName;
        existingCourse.DepartmentId = course.DepartmentId;
        existingCourse.IsActive = course.IsActive;

        var department = db.Departments.Find(course.DepartmentId);
        var shouldBeActive = course.IsActive == "Active" && department?.IsActive == "Active";

        var tutorOffers = db.TutorOfferings
            .Where(offer => offer.CourseId == course.CourseId)
            .ToList();
        foreach (var offer in tutorOffers)
        {
            offer.PostStatus = shouldBeActive ? "Active" : "Inactive";
        }

        var studentRequests = db.StudentRequests
            .Where(request => request.CourseId == course.CourseId)
            .ToList();
        foreach (var request in studentRequests)
        {
            request.PostStatus = shouldBeActive ? "Active" : "Inactive";
        }

        return db.SaveChanges() > 0;
    }

    public bool DeleteCourse(int id)
    {
        var course = db.Courses.Find(id);

        if (course == null)
        {
            return false;
        }

        course.IsActive = "Inactive";

        var tutorOffers = db.TutorOfferings
            .Where(offer => offer.CourseId == course.CourseId)
            .ToList();
        foreach (var offer in tutorOffers)
        {
            offer.PostStatus = "Inactive";
        }

        var studentRequests = db.StudentRequests
            .Where(request => request.CourseId == course.CourseId)
            .ToList();
        foreach (var request in studentRequests)
        {
            request.PostStatus = "Inactive";
        }

        return db.SaveChanges() > 0;
    }

    public bool EnableCourse(int id)
    {
        var course = db.Courses.Find(id);

        if (course == null)
        {
            return false;
        }

        course.IsActive = "Active";

        var department = db.Departments.Find(course.DepartmentId);
        var shouldBeActive = department?.IsActive == "Active";

        var tutorOffers = db.TutorOfferings
            .Where(offer => offer.CourseId == course.CourseId)
            .ToList();
        foreach (var offer in tutorOffers)
        {
            offer.PostStatus = shouldBeActive ? "Active" : "Inactive";
        }

        var studentRequests = db.StudentRequests
            .Where(request => request.CourseId == course.CourseId)
            .ToList();
        foreach (var request in studentRequests)
        {
            request.PostStatus = shouldBeActive ? "Active" : "Inactive";
        }

        return db.SaveChanges() > 0;
    }

    public bool CreateDepartment(Department department)
    {
        db.Departments.Add(department);
        return db.SaveChanges() > 0;
    }

    public bool UpdateDepartment(Department department)
    {
        var existingDepartment = db.Departments.Find(department.DepartmentId);

        if (existingDepartment == null)
        {
            return false;
        }

        existingDepartment.DepartmentName = department.DepartmentName;
        existingDepartment.IsActive = department.IsActive;

        var tutorOffers = db.TutorOfferings
            .Include(offer => offer.Course)
            .Where(offer => offer.Course.DepartmentId == existingDepartment.DepartmentId)
            .ToList();
        foreach (var offer in tutorOffers)
        {
            offer.PostStatus = department.IsActive == "Active" && offer.Course.IsActive == "Active"
                ? "Active"
                : "Inactive";
        }

        var studentRequests = db.StudentRequests
            .Include(request => request.Course)
            .Where(request => request.Course.DepartmentId == existingDepartment.DepartmentId)
            .ToList();
        foreach (var request in studentRequests)
        {
            request.PostStatus = department.IsActive == "Active" && request.Course.IsActive == "Active"
                ? "Active"
                : "Inactive";
        }

        return db.SaveChanges() > 0;
    }

    public bool DeleteDepartment(int id)
    {
        var department = db.Departments.Find(id);

        if (department == null)
        {
            return false;
        }

        department.IsActive = "Inactive";

        var tutorOffers = db.TutorOfferings
            .Where(offer => offer.Course.DepartmentId == department.DepartmentId)
            .ToList();
        foreach (var offer in tutorOffers)
        {
            offer.PostStatus = "Inactive";
        }

        var studentRequests = db.StudentRequests
            .Where(request => request.Course.DepartmentId == department.DepartmentId)
            .ToList();
        foreach (var request in studentRequests)
        {
            request.PostStatus = "Inactive";
        }

        return db.SaveChanges() > 0;
    }

    public bool EnableDepartment(int id)
    {
        var department = db.Departments.Find(id);

        if (department == null)
        {
            return false;
        }

        department.IsActive = "Active";
        var tutorOffers = db.TutorOfferings
            .Include(offer => offer.Course)
            .Where(offer => offer.Course.DepartmentId == department.DepartmentId)
            .ToList();
        foreach (var offer in tutorOffers)
        {
            offer.PostStatus = offer.Course.IsActive == "Active" ? "Active" : "Inactive";
        }

        var studentRequests = db.StudentRequests
            .Include(request => request.Course)
            .Where(request => request.Course.DepartmentId == department.DepartmentId)
            .ToList();
        foreach (var request in studentRequests)
        {
            request.PostStatus = request.Course.IsActive == "Active" ? "Active" : "Inactive";
        }

        return db.SaveChanges() > 0;
    }
}
