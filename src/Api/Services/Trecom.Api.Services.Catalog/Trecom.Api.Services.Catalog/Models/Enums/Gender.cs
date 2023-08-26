using Ardalis.SmartEnum;

namespace Trecom.Api.Services.Catalog.Models.Enums
{
    public class Gender:SmartEnum<Gender>
    {
        public static Gender Male = new Gender(nameof(Male), 0);
        public static Gender Female = new Gender(nameof(Female), 1);

        public Gender(string name, int value) : base(name, value)
        {
        }

        public Gender():base(null,0)
        {
            
        }
    }
}
