using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dungeon_noter.Models;

public class Notebook : RepoItem
{
  public string Title { get; set; }
  public string Icon { get; set; }
  public string Color { get; set; } = "#ffffff";
  public string CoverImg { get; set; }
  public int EntryCount { get; set; }
  public string CreatorId { get; set; }
  public Profile Creator { get; set; }

}
