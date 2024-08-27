


namespace keepr.Services;

public class KeepsService
{
  private readonly KeepsRepository _keepsRepository;
  private readonly VaultsService _vaultsService;

    public KeepsService(KeepsRepository keepsRepository, VaultsService vaultsService)
    {
        _keepsRepository = keepsRepository;
        _vaultsService = vaultsService;
    }

    internal Keep CreateKeep(Keep keepData)
    {
      Keep keep = _keepsRepository.CreateKeep(keepData);
      return keep;
    }

    internal string DeleteKeep(int keepId, string userId)
    {
      Keep keepToDelete = GetKeepById(keepId);
       if(keepToDelete.CreatorId != userId) throw new Exception("You cannot delete a keep you didn't create");
      _keepsRepository.DeleteKeep(keepId);
      return $"{keepToDelete.Name} was successfully deleted";
    }

    internal Keep EditKeep(Keep keepData, int keepId, string userId)
    {
      Keep keepToUpdate = GetKeepById(keepId);
      if(keepToUpdate.CreatorId != userId) throw new Exception("You cannot edit a keep you didn't create");

      keepToUpdate.Img = keepData.Img ?? keepToUpdate.Img;
      keepToUpdate.Name = keepData.Name ?? keepToUpdate.Name;
      keepToUpdate.Description = keepData.Description ?? keepToUpdate.Description;


      Keep editedKeep = _keepsRepository.EditKeep(keepToUpdate);
      return editedKeep;
    }

    internal List<Keep> GetAllKeeps(string userId)
    {
      List<Keep> keeps = _keepsRepository.GetAllKeeps(userId);
      return keeps;
    }


   
    internal Keep GetKeepById(int keepId)
    {
      Keep keep = _keepsRepository.GetKeepById(keepId);
      if(keep == null) throw new Exception($"No keep found with the id of ${keepId}");
      return keep;
    }

    internal List<Keep> GetKeepsByProfileId(string profileId)
    {
      List<Keep> keeps = _keepsRepository.GetKeepsByProfileId(profileId);
      return keeps;
    }

    internal List<Keep> GetKeepsInPrivateVault(int privateVaultId, string userId)
    {
      
      _vaultsService.GetVaultById(privateVaultId, userId);
      List<Keep> keeps = _keepsRepository.GetKeepsInPrivateVault(privateVaultId, userId);
      return keeps;
    }

    internal List<Keep> GetKeepsInPublicVault(int vaultId, string userId)
    {
      List<Keep> keeps = _keepsRepository.GetKeepsInPublicVault(vaultId);
      return keeps;
    }

    internal Keep IncrementViews(int keepId, string userId)
    {
      Keep keep = GetKeepById(keepId);
      // if(keep.CreatorId == userId) {
      //   return keep;
      // }
      keep.Views ++;

      _keepsRepository.EditKeep(keep);
      return keep;
    }
}
