CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) UNIQUE COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8mb4 COMMENT '';


CREATE TABLE keeps(
  id INT PRIMARY KEY AUTO_INCREMENT,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name VARCHAR(255) NOT NULL,
  description VARCHAR(1000) NOT NULL,
  img VARCHAR(1000) NOT NULL,
  views INT UNSIGNED DEFAULT 0 NOT NULL,
  creatorId VARCHAR(255) NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
);

ALTER TABLE keeps
  DROP COLUMN kept;

CREATE TABLE vaults(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name VARCHAR(255) NOT NULL,
  description VARCHAR(1000) NOT NULL,
  img VARCHAR(1000) NOT NULL,
  isPrivate BOOLEAN NOT NULL,
  creatorId VARCHAR(255) NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
);

CREATE TABLE vaultKeeps(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  keepId INT NOT NULL,
  vaultId INt NOT NULL,
  creatorId VARCHAR(255) NOT NULL,
  FOREIGN KEY (keepId) REFERENCES keeps (id) ON DELETE CASCADE,
  FOREIGN KEY (vaultId) REFERENCES vaults (id) ON DELETE CASCADE,
  FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
);
ALTER TABLE keeps
ADD vaultKeepId INT UNSIGNED;

DROP TABLE `vaultKeeps`;


SELECT 
`vaultKeeps`.*,
keeps.*,
vaults.*,
accounts.*
FROM `vaultKeeps`
JOIN keeps ON keeps.id = vaultKeeps.`keepId`
JOIN vaults ON vaults.id = vaultKeeps.vaultId
JOIN accounts ON accounts.id = keeps.`creatorId`
WHERE `vaultKeeps`.id = 1;

SELECT
* FROM `vaultKeeps` WHERE id = 1;

ALTER TABLE vaults
DROP COLUMN description;

SELECT * From vaults;

SELECT 
        vaults.*,
        COUNT(vaultKeeps.id) AS keepCount,
        accounts.*
        FROM vaults
        JOIN accounts ON accounts.id = vaults.creatorId
        LEFT JOIN vaultKeeps ON vaultKeeps.vaultId = vaults.id
        WHERE vaults.id = 241
        GROUP BY (vaults.id)

SELECT 
        vaultKeeps.*,
        accounts.*
        FROM vaultKeeps
        JOIN accounts ON accounts.id = vaultKeeps.creatorId
        WHERE vaultKeeps.Id = 1
        ;

        SELECT
        vaults.*,
        accounts.*
        FROM vaults
        JOIN accounts ON accounts.id = vaults.creatorId
        WHERE vaults.isPrivate = false AND vaults.creatorId = '668ed565e9d5c2965d8c0059';

        SELECT 
      keeps.*,
      COUNT(vaultKeeps.id) AS keptCount,
      accounts.*
      FROM keeps
      JOIN accounts on accounts.id = keeps.creatorId
      LEFT JOIN vaultKeeps ON vaultKeeps.keepId = keeps.id
      GROUP BY(keeps.id)
      ;

      SELECT 
        vaultKeeps.*,
        accounts.*
        FROM vaultKeeps
        JOIN accounts ON accounts.id = vaultKeeps.creatorId
        WHERE vaultKeeps.id = 288
        ;

        SELECT * FROM `vaultKeeps`;