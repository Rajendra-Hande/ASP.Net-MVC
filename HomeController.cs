using Microsoft.Reporting.WebForms;
using SchoolLeaving.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolLeaving.Controllers
{
    public class HomeController : Controller
    {
        LcDbEntities Db = new LcDbEntities();
        public ActionResult Index()
        {            
            return View(Db.StudentLCs.ToList());
        }

        public ActionResult Details(int? id)
        {
          StudentLC dpt = Db.StudentLCs.FirstOrDefault(d => d.RollNo == id);
            return View(dpt);
        }

        public ActionResult Report(int? Rollno, string ReportType)
        {
            LocalReport lr = new LocalReport();
            lr.ReportPath = Server.MapPath("~/Reports/StudentReport.rdlc");

            ReportDataSource rd = new ReportDataSource();
            rd.Name = "DataSet1";
            rd.Value = Db.StudentLCs.Where(d => d.RollNo == Rollno);
            lr.DataSources.Add(rd);

            string mt, enc, f;
            string[] s;
            Warning[] w;
            byte[] b = lr.Render(ReportType, null, out mt, out enc, out f, out s, out w);
            return File(b, mt);
        }
    }
}