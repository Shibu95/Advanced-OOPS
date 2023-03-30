using System;

public enum AdmissionStatus{Select,Admitted,Cancelled}
namespace StudentsAdmission
{
    public class AdmissionDetails 
    {
        private static int s_admissionID=1000;

        public string AdmissionID{get;}
        public string StudentID{get;set;}
        public string DepartmentID{get;set;}
        public DateTime AdmissionDate{get;set;}
        
        public AdmissionStatus AdmissionStatus{get;set;}
        


        public AdmissionDetails(string studentID,string departmentID,DateTime admissionDate,AdmissionStatus admissionStatus)
        {
            s_admissionID++;
            AdmissionID="AID"+s_admissionID;
            StudentID=studentID;
            DepartmentID=departmentID;
            AdmissionDate=admissionDate;
            AdmissionStatus=admissionStatus;
        
        }

    }
}