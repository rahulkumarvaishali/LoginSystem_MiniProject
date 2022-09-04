using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginAuth.Models
{
    public class DataClass
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string EmpEmail { get; set; }
        public string EmpDept { get; set; }
        public string EmpMob { get; set; }
        public string EmpAddress { get; set; }
        public Nullable<int> EmpSalary { get; set; }
        public string EmpZender { get; set; }
        public string EmpZip { get; set; }
        public string EmpAbout { get; set; }
    }
}