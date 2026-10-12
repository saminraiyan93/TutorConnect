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
    public class TutorOfferingService
    {
        TutorOfferingRepo repo;
        IMapper mapper;

        public TutorOfferingService(TutorOfferingRepo repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        public List<TutorOfferingInfoModel> GetTutorOfferingsByUserIdWithInfo(int userId)
        {
            var data = repo.GetTutorOfferingsByUserIdWithInfo(userId);
            return mapper.Map<List<TutorOfferingInfoModel>>(data);
        }

        public List<TutorOfferingInfoModel> GetAllTutorOfferingsWithInfo()
        {
            var data = repo.GetAllTutorOfferingsWithInfo();
            var mappedObj = mapper.Map<List<TutorOfferingInfoModel>>(data);
            return mappedObj;
        }

        public TutorOfferingInfoModel GetTutorOfferingWithInfoById(int id)
        {
            var data = repo.GetTutorOfferingWithInfoById(id);
            var mappedObj = mapper.Map<TutorOfferingInfoModel>(data);
            return mappedObj;
        }

        public List<TutorOfferingModel> GetAllTutorOfferings()
        {
            var data = repo.GetAllTutorOfferings();
            var mappedObj = mapper.Map<List<TutorOfferingModel>>(data);
            return mappedObj;
        }

        public TutorOfferingModel GetTutorOfferingById(int id)
        {
            var data = repo.GetTutorOfferingById(id);
            var mappedObj = mapper.Map<TutorOfferingModel>(data);
            return mappedObj;
        }

        public bool CreateTutorOffering(TutorOfferingModel obj)
        {
            var mappedObj = mapper.Map<TutorOffering>(obj);
            var data = repo.CreateTutorOffering(mappedObj);
            return data;
        }

        public bool UpdateTutorOffering(TutorOfferingModel obj)
        {
            var mappedObj = mapper.Map<TutorOffering>(obj);
            var data = repo.UpdateTutorOffering(mappedObj);
            return data;
        }

        public bool DeleteTutorOffering(int id, int userId)
        {
            var data = repo.DeleteTutorOffering(id, userId);
            return data;
        }
    }
}
