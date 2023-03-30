using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public enum Gender{Select,Male,Female,Transgender}


namespace StudentsAdmission
{
    public class StudentDetails
    {
        private static int s_studentID=3000;

        public string StudentID{get;}
        public string StudentName{get;set;}
        public string FatherName{get;set;}
        public DateTime DateOfBirth{get;set;}
        public Gender StudentGender{get;set;}
        public double PhysicsMark{get;set;}
        public double ChemistryMark{get;set;}
        public double MathsMark{get;set;}


        public StudentDetails(string studentName, string fatherName, DateTime dateOfBirth, Gender studentGender, double physicsMark, double chemistryMark, double mathsMark)
        {
            s_studentID++;
            StudentID="SF"+s_studentID;
            StudentName=studentName;
            FatherName=fatherName;
            DateOfBirth=dateOfBirth;
            StudentGender=studentGender;
            PhysicsMark=physicsMark;
            ChemistryMark=chemistryMark;
            MathsMark=mathsMark;

        }

        public bool EligibilityOrNot(double cutoff)
        {
            double total = PhysicsMark+ChemistryMark+MathsMark;
            double average = total/3;
            bool check= false;
            if(average>=75)
            {
                check=true;
            }
            return check;
        }

        




    }
}