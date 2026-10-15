using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL.Service;
using BLL.Models;

namespace AppLayerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TutorOfferingController : ControllerBase
    {
        TutorOfferingService service;
        public TutorOfferingController(TutorOfferingService service)
        {
            this.service = service;
        }

        [HttpGet("info/{id}")]
        public IActionResult GetTutorOfferingWithInfoById(int id)
        {
            var data = service.GetTutorOfferingWithInfoById(id);
            return Ok(data);
        }

        [HttpGet("all/info")]
        public IActionResult GetAllTutorOfferingsWithInfo()     // Tutor Offerings with UserName and CourseName
        {
            var data = service.GetAllTutorOfferingsWithInfo();
            return Ok(data);
        }

        [HttpGet("all")]
        public IActionResult GetAllTutorOfferings()
        {
            var data = service.GetAllTutorOfferings();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public IActionResult GetTutorOfferingById(int id)
        {
            var data = service.GetTutorOfferingById(id);
            return Ok(data);
        }

        [HttpPost]
        public IActionResult CreateTutorOffering(TutorOfferingModel TutorOfferingModel)
        {
            var data = service.CreateTutorOffering(TutorOfferingModel);

            if (data)
            {
                return Ok("TutorOffering Created Successfully!");
            }

            return BadRequest("TutorOffering Creation Failed!");
        }

        [HttpPut]
        public IActionResult UpdateTutorOffering(TutorOfferingModel TutorOfferingModel)
        {
            if (TutorOfferingModel.UserId <= 0)
            {
                return BadRequest("A valid UserId is required.");
            }

            var data = service.UpdateTutorOffering(
                TutorOfferingModel,
                TutorOfferingModel.UserId);

            if (data)
            {
                return Ok("TutorOffering Updated Successfully!");
            }

            return BadRequest("TutorOffering Update Failed!");
        }

        [HttpDelete("{id, userId}")]
        public IActionResult DeleteTutorOffering(int id, int userId)
        {
            var data = service.DeleteTutorOffering(id, userId);

            if (data)
            {
                return Ok("TutorOffering deleted Successfully!");
            }

            return BadRequest("TutorOffering deletion failed!");
        }

    }
}
