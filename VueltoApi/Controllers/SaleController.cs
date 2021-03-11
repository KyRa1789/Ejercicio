using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VueltoApi.Admin;
using VueltoApi.Context;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VueltoApi.Controllers
{
    [Route("api/sale")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private SaleAdmin _admin;
        public SaleController(AppDbContext context)
        {
            _admin = new Admin.SaleAdmin(context);
        }

        [HttpPost]
        [Route("addsale/{total}/{PaidAmount}")]
        public IActionResult AddSale(decimal total, decimal PaidAmount)
        {
            var result = _admin.SaveSale(total, PaidAmount);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _admin.GetAll();
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _admin.GetSaleById(id);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
