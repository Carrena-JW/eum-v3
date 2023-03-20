using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eum.Core.Service.Utilities
{
    /// <summary>
    /// Kestrel 서버 관련 유틸리티
    /// </summary>
    public static class KestrelUtil
    {
        /// <summary>
        /// 랜덤 포트를 사용하도록 Kestrel 서버를 구성합니다.
        /// </summary>
        /// <param name="builder"></param>
        public static void ConfigureKestrelWithRandomPort(this WebApplicationBuilder builder)
        {
            var portHttp = IpUtilities.GetAvailablePort(5070, 6000);
            var portHttps = IpUtilities.GetAvailablePort(7070, 8000);

            builder.WebHost.ConfigureKestrel(options =>
            {
                if(portHttp.HasValue)
                {
                    // Setup a HTTP/2 endpoint without TLS.
                    options.ListenLocalhost(portHttp.Value, o =>
                    {
                        o.Protocols = HttpProtocols.Http2;
                    });
                }

                if(portHttps.HasValue)
                {
                    // Setup a HTTP/2 endpoint with TLS.
                    options.ListenLocalhost(portHttps.Value, o =>
                    {
                        o.Protocols = HttpProtocols.Http2;
                        o.UseHttps();
                    });
                }
            });
        }
    }
}
