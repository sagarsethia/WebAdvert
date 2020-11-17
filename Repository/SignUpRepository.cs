using System.Threading.Tasks;
using Amazon.Extensions.CognitoAuthentication;
using Amazon.AspNetCore.Identity.Cognito;
using Microsoft.AspNetCore.Identity;
using WebAdvertisment.Contract;
using System.Collections.Generic;

namespace WebAdvertisment.Repository
{
    public class SignUpRepository : ISignUpUser
    {
        private SignInManager<CognitoUser> _signInManager;
        private UserManager<CognitoUser> _userManager;
        private CognitoUserPool _pool;
        public SignUpRepository(
            SignInManager<CognitoUser> signInManager,
            UserManager<CognitoUser> userManager,
            CognitoUserPool pool)
        {
            _pool = pool;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<bool> SignUpUser(string emailId, string password)
        {
            var user = _pool.GetUser(emailId);
            if (user.Status != null)
            {
                return false;
            }
            user.Attributes.Add(CognitoAttribute.Name.AttributeName,emailId);
            var createUser = await _userManager.CreateAsync(user, password);
            if (createUser.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> ConfirmUser(string emailId,string userCode ){
            var user = await _userManager.FindByEmailAsync(emailId);
            if (user == null)
            {
                return false;
            }
           var result=  await ((CognitoUserManager<CognitoUser>)_userManager).ConfirmSignUpAsync(user,userCode, true);
            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}