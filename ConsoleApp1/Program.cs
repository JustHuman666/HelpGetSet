using DAL.Context;

namespace ConsoleApp1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var context = new SiteContext())
            {
                context.Database.EnsureCreated();
            }
        }
    }
}