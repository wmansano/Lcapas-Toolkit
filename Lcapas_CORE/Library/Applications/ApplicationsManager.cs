using Lcapas.Core.distsvc;
using Lcapas.Core.Library;
using Lcapas.Core.Models.Lcappsdb;
using System;
using System.Threading;
using System.Web;

namespace Lcapas.Core.Logic
{
    public class ApplicationsManager : IDisposable
    {
        #region private properties
        // private properties
        private bool _Ready = false;
        private bool _Valid = true;
        private bool _Debugging = false;
        private bool _Production = false;
        private bool _Authenticated = false;
        private bool _Administrator = false;
        private bool _FaultyConfig = false;
        private bool _Received;
        private bool _Parsed;
        private bool _Submitted;
        private bool _Paid;
        private bool _Returned;
        private bool _Cancelled;
        private bool _Error;
        private string _AppRootPath = string.Empty;
        private string _ApasRootPath = string.Empty;
        private string _ApasReturnPath = string.Empty;
        private string _ApasLogoutPath = string.Empty;
        private string _ApasLoginPath = string.Empty;
        private string _ApasWriteGifPath = string.Empty;
        private string _ApplicationID = string.Empty;
        private string _ApasDataSetXML = string.Empty;
        private string _SessionId = string.Empty;
        private string _SecurityToken = string.Empty;
        private string _LocalApasCode = string.Empty;
        private string _UserName = string.Empty;
        private string _Environment = string.Empty;

        private string _Uuid = string.Empty;
        private string _TestUuid = string.Empty;

        private DateTime _StartTime = DateTime.Now;
        private string _LoadMsg = string.Empty;

        //LcapasLogic lcapasLogic = new LcapasLogic();
        //ApasLogic apasLogic = new ApasLogic();

        //private ApplicationMessage _ApplicationMessage;

        #endregion

        private void SetInitialVariables(string UserName, string SessionId, string SecurityToken)
        {
            _UserName = Functions.GetCurrentUser().ToString();

            if (!string.IsNullOrWhiteSpace(UserName)) { _UserName = Username; };
            if (!string.IsNullOrWhiteSpace(SessionId)) { _SessionId = SessionId; };
            if (!string.IsNullOrWhiteSpace(SecurityToken)) { _SecurityToken = SecurityToken; };

            _Debugging = HttpContext.Current.Request.Url.Host.Contains(Structs.Environment.Localhost);

            _TestUuid = "2d3f7ed1-1b49-45cd-b337-e8df6151d8dc";
        }

        #region constructors

        /// <summary>
        /// Loading Constructor
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="sessionId"></param>
        /// <param name="securityToken"></param>
        public ApplicationsManager(string Uuid, string UserName = null, string SessionId = null, string SecurityToken = null)
        {
            try
            {
                SetInitialVariables(UserName, SessionId, SecurityToken);

                this.Load(Uuid);
            }
            catch (Exception ex)
            {
                _Error = true;
                using (LcapasLogic lcapasLogic = new LcapasLogic()) {
                    lcapasLogic.SaveException(Structs.Project.LcapasUI, Structs.Class.ApplicationsManager, "ApplicationsManager", "Error", ex.ToString());
                }
            }
        }

        /// <summary>
        /// Loading Constructor
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="sessionId"></param>
        /// <param name="securityToken"></param>
        //public ApplicationsManager(string Uuid)
        //{
        //    try
        //    {
        //        SetInitialVariables();

        //        this.Load(Uuid);
        //    }
        //    catch (Exception ex)
        //    {
        //        _Error = true;
        //        //lcapasLogic.SaveException(Structs.Project.LcapasUI,Structs.Class.ApplicationsManager, "ApplicationsManager", "Error", ex.ToString());
        //    }
        //}
        #endregion

