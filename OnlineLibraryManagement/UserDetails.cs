using System;

public enum Gender{Select,Male,Female,Transgender}
public enum Department{Select,ECE,EEE,CSE}


namespace OnlineLibraryManagement

{
    public class UserDetails
    {

        private static int s_registrationID=3000;

        public string RegistrationID{get;}
        public string UserName{get;set;}
        public Gender Gender{get;set;}
        public Department UserDepartment{get;set;}
        public long MobileNumber{get;set;}
        public string MailID{get;set;}


        public UserDetails(string userName,Gender gender,Department userdepartment,long mobileNumber,string mailID)
        {
            s_registrationID++;
            RegistrationID="SF"+s_registrationID;
            UserName=userName;
            Gender=gender;
            UserDepartment=userdepartment;
            MobileNumber=mobileNumber;
            MailID=mailID;


        }

        
    }
}