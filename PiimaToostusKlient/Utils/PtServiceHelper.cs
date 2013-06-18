using PiimaToostusKlient.PtServiceRef;

namespace PiimaToostusKlient.Utils
{
    public static class PtServiceHelper
    {
        public static IPiimaToostusService GetServiceInstance()
        {
            return new PiimaToostusServiceClient("BasicHttpBinding_IPiimaToostusService");
        }
    }
}