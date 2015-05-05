using AkalTrucking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Net;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace AkalTrucking.Controllers
{
    public class LoadsController : Controller
    {
        AT1699Entities db = new AT1699Entities();
        // GET: Loads
        [Authorize]
        public ActionResult Index()
        {
            WeeklyLoadFuel wlf = new WeeklyLoadFuel();
            wlf.FuelList = getFuelList();
            wlf.LoadsList = getLoadList();
            return View(wlf);
        }

        //[HttpGet]
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Create(Load load)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Loads.Add(load);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(load);
        //}

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Load load = db.Loads.Find(id);
            if (load == null)
            {
                return HttpNotFound();
            }
            IList<Load> ld = new List<Load>();
            ld.Add(load);
            return View(ld);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Load_Number,Reference_Number,First_Pickup,P_U_Name,P_U_City,P_U_State_Prov,Last_Drop_off,D_O_Name,D_O_City,D_O_State_Prov,Status,Trailer_Number,Rate")] Load load)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(load).State = EntityState.Modified;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException e)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", e.ToString());
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            IList<Load> ld = new List<Load>();
            ld.Add(load);
            return View(ld);
        }

        #region Helper
        public List<Load> getLoadList()
        {
            List<Load> loads = new List<Load>();
            double gross = 0.0;
            double qp = 0.0;
            double dp = 0.0;
            double total = 0.0;

            foreach (var i in db.Loads.ToList())
            {
                DateTime input = DateTime.Today;
                int delta = DayOfWeek.Monday - input.DayOfWeek;
                DateTime monday = input.AddDays(delta);
                delta = DayOfWeek.Sunday - input.DayOfWeek + 7;
                DateTime sunday = input.AddDays(delta);
                if (i.First_Pickup >= monday && i.First_Pickup <= sunday)
                {

                    if (User.Identity.Name.Equals("akaltrucking@gmail.com") && i.Trailer_Number == 1902)
                    {
                        loads.Add(i);
                        gross += i.Rate;
                        qp += i.Rate * 0.015;
                        dp += i.Rate * 0.03;
                        total = gross - (qp + dp);
                    }
                    else if (User.Identity.Name.Equals("mprtrucking@gmail.com") && i.Trailer_Number == 1901)
                    {
                        loads.Add(i);
                        gross += i.Rate;
                        qp += i.Rate * 0.015;
                        dp += i.Rate * 0.03;
                        total = gross - (qp + dp);
                    }
                    else if (User.Identity.Name.Equals("akashtrucking@gmail.com") && i.Trailer_Number == 1900)
                    {
                        loads.Add(i);
                        gross += i.Rate;
                        qp += i.Rate * 0.015;
                        dp += i.Rate * 0.03;
                        total = gross - (qp + dp);
                    }
                    else if (User.Identity.Name.Equals("mptransport@gmail.com") && i.Trailer_Number == 1904)
                    {
                        loads.Add(i);
                        gross += i.Rate;
                        qp += i.Rate * 0.015;
                        dp += i.Rate * 0.03;
                        total = gross - (qp + dp);
                    }
                    else if (User.Identity.Name.Equals("sukh286@gmail.com"))
                    {
                        loads.Add(i);
                        gross += i.Rate;
                        qp += i.Rate * 0.015;
                        dp += i.Rate * 0.03;
                        total = gross - (qp + dp);
                    }
                }
            }
            if (qp.ToString().Contains("."))
            {
                ViewBag.QuickPay = qp;
            }
            else
            {
                ViewBag.QuickPay = qp.ToString() + ".00";
            } 
            if (dp.ToString().Contains("."))
            {
                ViewBag.Dispatch = dp;
            }
            else
            {
                ViewBag.Dispatch = dp.ToString() + ".00";
            } 
            if (total.ToString().Contains("."))
            {
                ViewBag.TotalLoadAmt = total;
            }
            else
            {
                ViewBag.TotalLoadAmt = total.ToString() + ".00";
            }
            ViewBag.LoadGross = gross;
            return loads;
        }

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