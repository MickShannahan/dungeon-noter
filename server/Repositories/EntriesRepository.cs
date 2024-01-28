using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dungeon_noter.Repositories;

public class EntriesRepository(IDbConnection db)
{
   private readonly IDbConnection _db = db;

    internal Entry CreateEntry(Entry entryData)
    {
      string sql = @"
      INSERT INTO entries
      (description, img, notebookId, creatorId, id)
      VALUES
      (@description, @img, @notebookId, @creatorId, @id);

      SELECT
      e.*,
      nb.*
      FROM entries e 
      LEFT JOIN notebooks nb ON nb.id = e.notebookId
      WHERE e.id = @id;
      ";
      Entry entry = _db.Query<Entry, Notebook, Entry>(sql, populateNotebook, entryData).FirstOrDefault();
      return entry;
    }

    internal int DeleteEntry(string entryId)
    {
      string sql = @"DELETE from entries WHERE id = @entryId;";
      int rows = _db.Execute(sql, new{entryId});
      return rows;
    }

    internal Entry GetEntryById(string entryId)
    {
      string sql = @" 
      SELECT
      e.*,
      nb.*
      FROM entries e 
      LEFT JOIN notebooks nb ON nb.id = e.notebookId
      WHERE e.id = @entryId;";

      Entry entry = _db.Query<Entry, Notebook, Entry>(sql, populateNotebook, new{entryId}).FirstOrDefault();
      return entry;
    }

    internal List<Entry> GetUserEntries(string userId)
    {
      string sql = @" 
      SELECT
      e.*,
      nb.*
      FROM entries e 
      LEFT JOIN notebooks nb ON nb.id = e.notebookId
      WHERE e.creatorId = @userId
      ORDER BY e.updatedAt DESC;";

      List<Entry> entries = _db.Query<Entry, Notebook, Entry>(sql, populateNotebook, new{userId}).ToList();
      return entries;
    }

    internal Entry UpdateEntry(Entry update)
    {
      string sql = @"
      UPDATE entries SET
        description = @description,
        img = @img,
        notebookId = @notebookId
      WHERE id = @id;

      SELECT
        e.*,
        nb.*
      FROM entries e 
      LEFT JOIN notebooks nb ON nb.id = e.notebookId
      WHERE e.id = @id;
      ";

      Entry entry = _db.Query<Entry, Notebook, Entry>(sql, populateNotebook, update).FirstOrDefault();
      return entry;
    }

    internal Entry populateNotebook(Entry entry, Notebook notebook){
      entry.Notebook = notebook;
      return entry;
    }

    internal List<Entry> GetNotebookEntries(string notebookId)
    {
        string sql = @" 
      SELECT
        e.*,
        nb.*
      FROM entries e 
      LEFT JOIN notebooks nb ON nb.id = e.notebookId
      WHERE e.notebookId = @notebookId
      ORDER BY e.updatedAT DESC;";

      List<Entry> entries = _db.Query<Entry, Notebook, Entry>(sql, populateNotebook, new{notebookId}).ToList();
      return entries;
    }
}
