using System.Security.Cryptography.X509Certificates;

namespace keepr.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth0Provider;
  private readonly VaultsService _vaultsService;
  private readonly KeepsService _keepsService;

    public AccountController(AccountService accountService, Auth0Provider auth0Provider, VaultsService vaultsService, KeepsService keepsService)
    {
        _accountService = accountService;
        _auth0Provider = auth0Provider;
        _vaultsService = vaultsService;
        _keepsService = keepsService;
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

    [HttpPut("update")]
    [Authorize]
    public async Task<ActionResult<Account>> UpdateAccount([FromBody] Account accountData)
    {
      try 
      {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext); 
      Account account = _accountService.Edit(accountData, userInfo.Id);
      return Ok(account);
      }
      catch (Exception exception)
      {
        return BadRequest(exception.Message);
      }
    }

    [HttpGet("vaults")]
    [Authorize] //NOTE might not be needed depending on what my front end is looking for
    public async Task<ActionResult<List<Vault>>> GetMyAccountVaults()
    {
      try 
      {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext); 
      List<Vault> vaults = _vaultsService.GetMyAccountVaults(userInfo.Id);
      return Ok(vaults);
      }
      catch (Exception exception)
      {
        return BadRequest(exception.Message);
      }
    }
}
