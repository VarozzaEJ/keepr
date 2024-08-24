



namespace keepr.Services;

public class VaultsRepository 
{
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Vault CreateVault(Vault vaultData)
    {
        string sql = @"
        INSERT INTO
        vaults(name, description, img, isPrivate, creatorId)
        VALUES(@Name, @Description, @Img, @IsPrivate, @CreatorId);

        SELECT 
        vaults.*,
        accounts.*
        FROM vaults
        JOIN accounts ON accounts.id = vaults.CreatorId
        WHERE vaults.id = LAST_INSERT_ID()
        ;";

        Vault vault = _db.Query<Vault, Profile, Vault>(sql, JoinCreator, vaultData).FirstOrDefault();
        return vault;
    }

    internal void DeleteVault(int vaultId)
    {
        string sql =@"
        DELETE FROM vaults where id = @vaultId LIMIT 1
        ;";

        _db.Execute(sql, new {vaultId});
    }

    internal Vault GetVaultById(int vaultId)
    {
        string sql = @"
        SELECT 
        vaults.*,
        accounts.*

        FROM vaults
        JOIN accounts ON accounts.id = vaults.creatorId

        WHERE vaults.id = @vaultId
        ;";

        Vault vault = _db.Query<Vault, Profile, Vault>(sql, JoinCreator, new {vaultId}).FirstOrDefault();
        return vault;
    }

    internal Vault UpdateVault(Vault vaultToUpdate)
    {
        string sql = @"
        UPDATE vaults
        SET
        img = @Img,
        description = @Description,
        isPrivate = @IsPrivate,
        name = @Name
        WHERE id = @Id LIMIT 1;

        SELECT 
        vaults.*,
        accounts.*
        FROM 
        vaults
        JOIN accounts ON accounts.id = vaults.creatorId
        WHERE vaults.id = @Id
        ;";

        Vault vault = _db.Query<Vault, Profile, Vault>(sql, JoinCreator, vaultToUpdate).FirstOrDefault();
        return vault;
    }

    private Vault JoinCreator(Vault vault, Profile profile)
  {
    vault.Creator = profile;
    return vault;
  }
}