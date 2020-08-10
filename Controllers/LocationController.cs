using LocationApi.Models;
using LocationApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace LocationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly IAddressService _addressService;
       public ILogger<LocationController> _logger { get; }
       private  IConfiguration _configuration ;
        public LocationController(IAddressService addressService,ILogger<LocationController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _addressService = addressService;
            _configuration = configuration;
            var val = _configuration.GetValue<string>("TestKey");
            _logger.LogInformation("Value of TestKey read from appsettings is :" + val);
        }
        

        [HttpGet]
        public ActionResult<List<Address>> Get()
        {
            return  _addressService.Get();
        }

        [HttpGet("{id}")]    
        public ActionResult<string> Test(string id)
        {
            _logger.LogInformation("Inside Test method");
            return  "TestString";
        }
           

        [HttpGet("{id:length(24)}", Name = "GetAddress")]
        public ActionResult<Address> Get(string id)
        {
            var book = _addressService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpPost]
        public ActionResult<Address> Create(Address address)
        {
            _addressService.Create(address);

            return CreatedAtRoute("GetAddress", new { id = address.Id.ToString() }, address);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Address addressIn)
        {
            var book = _addressService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _addressService.Update(id, addressIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var address = _addressService.Get(id);

            if (address == null)
            {
                return NotFound();
            }

            _addressService.Remove(address.Id);

            return NoContent();
        }
    }
}