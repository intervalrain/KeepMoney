using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KeepMoney.Api.Controllers;

public class SampleController : ApiController
{
    // 不需要額外標示，會繼承 Controller 的 Authorize
    [HttpGet("inherited-auth")]
    public ActionResult<string> InheritedAuth()
    {
        return Ok("This needs auth from controller");
    }

    // 明確允許匿名訪問，覆蓋 Controller 的設定
    [HttpGet("allow-anonymous")]
    [AllowAnonymous]
    public ActionResult<string> AllowAnonymous()
    {
        return Ok("This is anonymous despite controller auth");
    }

    // 明確要求授權（雖然這裡不必要，因為已經從 Controller 繼承了）
    [HttpGet("explicit-auth")]
    [Authorize]
    public ActionResult<string> ExplicitAuth()
    {
        return Ok("This needs auth explicitly");
    }
}

