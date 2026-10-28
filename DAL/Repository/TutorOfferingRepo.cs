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
    public class TutorOfferingRepo
    {
        TutorConnectContext db;
        public TutorOfferingRepo(TutorConnectContext db)
        {
            this.db = db;
        }

        public List<TutorOffering> GetTutorOfferingsByUserIdWithInfo(int userId)
        {
            return db.TutorOfferings
                .Include(t => t.User)
                .Include(t => t.Course)
                    .ThenInclude(c => c.Department)
                .Where(t => t.UserId == userId &&
                            t.Course.IsActive == "Active" &&
                            t.Course.Department.IsActive == "Active")
                .OrderByDescending(t => t.CreatedAt)
                .ToList();
        }

        public List<TutorOffering> GetAllTutorOfferingsWithInfo()
        {
            var data = (from t_o in db.TutorOfferings
                        .Include(t_o => t_o.User)
                        .Include(t_o => t_o.Course).ThenInclude(c => c.Department)
                        .Where(t_o => t_o.Course.IsActive == "Active" &&
                                      t_o.Course.Department.IsActive == "Active")
                        .OrderByDescending(t_o => t_o.CreatedAt)
                        select t_o).ToList();
            return data;
        }

        public TutorOffering GetTutorOfferingWithInfoById(int id)
        {
            var data = db.TutorOfferings
                .Include(t => t.User)
                .Include(t => t.Course)
                    .ThenInclude(c => c.Department)
                .FirstOrDefault(t => t.TutorOfferingId == id &&
                                     t.Course.IsActive == "Active" &&
                                     t.Course.Department.IsActive == "Active");

            return data;
        }

        public List<TutorOffering> GetAllTutorOfferings()
        {
            return db.TutorOfferings.ToList();
        }

        public TutorOffering GetTutorOfferingById(int id)
        {
            var data = db.TutorOfferings.Find(id);
            return data;
        }

        public bool CreateTutorOffering(TutorOffering TutorOffering)
        {
            TutorOffering.CreatedAt = DateTime.Now;

            var data = db.TutorOfferings.Add(TutorOffering);
            return db.SaveChanges() > 0;
        }

        public bool UpdateTutorOffering(TutorOffering tutorOffering, int userId)
        {
            var existingOffer = db.TutorOfferings.FirstOrDefault(t =>
                t.TutorOfferingId == tutorOffering.TutorOfferingId &&
                t.UserId == userId);

            if (existingOffer == null)
            {
                return false;
            }

            existingOffer.CourseId = tutorOffering.CourseId;
            existingOffer.CoverageType = tutorOffering.CoverageType;
            existingOffer.TopicDescription = tutorOffering.TopicDescription;
            existingOffer.PostTitle = tutorOffering.PostTitle;
            existingOffer.TeachingMode = tutorOffering.TeachingMode;
            existingOffer.PricingType = tutorOffering.PricingType;
            existingOffer.RateAmount = tutorOffering.RateAmount;
            existingOffer.Availability = tutorOffering.Availability;
            existingOffer.ContactVia = tutorOffering.ContactVia;
            existingOffer.ContactValue = tutorOffering.ContactValue;
            existingOffer.UpdatedAt = DateTime.Now;

            return db.SaveChanges() > 0;
        }

        public bool DeleteTutorOffering(int id, int userId)
        {
            var data = db.TutorOfferings
                .FirstOrDefault(t => t.TutorOfferingId == id && t.UserId == userId);
            if(data == null)
            {
                return false;
            }
            db.TutorOfferings.Remove(data);
            return db.SaveChanges() > 0;
        }
    }
}
