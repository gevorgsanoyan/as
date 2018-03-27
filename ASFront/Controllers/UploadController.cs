using ASFront.Models;
using jQuery_File_Upload.MVC5.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace ASFront.Controllers
{
    public class UploadController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        FilesHelper filesHelper;
        String tempPath = "~/UploadDir/Temp";
        String serverMapPath = "~/UploadDir";
        private string StorageRoot
        {
            get { return Path.Combine(HostingEnvironment.MapPath(serverMapPath)); }
        }
        private string UrlBase = "/UploadDir/";
        String DeleteURL = "/FileUpload/DeleteFile/?file=";
        String DeleteType = "GET";
        public UploadController()
        {
            filesHelper = new FilesHelper(DeleteURL, DeleteType, StorageRoot, UrlBase, tempPath, serverMapPath);
        }


        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DocsUpload()
        {
            long ApplicationID = 0;
            string ApplicationIDStr = Request.QueryString["ApplicationID"];

            if (!string.IsNullOrWhiteSpace(ApplicationIDStr))
                Int64.TryParse(ApplicationIDStr, out ApplicationID);
            DocsApllications item = new DocsApllications();
            item.ApplicationId = ApplicationID;


            long ClientID = 0;
            string ClientIDStr = Request.QueryString["ClientID"];


            if (!string.IsNullOrWhiteSpace(ClientIDStr))
                Int64.TryParse(ClientIDStr, out ClientID);
            item.clientId = ClientID;


            string EditStr = Request.QueryString["Edit"];
            bool EditBool = false;

            if (EditStr == true.ToString())
                EditBool = true;


            ViewBag.ClientID = ClientID;

            ViewBag.DocyType = new SelectList(db.DocType.OrderBy(p => p.Name).Distinct().ToList(), "ID", "Name");

            ViewBag.Edit = EditBool;

            ViewBag.ApplicationID = ApplicationID;

            return View(item);
        }
           [HttpPost]
        public ActionResult DocsUpload(DocsApllications item, HttpPostedFileBase upload, long ClientID, bool Edit)
        {
            if (Request.Form["Cancel"] != null && Request.Form["Cancel"].Equals(Resources.Page.Cancel))
            {


                if (item.ApplicationId > 0)
                {

                    ViewBag.ApplicationID = item.ApplicationId;

                    if (!Edit)
                    {
                        return RedirectToAction("ApplicationSummary", "Application", new { ApplicationID = item.ApplicationId });
                    }
                    else
                    {
                        return RedirectToAction($"Edit/{item.ApplicationId}", "Application");
                    }
                }
                else
                {
                    return RedirectToAction("Edit", "Clients", new { ClientID = ClientID });
                }




            }

            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    string FileName = "";

                    if(item.ApplicationId > 0)
                        FileName = item.ApplicationId + "-" + Guid.NewGuid().ToString() + System.IO.Path.GetExtension(upload.FileName);
                    else
                        FileName = ClientID.ToString()  + "-" + Guid.NewGuid().ToString() + System.IO.Path.GetExtension(upload.FileName);

                    //var fileName = Path.GetFileName(upload.FileName);
                    upload.SaveAs(Path.Combine(StorageRoot, FileName));

                    item.FileName = FileName;


                    db.DocsApllications.Add(item);
                    db.SaveChanges();
                }

                if (item.ApplicationId > 0)
                {
                    ViewBag.ApplicationID = item.ApplicationId;

                    if (!Edit)
                    {
                        return RedirectToAction("ApplicationSummary", "Application", new { ApplicationID = item.ApplicationId });
                    }
                    else
                    {
                        return RedirectToAction($"Edit/{item.ApplicationId}", "Application");
                    }
                }
                else
                {
                    return RedirectToAction("Edit", "Clients", new { ClientID = ClientID });
                }

            }


         

            ViewBag.DocyType = new SelectList(db.DocType.OrderBy(p => p.Name).Distinct().ToList(), "ID", "Name");


            return View(item);
        }

        public ActionResult Upload()
        {
            string dirName = string.Empty;
            string Dir = Request.QueryString["Dir"];

            if (!string.IsNullOrWhiteSpace(Dir))
                dirName = Dir;

            TempData["Dir"] = dirName;

            return View();
        }

      
          public ActionResult AttachFile()
        {

            return View();
        }

        [HttpPost]

        public JsonResult UploadFile()
        {
            string dirName =string.Empty;
            string Dir = Request.QueryString["Dir"];

            string m = TempData["Dir"]?.ToString();

            if (!string.IsNullOrWhiteSpace(Dir))
                dirName = Dir;

            var resultList = new List<ViewDataUploadFilesResult>();

            var CurrentContext = HttpContext;

            filesHelper.UploadAndShowResults(CurrentContext, resultList, dirName);
            JsonFiles files = new JsonFiles(resultList);

            bool isEmpty = !resultList.Any();
            if (isEmpty)
            {
                return Json("Error ");
            }
            else
            {
                return Json(files);
            }


        }
    public JsonResult GetFileList()
    {
        var list = filesHelper.GetFileList();
        return Json(list, JsonRequestBehavior.AllowGet);
    }
    [HttpGet]
    public JsonResult DeleteFile(string file)
    {
        filesHelper.DeleteFile(file);
        return Json("OK", JsonRequestBehavior.AllowGet);
    }
    public ActionResult SaveUploadFile(string name)
        {
            ViewBag.Name = name;
            return View();
        }
        public ActionResult UploadFiles()
        {



            return View();
        }


        private string EncodeFile(string fileName)
        {
            return Convert.ToBase64String(System.IO.File.ReadAllBytes(fileName));
        }

        //DONT USE THIS IF YOU NEED TO ALLOW LARGE FILES UPLOADS
        //Credit to i-e-b and his ASP.Net uploader for the bulk of the upload helper methods - https://github.com/i-e-b/jQueryFileUpload.Net
        //private void UploadPartialFile(string fileName, HttpRequestBase request, List<ViewDataUploadFilesResult> statuses)
        //{
        //    if (request.Files.Count != 1) throw new HttpRequestValidationException("Attempt to upload chunked file containing more than one fragment per request");
        //    var file = request.Files[0];
        //    var inputStream = file.InputStream;

        //    var fullName = Path.Combine(StorageRoot, Path.GetFileName(fileName));

        //    using (var fs = new FileStream(fullName, FileMode.Append, FileAccess.Write))
        //    {
        //        var buffer = new byte[1024];

        //        var l = inputStream.Read(buffer, 0, 1024);
        //        while (l > 0)
        //        {
        //            fs.Write(buffer, 0, l);
        //            l = inputStream.Read(buffer, 0, 1024);
        //        }
        //        fs.Flush();
        //        fs.Close();
        //    }
        //    statuses.Add(new ViewDataUploadFilesResult()
        //    {
        //        name = fileName,
        //        size = file.ContentLength,
        //        type = file.ContentType,
        //        url = "/Home/Download/" + fileName,
        //        delete_url = "/Home/Delete/" + fileName,
        //        thumbnail_url = @"data:image/png;base64," + EncodeFile(fullName),
        //        delete_type = "GET",
        //    });
        //}

        //DONT USE THIS IF YOU NEED TO ALLOW LARGE FILES UPLOADS
        //Credit to i-e-b and his ASP.Net uploader for the bulk of the upload helper methods - https://github.com/i-e-b/jQueryFileUpload.Net
        //private void UploadWholeFile(HttpRequestBase request, List<ViewDataUploadFilesResult> statuses)
        //{
        //    for (int i = 0; i < request.Files.Count; i++)
        //    {
        //        var file = request.Files[i];

        //        var fullPath = Path.Combine(StorageRoot, Path.GetFileName(file.FileName));

        //        file.SaveAs(fullPath);

        //        statuses.Add(new ViewDataUploadFilesResult()
        //        {
        //            name = file.FileName,
        //            size = file.ContentLength,
        //            type = file.ContentType,
        //            url = "/Home/Download/" + file.FileName,
        //            delete_url = "/Home/Delete/" + file.FileName,
        //            thumbnail_url = @"data:image/png;base64," + EncodeFile(fullPath),
        //            delete_type = "GET",
        //        });
        //    }
        //}
  

    //public class ViewDataUploadFilesResult
    //    {
    //        public string name { get; set; }
    //        public int size { get; set; }
    //        public string type { get; set; }
    //        public string url { get; set; }
    //        public string delete_url { get; set; }
    //        public string thumbnail_url { get; set; }
    //        public string delete_type { get; set; }
    //    }



    }
}