        #region private methods
        /// <summary>
        /// Loading Constructor
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="sessionId"></param>
        /// <param name="securityToken"></param>
        public void Load(string Uuid)
        {
            if (!string.IsNullOrWhiteSpace(Uuid)) _Uuid = Uuid;

            try
            {
                // 2. check if developing locally
                SetDebugging();
                //_LoadMsg += ", SetDebugging: " + DateTime.Now.Subtract(_StartTime).TotalSeconds;

                // 1. Load Session
                LoadSession();
                //_LoadMsg += ", LoadSession: " + DateTime.Now.Subtract(_StartTime).TotalSeconds;

                // 3. set/replace private variables
                CheckValidSession();
                //_LoadMsg += ", CheckValidSession: " + DateTime.Now.Subtract(_StartTime).TotalSeconds;

                // 4. refresh tokens from apas
                RefreshToken();
                //_LoadMsg += ", RefreshToken: " + DateTime.Now.Subtract(_StartTime).TotalSeconds;

                // 5. get apas user via web service
                GetApasUser();
                //_LoadMsg += ", GetApasUser: " + DateTime.Now.Subtract(_StartTime).TotalSeconds;

                // 6. set navigation paths
                SetNavigationPaths();
                //_LoadMsg += ", SetNavigationPaths: " + DateTime.Now.Subtract(_StartTime).TotalSeconds;

                // 7. retrieve application from apas
                RetrieveApplication();
                //_LoadMsg += ", RetrieveApplication: " + DateTime.Now.Subtract(_StartTime).TotalSeconds;

                // 8. parse/import retrieved application
                ParseApplication();
                //_LoadMsg += ", ParseApplication: " + DateTime.Now.Subtract(_StartTime).TotalSeconds;

                // 9. save values to process status table
                SaveSession();
                //_LoadMsg += ", SaveSession: " + DateTime.Now.Subtract(_StartTime).TotalSeconds;
                
                // 10. Record load time
                // lcapasLogic.SaveException("LcapasUI", "SessionManager", "Load Action", "Load Time", "Total: " + DateTime.Now.Subtract(_StartTime).TotalSeconds + _LoadMsg);
            }
            catch (Exception ex)
            {
                _Error = true; 
                using (LcapasLogic lcapasLogic = new LcapasLogic()) {
                    lcapasLogic.SaveException(Structs.Project.LcapasUI, Structs.Class.ApplicationsManager, "Loading", "Error", ex.ToString());
                }
            }
        }
        private bool SetDebugging()
        {
            bool success = false;
            try
            {
                if (_Debugging)
                {
                    if (!string.IsNullOrWhiteSpace(_TestUuid))
                    {
                        using (LcapasLogic lcapasLogic = new LcapasLogic())
                        {
                            _Uuid = lcapasLogic.ValidateTestingUUID(_TestUuid);
                        }
                    }

                    _Authenticated = true;
                }
                success = true;
            }
            catch (Exception ex)
            {
                _Error = true;
                using (LcapasLogic lcapasLogic = new LcapasLogic()) {
                    lcapasLogic.SaveException(Structs.Project.LcapasUI, Structs.Class.ApplicationsManager, "SetEnvironment", "Error", ex.ToString());
                }
            }
            return success;
        }

