using System.Threading.Tasks;
using WebAdvertApi.Model;

namespace WebAdvertApi.Contract
{
    public interface IAdvertStorage
    {
         Task<string> Add(AdvertModel model);

         Task Confirm(ConfirmAdvertModel model);

         Task<bool> CheckHealthAsync();
         
    }
}