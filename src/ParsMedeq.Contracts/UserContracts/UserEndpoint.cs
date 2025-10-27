using SRH.PresentationApi.ApiEndpoint;

namespace ParsMedeQ.Contracts.UserContracts;

public sealed class UserEndpoint : ApiEndpointBase
{
    const string _tag = "User";

    protected override ApiEndpointItem MyEndpoint => EndpointMetadata.User;

    private ApiEndpointItem UserSigninWithMobile => new("Signin/mobile", this.MyEndpoint);


    public EndpointInfo SigninWithMobile_SendOtp { get; private set; }
    public EndpointInfo SendOtpToExistingMobile { get; private set; }
    public EndpointInfo SendPasswordOtpBySMS { get; private set; }
    public EndpointInfo SendPasswordOtpByEmail { get; private set; }
    public EndpointInfo User_SendPasswordOtpBySMS { get; private set; }
    public EndpointInfo User_SendPasswordOtpByEmail { get; private set; }
    public EndpointInfo SigninWithMobile { get; private set; }
    public EndpointInfo SigninWithExistingMobile { get; private set; }

    //public EndpointInfo SignupPersonalInquery { get; private set; }
    //public EndpointInfo SignupLegalInquery { get; private set; }
    //public EndpointInfo SignupCivilPartnershipInquery { get; private set; }
    //public EndpointInfo SignupForeignerInquery { get; private set; }

    public EndpointInfo SendOtp { get; private set; }
    public EndpointInfo VerifyOtp { get; private set; }
    //public EndpointInfo SignupPersonal { get; private set; }

    public EndpointInfo GetSignupingTaxPayers { get; private set; }
    public EndpointInfo Profile { get; private set; }
    public EndpointInfo UpdateProfile { get; private set; }
    public EndpointInfo SetPassword { get; init; }
    public EndpointInfo ChangePassword { get; init; }
    public EndpointInfo ResetPassword { get; init; }
    public EndpointInfo Comments { get; init; }
    public EndpointInfo Tickets { get; init; }
    public EndpointInfo Orders { get; init; }
    public EndpointInfo Payments { get; init; }



