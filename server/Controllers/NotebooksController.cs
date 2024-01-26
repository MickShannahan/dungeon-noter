using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Services;

namespace dungeon_noter.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotebooksController : ControllerBase
{
private readonly NotebooksService _notebooksService;
private readonly Auth0Provider _auth0;

private readonly EntriesService _entriesService;

    public NotebooksController(NotebooksService notebooksService, Auth0Provider auth0, EntriesService entriesService)
    {
        _notebooksService = notebooksService;
        _entriesService = entriesService;
        _auth0 = auth0;
    }


  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Notebook>> CreateNotebook([FromBody] Notebook notebookData){
    try
    {
      Account user = await _auth0.GetUserInfoAsync<Account>(HttpContext);
      notebookData.CreatorId = user.Id;
      Notebook notebook = _notebooksService.CreateNotebook(notebookData);
      return Ok(notebook);
    } catch (HttpRequestException e)
    {
      return StatusCode((int)e.StatusCode, e.Message ); // Catches our Custom Errors,
    } catch (Exception e){
      return BadRequest(e.Message); // Catches system errors
    }
  }

  [HttpGet]
  [Authorize]
  public async Task< ActionResult<List<Notebook>>> GetUserNotebooks(){
    try
    {
      Account user = await _auth0.GetUserInfoAsync<Account>(HttpContext);
      List<Notebook> notebooks = _notebooksService.GetUserNotebooks(user.Id);
      return Ok(notebooks);
    } catch (HttpRequestException e)
    {
      return StatusCode((int)e.StatusCode, e.Message ); // Catches our Custom Errors,
    } catch (Exception e){
      return BadRequest(e.Message); // Catches system errors
    }
  }

  [HttpGet("{notebookId}")]
  public ActionResult<Notebook> GetNoteBookById(string notebookId){
    try
    {
      Notebook notebook = _notebooksService.GetNotebookById(notebookId);
      return Ok(notebook);
    } catch (HttpRequestException e)
    {
      return StatusCode((int)e.StatusCode, e.Message ); // Catches our Custom Errors,
    } catch (Exception e){
      return BadRequest(e.Message); // Catches system errors
    }
  }

  [HttpGet("{notebookId}/entries")]
  public ActionResult<List<Entry>> GetNotebookEntries(string notebookId){
        try
    {
      List<Entry> entries = _entriesService.GetNotebookEntries(notebookId);
      return Ok(entries);
    } catch (HttpRequestException e)
    {
      return StatusCode((int)e.StatusCode, e.Message ); // Catches our Custom Errors,
    } catch (Exception e){
      return BadRequest(e.Message); // Catches system errors
    }
  }

  [HttpPut("{notebookId}")]
  [Authorize]
  public async Task<ActionResult<Notebook>> UpdateNotebook([FromBody]Notebook update, string notebookId){
    try
    {
      Account user = await _auth0.GetUserInfoAsync<Account>(HttpContext);
      update.Id = notebookId;
      Notebook notebook = _notebooksService.UpdateNotebook(update, user.Id);
      return Ok(notebook);

    } catch (HttpRequestException e){
      return StatusCode((int)e.StatusCode, e.Message ); // Catches our Custom Errors,
    } catch (Exception e){
      return BadRequest(e.Message); // Catches system errors
    }
  }

  [HttpDelete("{notebookId}")]
  [Authorize]
  public async Task<ActionResult<string>> DeleteNotebook(string notebookId){
    try
    {
      Account user = await _auth0.GetUserInfoAsync<Account>(HttpContext);
      string message = _notebooksService.DeleteNotebook(notebookId, user.Id);
      return Ok(message);
    } 
    catch (HttpRequestException e){
      return StatusCode((int)e.StatusCode, e.Message ); // Catches our Custom Errors,
    } catch (Exception e){
      return BadRequest(e.Message); // Catches system errors
    }
  }


}
