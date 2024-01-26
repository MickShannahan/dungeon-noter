using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dungeon_noter.Models;

public class RepoItem
{
  public string Id { get; set; } = Guid.NewGuid().ToString();
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}
