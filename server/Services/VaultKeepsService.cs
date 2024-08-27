







using System.Diagnostics;

namespace keepr.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _vaultKeepsRepository;
  private readonly VaultsService _vaultsService;


    public VaultKeepsService(VaultKeepsRepository vaultKeepsRepository, VaultsService vaultsService)
    {
        _vaultKeepsRepository = vaultKeepsRepository;
        _vaultsService = vaultsService;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData, string userId)
    {
      Vault vault = _vaultsService.GetVaultById(vaultKeepData.VaultId, userId);
      if(vault.CreatorId != userId) throw new Exception($"No vault found with the id of {vault.Id}");
      VaultKeep vaultKeep = _vaultKeepsRepository.CreateVaultKeep(vaultKeepData);
      return vaultKeep;
    }

    internal string DeleteVaultKeep(int vaultKeepId, string userId)
    {
      VaultKeep vaultKeep = GetVaultKeepById(vaultKeepId, userId);
      if(vaultKeep.CreatorId != userId) throw new Exception("You cannot delete a saved keep if you didn't create the vault");
      _vaultKeepsRepository.DeleteVaultKeep(vaultKeepId);
      string message = $"Keep was deleted from the vault";
      return message;
      
    }

    internal VaultKeep GetVaultKeepById(int vaultKeepId, string userId)
    {
      VaultKeep vaultKeep = _vaultKeepsRepository.GetVaultKeepById(vaultKeepId);
      return vaultKeep;
    }
}
