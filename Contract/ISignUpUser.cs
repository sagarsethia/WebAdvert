using System.Threading.Tasks;

namespace WebAdvertisment.Contract
{

    public interface ISignUpUser
    {
         public Task<bool> SignUpUser(string emailId, string password);
         public Task<bool> ConfirmUser(string userCode ,string emailId);
    }
}