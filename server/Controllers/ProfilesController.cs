namespace keepr.Controllers;

[ApiController]
[Route("api/profiles")]
public class ProfilesController : ControllerBase
{
  private readonly Auth0Provider _auth0Provider;
    private readonly VaultsService _vaultsService;
  private readonly KeepsService _keepsService;
  private readonly AccountService _accountService;

    public ProfilesController(Auth0Provider auth0Provider, VaultsService vaultsService, KeepsService keepsService, AccountService accountService)
    {
        _auth0Provider = auth0Provider;
        _vaultsService = vaultsService;
        _keepsService = keepsService;
        _accountService = accountService;
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

  [HttpGet("{profileId}/keeps")]
    public ActionResult<List<Keep>> GetKeepsByProfileId(string profileId)
    {
      try 
      {
      List<Keep> keeps = _keepsService.GetKeepsByProfileId(profileId);
      return Ok(keeps);
      }
      catch (Exception exception)
      {
        return BadRequest(exception.Message);
      }
    }

    [HttpGet("{creatorId}/vaults")]
    public async Task<ActionResult<List<Vault>>> GetVaultsByCreatorId(string creatorId)
    {
      try 
      {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext); 
      
      List<Vault> vaults = _vaultsService.GetVaultsForOtherProfiles(creatorId);
      return Ok(vaults);

      }
      catch (Exception exception)
      {
        return BadRequest(exception.Message);
      }
    }


}
