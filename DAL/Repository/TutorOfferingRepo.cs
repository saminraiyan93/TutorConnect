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
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.CreatedAt)
                .ToList();
        }

        public List<TutorOffering> GetAllTutorOfferingsWithInfo()
        {
            var data = (from t_o in db.TutorOfferings
                        .Include(t_o => t_o.User)
                        .Include(t_o => t_o.Course).ThenInclude(c => c.Department)
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
                .FirstOrDefault(t => t.TutorOfferingId == id);

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

        public bool UpdateTutorOffering(TutorOffering TutorOffering)
        {
            var exTutorOffering = db.TutorOfferings.Find(TutorOffering.TutorOfferingId);

            if (exTutorOffering == null)
            {
                return false;
            }

            exTutorOffering.UserId = TutorOffering.UserId;
            exTutorOffering.CourseId = TutorOffering.CourseId;
            exTutorOffering.CoverageType = TutorOffering.CoverageType;
            exTutorOffering.TopicDescription = TutorOffering.TopicDescription;
            exTutorOffering.PostTitle = TutorOffering.PostTitle;
            exTutorOffering.TeachingMode = TutorOffering.TeachingMode;
            exTutorOffering.PricingType = TutorOffering.PricingType;
            exTutorOffering.RateAmount = TutorOffering.RateAmount;
            exTutorOffering.Availability = TutorOffering.Availability;
            exTutorOffering.ContactVia = TutorOffering.ContactVia;
            exTutorOffering.ContactValue = TutorOffering.ContactValue;
            exTutorOffering.UpdatedAt = TutorOffering.UpdatedAt;
            exTutorOffering.PostStatus = TutorOffering.PostStatus;
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
