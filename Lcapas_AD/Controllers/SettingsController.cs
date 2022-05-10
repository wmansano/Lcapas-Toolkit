using Lcapas.Core.Library;
using Lcapas.Core.Logic;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Lcapas.AD.Controllers
{
    public class SettingsController : Controller
    {
        private LcapasLogic lcapasLogic = new LcapasLogic();

        [AuthorizationRequired]
        public ActionResult Index()
        {
            ViewBag.Environment = Functions.GetEnvironment();

            return View();
        }

        [AuthorizationRequired]
        public ActionResult SystemSettings()
        {
            ViewBag.Environment = Functions.GetEnvironment();
            SystemSettingsViewObj _SystemSettings = new SystemSettingsViewObj();

            try
            {
                _SystemSettings.Load();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "SystemSettings - Get Method", "Error: ", ex.ToString());
            }
            return View(_SystemSettings);
        }

        [HttpPost]
        [AuthorizationRequired]
        public ActionResult SystemSettings(SystemSettingsViewObj systemSettings)
        {
            ViewBag.Environment = Functions.GetEnvironment();
            SystemSettingsViewObj _SystemSettings = systemSettings ?? new SystemSettingsViewObj();

            try
            {
                _SystemSettings.Save();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "SystemSettings - Post Method", "Error: ", ex.ToString());
            }
            return View(_SystemSettings);
        }

        [AuthorizationRequired]
        public ActionResult ServerStatus()
        {
            ViewBag.Environment = Functions.GetEnvironment();
            SystemSettingsViewObj _SystemSettings = new SystemSettingsViewObj();

            try
            {
                _SystemSettings.GetServerStatus();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "ServerStatus", "Error: ", ex.ToString());
            }
            return View(_SystemSettings);
        }

        [AuthorizationRequired]
        public ActionResult Permissions()
        {
            ViewBag.Environment = Functions.GetEnvironment();
            PermissionsViewObj _Permissions = new PermissionsViewObj();

            try
            {
                _Permissions.Load();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "Permissions", "Error: ", ex.ToString());
            }
            return View(_Permissions);
        }

        // POST: ControllerType/Create
        [HttpPost]
        [AuthorizationRequired]
        public ActionResult CreatePermissionType(string itemCode, string itemDesc, string type, bool active, string objectId)
        {
            UserResultObj _UserResultModel = new UserResultObj();

            try
            {
                if (Int32.TryParse(type, out int intType))
                {
                    switch (intType)
                    {
                        case (Int32)Enums.PermissionSaveType.ControllerType:
                            _UserResultModel = lcapasLogic.CreateControllerType(itemCode, itemDesc);
                            break;
                        case (Int32)Enums.PermissionSaveType.ActionType:
                            _UserResultModel = lcapasLogic.CreateActionType(itemCode, itemDesc, active, objectId);
                            break;
                        case (Int32)Enums.PermissionSaveType.PermissionRecord:
                            _UserResultModel = lcapasLogic.CreatePermissionRecord(itemCode, itemDesc, active, objectId);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "CreatePermissionType", "Error: ", ex.ToString());
            }

            return Json(_UserResultModel);
        }

        // POST: ApplicationFee/Delete
        [HttpPost]
        [AuthorizationRequired]
        public ActionResult DeletePermissionType(int id, string type)
        {
            UserResultObj _UserResultModel = new UserResultObj();

            try
            {
                if (Int32.TryParse(type, out int intType))
                {
                    switch (intType)
                    {
                        case (Int32)Enums.PermissionSaveType.ControllerType:
                            _UserResultModel = lcapasLogic.DeleteControllerType(id);
                            break;
                        case (Int32)Enums.PermissionSaveType.ActionType:
                            _UserResultModel = lcapasLogic.DeleteActionType(id);
                            break;
                        case (Int32)Enums.PermissionSaveType.PermissionRecord:
                            _UserResultModel = lcapasLogic.DeletePermissionRecord(id);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "ApplicationFeeDelete", "Error: ", ex.ToString());
            }

            return Json(_UserResultModel);
        }

        [AllowAnonymous]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                lcapasLogic.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}