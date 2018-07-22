using System.Security.Claims;
using System.Security.Principal;

public static class IdentityHelper
{
    public static string GetLastName(this IIdentity identity)
    {
        var claimIdentity = identity as ClaimsIdentity;
        return claimIdentity != null && claimIdentity.HasClaim(c => c.Type == "LastName")
            ? claimIdentity.FindFirst("LastName").Value : string.Empty;
    }

    public static string GetFirstName(this IIdentity identity)
    {
        var claimIdentity = identity as ClaimsIdentity;
        return claimIdentity != null && claimIdentity.HasClaim(c => c.Type == "FirstName")
            ? claimIdentity.FindFirst("FirstName").Value : string.Empty;
    }

    public static string GetEmail(this IIdentity identity)
    {
        var claimIdentity = identity as ClaimsIdentity;
        return claimIdentity != null && claimIdentity.HasClaim(c => c.Type == "Email")
            ? claimIdentity.FindFirst("Email").Value : string.Empty;
    }     

    public static string GetPhoneNumber(this IIdentity identity)
    {
        var claimIdentity = identity as ClaimsIdentity;
        return claimIdentity != null && claimIdentity.HasClaim(c => c.Type == "PhoneNumber")
            ? claimIdentity.FindFirst("PhoneNumber").Value : string.Empty;
    }
}