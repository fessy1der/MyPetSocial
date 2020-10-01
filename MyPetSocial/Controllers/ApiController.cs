using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyPetSocial.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
    }
}
