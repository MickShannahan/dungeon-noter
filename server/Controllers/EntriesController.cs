using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Services;

namespace dungeon_noter.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EntriesController(EntriesService entriesService, Auth0Provider auth) : ControllerBase
{
  private readonly EntriesService _entriesService = entriesService;
  private readonly Auth0Provider _auth = auth;

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Entry>> CreateEntry([FromBody] Entry entryData){
    try
    {
      Account user = await _auth.GetUserInfoAsync<Account>(HttpContext);
      entryData.CreatorId = user.Id;
      Entry entry = _entriesService.CreateEntry(entryData);
      return Ok(entry);
    }
    catch (HttpRequestException e)
    {
      return StatusCode((int)e.StatusCode, e.Message ); // Catches our Custom Errors,
    } catch (Exception e){
      return BadRequest(e.Message); // Catches system errors
    }
  }

  [HttpGet]
  [Authorize]
  public async Task<ActionResult<List<Entry>>> GetUserEntries(){
    try
    {
      Account user = await _auth.GetUserInfoAsync<Account>(HttpContext);
      List<Entry> entries = _entriesService.GetUserEntries(user.Id);
      return Ok(entries);
    }
     catch (HttpRequestException e)
    {
      return StatusCode((int)e.StatusCode, e.Message ); // Catches our Custom Errors,
    } catch (Exception e){
      return BadRequest(e.Message); // Catches system errors
    }
  }

  [HttpPut("{entryId}")]
  [Authorize]
  public async Task<ActionResult<Entry>> UpdateEntry([FromBody] Entry update, string entryId){
    try
    {
      Account user = await _auth.GetUserInfoAsync<Account>(HttpContext);
      update.Id = entryId;
      Entry entry = _entriesService.UpdateEntry(update, user.Id);
      return Ok(entry);
    }
     catch (HttpRequestException e)
    {
      return StatusCode((int)e.StatusCode, e.Message ); // Catches our Custom Errors,
    } catch (Exception e){
      return BadRequest(e.Message); // Catches system errors
    }
  }

  [HttpDelete("{entryId}")]
  [Authorize]
  public async Task<ActionResult<string>> DeleteEntry(string entryId){
        try
    {
      Account user = await _auth.GetUserInfoAsync<Account>(HttpContext);
      string message = _entriesService.DeleteEntry(entryId, user.Id);
      return Ok(message);
    }
     catch (HttpRequestException e)
    {
      return StatusCode((int)e.StatusCode, e.Message ); // Catches our Custom Errors,
    } catch (Exception e){
      return BadRequest(e.Message); // Catches system errors
    }
  }
}
