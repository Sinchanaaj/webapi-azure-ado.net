using System;
using Hospital.BaseClasses.Models;
using System.Collections.Generic;

namespace Hospital.BaseClasses.Intefaces
{
    public interface IHospital
    {
         List<HospitalCentre> GetHospitals();

        List<HospitalCentre> GetHospitalName(string name);

        List<HospitalCentre> PostHospitalName(int id,string name,string address,string city,int pincode);

        List<HospitalCentre> PatchHospitalName(int id,string name);

        List<HospitalCentre> DeleteHospitalName(int id);
    }
}
