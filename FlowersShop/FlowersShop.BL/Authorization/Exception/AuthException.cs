namespace FlowersShop.BL.Authorization.Exception;

public class AuthException : System.Exception
{
    public ResultCode? Code { get; set; }
    public AuthException(string message) : base(message)
    {
    }
    public AuthException(ResultCode code) : base(code.ToString())
    {
        Code = code;
    }
}