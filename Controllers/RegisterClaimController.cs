using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RegisterClaim.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace RegisterClaim.Controllers
{
    public class RegisterClaimController : Controller
    {
        // GET: RegisterClaim
        Hospital_MSEntities2 objlist = new Hospital_MSEntities2();
        Hospital_MSEntities3 newlist = new Hospital_MSEntities3();
        public ActionResult Index()
        {
            List<Dependent> DependentList = objlist.Dependents.ToList();
            ViewBag.dependentsTable = new SelectList(DependentList, "DependentsID", "DependentName");
            List<AllEmployee> MyEmp = objlist.AllEmployees.ToList();
            ViewBag.EmpList = new SelectList(MyEmp, "EmpID", "Employees");
            List<AllEmployee> cn = objlist.AllEmployees.ToList();
            ViewBag.NumberList = new SelectList(cn, "EmpID", "CardNumber");
            List<Disease> DiseaseList = newlist.Diseases.ToList();
            ViewBag.diseaseTable = new SelectList(DiseaseList, "id", "DiseaseName");
            List<Disease> CodeList = newlist.Diseases.ToList();
            ViewBag.codeTable = new SelectList(CodeList, "id", "DiseaseCode");
            List<Hospital> HospitalList = newlist.Hospitals.ToList();
            ViewBag.hospitalTable = new SelectList(HospitalList, "id", "HospitalName");
          
            return View();
        }

        [HttpPost]
        public ActionResult Index(RegisterClass1 rc)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["myconnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
           
            string sqlquery = "insert into Claim_Register (CardNumber,EmployeeName,PatientName,HospitalName,HospitalID,ClaimCategory,DiseaseName,DiseaseCode,MedicalRecordNumber,Contact,Approval_Amount,TreatingConsultant,ExternalRemarks,InternalRemarks,CreatedDate) values (@CardNumber,@EmployeeName,@PateintName,@HospitalName,@HospitalID,@ClaimCategory,@DiseaseName,@DiseaseCode,@MedicalRecordNumber,@Contact,@Approval_Amount,@TreatingConsultant,@ExternalRemarks,@InternalRemarks,@CreatedDate);" + "insert into Files (FileName) values (@FileName)";
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
            sqlconn.Open();
           
            sqlcomm.Parameters.AddWithValue("@CardNumber", rc.CardNumber);
            sqlcomm.Parameters.AddWithValue("@EmployeeName", rc.EmployeeName);
            sqlcomm.Parameters.AddWithValue("@PateintName", rc.PatientName);
            sqlcomm.Parameters.AddWithValue("@HospitalName", rc.HospitalName);
            sqlcomm.Parameters.AddWithValue("@HospitalID", rc.HospitalID);
            sqlcomm.Parameters.AddWithValue("@ClaimCategory", rc.ClaimCategory);
            sqlcomm.Parameters.AddWithValue("@DiseaseName", rc.DiseaseName);
            sqlcomm.Parameters.AddWithValue("@DiseaseCode", rc.DiseaseCode);
            sqlcomm.Parameters.AddWithValue("@MedicalRecordNumber", rc.MedicalRecordNumber);
            sqlcomm.Parameters.AddWithValue("@Contact", rc.Contact);
            sqlcomm.Parameters.AddWithValue("@Approval_Amount", rc.Approval_Amount);
            sqlcomm.Parameters.AddWithValue("@TreatingConsultant", rc.TreatingConsultant);
            sqlcomm.Parameters.AddWithValue("@ExternalRemarks", rc.ExternalRemarks);
            sqlcomm.Parameters.AddWithValue("@InternalRemarks", rc.InternalRemarks);
            sqlcomm.Parameters.AddWithValue("@CreatedDate", rc.CreatedDate);

            /*if (file != null && file.ContentLength > 0) {
                string filename = Path.GetFileName(file.FileName);
                string imgpath = Path.Combine(Server.MapPath("~/documents/"),filename);
                file.SaveAs(imgpath);
            }*/



            Task.Run(() => UploadFile(rc.FileName));
            
          //  Task.WaitAll();
            //return RedirectToAction("Files");


            sqlcomm.Parameters.AddWithValue("@FileName", "~/documents/" + rc.FileName);
            sqlcomm.ExecuteNonQuery();


            sqlconn.Close();
            ViewData["Message"] = "Registration Successfull";



            return View("Index");
            
        }

        public async Task<string> UploadFile(IEnumerable<HttpPostedFileBase> file)
        {

            //Console.WriteLine(file.FileName);
            /*if (file == null || file.Length == 0)
                return false;//return Content("file not selected");

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot",
                        file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }*/

            //foreach (HttpPostedFileBase file in files)
            //{
            //Checking file is available to save.
            var supportedTypes = new[] { ".jpg", ".jpeg", ".png",".pdf"};

           string extension = "";
            int file_length = file.Count();
           

            foreach (HttpPostedFileBase files in file)
            {
                extension = Path.GetExtension(files.FileName);


                if (supportedTypes.Contains(extension.ToLower()) && files.ContentLength <= (5 * 1024))
                       {
                            if (files != null && file_length < 6)
                            {
                                var InputFileName = DateTime.Now.ToString("yyyymmddMMss") + Path.GetFileName(files.FileName);
                                var ServerSavePath = Path.Combine(Server.MapPath("~/documents/") + InputFileName);
                                //Save file to server folder  
                                files.SaveAs(ServerSavePath);
                                //assigning file uploaded status to ViewBag for showing message to user.  
                                ViewBag.message = " files uploaded successfully.";
                            }
                            else if (files == null) { ViewBag.message = "No file Found"; }
                        }
                else { ViewBag.message = "only jpg,png and jpeg"; }
            }

          //  Task.WaitAll();
            //}
            //await Task.Yield();
            return ViewBag.message;

            

            //return RedirectToAction("Files");
        }
    }
}