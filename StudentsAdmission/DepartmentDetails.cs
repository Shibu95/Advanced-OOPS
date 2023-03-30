using System;

namespace StudentsAdmission
{
    public class DepartmentDetails
    {
        private static int s_departmentID=100;

        public string DepartmentID{get;}
        public string DepartmentName{get;set;}
        public int NoOfSeats{get;set;}

        public DepartmentDetails(string departmentName,int noOfSeats)
        {
            s_departmentID++;
            DepartmentID="DID"+s_departmentID;
            DepartmentName=departmentName;
            NoOfSeats=noOfSeats;
        }

    }
}