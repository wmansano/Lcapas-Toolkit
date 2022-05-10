using Lcapas.Core.Logic;
using Lcapas.Core.Library;
using Lcapas.Core.Models.Lcappsdb;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Collections.Specialized;

namespace Lcapas.UI.Controllers
{
    public class PaymentController : Controller
    {
        //private LcapasLogic lcapasLogic = new LcapasLogic();

        [HttpPost]
        public ActionResult MakePayment(string uuid)
        {
            bool success = false;
            bool prevpaid = false;
            string token = string.Empty;
            string tokenId = string.Empty;
            string payPalPath = string.Empty;
            string _exMsg = string.Empty;
            NameValueCollection paypalParameters = new NameValueCollection();

            try
            {
                if (uuid != null)
                {
                    using (ApplicationsManager _ApplicationsManager = new ApplicationsManager(uuid))
                    {
                        if (_ApplicationsManager.Debugging || _ApplicationsManager.Ready)
                        {
                            using (LcapasLogic lcapasLogic = new LcapasLogic())
                            {
                                prevpaid = lcapasLogic.ConfirmPaymentComplete(uuid);

                                if (prevpaid)
                                {
                                    // if payment was made to this ApplicationID (any UUID for this ApplicationID) Skip PayPal and redirect to Confirm Payment
                                    return ConfirmPayment(uuid, true);
                                } 
                                else
                                {
                                    NVPAPICaller caller = new NVPAPICaller();
                                    //caller.SeedPalSettings();
                                    caller.LoadPayPalSettings(_ApplicationsManager.ApplicationID);

                                    string serverURL = HttpContext.Request.Url.GetLeftPart(UriPartial.Authority) + System.Web.VirtualPathUtility.ToAbsolute("~/");
                                    success = caller.SetExpressCheckout(serverURL, ref token, ref tokenId, ref payPalPath, ref paypalParameters, _ApplicationsManager.Username, uuid, _ApplicationsManager.ApplicationID);

                                    if (success)
                                    {
                                        PayPalResponse response = new PayPalResponse()
                                        {
                                            UUID = uuid,
                                            SECURETOKEN = token
                                        };

                                        lcapasLogic.CreatePayPalResponse(response);

                                        //return Redirect(payPalPath);  // Method = GET
                                        return Content(caller.SubmitForm(payPalPath, paypalParameters, "POST"));  // Method = POST
                                    }
                                }
                            }
                        }
                        else
                        {
                            _exMsg = ", Error: " + Structs.ErrorMessages.SessionManagerNotReady;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _exMsg = ", Error: " + ex.ToString();
            }

            // Capture Error
            if (!string.IsNullOrWhiteSpace(_exMsg))
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasUI, Structs.Class.PaymentController, "MakePayment", "Payment Error (UUID): " + uuid, _exMsg);
                }
            }

            return RedirectToAction("Error", "Error");
        }

