using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Hospital.BaseClasses.Intefaces;
using Hospital.BaseClasses.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Hospital.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HospitalController : ControllerBase
    {
    
        private readonly ILogger<HospitalController> _logger;
        private readonly IHospital _hospital;

        public HospitalController(IHospital iHospital, ILogger<HospitalController> logger)
        {
            _hospital = iHospital;
            _logger = logger;
        }

        [HttpGet]
        public List<HospitalCentre> Get()
        {
            /*return Hospials.ToList();*/
            _logger.LogInformation("Inside cotroller get");
            return _hospital.GetHospitals();
        }

        [HttpGet("{id:int}")]
        public HospitalCentre GetCentre(int id)
        {
            var val=_hospital.GetHospitals().Find(x => x.Id==id);
            return val;
        }

        [HttpGet("{name}")]
        public List<HospitalCentre> GetList1(string name)
        {
            
            _logger.LogInformation("Inside cotroller getname");
            return _hospital.GetHospitalName(name);
        }

         [HttpPost]
        public List<HospitalCentre> AddtnNewHospital(JObject jsonResult)
        {
             HospitalCentre item=JsonConvert.DeserializeObject<HospitalCentre>(jsonResult.ToString());
            return _hospital.PostHospitalName(item.Id,item.Name,item.Address,item.City,item.Pincode);
        }

        

        [HttpPatch("{id}")]
        public List<HospitalCentre> AddNewHospital(JObject jsonResult,int id)
        {
             HospitalCentre item=JsonConvert.DeserializeObject<HospitalCentre>(jsonResult.ToString());
            return _hospital.PatchHospitalName(id,item.Name);
        }

        [HttpDelete("{id}")]
        public List<HospitalCentre> DeleteHospital(int id)
        {
            return _hospital.DeleteHospitalName(id);
        }

    }
}