using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OvertimeApi.DataAceess;

namespace OvertimeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JWTAuthController : ControllerBase
    {
        private readonly Context _context;

        public JWTAuthController(Context context)
        {
            _context= context;
        }


    }
}
