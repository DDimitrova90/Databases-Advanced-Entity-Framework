namespace _02_Photographers
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Startup
    {
        public static void Main()
        {
            PhotographersContext context = new PhotographersContext();

            context.Database.Initialize(true);

            // Problem 8
            //Tag tag = new Tag() { Label = "Very Cool" };
            //context.Tags.Add(tag);

            //try
            //{
            //    context.SaveChanges();
            //}
            //catch (DbEntityValidationException)
            //{
            //    tag.Label = TagTransformer.Transform(tag.Label);
            //    context.SaveChanges();
            //}
        }
    }
}
