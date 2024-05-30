using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using stonictransaccionesbe.model;
using stonictranaccionesbe.logic;

namespace stonictransaccionesbe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransaccionB2CController
    {
        [HttpPost]
        public ReturnValue Post([FromBody] TransaccionB2C item)
        {
            TransaccionB2CLogic TB2CLogic = new TransaccionB2CLogic();
            return TB2CLogic.Registrar(item);
        }
    }
}
