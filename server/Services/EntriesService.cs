

using System.Net.Http;

namespace server.Services;

public class EntriesService (EntriesRepository repo, NotebooksService notebooksService)
{
    private readonly EntriesRepository _repo = repo;
    private readonly NotebooksService _notebooksService = notebooksService;

    internal Entry CreateEntry(Entry entryData)
    {

      if(entryData.NotebookId != "" && entryData.NotebookId != null){
        Notebook toNotebook = _notebooksService.GetNotebookById(entryData.NotebookId);
        if(toNotebook.CreatorId != entryData.CreatorId) throw new HttpRequestException("Not Authorized to Post to this notebook", null, HttpStatusCode.Forbidden); 
      }

      Entry entry = _repo.CreateEntry(entryData);

      return entry;
    }

    internal string DeleteEntry(string entryId, string userId)
    {
      Entry original = _repo.GetEntryById(entryId);
      if(original.CreatorId != userId) throw new HttpRequestException("Not Authorized to Delete", null, HttpStatusCode.Forbidden);

      _repo.DeleteEntry(entryId);
      return $"Entry Deleted";
    }

    internal List<Entry> GetNotebookEntries(string notebookId)
    {
      List<Entry> entries = _repo.GetNotebookEntries(notebookId);
      return entries;
    }

    internal List<Entry> GetUserEntries(string userId)
    {
      List<Entry> entries = _repo.GetUserEntries(userId);
      return entries;
    }

    internal Entry UpdateEntry(Entry update, string userId)
    {
      Entry original = _repo.GetEntryById(update.Id);
      if(original.CreatorId != userId) throw new HttpRequestException("Not Authorized to delete Entry", null, HttpStatusCode.Forbidden); 

      original.Description = update.Description ?? original.Description;
      original.Img = update.Img ?? original.Img;
      original.NotebookId = update.NotebookId ?? original.NotebookId;

      if(original.NotebookId != "" && original.NotebookId != null){
        Notebook toNotebook = _notebooksService.GetNotebookById(original.NotebookId);
        if(toNotebook.CreatorId != userId) throw new HttpRequestException("Not Authorized to Move to this notebook", null, HttpStatusCode.Forbidden);  
      }

      Entry entry =_repo.UpdateEntry(original);
      return entry;
    }
}
