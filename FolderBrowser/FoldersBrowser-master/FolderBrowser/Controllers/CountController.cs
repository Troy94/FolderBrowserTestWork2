using FolderBrowser.Models;
using System.IO;
using System.Web.Http;

namespace FolderBrowser.Controllers
{
    public class CountController : ApiController
    {
        // GET api/<controller> - counter the files of the directory
        public dirInfo Get(string dir)
        {
            if (dir != null)
            {
                var checkDir = new DirectoryInfo(dir);
                if (checkDir.Exists)
                {
                    return dirInfo.DirStatistic(dir);
                }
            }
            return null;
        }
    }
}