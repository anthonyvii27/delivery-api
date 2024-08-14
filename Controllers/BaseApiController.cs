using Microsoft.AspNetCore.Mvc;

namespace basic_delivery_api.Controllers;

[Route("/api/v1/[controller]")]
[Produces("application/json")]
[ApiController]
public class BaseApiController : ControllerBase { }