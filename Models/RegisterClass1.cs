using System;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RegisterClaim.Models
{
    public class RegisterClass1

    {
        [Required(ErrorMessage = "Please Enter Registration Card Num."),MaxLength(30)]
        [Display(Name = "Card Number")]
       // [DataType(DataType.Text)]
        public string CardNumber { get; set; }
        [Required(ErrorMessage = "Please Enter the Employee Name"),MaxLength(30)]
        [Display(Name ="Employee Name")]
       // [DataType(DataType.Text)]
        public string EmployeeName { get; set; }
        [Required(ErrorMessage = "Please Enter the Pateint Name"),MaxLength(30)]
        [Display(Name ="PatientName")]
       // [DataType(DataType.Text)]
        public string PatientName { get; set; }
        [Required(ErrorMessage ="Please Select Desired Hospital")]
        [Display(Name = "Select Hospital")]
       // [DataType(DataType.Text)]
        public string HospitalName { get; set; }
        [Required(ErrorMessage ="Please Enter Select Hospital ID")]
        [Display(Name ="Hospital ID")]
        //[DataType(DataType.PhoneNumber)]
        public  string HospitalID { get; set; }
        [Required(ErrorMessage =" Please select the claim category")]
        [Display(Name ="Claim Category")]
        
        public string ClaimCategory { get; set; }
        [Required(ErrorMessage = "Please Select Disease")]
        [Display(Name = "Disease Name")]
        public string DiseaseName { get; set; }
        [Required(ErrorMessage = "Please Select Code")]
        [Display(Name = "Disease Code")]
        public string DiseaseCode { get; set; }
        [Required(ErrorMessage = "Please Enter Medical Record Number")]
        [Display(Name = "Medical Record Number")]
        public string MedicalRecordNumber { get; set; }
        [Required(ErrorMessage ="Please Enter Contact Number")]
        [Display(Name ="Contact")]
        public string Contact { get; set; }
        [Required(ErrorMessage ="Please Select the approval Amount")]
        [Display(Name ="Approval Amount")]
       // [DataType(DataType.Currency)]
        public string Approval_Amount { get; set; }
        [Required(ErrorMessage ="Please Select one of the option below")]
        [Display(Name = "Is the Treating consultant a visiting faculty (Y/N)")]
        public string TreatingConsultant { get; set; }
        [Required(ErrorMessage = "Please give remarks")]
        [Display(Name ="External Remarks")]
        public string ExternalRemarks { get; set; }
        [Display(Name ="Internal Remarks")]

        public string InternalRemarks { get; set; }
        [Display(Name = "Date Of Admission")]
        [Required(ErrorMessage ="Please Select Date")]
        //[DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Document Upload, Only jpg,jpeg,png,gif Allowed")]
        [Required(ErrorMessage = "Please Upload required documents")]
       // [DataType(DataType.Upload)]
        //[FileExtensions(ErrorMessage ="Select Image only")]


        public IEnumerable<HttpPostedFileBase> FileName { get; set; }

       public AllEmployee MyEmployee { get; set; }
        public Dependent MyDependents { get; set; }
        public Disease AllDisease { get; set; } 
        public Hospital AllHospitals { get; set; }  
       
    }
}