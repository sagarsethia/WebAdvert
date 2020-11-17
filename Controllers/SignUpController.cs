using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAdvertisment.Contract;

namespace WebAdvertisment.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class SignUpController : ControllerBase
    {
        private ISignUpUser _signUpUser { get; set; }
        public SignUpController(ISignUpUser signUpUser)
        {
            _signUpUser = signUpUser;
        }

        
        [HttpPost ("SignUpUser")]
         public async Task<IActionResult> SignUpUser(string emailId, string password)
        {
            var isUserCreated = await _signUpUser.SignUpUser(emailId, password);
            if (isUserCreated)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }

        }
        [HttpPost ("ConfirmUser")]
         public async Task<IActionResult> ConfirmUser(string emailId, string passCode)
        {
            var isUserConfirm = await _signUpUser.ConfirmUser(emailId,passCode);
            if (isUserConfirm)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }

        }
    }
}
