using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dungeon_noter.Models;

public class Entry : RepoItem
{
    public string Description { get; set; } = "";
    public string Img { get; set; }
    public string NotebookId { get; set; }
    public string CreatorId { get; set; }

    public Notebook Notebook { get; set; }
}
