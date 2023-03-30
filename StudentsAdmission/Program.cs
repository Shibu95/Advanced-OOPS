using System;
using System.Collections.Generic;
namespace StudentsAdmission;
class program
{
    //static Lists
    static List<StudentDetails> studentList = new List<StudentDetails>();
    static List<DepartmentDetails> departmentList = new List<DepartmentDetails>();
    static List<AdmissionDetails> admissionList = new List<AdmissionDetails>();
    //static Variable
    static StudentDetails currentLoggedInUser;
    public static void Main(string[] args)
    {
        LoadDefaultData();
        int mainOption = 0;
        string mainChoice = "YES";
        do
        {
            System.Console.WriteLine("***************Syncfusion College of Engineering and Technology***************");
            System.Console.WriteLine("Select an option from Main Menu\n1.Student Registration\n2.Student Login\n3.Check Departmetwise Seat Avialability\n4.Exit");
            mainOption = int.Parse(Console.ReadLine());
            switch(mainOption)
            {
                case 1:
                {
                    Registration();
                    break;
                }
                case 2:
                {
                    Login();
                    break;
                }
                case 3:
                {
                    DepartmentWiseSeatAvailability();
                    break;
                }
                case 4:
                {
                    System.Console.WriteLine("Application Exited Successfully");
                    mainChoice = "NO";
                    break;
                }
                default:
                {
                    System.Console.WriteLine("Entered option is Invalid. Please select an valid option.");
                    break;
                }
            }
        }while(mainChoice == "YES");
        
    }//Main Ends

    static void Registration()
    {
        System.Console.WriteLine("*********Registration Process*********");
        System.Console.WriteLine("Enter the Student Name");
        string studentName = Console.ReadLine();
        System.Console.WriteLine("Enter Student's Father Name");
        string fatherName = Console.ReadLine();
        System.Console.WriteLine("Enter student's Date of Birth");
        DateTime dob = DateTime.ParseExact(Console.ReadLine(),"dd/MM/yyyy",null);
        System.Console.WriteLine("Enter student's Gender");
        Gender studentGender = Enum.Parse<Gender>(Console.ReadLine(),true);
        System.Console.WriteLine("Enter Physics Mark");
        int physics = int.Parse(Console.ReadLine());
        System.Console.WriteLine("Enter Chemistry Mark");
        int chemistry = int.Parse(Console.ReadLine());
        System.Console.WriteLine("Enter Maths Mark");
        int maths = int.Parse(Console.ReadLine());

        StudentDetails student = new StudentDetails(studentName,fatherName,dob,studentGender,physics,chemistry,maths);
        studentList.Add(student);

        System.Console.WriteLine($"Registration was successfull. Your Student ID: {student.StudentID}");
    }//registration ends

    static void Login()
    {
        System.Console.WriteLine("Enter your student ID for login");
        bool flag = true;
        string passID = Console.ReadLine().ToUpper();
        foreach(StudentDetails student in studentList)
        {
            if(passID == student.StudentID)
            {
                flag = false;
                System.Console.WriteLine("You have logged in successfully.");
                currentLoggedInUser = student;
                SubMenu();
            }
        }
        if(flag)
        {
            System.Console.WriteLine("Invalid Student ID");
        }
    }//login ends

    static void SubMenu()
    {
        int subOption = 0;
        string subChoice = "YES";
        do
        {
            System.Console.WriteLine("Select an Option from Sub Menu\n1.Check Eligibility\n2.Show Details\n3.Take Admission\n4.Cancel Admission\n5.Show Admission Details\n6.Exit");
            subOption = int.Parse(Console.ReadLine());
            switch(subOption)
            {
                case 1:
                {
                    StudentEligibility();
                    break;
                }
                case 2:
                {
                    ShowDetails();
                    break;
                }
                case 3:
                {
                    TakeAdmission();
                    break;
                }
                case 4:
                {
                    CancelAdmission();
                    break;
                }
                case 5:
                {
                    ShowAdmissionDetails();
                    break;
                }
                case 6:
                {
                    System.Console.WriteLine("Taking back to Main Menu");
                    subChoice = "NO";
                    break;
                }
                default:
                {
                    System.Console.WriteLine("Entered option is Invalid. Please select an valid option.");
                    break;
                }
            }
        }while(subChoice == "YES");
    }//Sub Menu ends

    static void StudentEligibility()
    {
        foreach(StudentDetails student in studentList)
        {
            if(currentLoggedInUser.StudentID == student.StudentID)
            {
                bool check = currentLoggedInUser.EligibilityOrNot(75.0);
                if(check)
                {
                    System.Console.WriteLine("You are eligible");
                }
                else
                {
                    System.Console.WriteLine("You are not eigible");
                }
            }
        }
    }//check Eligibility ends

