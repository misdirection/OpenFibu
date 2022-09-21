using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenFibu.API.DTO;

namespace OpenFibu.API.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public ActionResult<AppInfoResponse> GetHealth() => Ok(new HealthResponse());
}