        private bool LoadSession()
        {
            bool success = false;
            try
            {
                if (!string.IsNullOrWhiteSpace(_Uuid))
                {
                    ApplicationMessage _ApplicationMessage = new ApplicationMessage();
                    using (LcapasLogic lcapasLogic = new LcapasLogic())
                    {
                        _ApplicationMessage = lcapasLogic.GetApplicationMessageByUuid(_Uuid);
                        _LocalApasCode = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.LocalAPASCode);
                    }

                    if (_ApplicationMessage != null)
                    {
                        if (string.IsNullOrWhiteSpace(_SessionId))
                        {
                            _SessionId = _ApplicationMessage.SessionId;
                        }

                        if (string.IsNullOrWhiteSpace(_SecurityToken))
                        {
                            _SecurityToken = _ApplicationMessage.SecurityToken;
                        }

                        if (!_Debugging)
                        {
                            _Debugging = _ApplicationMessage.Debugging;
                        }

                        _Administrator = _ApplicationMessage.Administrator;
                        _Received = _ApplicationMessage.ReceivedDateTime != null;
                        _Parsed = _ApplicationMessage.ParsedDateTime != null;
                        _Submitted = _ApplicationMessage.SubmittedDateTime != null;
                        _Paid = _ApplicationMessage.PaidDateTime != null;
                        _Returned = _ApplicationMessage.ReturnedDateTime != null;
                        _Cancelled = _ApplicationMessage.CancelledDateTime != null;
                        _ApplicationID = _ApplicationMessage.ApplicationID;

                        _Environment = Functions.GetEnvironment();

                        if (_Environment == Structs.Environment.Prod) { _Production = true; }

                        if (string.IsNullOrWhiteSpace(_Environment)) { _FaultyConfig = true; }

                        if (!Functions.IsUserNameEmpty(_ApplicationMessage.Username) && !_Debugging) _UserName = _ApplicationMessage.Username;

                        success = true;
                    }
                    else {
                        // lcapasLogic.SaveException(Structs.Project.LcapasUI, Structs.Class.ApplicationsManager, "LoadSession", "New Application", "Session ID: " + _SessionId + " - Security Token: " + _SecurityToken + ", Username: " + _UserName + ", UUID: " + _Uuid);
                    }
                }
            }
            catch (Exception ex)
            {
                _Error = true;
                using (LcapasLogic lcapasLogic = new LcapasLogic()) {
                    lcapasLogic.SaveException(Structs.Project.LcapasUI, Structs.Class.ApplicationsManager, "loadSession", "Error", ex.ToString());
                }
            }
            return success;
        }

        private bool CheckValidSession()
        {
            bool success = false;
            try
            {
                if (!_Debugging)
                {
                    if (string.IsNullOrWhiteSpace(_Uuid) || string.IsNullOrWhiteSpace(_SessionId) || string.IsNullOrWhiteSpace(_SecurityToken))
                    {
                        _Valid = false;
                        using (LcapasLogic lcapasLogic = new LcapasLogic()) {
                            lcapasLogic.SaveException(Structs.Project.LcapasUI, Structs.Class.ApplicationsManager, "CheckValidSession", "Unsuccessful", "SessionID: " + _SessionId + ", SecurityToken: " + _SecurityToken + ", UUID: " + _Uuid);
                        }
                    }
                    //else {
                    //    lcapasLogic.SaveException(Structs.Project.LcapasUI, Structs.Class.ApplicationsManager, "CheckValidSession", "Successful", "SessionID: " + _SessionId + ", SecurityToken: " + _SecurityToken + ", UUID: " + _Uuid);
                    //}
                }
                success = true;
            }
            catch (Exception ex)
            {
                _Error = true;
                using (LcapasLogic lcapasLogic = new LcapasLogic()) {
                    lcapasLogic.SaveException(Structs.Project.LcapasUI, Structs.Class.ApplicationsManager, "CheckValidSession", "Error", ex.ToString());
                }
            }
            return success;
        }

