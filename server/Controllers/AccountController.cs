using System.Security.Cryptography.X509Certificates;

namespace keepr.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth0Provider;
  private readonly VaultsService _vaultsService;

    public AccountController(AccountService accountService, Auth0Provider auth0Provider, VaultsService vaultsService)
    {
        _accountService = accountService;
        _auth0Provider = auth0Provider;
        _vaultsService = vaultsService;
    }

    [HttpGet]
    [Authorize]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateAccount(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{profileId}")]
  public ActionResult<Profile> GetProfileByProfileId(string profileId)
  {
    try 
    {
      Profile profile = _accountService.GetProfileByProfileId(profileId);
      return Ok(profile);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

   [HttpGet("{creatorId}/vaults")]
    public  ActionResult<List<Vault>> GetVaultsByCreatorId(string creatorId)
    {
      try 
      {
      List<Vault> vaults = _vaultsService.GetVaultsByCreatorId(creatorId);
      return Ok(vaults);

      }
      catch (Exception exception)
      {
        return BadRequest(exception.Message);
      }
    }
}
