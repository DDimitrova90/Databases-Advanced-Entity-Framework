namespace Sales
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Startup
    {
        public static void Main()
        {
            SalesContext context = new SalesContext();

            context.Database.Initialize(true);
        }
    }
}