        /// <summary>
        /// This function can be hit from MakePayment in the case that payment has previously been made
        /// Alternatively, this function is the return URL from PayPal
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="prevPaid"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ConfirmPayment(string uuid = null, bool prevPaid = false)
        {
            string _exMsg = string.Empty;
            bool _PayPalDuplicate = false;
            string _PaymentResult = Structs.EmailType.ApplicationPaymentError;
            string _UUID = string.Empty;
            string _SecurityToken = string.Empty;
            SendEmail _Email = new SendEmail();

            try
            {
                // If a uuid is passed in, it means payment has already been made
                // we will doublecheck the database to make sure (to prevent hacking)
                //bool _PreviouslyPaid = !string.IsNullOrWhiteSpace(uuid); //&& lcapasLogic.ConfirmPaymentComplete(uuid);

                // If previously paid, we will set the main _UUID to the uuid passed in
                if (uuid != null)
                {
                    _UUID = uuid;
                }
                //else
                {
                    // Otherwise we will attempt to get it from the SECURETOKEN value returned by PayPal
                    // get secure token
                    _SecurityToken = Request[Structs.Literals.SecureToken];

                    // Check for duplicate paypal payment
                    _PayPalDuplicate = Request[Structs.Literals.Duplicate] == "2";

                    // Update the paypal response and return the UUID
                    // if the response is not null
                    // and has not already been returned
                    if (!string.IsNullOrWhiteSpace(_SecurityToken) && !string.IsNullOrWhiteSpace(Request["PNREF"]))
                    {
                        using (LcapasLogic lcapasLogic = new LcapasLogic())
                        {
                            _UUID = lcapasLogic.UpdatePayPalResponse(Request);
                        }
                    }
                }

                // Make sure we finally have a valid _UUID
                if (!string.IsNullOrWhiteSpace(_UUID))
                {
                    using (ApplicationsManager _ApplicationsManager = new ApplicationsManager(_UUID))
                    {
                        // Double check approved payment in the database to prevent hacking
                        bool alreadyPaid = false;
                        using (LcapasLogic lcapasLogic = new LcapasLogic())
                        {
                            alreadyPaid = lcapasLogic.ConfirmPaymentComplete(uuid);

                            if (!alreadyPaid)
                            {
                                lcapasLogic.SaveException(Structs.Project.LcapasUI, Structs.Class.PaymentController, "ConfirmPayment", "Redirect to the Make Payment", "UUID: " + uuid + " - Already Paid: " + alreadyPaid);
                                
                                // If it's not already paid, redirect to the Make Payment page
                                return MakePayment(uuid);
                            }
                        }

                        // if not, redirect back to ApplyAlberta - there is nothing more we can do
                        //if (_ApplicationsManager != null && !string.IsNullOrWhiteSpace(_ApplicationsManager.UUID)) //  && !_PreviouslyPaid && !_PreviouslyReturned
                        if (_ApplicationsManager.Debugging || _ApplicationsManager.Ready)
                        {
                            // if we make it to this point, payment has been made so mark it as paid (just to be sure)
                            if (!prevPaid || _PayPalDuplicate)
                            {
                                if (!_ApplicationsManager.Paid)
                                {
                                    _ApplicationsManager.Paid = true;
                                }
                            }

                            // return the application if it has been paid and hasn't been returned
                            if (!_ApplicationsManager.Returned)
                            {
                                // send application and mark as returned
                                using (ApasLogic apasLogic = new ApasLogic())
                                {
                                    _ApplicationsManager.Returned = apasLogic.SendApasApplication(_UUID);
                                }
                            }

                            // Check if Application Fee is $0.00, to avoid Double Payment Error
                            bool freeApplicationFee = false;
                            if (prevPaid)
                            {
                                using (LcapasLogic lcapasLogic = new LcapasLogic())
                                {
                                    freeApplicationFee = lcapasLogic.GetApplicationFee().ApplicationFeeAmt <= 0;
                                }
                            }

                            if ((prevPaid && !freeApplicationFee)|| _PayPalDuplicate)
                            {
                                _PaymentResult = Structs.EmailType.ApplicationDoublePaymentError;
                            }
                            else if (_ApplicationsManager.Returned)
                            {
                                _PaymentResult = Structs.EmailType.ApplicationPaymentConfirm;
                            }

                            // don't send emails if we are working locally
                            // set send to 'false' if debugging
                            bool send = !_ApplicationsManager.Debugging;

                            // construct email and return contents to payment confirmation view
                            switch (_PaymentResult)
                            {
                                case Structs.EmailType.ApplicationPaymentConfirm:
                                    _Email = new SendEmail(Structs.EmailType.ApplicationPaymentConfirm, Structs.EmailReceiverGroup.Student, _UUID, send);
                                    break;
                                case Structs.EmailType.ApplicationDoublePaymentError:
                                    //_Email = new SendEmail(Structs.EmailType.ApplicationDoublePaymentError, Structs.EmailReceiverGroup.Student, _UUID, send);
                                    new SendEmail(Structs.EmailType.ApplicationDoublePaymentError, Structs.EmailReceiverGroup.Admissions, _UUID, send);
                                    new SendEmail(Structs.EmailType.ApplicationDoublePaymentError, Structs.EmailReceiverGroup.Technical, _UUID, true);

                                    _Email.Title = "Payment has already been made!";
                                    _Email.Subtitle = "Payment was already made for this application - you were not charged again.";
                                    _Email.Body = "Please contact us if you have any concerns (contact information is below).";

                                    break;
                                case Structs.EmailType.ApplicationPaymentError:
                                    _Email = new SendEmail(Structs.EmailType.ApplicationPaymentError, Structs.EmailReceiverGroup.Student, _UUID, send);
                                    new SendEmail(Structs.EmailType.ApplicationPaymentError, Structs.EmailReceiverGroup.Admissions, _UUID, send);
                                    new SendEmail(Structs.EmailType.ApplicationPaymentError, Structs.EmailReceiverGroup.Technical, _UUID, true);
                                    break;
                            }

                            ViewBag.UUID = _UUID;
                            ViewBag.Title = _Email.Title;
                            ViewBag.SubTitle = _Email.Subtitle;
                            ViewBag.Body = _Email.Body;
                            ViewBag.ApasReturnPath = _ApplicationsManager.ApasReturnPath;
                            ViewBag.ApasLogoutPath = _ApplicationsManager.ApasLogoutPath;
                            ViewBag.ApasWriteGifPath = _ApplicationsManager.ApasWriteGifPath;
                            ViewBag.PrevPaid = prevPaid;

                            using (LcapasLogic lcapasLogic = new LcapasLogic())
                            {
                                ViewBag.ApplicationFee = lcapasLogic.GetApplicationFee().ApplicationFeeAmt.ToString();
                            }

                            ViewBag.Environment = Functions.GetEnvironment();

                            return View("ConfirmPayment");
                        }
                        else
                        {
                            _exMsg = ", Error: " + Structs.ErrorMessages.SessionManagerNotReady;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _exMsg = ", Error: " + ex.ToString();
            }

            // Capture Error
            using (LcapasLogic lcapasLogic = new LcapasLogic())
            {
                lcapasLogic.SaveException(Structs.Project.LcapasUI, Structs.Class.PaymentController, "ConfirmPayment Action", "Error", "UUID: " + _UUID + _exMsg);
            }

            return RedirectToAction("Error", "Error");
        }

        public ActionResult CancelPayment()
        {
            string _exMsg = string.Empty;
            string _UUID = string.Empty;
            string _AppFee = string.Empty;
            try
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    _UUID = lcapasLogic.UpdatePayPalResponse(Request);
                    _AppFee = lcapasLogic.GetApplicationFee().ApplicationFeeAmt.ToString();
                }

                ApplicationsManager _ApplicationsManager = new ApplicationsManager(_UUID);

                if (_ApplicationsManager.Ready)
                {
                    ViewBag.UUID = _UUID;
                    ViewBag.ApasReturnPath = _ApplicationsManager.ApasReturnPath;
                    ViewBag.ApasLogoutPath = _ApplicationsManager.ApasLogoutPath;
                    ViewBag.ApasWriteGifPath = _ApplicationsManager.ApasWriteGifPath;

                    ViewBag.ApplicationFee = _AppFee;

                    ViewBag.Environment = Functions.GetEnvironment();

                    return View();
                }
                else
                {
                    _exMsg = ", Error: " + Structs.ErrorMessages.SessionManagerNotReady;
                }
            }
            catch (Exception ex)
            {
                _exMsg = ", Error: " + ex.ToString();
            }

            // Capture the error
            using (LcapasLogic lcapasLogic = new LcapasLogic())
            {
                lcapasLogic.SaveException(Structs.Project.LcapasUI, Structs.Class.PaymentController, "CancelPayment Action", "Error", "UUID: " + _UUID + _exMsg);
            }

            // Redirect back to APAS
            return RedirectToAction("Error", "Error");
        }

        public ActionResult PaymentError()
        {
            string _exMsg = string.Empty;
            string _UUID = string.Empty;
            string _ErrorMessage = string.Empty;
            string _ErrorCode = string.Empty;
            string _AppFee = string.Empty;
            try
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    _UUID = lcapasLogic.UpdatePayPalResponse(Request);
                    _ErrorCode = lcapasLogic.GetErrorCode(Request);
                    _ErrorMessage = lcapasLogic.CheckPaypalError(Request);
                    _AppFee = lcapasLogic.GetApplicationFee().ApplicationFeeAmt.ToString();
                }

                using (ApplicationsManager _ApplicationsManager = new ApplicationsManager(_UUID))
                {
                    if (_ApplicationsManager.Ready)
                    {
                        ViewBag.UUID = _UUID;
                        ViewBag.ApasReturnPath = _ApplicationsManager.ApasReturnPath;
                        ViewBag.ApasLogoutPath = _ApplicationsManager.ApasLogoutPath;
                        ViewBag.ApasWriteGifPath = _ApplicationsManager.ApasWriteGifPath;
                        ViewBag.ErrorCode = _ErrorCode;
                        ViewBag.ErrorMessage = _ErrorMessage;

                        ViewBag.ApplicationFee = _AppFee;

                        ViewBag.Environment = Functions.GetEnvironment();

                        return View();
                    }
                    else
                    {
                        _exMsg = ", Error: " + Structs.ErrorMessages.SessionManagerNotReady;
                    }
                }
            }
            catch (Exception ex)
            {
                _exMsg = ", Error: " + ex.ToString();
            }

            if (_ErrorCode == Structs.PaypalErrors.DuplicateTransaction)
            {
                return RedirectToAction("ConfirmPayment", new { uuid = _UUID, prevPaid = true });
            }

            // Record an Exception
            using (LcapasLogic lcapasLogic = new LcapasLogic())
            {
                lcapasLogic.SaveException(Structs.Project.LcapasUI, Structs.Class.PaymentController, "PaymentError Action", "Error", "UUID: " + _UUID + _exMsg);
            }


            // Redirect back to APAS
            return RedirectToAction("Error", "Error");
        }

        protected override void Dispose(bool disposing)
        {
            //if (disposing)
            //{
            //    lcapasLogic.Dispose();
            //}
            base.Dispose(disposing);
        }

    }
}