    public UserEndpoint()
    {
        SigninWithMobile_SendOtp = new EndpointInfo(
            this.GetUrl($"{UserSigninWithMobile.Endpoint}/sendotp"),
            this.GetUrl($"{UserSigninWithMobile.Endpoint}/sendotp"),
            "Send OTP to mobile",
            "ارسال رمز یکبار مصرف به موبایل",
            _tag);

        SendOtpToExistingMobile = new EndpointInfo(
            this.GetUrl($"{UserSigninWithMobile.Endpoint}/sendOtpToExistingMobile"),
            this.GetUrl($"{UserSigninWithMobile.Endpoint}/sendOtpToExistingMobile"),
            "Send OTP to existing mobile",
            "ارسال رمز یکبار مصرف به موبایل موجود در سیستم",
            _tag);

        SendPasswordOtpBySMS = new EndpointInfo(
            this.GetUrl("Signin/sendPasswordOtpBySms"),
            this.GetUrl("Signin/sendPasswordOtpBySms"),
            "send password otp by sms",
            "ارسال رمز یکبار مصرف به موبایل",
            _tag);

        SendPasswordOtpByEmail = new EndpointInfo(
            this.GetUrl("Signin/sendPasswordOtpByEmail"),
            this.GetUrl("Signin/sendPasswordOtpByEmail"),
            "send password otp by email",
            "ارسال رمز یکبار مصرف به ایمیل",
            _tag);

        User_SendPasswordOtpBySMS = new EndpointInfo(
            this.GetUrl("sendPasswordOtpBySms"),
            this.GetUrl("sendPasswordOtpBySms"),
            "send user password otp by sms",
            "ارسال رمز یکبار مصرف به موبایل",
            _tag);

        User_SendPasswordOtpByEmail = new EndpointInfo(
            this.GetUrl("sendPasswordOtpByEmail"),
            this.GetUrl("sendPasswordOtpByEmail"),
            "send user password otp by email",
            "ارسال رمز یکبار مصرف به ایمیل",
            _tag);

        SigninWithMobile = new EndpointInfo(
            this.GetUrl($"{UserSigninWithMobile.Endpoint}"),
            this.GetUrl($"{UserSigninWithMobile.Endpoint}"),
            "Sign in with mobile",
            "ورود توسط موبایل و رمز یکبار مصرف",
            _tag);

        SigninWithExistingMobile = new EndpointInfo(
            this.GetUrl("signin/existingMobile"),
            this.GetUrl("signin/existingMobile"),
            "Sign in with existsing mobile",
            "ورود توسط موبایل موجود در سیستم و رمز یکبار مصرف",
            _tag);

        //SignupPersonalInquery = new EndpointInfo(
        //    this.GetUrl("signup/personal/inquery"),
        //    this.GetUrl("signup/personal/inquery"),
        //    "SignupPersonalInquery",
        //    "استعلام اطلاعات شخص حقیقی",
        //    _tag);

        //SignupLegalInquery = new EndpointInfo(
        //    this.GetUrl("signup/legal/inquery"),
        //    this.GetUrl("signup/legal/inquery"),
        //    "SignupLegalInquery",
        //    "استعلام اطلاعات شخص حقوقی",
        //    _tag);

        //SignupCivilPartnershipInquery = new EndpointInfo(
        //    this.GetUrl("signup/civilpartnership/inquery"),
        //    this.GetUrl("signup/civilpartnership/inquery"),
        //    "SignupCivilPartnershipInquery",
        //    "استعلام اطلاعات مشارکت مدنی",
        //    _tag);

        //SignupForeignerInquery = new EndpointInfo(
        //    this.GetUrl("signup/foreigner/inquery"),
        //    this.GetUrl("signup/foreigner/inquery"),
        //    "SignupForeignerInquery",
        //    "استعلام اطلاعات اتباع غیر ایرانی",
        //    _tag);

        SendOtp = new EndpointInfo(
           this.GetUrl("SendOtp"),
           this.GetUrl("SendOtp"),
           "SendOtp",
           "ارسال کد یکبارمصرف",
           _tag);

        VerifyOtp = new EndpointInfo(
            this.GetUrl("VerifyOtp"),
            this.GetUrl("VerifyOtp"),
            "VerifyOtp",
            "اعتبارسنجی کد یکبارمصرف",
            _tag);

        //SignupPersonal = new EndpointInfo(
        //   this.GetUrl("signup/personal"),
        //   this.GetUrl("signup/personal"),
        //   "SignupPersonal",
        //   "ثبت نام کاربر حقیقی",
        //   _tag);

        GetSignupingTaxPayers = new EndpointInfo(
           this.GetUrl("GetSignupingTaxPayers"),
           this.GetUrl("GetSignupingTaxPayers"),
           "GetSignupingTaxPayers",
           "لیست کاربرانی که من برای آنها ثبت نام کردم",
           _tag);

        Profile = new EndpointInfo(
           this.GetUrl("Profile"),
           this.GetUrl("Profile"),
           "Profile",
           "Profile",
           _tag);

        UpdateProfile = new EndpointInfo(
           this.GetUrl("UpdateProfile"),
           this.GetUrl("UpdateProfile"),
           "Update Profile",
           "Update Profile",
           _tag);

        this.ChangePassword = new EndpointInfo(
            GetUrl("ChangePassword"),
            GetUrl("ChangePassword"),
            "Change Password",
            "Change Password",
            _tag);

        this.SetPassword = new EndpointInfo(
            GetUrl("SetPassword"),
            GetUrl("SetPassword"),
            "Set Password",
            "Set Password",
            _tag);

        this.ResetPassword = new EndpointInfo(
            this.GetUrl("Signin/resetPassword"),
            this.GetUrl("Signin/resetPassword"),
            "Reset Password",
            "Reset Password",
            _tag);

        this.Comments = new EndpointInfo(
            this.GetUrl("comment/list"),
            this.GetUrl("comment/list"),
            "User Comments",
            "User Comments",
            _tag);

        this.Tickets = new EndpointInfo(
            this.GetUrl("ticket/list"),
            this.GetUrl("ticket/list"),
            "User Tickets",
            "User Tickets",
            _tag);

        this.Orders = new EndpointInfo(
            this.GetUrl("order/list"),
            this.GetUrl("order/list"),
            "User Orders",
            "User Orders",
            _tag);

        this.Payments = new EndpointInfo(
            this.GetUrl("payment/list"),
            this.GetUrl("payment/list"),
            "User Payments",
            "User Payments",
            _tag);
    }
}

