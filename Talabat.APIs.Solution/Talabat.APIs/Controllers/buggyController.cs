using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Repository.Data;

namespace Talabat.APIs.Controllers
{

    public class buggyController : BaseApiController
    {
        private readonly StoreContext _dbContext;

        public buggyController(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet("notfound")] // Get: api/buggy/notfound
        public ActionResult GetNotFound()
        {
            var product = _dbContext.Products.Find(100);
            if(product is null)
                return NotFound(new ApiResponse(404,"Product not found"));
            return Ok(product);
        }


        [HttpGet("servererror")]  // Get: api/buggy/servererror
        public ActionResult GetServerError()
        {
            var product = _dbContext.Products.Find(100);
            var productToReturn = product.ToString();
            return Ok(productToReturn);
        }

        [HttpGet("badrequest")] //Get: api/buggy/badrequest
      public ActionResult GetBadRequest()
        {

            return BadRequest(new ApiResponse(400));

        }

        [HttpGet("badrequest/{id}")] //Get: api/buggy/badrequest/five
        public ActionResult GetBadRequest(int id) //Validation Error
        {

            return Ok();

        }

    }
}