    static void ShowDetails()
    {
        foreach(StudentDetails student in studentList)
        {
            if(currentLoggedInUser.StudentID == student.StudentID)
            {
                System.Console.WriteLine($"Student ID: {student.StudentID}");
                System.Console.WriteLine($"Student Name: {student.StudentName}");
                System.Console.WriteLine($"Student's Father Name: {student.FatherName}");
                System.Console.WriteLine($"Student DOB: {student.DateOfBirth.ToString("dd/MM/yyyy")}");
                System.Console.WriteLine($"Student Gender: {student.StudentGender}");
                System.Console.WriteLine($"Student Physics Mark: {student.PhysicsMark}");
                System.Console.WriteLine($"Student Chemistry Mark: {student.ChemistryMark}");
                System.Console.WriteLine($"Student maths Mark: {student.MathsMark}");
            }
        }
    }//show details ends

    static void TakeAdmission()
    {
        foreach(DepartmentDetails department in departmentList)
        {
            System.Console.WriteLine($"{department.DepartmentID}    {department.DepartmentName}    {department.NoOfSeats}");
        }
        System.Console.WriteLine("Enter the department ID to select");
        string selectDept = Console.ReadLine().ToUpper();
        bool flag = true;
        bool flag2 = true;
        foreach (DepartmentDetails department in departmentList)
        {
            if(selectDept == department.DepartmentID)
            {
                flag = false;
                bool check = currentLoggedInUser.EligibilityOrNot(75.0);
                if(check)
                {
                    foreach(AdmissionDetails admission in admissionList)
                    {
                        if(currentLoggedInUser.StudentID == admission.StudentID)
                        {
                            flag2 = false;
                            System.Console.WriteLine("You cannot take another admission as you have done already admitted.\nplease cancel the previous admission and proceed with new admission");
                        }
                        if(flag2)
                        {
                            department.NoOfSeats--;
                            AdmissionDetails admission1 = new AdmissionDetails(currentLoggedInUser.StudentID,department.DepartmentID,DateTime.Now,AdmissionStatus.Admitted);
                            admissionList.Add(admission1);
                            System.Console.WriteLine($"Admission process was successfull. Your Admission ID: {admission1.AdmissionID}");
                        }
                    }
                }
                else
                {
                    System.Console.WriteLine("You are not eligible");
                }
            }
        }
        if(flag)
        {
            System.Console.WriteLine("Invalid Department ID");
        }
    }//Take admission ends

    static void CancelAdmission()
    {
        foreach(AdmissionDetails admission in admissionList)
        {
            if(currentLoggedInUser.StudentID == admission.StudentID && admission.AdmissionStatus == AdmissionStatus.Admitted)
            {
                admission.AdmissionStatus = AdmissionStatus.Cancelled;
                System.Console.WriteLine($"{admission.AdmissionID}  {admission.StudentID}   {admission.DepartmentID}   {admission.AdmissionDate}   {admission.AdmissionStatus}");
                System.Console.WriteLine("Your admission is cancelled sucessfully");
            }
            foreach(DepartmentDetails department in departmentList)
            {
                if(currentLoggedInUser.StudentID == admission.StudentID)
                {
                    if(admission.DepartmentID == department.DepartmentID)
                    {
                        department.NoOfSeats++;
                    }
                }
            }
        }
    }//cancel admission ends

    static void ShowAdmissionDetails()
    {
        bool flag = true;
        foreach(AdmissionDetails admission in admissionList)
        {
            if(currentLoggedInUser.StudentID == admission.StudentID)
            {
                flag = false;
                System.Console.WriteLine($"{admission.AdmissionID}  {admission.StudentID}   {admission.DepartmentID}    {admission.AdmissionDate}   {admission.AdmissionStatus}");
            }
        }
        if(flag)
            {
                System.Console.WriteLine("No admission data to display");
            }
    }//show admission details ends

    static void DepartmentWiseSeatAvailability()
    {
        foreach(DepartmentDetails department in departmentList)
        {
            System.Console.WriteLine($"{department.DepartmentID}     {department.DepartmentName}     {department.NoOfSeats}");
        }
    }//department wise seat availability ends

    static void LoadDefaultData()
    {
        //default student data
        StudentDetails student1 = new StudentDetails("Ravichandran E","Ettapparajan",new DateTime(1999,11,11),Gender.Male,95,95,95);
        studentList.Add(student1);
        StudentDetails student2 = new StudentDetails("Baskaran S","Sethurajan",new DateTime(1999,11,11),Gender.Male,95,95,95);
        studentList.Add(student2);

        //default department data
        DepartmentDetails department1 = new DepartmentDetails("EEE",29);
        departmentList.Add(department1);
        DepartmentDetails department2 = new DepartmentDetails("CSE",29);
        departmentList.Add(department2);
        DepartmentDetails department3 = new DepartmentDetails("MECH",30);
        departmentList.Add(department3);
        DepartmentDetails department4 = new DepartmentDetails("ECE",30);
        departmentList.Add(department4);

        //default admission data
        AdmissionDetails admission1 = new AdmissionDetails(studentList[0].StudentID,departmentList[0].DepartmentID,new DateTime(2022,05,11),AdmissionStatus.Admitted);
        admissionList.Add(admission1);
        AdmissionDetails admission2 = new AdmissionDetails(studentList[1].StudentID,departmentList[1].DepartmentID,new DateTime(2022,05,12),AdmissionStatus.Admitted);
        admissionList.Add(admission2);
    }//Load Deafult data ends
}
