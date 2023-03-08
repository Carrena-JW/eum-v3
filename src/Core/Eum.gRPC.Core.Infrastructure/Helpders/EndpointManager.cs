using Eum.gRPC.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eum.gRPC.Shared.Helpers
{
    public class EndpointManager
    {

        private const string BASE_URL = "https://localhost";
        private const string COMMONL_ENDPOINT = $"{BASE_URL}:8401/";
        private const string MAIL_ENDPOINT = $"{BASE_URL}:8402/";
        private const string PORTAL_ENDPOINT = $"{BASE_URL}:8403/";
        private const string WORKFLOW_ENDPOINT = $"{BASE_URL}:8404/";

        public static string GetEndpoint(EnumEndpointTarget target)
        {
            if (target == EnumEndpointTarget.Common) return COMMONL_ENDPOINT;
            if (target == EnumEndpointTarget.Mail) return MAIL_ENDPOINT;
            if (target == EnumEndpointTarget.Workflow) return WORKFLOW_ENDPOINT;
            if (target == EnumEndpointTarget.Portal) return PORTAL_ENDPOINT;

            return "";
        }

    }
}
