using System.ServiceModel;
using ServiceReference;

namespace MyCustomUmbracoProject.Helpers;

public static class WcfClientExtensions
{
    public static void SafeClose(this ICommunicationObject client)
    {
        try
        {
            if (client.State != CommunicationState.Faulted)
            {
                client.Close();
            }
            else
            {
                client.Abort();
            }
        }
        catch
        {
            client.Abort();
        }
    }
}