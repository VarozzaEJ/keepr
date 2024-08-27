namespace keepr.Controllers;

[ApiController]
[Route("api/keeps")]
public class KeepsController : ControllerBase
{
  private readonly KeepsService _keepsService;
  private readonly Auth0Provider _auth0Provider;

    public KeepsController(KeepsService keepsService, Auth0Provider auth0Provider)
    {
        _keepsService = keepsService;
        _auth0Provider = auth0Provider;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep keepData)
    {
      try 
      {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      keepData.CreatorId = userInfo.Id;
      Keep keep = _keepsService.CreateKeep(keepData);
      return Ok(keep);
      }
      catch (Exception exception)
      {
        return BadRequest(exception.Message);
      }
    }

    [HttpGet]
    public async Task<ActionResult<List<Keep>>> GetAllKeeps()
    {
      try 
      {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<Keep> keeps = _keepsService.GetAllKeeps(userInfo?.Id);
      return Ok(keeps);
      }
      catch (Exception exception)
      {
        return BadRequest(exception.Message);
      }
    }

    [HttpGet("{keepId}")]
    public async Task<ActionResult<Keep>> GetKeepById(int keepId)
    {
      try 
      {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Keep keep = _keepsService.IncrementViews(keepId, userInfo?.Id);
      
      return Ok(keep);
      }
      catch (Exception exception)
      {
        return BadRequest(exception.Message);
      }
    }

    

    [HttpPut("{keepId}")]
    [Authorize]
    public async Task<ActionResult<Keep>> EditKeep([FromBody] Keep keepData,int keepId) 
    {
      try 
      {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);

      Keep keep = _keepsService.EditKeep(keepData, keepId, userInfo.Id);
      return Ok(keep);
      }
      catch (Exception exception)
      {
        return BadRequest(exception.Message);
      }
    }

    [HttpDelete("{keepId}")]
    [Authorize]

    public async Task<ActionResult<string>> DeleteKeep(int keepId)
    {
      try 
      {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string message = _keepsService.DeleteKeep(keepId, userInfo.Id);
      return Ok(message);
      }
      catch (Exception exception)
      {
        return BadRequest(exception.Message);
      }
    }
}
