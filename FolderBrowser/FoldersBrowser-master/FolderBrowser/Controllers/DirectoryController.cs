using FolderBrowser.Models;
using System.Web.Http;

namespace FolderBrowser.Controllers
{
    /// <summary>
    /// Getting the structure of the directory 
    /// </summary>
    public class DirectoryController : ApiController
    {
        public dirStructure GetDirectory()
        {
            return dirStructure.GetRoot();
        }

        [HttpGet()]
        public dirStructure GetDirectory(string dir)
        {
            if (dir != null)
            {
                if (dir.Length > 1)
                {
                    var resultStructure = dirStructure.GetDir(dir);
                    return resultStructure;
                }
            }
            return dirStructure.GetRoot();
        }
    }
}