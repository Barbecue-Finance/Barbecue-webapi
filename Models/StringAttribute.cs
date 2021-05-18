using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class StringAttribute : StringLengthAttribute
    {
        public StringAttribute(int minimumLength, int maximumLength) : base(maximumLength)
        {
            MinimumLength = minimumLength;
        }
    }
}