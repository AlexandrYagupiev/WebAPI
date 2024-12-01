using WebAPI.Models.Base;

namespace WebAPI.Models
{
    public class Car : BaseModel
    {
        public string? Name { get; set; }
        public string? Number { get; set; }
    }
}
