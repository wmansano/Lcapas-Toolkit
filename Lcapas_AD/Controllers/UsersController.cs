using Lcapas.Core.Library;
using Lcapas.Core.Logic;
using Lcapas.Core.Models.Lcappsdb;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lcapas.AD.Controllers
{
    public class UsersController : Controller
    {
        private LcapasLogic lcapasLogic = new LcapasLogic();

        #region Users

        [AuthorizationRequired]
        public ActionResult Index()
        {
            ViewBag.Environment = Functions.GetEnvironment();
            return View();
        }


        // GET: Users
        [AuthorizationRequired]
        public ActionResult User()
        {
            List<User> _Users = new List<User>();
            try
            {
                _Users = lcapasLogic.GetUsers(false);
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "Users", "Error: ", ex.ToString());
            }

             return View("User", _Users);
        }

        // GET: Users Access Groups
        [AuthorizationRequired]
        public ActionResult UserAccessGroup()
        {
            UserAccessViewObj _UserAccess = new UserAccessViewObj();
            try
            {
                ViewBag.Environment = Functions.GetEnvironment();

                _UserAccess.LoadObject();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "UserAccessGroup", "Error: ", ex.ToString());
            }

            //return View(_UserAccess);
            return View(_UserAccess);
        }

        // POST: Set Users Access Groups
        [AuthorizationRequired]
        public ActionResult SetUsersGroups(int? userId, int[] accessGroupIdList)
        {
            UserResultObj _UserResultModel = new UserResultObj() {
                Success = false,
                Message = Structs.Literals.ContactHelpDesk,
            };

            try
            {
                if (userId != null)
                {
                    _UserResultModel.Success = lcapasLogic.SetAccessGroup(userId, accessGroupIdList);
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "SetUsersGroups", "Error: ", ex.ToString());
            }

            return Json(_UserResultModel);
        }

        // POST: Users/Create
        [HttpPost]
        [AuthorizationRequired]
        public ActionResult UserCreate(string snumber,string firstname, string lastname, bool active)
        {
            UserResultObj _UserResultModel = new UserResultObj();

            try
            {
                if (ModelState.IsValid)
                {
                    _UserResultModel = lcapasLogic.CreateUser(snumber, firstname, lastname, active);
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "UserCreate", "Error: ", ex.ToString());
            }
            return Json(_UserResultModel);
        }

        // POST: Activate/Deactivate User
        [HttpPost]
        [AuthorizationRequired]
        public ActionResult UserActivate(int id, bool active)
        {
            bool success = false;
            try
            {
                lcapasLogic.ActivateUser(id, active);
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "UserActivate", "Error: ", ex.ToString());
            }
            return Json(success);
        }

        // POST: Users/Order
        [HttpPost]
        [AuthorizationRequired]
        public ActionResult UserOrder(List<int> itemOrder)
        {
            bool success = false;
            try
            {
                success = lcapasLogic.OrderUser(itemOrder);
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "UserOrder", "Error: ", ex.ToString());
            }
            return Json(success);
        }

        // POST: Users/Delete
        [HttpPost]
        [AuthorizationRequired]
        public ActionResult UserDelete(int id)
        {
            UserResultObj _UserResultModel = new UserResultObj();
            _UserResultModel.Success = false;
            try
            {
                _UserResultModel = lcapasLogic.DeleteUser(id);
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "UserDelete", "Error: ", ex.ToString());
            }
            return Json(_UserResultModel);
        }

        #endregion
    }
}