        private bool RefreshToken()
        {
            bool success = false;
            string tempToken = string.Empty;
            int attempts = 0;
            try
            {
                if (!_Debugging && _Valid)
                {

                    while (string.IsNullOrWhiteSpace(tempToken) && attempts <= 2)
                    {
                        //lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ApplicationsManager, "ApasLogic.RefreshToken", "Pre-Refresh Token", "SessionId: " + _SessionId + ", SecurityToken: " + _SecurityToken, false);
                        using (ApasLogic apasLogic = new ApasLogic()) {
                            tempToken = apasLogic.RefreshApasToken(_SessionId, _SecurityToken);
                        }

                        //lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ApplicationsManager, "ApasLogic.RefreshToken", "Post-Refresh Token", "SessionId: " + _SessionId + ", SecurityToken: " + _SecurityToken, false);

                        if (string.IsNullOrWhiteSpace(tempToken))
                        {
                            Thread.Sleep(TimeSpan.FromMilliseconds(500));
                            attempts++;
                        }
                        else
                        {
                            _SecurityToken = tempToken;
                            _Authenticated = true;
                            success = true;
                        }
                    }

                    if (string.IsNullOrWhiteSpace(tempToken))
                    {
                    // lcapasLogic.SaveException(Structs.Project.LcapasUI, Structs.Class.ApplicationsManager, "RefreshToken", "Unsuccessful", "SessionID: " + _SessionId + ", SecurityToken: " + _SecurityToken + ", NEW TOKEN: " + tempToken + "UUID: " + _Uuid + ", Attempts: " + attempts.ToString());
                        using (ApasLogic apasLogic = new ApasLogic()) {
                            apasLogic.GetApasReceivedErrors();
                        }
                        
                        _Ready = false;
                    }
                }
            }
            catch (Exception ex)
            {
                _Error = true;
                using (LcapasLogic lcapasLogic = new LcapasLogic()) {
                    lcapasLogic.SaveException(Structs.Project.LcapasUI, Structs.Class.ApplicationsManager, "RefreshToken", "Error", "UUID: " + _Uuid + ", TOKEN: " + tempToken + ", Attempts: " + attempts.ToString() + ", Error: " + ex.ToString());
                }
            }
            return success;
        }

