using MovieStoreApp.Model;
using System.Configuration;
namespace MovieStoreApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
           MovieController controller = new MovieController();
            controller.Start();
        }
    }
}
