using Newtonsoft.Json;

namespace WebApplication2.Models
{
    public class BaseModel
    {
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
