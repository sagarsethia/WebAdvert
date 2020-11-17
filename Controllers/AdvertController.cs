using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAdvertApi.Contract;
using WebAdvertApi.Model;

namespace WebAdvertApi.Controllers
{
    [ApiController]
    [Route("adverts/v1")]
    public class AdvertController : ControllerBase
    {
        private IAdvertStorage _advertStorageService { get; set; }
        public AdvertController(IAdvertStorage service)
        {
            _advertStorageService = service;
        }
        [HttpPost("create")]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(CreateAdvertRespone),200)]
        public async Task<IActionResult> Create(AdvertModel model)
        {
            string isAdvertAdded;
            try
            {
                isAdvertAdded = await _advertStorageService.Add(model);
            }
            catch (KeyNotFoundException)
            {
                return new NotFoundResult();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return StatusCode(201, isAdvertAdded);
            
        
        }
        [HttpPost("confirm")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Confirm(ConfirmAdvertModel model){
           try {
               await _advertStorageService.Confirm(model);
           }
           catch(KeyNotFoundException){
                return new NotFoundResult();
           }
           catch(Exception ex){
               return StatusCode(500,ex.Message);
           }
           return Ok();
        }
    }
}