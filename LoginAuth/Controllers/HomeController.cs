using LoginAuth.Database;
using LoginAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LoginAuth.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        RahulEntities entobj = new RahulEntities();

        public ActionResult Index()
        {
            var res = entobj.EmpTables.ToList();
            List<DataClass> clsobj = new List<DataClass>();
            foreach (var item in res)
            {
                clsobj.Add(new DataClass
                {
                    EmpId = item.EmpId,
                    EmpName=item.EmpName,
                    EmpEmail=item.EmpEmail,
                    EmpDept=item.EmpDept,
                    EmpMob=item.EmpMob,
                    EmpAddress=item.EmpAddress,
                    EmpSalary=item.EmpSalary,
                    EmpZender=item.EmpZender,
                    EmpZip=item.EmpZip,
                    EmpAbout=item.EmpAbout

                });
            }
            return View(clsobj);
        }

        public ActionResult Delete(int EmpId)
        {
            var delitem=entobj.EmpTables.Where(m=>m.EmpId==EmpId).First();
            entobj.EmpTables.Remove(delitem);
            entobj.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Form()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Form(DataClass obj)
        {
            EmpTable tblobj= new EmpTable();
            tblobj.EmpId = obj.EmpId;
            tblobj.EmpName = obj.EmpName;
            tblobj.EmpEmail = obj.EmpEmail;
            tblobj.EmpDept = obj.EmpDept;
            tblobj.EmpMob = obj.EmpMob;
            tblobj.EmpAddress = obj.EmpAddress;
            tblobj.EmpSalary = obj.EmpSalary;
            tblobj.EmpZender = obj.EmpZender;
            tblobj.EmpZip = obj.EmpZip;
            tblobj.EmpAbout = obj.EmpAbout;
            if (obj.EmpId == 0)
            {
                entobj.EmpTables.Add(tblobj);
                entobj.SaveChanges();
            }
            else
            {
                entobj.Entry(tblobj).State = System.Data.Entity.EntityState.Modified;
                entobj.SaveChanges();
            }
            
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int EmpId)
        {
            var edititem= entobj.EmpTables.Where(m=>m.EmpId==EmpId).First();
            DataClass clsobj = new DataClass();
            clsobj.EmpId=edititem.EmpId;
            clsobj.EmpName = edititem.EmpName;
            clsobj.EmpEmail = edititem.EmpEmail;
            clsobj.EmpDept = edititem.EmpDept;
            clsobj.EmpMob = edititem.EmpMob;
            clsobj.EmpAddress = edititem.EmpAddress;
            clsobj.EmpSalary = edititem.EmpSalary;
            clsobj.EmpZender = edititem.EmpZender;
            clsobj.EmpZip = edititem.EmpZip;
            clsobj.EmpAbout = edititem.EmpAbout;

            return View("Form",clsobj);
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult LoginForm()
        {

            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult LoginForm(LoginClass obj)
        {
            var res = entobj.LoginTables.Where(m => m.Email == obj.Email).FirstOrDefault();
            if(res == null)
            {
                TempData["email"] = "Wrong Email Please Enter Valid Input";
            }
            else
            {
                if(res.Email == obj.Email && res.Password == obj.Password)
                {
                    FormsAuthentication.SetAuthCookie(res.Email, false);
                    Session["email"]=res.Email;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Pass"] = "Wrong Password Please Enter Valid Password";
                }
            }
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LoginForm");
        }
    }
}