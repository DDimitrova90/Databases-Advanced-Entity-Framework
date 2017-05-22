namespace _02_Photographers.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TagAttribute : ValidationAttribute
    {
        public override bool IsValid(object tag)
        {
            string tagValue = (string)tag;

            if (!tagValue.StartsWith("#"))
            {
                return false;
            }

            if (tagValue.Contains(" ") || tagValue.Contains("\t"))
            {
                return false;
            }

            if (tagValue.Length > 20)
            {
                return false;
            }

            return true;
        }
    }

    /*
      Example of another Attribute => and now we can put Attribute 
      for example above Tag.Label: [CustomMinLength(MinLengthValue = 5)]

    public class CustomMinLengthAttribute : ValidationAttribute
    {
        public int MinLengthValue { get; set; }

        public override bool IsValid(object value)
        {
            string valueAsStr = (string)value;

            if (valueAsStr.Length < this.MinLengthValue)
            {
                return false;
            }

            return true;
        }
    }
    */
}
