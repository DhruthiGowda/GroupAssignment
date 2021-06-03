using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApiMVCSchool.Models;


namespace WebApiMVCSchool.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            IEnumerable<StudentModel> studdata = null;
            using (WebClient webClient = new WebClient())
            {
                webClient.BaseAddress = "https://localhost:44363/api/";

                var json = webClient.DownloadString("Students");
                var list = JsonConvert.DeserializeObject<List<StudentModel>>(json);
                studdata = list.ToList();
                return View(studdata);
            }
        }

        // GET: Student/Details/5
        public ActionResult Details(int id)
        {
            StudentModel studdata;
            using (WebClient webClient = new WebClient())
            {
                webClient.BaseAddress = "https://localhost:44363/api/";

                var json = webClient.DownloadString("Students/" + id);

                studdata = JsonConvert.DeserializeObject<StudentModel>(json);
            }
            return View(studdata);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        public ActionResult Create(StudentModel model)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.BaseAddress = "https://localhost:44363/api/";
                    var url = "Students/POST";
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    string data = JsonConvert.SerializeObject(model);
                    var response = webClient.UploadString(url, data);
                    JsonConvert.DeserializeObject<StudentModel>(response);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int id)
        {
            StudentModel studdata;
            using (WebClient webClient = new WebClient())
            {
                webClient.BaseAddress = "https://localhost:44363/api/";

                var json = webClient.DownloadString("Students/" + id);

                studdata = JsonConvert.DeserializeObject<StudentModel>(json);
            }
            return View(studdata);
        }

        // POST: Student/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, StudentModel model)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.BaseAddress = "https://localhost:44363/api/Students/" + id;
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    string data = JsonConvert.SerializeObject(model);

                    var response = webClient.UploadString(webClient.BaseAddress, "PUT", data);

                    StudentModel modeldata = JsonConvert.DeserializeObject<StudentModel>(response);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }


        // GET: Student/Delete/5
        public ActionResult Delete(int id)
        {
            StudentModel studata;
            using (WebClient webClient = new WebClient())
            {
                webClient.BaseAddress = "https://localhost:44363/api/";
                var json = webClient.DownloadString("Students/" + id);

                studata = JsonConvert.DeserializeObject<StudentModel>(json);
            }
            return View(studata);
        }

        // POST: Student/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, StudentModel model)
        {
            {
                try
                {
                    using (WebClient webClient = new WebClient())
                    {
                        NameValueCollection nv = new NameValueCollection();
                        var url = "https://localhost:44363/api/Students/" + id;
                        var response = Encoding.ASCII.GetString(webClient.UploadValues(url, "DELETE", nv));
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return RedirectToAction("Index");
            }
        }
    }
}
