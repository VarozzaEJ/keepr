


namespace keepr.Services;

public class KeepsService
{
  private readonly KeepsRepository _keepsRepository;

    public KeepsService(KeepsRepository keepsRepository)
    {
        _keepsRepository = keepsRepository;
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
}
