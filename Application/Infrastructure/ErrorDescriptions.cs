using System.Net.NetworkInformation;
using System.Text;

namespace Web.Api.Infrastructure;

public static class ErrorDescriptions
{
    public static string Describe400()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Error code 400 (Bad Request) indicates that the server cannot or will not process ");
        sb.AppendLine("the request due to something that is perceived to be a client error (e.g. invalid input)!");
        return sb.ToString();
    }

    public static string Describe404()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Error code 404 (Not Found) indicates that the origin server did not find a current representation ");
        sb.AppendLine("for the target resource.");
        return sb.ToString();
    }

    public static string Describe409()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Error code 409 (Conflict) indicated that the request could not be completed due to a conflict ");
        sb.AppendLine("with the current state of target resource.");
        return sb.ToString();
    }

    public static string Describe500()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Error code 500 (Internal Server Error) indicates that the server encountered an unexpected condition ");
        sb.AppendLine("that prevented it from fulfilling the request.");
        return sb.ToString();
    }
}
