using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceTracker.Shared.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Label> Labels { get; set; }
    }
}
