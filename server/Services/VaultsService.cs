





namespace keepr.Services;
public class VaultsService
{
  private readonly VaultsRepository _vaultsRepository;

    public VaultsService(VaultsRepository vaultsRepository)
    {
        _vaultsRepository = vaultsRepository;
    }

    internal Vault CreateVault(Vault vaultData)
    {
      Vault vault = _vaultsRepository.CreateVault(vaultData);
      return vault;
    }

    internal string DeleteVault(int vaultId, string userId)
    {
      Vault vault = GetVaultById(vaultId);
      if(vault.CreatorId != userId) throw new Exception("You cannot delete a vault you didn't create");
      _vaultsRepository.DeleteVault(vaultId);
      return $"{vault.Name} was deleted successfully";
    }

    internal List<Vault> GetMyAccountVaults(string userId)
    {
      List<Vault> vaults = _vaultsRepository.GetMyAccountVaults(userId);
      return vaults;
    }

    internal Vault GetVaultById(int vaultId, string userId)
    {
      
    Vault vault = GetVaultById(vaultId);
    if (vault.CreatorId != userId && vault.IsPrivate == true)
    {
      throw new Exception($"No vault found with the id of {vaultId} 🅇");
    }



    return vault;
    }

    internal List<Vault> GetVaultsForOtherProfiles(string creatorId)
    {

      List<Vault> vaults = _vaultsRepository.GetVaultsForOtherProfiles(creatorId);
      return vaults;
    }

    internal Vault UpdateVault(Vault vaultData, int vaultId, string userId)
    {
      Vault vaultToUpdate = GetVaultById(vaultId);
      if(vaultToUpdate.CreatorId != userId) throw new Exception("You cannot update a vault you didn't create");

      vaultToUpdate.Description = vaultData.Description ?? vaultToUpdate.Description;
      vaultToUpdate.Name = vaultData.Name ?? vaultToUpdate.Name;
      vaultToUpdate.Img = vaultData.Img ?? vaultToUpdate.Img;
      vaultToUpdate.IsPrivate = vaultData.IsPrivate ?? vaultToUpdate.IsPrivate;

      Vault updatedVault = _vaultsRepository.UpdateVault(vaultToUpdate);
      return updatedVault;
    }

    private Vault GetVaultById(int vaultId)
    {
       Vault vault = _vaultsRepository.GetVaultById(vaultId);

    if (vault == null)
    {
      throw new Exception($"No vault found with the id of {vaultId}");
    }

    return vault;
    }
}
