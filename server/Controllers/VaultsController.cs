namespace keepr.Controllers;

[ApiController]
[Route("api/vaults")]
public class VaultsController : ControllerBase
{
  private readonly VaultsService _vaultsService;
    private readonly Auth0Provider _auth0Provider;
    private readonly KeepsService _keepsService;

    public VaultsController(VaultsService vaultsService, Auth0Provider auth0Provider, KeepsService keepsService)
    {
        _vaultsService = vaultsService;
        _auth0Provider = auth0Provider;
        _keepsService = keepsService;
    }


    [HttpPost]
    [Authorize]

    public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault vaultData)
    {
      try 
      {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      vaultData.CreatorId = userInfo.Id;
      Vault vault = _vaultsService.CreateVault(vaultData);
      return Ok(vault);
      }
      catch (Exception exception)
      {
        return BadRequest(exception.Message);
      }
    }

    [HttpGet("{vaultId}")]
    public async Task<ActionResult<Vault>> GetVaultById(int vaultId)
    {
      try 
      {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);

      Vault vault = _vaultsService.GetVaultById(vaultId, userInfo?.Id);
      return vault;
      }
      catch (Exception exception)
      {
        return BadRequest(exception.Message);
      }
    }

    [HttpPut("{vaultId}")]
    [Authorize]

    public async Task<ActionResult<Vault>> UpdateVault([FromBody] Vault vaultData,int vaultId)
    {
      try 
      {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Vault vault = _vaultsService.UpdateVault(vaultData, vaultId, userInfo.Id);
      return Ok(vault);      
      }
      catch (Exception exception)
      {
        return BadRequest(exception.Message);
      }
    }

    [HttpDelete("{vaultId}")]
    [Authorize]

    public async Task<ActionResult<string>> DeleteVault(int vaultId)
    {
      try 
      {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string message = _vaultsService.DeleteVault(vaultId, userInfo.Id);
      return Ok(message);
      }
      catch (Exception exception)
      {
        return BadRequest(exception.Message);
      }
    }

    [HttpGet("{vaultId}/keeps")]
    public async Task<ActionResult<List<Keep>>> GetKeepsInPublicVault(int vaultId)
    {
      try 
      {
      Account userInfo =  await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<Keep> keeps = _keepsService.GetKeepsInPublicVault(vaultId, userInfo?.Id);
      return Ok(keeps);
      }
      catch (Exception exception)
      {
        return BadRequest(exception.Message);
      }
    }

    [HttpGet("{privateVaultId}/keeps/private")]

    public async Task<ActionResult<List<Keep>>> GetKeepsInPrivateVault(int privateVaultId)
    {
      try 
      {
      Account userInfo =  await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<Keep> keeps = _keepsService.GetKeepsInPrivateVault(privateVaultId, userInfo?.Id);
      return Ok(keeps);
      }
      catch (Exception exception)
      {
        return BadRequest(exception.Message);
      }
    }

}
