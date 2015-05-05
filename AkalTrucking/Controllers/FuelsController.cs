using AkalTrucking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AkalTrucking.Controllers
{
    public class FuelsController : Controller
    {
        AT1699Entities db = new AT1699Entities();
        // GET: Fuels
        [Authorize]
        public ActionResult Index()
        {
           
            return View(getFuelList());
        }


        #region Helper
        public List<Fuel> getFuelList()
        {
            List<Fuel> fuels = new List<Fuel>();
            double gross = 0.0;
            double dscnts = 0.0;
            double totals = 0.0;
            foreach (var i in db.Fuels.ToList())
            {
                double dscnt = double.Parse(i.Discnt) / 2;
                i.Discnt = dscnt.ToString();
                double total = double.Parse(i.Diesel_Amt) - dscnt;
                total += double.Parse(i.Other);
                i.Invoice_Tot = total.ToString();

                DateTime input = DateTime.Today;
                int delta = DayOfWeek.Monday - input.DayOfWeek;
                DateTime monday = input.AddDays(delta);
                delta = DayOfWeek.Sunday - input.DayOfWeek + 7;
                DateTime sunday = input.AddDays(delta);
                if (DateTime.Parse(i.Date) >= monday && DateTime.Parse(i.Date) <= sunday)
                {

                    if (User.Identity.Name.Equals("akaltrucking@gmail.com") && int.Parse(i.Unit_) == 2002)
                    {
                        fuels.Add(i);
                        gross += double.Parse(i.Diesel_Amt);
                        dscnts += double.Parse(i.Discnt);
                        totals += double.Parse(i.Invoice_Tot);
                    }
                    else if (User.Identity.Name.Equals("mprtrucking@gmail.com") && int.Parse(i.Unit_) == 2001)
                    {
                        fuels.Add(i);
                        gross += double.Parse(i.Diesel_Amt);
                        dscnts += double.Parse(i.Discnt);
                        totals += double.Parse(i.Invoice_Tot);
                    }
                    else if (User.Identity.Name.Equals("akashtrucking@gmail.com") && int.Parse(i.Unit_) == 2000)
                    {
                        fuels.Add(i);
                        gross += double.Parse(i.Diesel_Amt);
                        dscnts += double.Parse(i.Discnt);
                        totals += double.Parse(i.Invoice_Tot);
                    }
                    else if (User.Identity.Name.Equals("mptransport@gmail.com") && int.Parse(i.Unit_) == 2004)
                    {
                        fuels.Add(i);
                        gross += double.Parse(i.Diesel_Amt);
                        dscnts += double.Parse(i.Discnt);
                        totals += double.Parse(i.Invoice_Tot);
                    }
                    else if (User.Identity.Name.Equals("sukh286@gmail.com"))
                    {
                        fuels.Add(i);
                        gross += double.Parse(i.Diesel_Amt);
                        dscnts += double.Parse(i.Discnt);
                        totals += double.Parse(i.Invoice_Tot);
                    }
                }
            }
            ViewBag.FuelGross = gross;
            ViewBag.FuelDiscount = dscnts;
            ViewBag.FuelTotal = totals;
            return fuels;
        }
        #endregion

    }
}