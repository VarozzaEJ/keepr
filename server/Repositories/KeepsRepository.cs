





namespace keepr.Repositories;

public class KeepsRepository
{
  private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Keep CreateKeep(Keep keepData)
    {
      string sql = @"
      INSERT INTO
      keeps(name, description, img, views, creatorId)
      VALUES(@Name, @Description, @Img, @Views, @CreatorId);

      SELECT 
      keeps.*,
      accounts.*
      FROM keeps
      JOIN accounts ON accounts.id = keeps.creatorId
      WHERE keeps.id = LAST_INSERT_ID()
      ;";

      Keep keep = _db.Query<Keep, Profile, Keep>(sql, JoinCreator, keepData).FirstOrDefault();
      return keep;
    }

    internal void DeleteKeep(int keepId)
    {
      string sql = @"
      DELETE FROM keeps where id = @keepId LIMIT 1
      ;";

      _db.Execute(sql, new {keepId});
    }

    internal Keep EditKeep(Keep keepToUpdate)
    {
      string sql = @"
      UPDATE keeps
      SET
      img = @Img,
      name = @Name,
      description = @Description
      WHERE id = @Id LIMIT 1;

     SELECT 
      keeps.*,
      accounts.*
      FROM keeps
      JOIN accounts ON accounts.id = keeps.creatorId
      WHERE keeps.id = @Id
      ;";

      Keep updatedKeep = _db.Query<Keep, Profile, Keep>(sql, JoinCreator, keepToUpdate).FirstOrDefault();
      return updatedKeep;
    }

    internal List<Keep> GetAllKeeps(string userId)
    {
      string sql = @"
      SELECT 
      keeps.*,
      accounts.*
      FROM keeps
      JOIN accounts on accounts.id = keeps.creatorId
      ;";

      List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, JoinCreator, new {userId}).ToList();
      return keeps;
    }

    internal Keep GetKeepById(int keepId)
    {
      string sql = @"
      SELECT 
      keeps.*,
      accounts.*
      FROM keeps
      JOIN accounts ON accounts.id = keeps.creatorId
      WHERE keeps.id = @keepId
      ;";

      Keep keep = _db.Query<Keep, Profile, Keep>(sql, JoinCreator, new {keepId}).FirstOrDefault();
      return keep;
    }

    internal List<Keep> GetKeepsByProfileId(string profileId)
    {
      string sql = @"
      SELECT
      keeps.*,
      accounts.*
      FROM keeps
      JOIN accounts ON accounts.id = keeps.creatorId
      WHERE keeps.creatorId = @profileId
      ;";

      List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, JoinCreator, new {profileId}).ToList();
      return keeps;
    }

    private Keep JoinCreator(Keep keep, Profile profile)
  {
    keep.Creator = profile;
    return keep;
  }
}

