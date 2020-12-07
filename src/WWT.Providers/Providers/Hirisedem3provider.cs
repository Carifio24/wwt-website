#nullable disable

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using WWTWebservices;

namespace WWT.Providers
{
    [RequestEndpoint("/wwtweb/HiriseDem3.aspx")]
    public class HiriseDem3Provider : RequestProvider
    {
        private readonly IPlateTilePyramid _plateTiles;
        private readonly WwtOptions _options;

        public HiriseDem3Provider(IPlateTilePyramid plateTiles, WwtOptions options)
        {
            _plateTiles = plateTiles;
            _options = options;
        }

        // This content-type isn't correct since this is a DEM provider, but it's
        // what the server has historically reported.
        public override string ContentType => ContentTypes.Png;

        public override async Task RunAsync(IWwtContext context, CancellationToken token)
        {
            string query = context.Request.Params["Q"];
            string[] values = query.Split(',');
            int level = Convert.ToInt32(values[0]);
            int tileX = Convert.ToInt32(values[1]);
            int tileY = Convert.ToInt32(values[2]);

            using (Stream s = await _plateTiles.GetStreamAsync(_options.WwtTilesDir, "marsToastDem.plate", -1, level, tileX, tileY, token))
            {
                if (s.Length == 0)
                {
                    context.Response.Clear();
                    context.Response.ContentType = "text/plain";
                    await context.Response.WriteAsync("No image", token);
                } else {
                    await s.CopyToAsync(context.Response.OutputStream, token);
                    context.Response.Flush();
                }
            }

            context.Response.End();
        }
    }
}
