







namespace keepr.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _vaultKeepsRepository;

    public VaultKeepsService(VaultKeepsRepository vaultKeepsRepository)
    {
        _vaultKeepsRepository = vaultKeepsRepository;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
    {
      VaultKeep vaultKeep = _vaultKeepsRepository.CreateVaultKeep(vaultKeepData);
      return vaultKeep;
    }

    internal string DeleteVaultKeep(int vaultKeepId, string userId)
    {
      GetVaultKeepById(vaultKeepId, userId);
      _vaultKeepsRepository.DeleteVaultKeep(vaultKeepId);
      string message = $"Keep was deleted from the vault";
      return message;
      
    }

    internal VaultKeep GetVaultKeepById(int vaultKeepId, string userId)
    {
      VaultKeep vaultKeep = _vaultKeepsRepository.GetVaultKeepById(vaultKeepId);
      if(vaultKeep.CreatorId != userId) throw new Exception("You cannot delete a saved keep if you didn't create it");
      return vaultKeep;
    }
}
