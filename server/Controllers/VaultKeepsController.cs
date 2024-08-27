namespace keepr.Controllers;

[ApiController]
[Route("api/vaultkeeps")]
public class VaultKeepsController : ControllerBase
{
  private readonly VaultKeepsService _vaultKeepsService;
    private readonly Auth0Provider _auth0Provider;

    public VaultKeepsController(VaultKeepsService vaultKeepsService, Auth0Provider auth0Provider)
    {
        _vaultKeepsService = vaultKeepsService;
        _auth0Provider = auth0Provider;
    }

    [HttpPost]
    [Authorize]

    public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep vaultKeepData)
    {
      try 
      {
        // FIXME invalid auth for create vault keep expects a 400 but I am not sure where to get the data to determine whether or not you are the owner of the vaultkeep that you created????
        Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
        vaultKeepData.CreatorId = userInfo.Id;
        VaultKeep vaultKeep = _vaultKeepsService.CreateVaultKeep(vaultKeepData, userInfo.Id);
        return Ok(vaultKeep);
      }
      catch (Exception exception)
      {
        return BadRequest(exception.Message);
      }
    }

    [HttpDelete("{vaultKeepId}")]
    [Authorize]

    public async Task<ActionResult<string>> DeleteVaultKeep(int vaultKeepId)
    {
      try 
      {
        
        Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
        string message = _vaultKeepsService.DeleteVaultKeep(vaultKeepId, userInfo.Id);
        return Ok(message);
      }
      catch (Exception exception)
      {
        return BadRequest(exception.Message);
      }
    }

}
