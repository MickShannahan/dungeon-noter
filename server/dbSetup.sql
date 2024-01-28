CREATE TABLE IF NOT EXISTS dungeon_noter_accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT 'utf8mb4';


CREATE TABLE notebooks(
  id VARCHAR(36) NOT NULL PRIMARY KEY,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  title VARCHAR(300) NOT NULL,
  CoverImg VARCHAR(500) NOT NULL,
  icon VARCHAR(50) NOT NULL,
  color VARCHAR(10) NOT NULL,
  creatorId VARCHAR(255) NOT NULL,
  Foreign Key (creatorId) REFERENCES dungeon_noter_accounts (id) ON DELETE CASCADE
)default charset utf8 COMMENT 'utf8mb4';

CREATE TABLE entries(
  id VARCHAR(36) NOT NULL PRIMARY KEY,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  description VARCHAR(3000),
  img VARCHAR(500),
  notebookId VARCHAR(36),
  creatorId VARCHAR(255) NOT NULL
)default charset utf8 COMMENT 'utf8mb4';

SELECT
    notebooks.*,
    COUNT(entries.id) AS entryCount,
    accounts.*
  FROM notebooks
  JOIN accounts ON accounts.id = notebooks.creatorId
  LEFT JOIN entries ON notebooks.id = entries.notebookId
  WHERE notebooks.id = LAST_INSERT_ID()
  GROUP BY(notebooks.id);

DELETE FROM notebooks WHERE id = 1;
