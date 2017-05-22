namespace PhotographyWorkshop.Models
{
    public class Len
    {
        private double? maxAperture;

        public int Id { get; set; }

        public string Make { get; set; }

        public int? FocalLength { get; set; }

        public double? MaxAperture
        {
            get { return this.maxAperture; }
            set
            {
                if (value.ToString().Contains("."))
                {
                    if (value.ToString().Split('.')[1].Length == 1)
                    {
                        this.maxAperture = value;
                    } 
                }

                this.maxAperture = value;
            }
        }

        public string CompatibleWith { get; set; }

        public int? OwnerId { get; set; }

        public virtual Photographer Owner { get; set; }
    }
}