        private bool GetApasUser()
        {
            bool success = false;
            try
            {
                if (!_Debugging && _Valid && Functions.IsUserNameEmpty(_UserName))
                {
                    UserInfo _UserInfo = null;
                    using (ApasLogic apasLogic = new ApasLogic()) {
                        _UserInfo = apasLogic.GetApasUserInfo(_SessionId, _SecurityToken);
                    }

                    if (_UserInfo != null) {
                        _Administrator = _UserInfo.IsAdministrationModuleUser;
                        _UserName = _UserInfo.UserName;
                        success = true;
                        //using (LcapasLogic lcapasLogic = new LcapasLogic())
                        //{
                        //    lcapasLogic.SaveException(Structs.Project.LcapasUI, Structs.Class.ApplicationsManager, "GetApasUser", "Successful", "UserName: " + _UserName + ", SessionID: " + _SessionId + ", SecurityToken: " + _SecurityToken + ", UUID: " + _Uuid);
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                _Error = true;
                using (LcapasLogic lcapasLogic = new LcapasLogic()) {
                    lcapasLogic.SaveException(Structs.Project.LcapasUI, Structs.Class.ApplicationsManager, "GetApasUser", "Error", ex.ToString());
                }
            }
            return success;
        }

        private bool SetNavigationPaths()
        {
            bool success = false;
            try
            {
                if (_Valid)
                {
                    SetAppRootPath();
                    SetApasRootPath();
                    SetApasLoginPath();
                    SetApasLogoutPath();
                    SetApasReturnPath();
                    SetApasWriteGifPath();
                }
                success = true;
            }
            catch (Exception ex)
            {
                _Error = true;
                using (LcapasLogic lcapasLogic = new LcapasLogic()) {
                    lcapasLogic.SaveException(Structs.Project.LcapasUI, Structs.Class.ApplicationsManager, "SetNavigationPaths", "Error", ex.ToString());
                }
            }
            return success;
        }

        private bool RetrieveApplication()
        {
            bool success = false;
            try
            {
                if (_Valid && _Authenticated)
                {
                    bool _MessageExists = false;
                    using (LcapasLogic lcapasLogic = new LcapasLogic()) {
                        _MessageExists = lcapasLogic.ApasMessageExists(_Uuid, Enums.MessageTypes.Application);
                    }

                    if (_MessageExists) { _Received = true; }

                    if (!_Received) {

                        using (ApasLogic apasLogic = new ApasLogic()) {
                            _Received = apasLogic.GetApasReceivedApplicationByUUID(_Uuid);
                        }
                        
                        //if (_Received)
                        //{
                        //    lcapasLogic.SaveException(Structs.Project.LcapasUI, Structs.Class.ApplicationsManager, "RetrieveApplication", "Successful", "SessionID: " + _SessionId + ", SecurityToken: " + _SecurityToken + ", UUID: " + _Uuid);
                        //}
                    }
                }
                success = true;
            }
            catch (Exception ex)
            {
                _Error = true;
                using (LcapasLogic lcapasLogic = new LcapasLogic()) {
                    lcapasLogic.SaveException(Structs.Project.LcapasUI, Structs.Class.ApplicationsManager, "RetrieveApplication", "Error", ex.ToString());
                }
            }
            return success;
        }

        private bool ParseApplication()
        {
            bool success = false;
            try
            {
                if (_Valid && _Authenticated && _Received)
                {
                    using (LcapasLogic lcapasLogic = new LcapasLogic()) {
                        _Parsed = lcapasLogic.ParseApplicationMessage(_Uuid);
                        if (_Parsed)
                        {
                            _Ready = true;
                            //lcapasLogic.SaveException(Structs.Project.LcapasUI, Structs.Class.ApplicationsManager, "ParseApplication", "Successful", "SessionID: " + _SessionId + ", SecurityToken: " + _SecurityToken + ", UUID: " + _Uuid);
                        }
                        else
                        {
                            _Error = true;
                            lcapasLogic.SaveException(Structs.Project.LcapasUI, Structs.Class.ApplicationsManager, "ParseApplication", "Unsuccessful", "SessionID: " + _SessionId + ", SecurityToken: " + _SecurityToken + ", UUID: " + _Uuid);
                        }
                    }
                }

                success = true;
            }
            catch (Exception ex)
            {
                _Error = true;
                using (LcapasLogic lcapasLogic = new LcapasLogic()) {
                    lcapasLogic.SaveException(Structs.Project.LcapasUI, Structs.Class.ApplicationsManager, "ParseApplication", "Error", ex.ToString());
                }
            }
            return success;
        }

        private bool SaveSession()
        {
            bool success = false;

            try
            {
                ApplicationMessage _ApplicationMessage = new ApplicationMessage();

                using (LcapasLogic lcapasLogic = new LcapasLogic()) {
                    _ApplicationMessage = lcapasLogic.GetApplicationMessageByUuid(_Uuid);
                }
                
                if (!string.IsNullOrWhiteSpace(_Uuid)) { _ApplicationMessage.UUID = _Uuid; };
                if (!string.IsNullOrWhiteSpace(_SessionId)) { _ApplicationMessage.SessionId = _SessionId; };
                if (!string.IsNullOrWhiteSpace(_SecurityToken)) { _ApplicationMessage.SecurityToken = _SecurityToken; };

                if (_Valid) { _ApplicationMessage.Valid = _Valid; }

                if (_Production) { _ApplicationMessage.Production = _Production; }
                if (_Administrator) { _ApplicationMessage.Administrator = _Administrator; }
                if (_Debugging) { _ApplicationMessage.Debugging = _Debugging; }

                if (_Parsed && _ApplicationMessage.ParsedDateTime == null) { _ApplicationMessage.ParsedDateTime = DateTime.Now; }
                if (_Received && _ApplicationMessage.ReceivedDateTime == null) { _ApplicationMessage.ReceivedDateTime = DateTime.Now; }
                if (_Submitted && _ApplicationMessage.SubmittedDateTime == null) { _ApplicationMessage.SubmittedDateTime = DateTime.Now; }
                if (_Paid && _ApplicationMessage.PaidDateTime == null) { _ApplicationMessage.PaidDateTime = DateTime.Now; }
                if (_Cancelled && _ApplicationMessage.CancelledDateTime == null) { _ApplicationMessage.CancelledDateTime = DateTime.Now; }
                if (_Returned && _ApplicationMessage.ReturnedDateTime == null) { _ApplicationMessage.ReturnedDateTime = DateTime.Now; }

                // If line below is removed, it doesn't save Session ID or Securety Token ?????
                using (LcapasLogic lcapasLogic = new LcapasLogic()) {
                    lcapasLogic.SaveException(Structs.Project.LcapasUI, Structs.Class.ApplicationsManager, "SaveSession", "Session Variables", "SessionID: " + _SessionId + ", SecurityToken: " + _SecurityToken + ", UUID: " + _Uuid, false);
                }

                if (Functions.IsUserNameEmpty(_ApplicationMessage.Username))
                {
                    if (!string.IsNullOrWhiteSpace(_UserName)) { _ApplicationMessage.Username = _UserName; } else { _ApplicationMessage.Username = "Unknown"; }
                }

                if (string.IsNullOrWhiteSpace(_ApplicationMessage.CreatedBy))
                {
                    _ApplicationMessage.CreatedBy = Functions.GetCurrentUser().ToString();
                    _ApplicationMessage.CreatedDateTime = DateTime.Now;
                }

                _ApplicationMessage.ModifiedBy = Functions.GetCurrentUser().ToString();
                _ApplicationMessage.ModifiedDateTime = DateTime.Now;

                using (LcapasLogic lcapasLogic = new LcapasLogic()) {
                    success = lcapasLogic.UpdateApplication(_ApplicationMessage);
                }
            }
            catch (Exception ex)
            {
                _Error = true;
                using (LcapasLogic lcapasLogic = new LcapasLogic()) {
                    lcapasLogic.SaveException(Structs.Project.LcapasUI, Structs.Class.ApplicationsManager, "SaveSession", "Error", ex.ToString());
                }
            }
            return success;
        }

        #endregion

        #region setpaths

        //private string GetAppRootPath()
        //{
        //    string path = string.Empty;
        //    try
        //    {
        //        path =  HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath;
        //    }
        //    catch (Exception ex)
        //    {
        //        _Error = true; lcapasLogic.SaveException(Structs.Project.LcapasUI,Structs.Class.ApplicationsManager, "getAppRootPath", "Error", ex.ToString());
        //    }
        //    return path;
        //}

        private void SetAppRootPath()
        {
            try
            {
                _AppRootPath = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath;
            }
            catch (Exception ex)
            {
                _Error = true;
                using (LcapasLogic lcapasLogic = new LcapasLogic()) {
                    lcapasLogic.SaveException(Structs.Project.LcapasUI, Structs.Class.ApplicationsManager, "setAppRootPath", "Error", ex.ToString());
                }
            }
        }

        private void SetApasRootPath()
        {
            try
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic()) {
                    if (_Production)
                    {
                        _ApasRootPath = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.ApasProdPath);
                    }
                    else
                    {
                        _ApasRootPath = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.ApasSuppPath);             
                    }
                }
            }
            catch (Exception ex)
            {
                _Error = true;
                using (LcapasLogic lcapasLogic = new LcapasLogic()) {
                    lcapasLogic.SaveException(Structs.Project.LcapasUI, Structs.Class.ApplicationsManager, "setApasRootPath", "Error", ex.ToString());
                }
            }
        }

        private void SetApasWriteGifPath()
        {
            try
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic()) {
                    if (_Valid)
                    {
                        if (_Administrator)
                        {
                            _ApasWriteGifPath = string.Format(_ApasRootPath + lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.AdminWriteGifPath), _SessionId, _SecurityToken, _LocalApasCode);
                        }
                        else
                        {
                            _ApasWriteGifPath = string.Format(_ApasRootPath + lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.ApplicantWriteGifPath), _SessionId, _SecurityToken, _LocalApasCode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _Error = true;
                using (LcapasLogic lcapasLogic = new LcapasLogic()) {
                    lcapasLogic.SaveException(Structs.Project.LcapasUI, Structs.Class.ApplicationsManager, "setApasWriteGifPath", "Error", ex.ToString());
                }
            }
        }

        private void SetApasLoginPath()
        {
            try
            {
                if (_Valid)
                {
                    using (LcapasLogic lcapasLogic = new LcapasLogic()) {
                        if (_Administrator)
                        {
                            _ApasLoginPath = string.Format(_ApasRootPath + lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.ApasRestoreAdminLoginPath), _SessionId, _SecurityToken, _LocalApasCode);
                        }
                        else
                        {
                            _ApasLoginPath = string.Format(_ApasRootPath + lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.ApasRestoreLoginPath), _SessionId, _SecurityToken, _LocalApasCode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _Error = true;
                using (LcapasLogic lcapasLogic = new LcapasLogic()) {
                    lcapasLogic.SaveException(Structs.Project.LcapasUI, Structs.Class.ApplicationsManager, "setApasLoginPath", "Error", ex.ToString());
                }
            }

        }

        private void SetApasReturnPath()
        {
            try
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic()) {
                    _ApasReturnPath = string.Format(_ApasRootPath + lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.ApasRedirectPath), _SessionId, _SecurityToken, _LocalApasCode);
                }
            }
            catch (Exception ex)
            {
                _Error = true;
                using (LcapasLogic lcapasLogic = new LcapasLogic()) {
                    lcapasLogic.SaveException(Structs.Project.LcapasUI, Structs.Class.ApplicationsManager, "setApasReturnPath", "Error", ex.ToString());
                }
            }
        }

        private void SetApasLogoutPath()
        {
            try
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic()) {
                    _ApasLogoutPath = string.Format(_ApasRootPath + lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.ApasLogoutPath), _SessionId, _SecurityToken, _LocalApasCode);
                }
            }
            catch (Exception ex)
            {
                _Error = true;
                using (LcapasLogic lcapasLogic = new LcapasLogic()) {
                    lcapasLogic.SaveException(Structs.Project.LcapasUI, Structs.Class.ApplicationsManager, "setApasLogoutPath", "Error", ex.ToString());
                }
            }
        }

        #endregion

        #region public readonly properties

        public bool Debugging {
            get { return _Debugging; }
            set { _Debugging = value; }
        }

        public string SessionId {
            get { return _SessionId; }
            set { _SessionId = value; }
        }

        public string SecurityToken {
            get { return _SecurityToken; }
            set { _SecurityToken = value; }
        }

        public bool Authenticated {
            get { return _Authenticated; }
        }

        public string UUID {
            get { return _Uuid; }
            set { _Uuid = value; }
        }

        public string TestUuid {
            get { return _TestUuid; }
            set { _TestUuid = value; }
        }

        public string AppRootPath {
            get { return _AppRootPath; }
        }

        public string ApasReturnPath {
            get { return _ApasReturnPath; }
        }

        public string ApasLogoutPath {
            get { return _ApasLogoutPath; }
        }

        public string ApasLoginPath {
            get { return _ApasLoginPath; }
        }

        public string ApasWriteGifPath {
            get { return _ApasWriteGifPath; }
        }

        public bool Ready {
            get { return _Ready; }
        }

        public bool Paid {
            get { return _Paid; }
            set {
                _Paid = value;
                if (_Paid) {
                    SaveSession();
                }
            }
        }

        public bool Submitted {
            get { return _Submitted; }
            set {
                _Submitted = value;
                if (_Submitted) {
                    SaveSession();
                }
            }
        }

        public bool Returned {
            get { return _Returned; }
            set {
                _Returned = value;
                if (_Returned)
                {
                    SaveSession();
                }
            }
        }

        public bool Cancelled {
            get { return _Cancelled; }
            set {
                _Cancelled = value;
                if (_Cancelled)
                {
                    SaveSession();
                }
            }
        }

        public string ApplicationID {
            get { return _ApplicationID; }
        }

        public string ApasDataSetXML {
            get { return _ApasDataSetXML; }
        }

        public string LocalApasCode {
            get { return _LocalApasCode; }
            set { _LocalApasCode = value; }
        } 

        public bool Production {
            get { return _Production; }
            set { _Production = value; }
        }

        public bool Error {
            get { return _Error; }
            set { _Error = value; }
        }
        public bool FaultyConfig {
            get { return _FaultyConfig; }
            set { _FaultyConfig = value; }
        }

        public string Username {
            get { return _UserName; }
        }
        public string Environment {
            get { return _Environment; }
        }
        #endregion
        public void Dispose()
        {
            //ctx.Dispose();
        }
    }
}