﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Memcomb.Models;
using System.Data.Entity.Validation;

namespace Memcomb.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            List<Following> followingList = new List<Following>();
            using (memcombdbEntities db = new memcombdbEntities())
            {
                if (HttpContext.Request.Cookies["userIDCookie"] != null)
                {
                    HttpCookie cookie = HttpContext.Request.Cookies.Get("userIDCookie");
                    var v = db.Users.Where(a => a.Email_ID == cookie.Value).FirstOrDefault();
                    var data = db.Followings.Include(f => f.User);

                   // return View(data.ToList());
                }
            }
                
            User user = new User();
            using (memcombdbEntities dc = new memcombdbEntities())
            {
                if (HttpContext.Request.Cookies["userIDCookie"] != null)
                {
                    var cookie = HttpContext.Request.Cookies.Get("userIDCookie");
                    var v = dc.Users.Where(a => a.Email_ID == cookie.Value);
                    foreach (var u in v)
                    {
                        user.First_Name = u.First_Name;
                        user.Last_Name = u.Last_Name;
                        if (u.Profile_Picture != null)
                        {
                            user.Profile_Picture = u.Profile_Picture;
                        }
                        else
                        {
                            user.Profile_Picture = @"C:\Users\Isaac\Documents\GitHub\Memcomb\Users\Default\Profile_Pic\rename.jpg";
                        }
                        if (u.Background_Pic != null)
                        {
                            user.Background_Pic = u.Background_Pic;
                        }
                        else
                        {
                            user.Background_Pic = @"C:\Users\Isaac\Documents\GitHub\Memcomb\Users\Default\Background_Pic\default.jpg";
                        }
                    }
                }
            }
            //ProfilePageModel profiler = new ProfilePageModel();
            return View(user);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ProfilePicture(User model)
        {
            bool Status = false;
            string message = "";
            if (ModelState.IsValid)
            {
                using (memcombdbEntities dc = new memcombdbEntities())
                {
                    if (HttpContext.Request.Cookies["userIDCookie"] != null)
                    {
                        HttpCookie cookie = HttpContext.Request.Cookies.Get("userIDCookie");
                        var v = dc.Users.Where(a => a.Email_ID == cookie.Value).FirstOrDefault();
                        Directory.CreateDirectory(Server.MapPath("~/Users/User_ID_" + v.User_ID + "/Profile_Pic"));
                        HttpPostedFileBase file = model.Profile_Picture_imgPath;
                        if (file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var path = Path.Combine(Server.MapPath("~/Users/User_ID_" + v.User_ID + "/Profile_Pic"), fileName);
                            file.SaveAs(path);
                            /*model.User_ID = v.User_ID;
                            model.First_Name = v.First_Name;
                            model.Last_Name = v.Last_Name;
                            model.Email_ID = v.Email_ID;
                            model.Password = v.Password;*/
                            v.Profile_Picture = path;
                            //model.Background_Pic = v.Background_Pic;*/
                      
                        }
                        dc.Users.Include(v.Profile_Picture);
                       // try
                        //{
                            dc.SaveChanges();
                        //}
                        /*catch (DbEntityValidationException ex)
                        {
                            // Retrieve the error messages as a list of strings.
                            var errorMessages = ex.EntityValidationErrors
                                    .SelectMany(x => x.ValidationErrors)
                                    .Select(x => x.ErrorMessage);

                            // Join the list to a single string.
                            var fullErrorMessage = string.Join("; ", errorMessages);

                            // Combine the original exception message with the new one.
                            var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                            // Throw a new DbEntityValidationException with the improved exception message.
                            throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
                        }*/
                        Status = true;
                    }
                }
            }
            else
            {
                message = "Invalid request";
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult BackgroundPicture(User model)
        {
            bool Status = false;
            string message = "";
            if (ModelState.IsValid)
            {
                using (memcombdbEntities dc = new memcombdbEntities())
                {
                    if (HttpContext.Request.Cookies["userIDCookie"] != null)
                    {
                        HttpCookie cookie = HttpContext.Request.Cookies.Get("userIDCookie");
                        var v = dc.Users.Where(a => a.Email_ID == cookie.Value).FirstOrDefault();
                        Directory.CreateDirectory(Server.MapPath("~/Users/User_ID_" + v.User_ID + "/Background_Pic"));
                        HttpPostedFileBase file = model.Background_Photo;
                        if (file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var path = Path.Combine(Server.MapPath("~/Users/User_ID_" + v.User_ID + "/Background_Pic"), fileName);
                            file.SaveAs(path);
                            v.Background_Pic = path;
                        }
                        dc.Users.Include(v.Background_Pic);
                        try
                        {
                            dc.SaveChanges();
                        }
                        catch (DbEntityValidationException ex)
                        {
                            // Retrieve the error messages as a list of strings.
                            var errorMessages = ex.EntityValidationErrors
                                    .SelectMany(x => x.ValidationErrors)
                                    .Select(x => x.ErrorMessage);

                            // Join the list to a single string.
                            var fullErrorMessage = string.Join("; ", errorMessages);

                            // Combine the original exception message with the new one.
                            var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                            // Throw a new DbEntityValidationException with the improved exception message.
                            throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
                        }
                        Status = true;
                    }
                }
            }
            else
            {
                message = "Invalid request";
            }
            return RedirectToAction("Index");
        }

        // GET: Followings/Email_ID
        public ActionResult Followings_modal(String id)
        {
            using (memcombdbEntities db = new memcombdbEntities())
            {
                if (HttpContext.Request.Cookies["userIDCookie"] != null)
                {
                    HttpCookie cookie = HttpContext.Request.Cookies.Get("userIDCookie");
                    var v = db.Users.Where(a => a.Email_ID == cookie.Value).FirstOrDefault();
                    //id = v.Email_ID;

                    var data = db.Followings.Include(f => f.User).Where(f => f.User.Email_ID == id);

                   // var followed_user = db.Followings.Where(a => a.User_Followed == v.User_ID).FirstOrDefault();

                   // var get_UserID = db.Followings.Where(a => a.User_Followed == v.User_ID).FirstOrDefault();


                    var first_name = "default f_name";

                    var last_name = "default l_name";

                    /*var new_following = new Following()
                    {
                        User_Following = get_UserID.User_Following,
                        User_Followed = get_UserID.User_Followed,
                        User_Followed_First_Name = first_name,
                        User_Followed_Last_Name = last_name
                    };

                    var user_following_join = db.Users.Join(db.Followings,
                        x => x.User_ID,
                        y => y.User_Followed,
                        (x, y) => new {
                            User_First_Name = x.First_Name,
                            User_Last_Name = x.Last_Name
                        }); */

                    return View(data);
                }
                else
                {
                    var followings = db.Followings.Include(f => f.User);

                    return View(followings.ToList());
                }
            }
            
            //return View();
        }

        // GET: Profile/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Profile/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Profile/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Profile/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Profile/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Profile/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
