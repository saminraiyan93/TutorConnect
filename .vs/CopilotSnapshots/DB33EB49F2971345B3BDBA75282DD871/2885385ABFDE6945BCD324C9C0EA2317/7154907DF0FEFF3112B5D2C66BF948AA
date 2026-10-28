using DAL.EF;
using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace DAL.Repository
{
    public class CourseRepo
    {
        TutorConnectContext db;
        public CourseRepo(TutorConnectContext db)
        {
            this.db = db;
        }

        public List<Course> GetAllCourseWithInfo()
        {
            var data = (from c in db.Courses.Include(c => c.Department)
                        select c).ToList();
            return data;
        }

        public List<Course> GetAllCourses()
        {
            return db.Courses.ToList();
        }

        public Course GetCourseById(int id)
        {
            var data = db.Courses.Find(id);
            return data;
        }

        public bool CreateCourse(Course Course)
        {
            var data = db.Courses.Add(Course);
            return db.SaveChanges() > 0;
        }

        public bool UpdateCourse(Course Course)
        {
            var exCourse = db.Courses.Find(Course.CourseId);

            if (exCourse == null)
            {
                return false;
            }

            exCourse.CourseName = Course.CourseName;
            exCourse.DepartmentId = Course.DepartmentId;
            exCourse.CourseCode = Course.CourseCode;
            exCourse.IsActive = Course.IsActive;

            return db.SaveChanges() > 0;
        }

        public bool DeleteCourse(int id)
        {
            var data = db.Courses.Find(id);
            db.Courses.Remove(data);
            return db.SaveChanges() > 0;
        }
    }
}
