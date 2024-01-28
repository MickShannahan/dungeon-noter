using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dungeon_noter.Repositories;

public class NotebooksRepository(IDbConnection db)
{
    private readonly IDbConnection _db = db;

    internal Notebook CreateNotebook(Notebook notebookData)
    {
      string sql = @"
        INSERT INTO notebooks
        (title, icon, color, coverImg, creatorId, id)
        VALUES
        (@title, @icon, @color, @coverImg, @creatorId, @id);
        
        SELECT
          notebooks.*,
          COUNT(entries.id) AS entryCount,
          dungeon_noter_accounts.*
        from notebooks
        JOIN dungeon_noter_accounts ON dungeon_noter_accounts.id = notebooks.creatorId
        LEFT JOIN entries ON notebooks.id = entries.notebookId
        WHERE notebooks.id = @id
        GROUP BY(notebooks.id)
      ;";
      Notebook notebook = _db.Query<Notebook, Profile, Notebook>(sql, populateCreator, notebookData).FirstOrDefault();
      return notebook;
    }

    internal List<Notebook> GetNotebooks(){
      string sql = @"
         SELECT
          notebooks.*,
          COUNT(entries.id) AS entryCount,
          dungeon_noter_accounts.*
        from notebooks
        JOIN dungeon_noter_accounts ON dungeon_noter_accounts.id = notebooks.creatorId
        LEFT JOIN entries ON notebooks.id = entries.notebookId
        GROUP BY(notebooks.id)
      ;";
      List<Notebook> notebooks = _db.Query<Notebook, Profile, Notebook>(sql, populateCreator).ToList();
      return notebooks;
    }

    internal Notebook GetNoteBookById(string notebookId){
      string sql = @"
        SELECT
          notebooks.*,
          COUNT(entries.id) AS entryCount,
          dungeon_noter_accounts.*
        from notebooks
        JOIN dungeon_noter_accounts ON dungeon_noter_accounts.id = notebooks.creatorId
        LEFT JOIN entries ON notebooks.id = entries.notebookId
        WHERE notebooks.id = @notebookId
        GROUP BY(notebooks.id)
      ;";
      Notebook notebook = _db.Query<Notebook, Profile, Notebook>(sql, populateCreator, new{notebookId}).FirstOrDefault();
      return notebook;
    }

    internal Notebook UpdateNotebook(Notebook notebookData){
      string sql = @"
      UPDATE notebooks SET
      title = @title,
      icon = @icon,
      color = @color,
      coverImg = @coverImg
      WHERE id = @id;

      SELECT
        notebooks.*,
        COUNT(entries.id) AS entryCount,
        dungeon_noter_accounts.*
      from notebooks
      JOIN dungeon_noter_accounts ON dungeon_noter_accounts.id = notebooks.creatorId
      LEFT JOIN entries ON notebooks.id = entries.notebookId
      WHERE notebooks.id = @id
      GROUP BY(notebooks.id)
      ";
      Notebook notebook = _db.Query<Notebook, Profile, Notebook>(sql, populateCreator, notebookData).FirstOrDefault();
      return notebook;
    }

    internal int DeleteNotebook(string notebookId){
      string sql = @"
      DELETE FROM notebooks
      WHERE id = @notebookId LIMIT 1;
      ";

      int rowsAffected = _db.Execute(sql, new{notebookId});
      return rowsAffected;
    }

    internal List<Notebook> GetUserNotebooks(string userId)
    {
         string sql = @"
         SELECT
          notebooks.*,
          COUNT(entries.id) AS entryCount,
          dungeon_noter_accounts.*
        from notebooks
        JOIN dungeon_noter_accounts ON dungeon_noter_accounts.id = notebooks.creatorId
        LEFT JOIN entries ON notebooks.id = entries.notebookId
        WHERE notebooks.creatorId = @userId
        GROUP BY(notebooks.id)
      ;";
      List<Notebook> notebooks = _db.Query<Notebook, Profile, Notebook>(sql, populateCreator, new {userId}).ToList();
      return notebooks;
    }
internal Notebook populateCreator(Notebook notebook, Profile creator){
  notebook.Creator = creator;
  return notebook;
}
}

