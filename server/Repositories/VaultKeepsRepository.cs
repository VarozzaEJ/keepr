


namespace keepr.Repositories;

public class VaultKeepsRepository
{
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
    {
        string sql = @"
        INSERT INTO 
        vaultKeeps(keepId, vaultId, creatorId)
        VALUES(@KeepId, @VaultId, @CreatorId );

        SELECT
        vaultKeeps.*,
        vaults.*,
        keeps.*
        FROM vaultKeeps
        JOIN vaults ON vaults.Id = vaultKeeps.VaultId
        JOIN keeps ON keeps.Id = vaultKeeps.KeepId
        WHERE vaultKeeps.Id = LAST_INSERT_ID()
        ;";

        VaultKeep vaultKeep = _db.Query<VaultKeep, Vault, Keep, VaultKeep>(sql, (vaultKeep, vault, keep) => {
            vaultKeep.VaultId = vault.Id;
            vaultKeep.KeepId = keep.Id;
            return vaultKeep;
        }, vaultKeepData).FirstOrDefault();
        return vaultKeep;
    }

    internal void DeleteVaultKeep(int vaultKeepId)
    {
        string sql = @"
        DELETE FROM vaultKeeps where id = @vaultKeepId LIMIT 1
        ;";

        _db.Execute(sql, new {vaultKeepId});
    }

    internal VaultKeep GetVaultKeepById(int vaultKeepId)
    {
        string sql = @"
        SELECT 
        vaultKeeps.*,
        accounts.*
        FROM vaultKeeps
        JOIN accounts ON accounts.id = vaultKeeps.creatorId
        WHERE vaultKeeps.id = @vaultKeepId
        ;";

        VaultKeep vaultKeep = _db.Query<VaultKeep, Profile, VaultKeep>(sql, (vaultKeep, profile) => {
            vaultKeep.CreatorId = profile.Id;
            return vaultKeep;
        } ,new {vaultKeepId}).FirstOrDefault();

        return vaultKeep;
    }
}