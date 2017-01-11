using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using YoutubeExtractor;
namespace Slaxer
{
    public struct entityx
    {
        public entityx(Boolean x)
        {
            this.urlsVideoList = null;
            this.finalVideo = null;
            this.videoDownloader = null;
            this.x = null;
            this.records = null;
        }
        public void clear()
        {
            this.urlsVideoList = null;
            this.finalVideo = null;
            this.videoDownloader = null;
            this.x = null;
            this.records = null;
        }
        public IEnumerable<VideoInfo> urlsVideoList;
        public VideoInfo finalVideo;
        public VideoDownloader videoDownloader;
        public Thread x;
        public List<int> records;
    };

    public static class Definitions
    {
        public static void ok(string h)
        {
            MessageBox.Show(h,"Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void error(string h)
        {
            MessageBox.Show(h, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static void warning(string h)
        {
            MessageBox.Show(h, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, ControlThread = true)]

        public static void KillTheThread(ref Thread seigon)
        {
            seigon.Abort();
        }
    }
}
