using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.HttpSys;

namespace dungeon_noter.Services;

public class NotebooksService
{

  private readonly NotebooksRepository _repo;

    public NotebooksService(NotebooksRepository repo)
    {
        _repo = repo;
    }

    internal Notebook CreateNotebook(Notebook notebookData)
    {
      Notebook notebook = _repo.CreateNotebook(notebookData);
      return notebook;
    }

    internal List<Notebook> GetNotebooks(){
      List<Notebook> notebooks = _repo.GetNotebooks();
      return notebooks;
    }

    internal Notebook GetNotebookById(string notebookId){
      Notebook notebook = _repo.GetNoteBookById(notebookId);
      if(notebook == null){
        throw new Exception($"no notebook at id: {notebookId}");
      }
        return notebook;
    }

    internal Notebook UpdateNotebook(Notebook notebookData, string userId){
      Notebook original = GetNotebookById(notebookData.Id);
      if(original.CreatorId != userId) throw new HttpRequestException("Not Authorized to edit", null, HttpStatusCode.Forbidden);

      original.Title = notebookData.Title ?? original.Title;
      original.Icon = notebookData.Icon ?? original.Icon;
      original.Color = notebookData.Color ?? original.Color;
      original.CoverImg = notebookData.CoverImg ?? original.CoverImg;

      Notebook update = _repo.UpdateNotebook(original);
      return update;
    }

    internal string DeleteNotebook(string notebookId, string userId){
      Notebook original = GetNotebookById(notebookId);
      if(original.CreatorId != userId) throw new HttpRequestException("Not Authorized to delete", null, HttpStatusCode.Forbidden);

      _repo.DeleteNotebook(notebookId);
      return $"{original.Title} was deleted.";
    }

    internal List<Notebook> GetUserNotebooks(string id)
    {
           List<Notebook> notebooks = _repo.GetUserNotebooks(id);
      return notebooks;
    }
}

