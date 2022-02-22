using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontifyApiConsumer.Models
{
    public class ProjectAssets
    {
        public ProjectAssetsData Data { get; set; }
        public Project Project { get; set; }
    }

    public class ProjectAssetsData
    {
        public Project Project { get; set; }
    }

    public class Project
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int AssetCount { get; set; }
        public Assets Assets { get; set; }
    }

    public class Assets
    {
        public int Page { get; set; }
        public int Total { get; set; }
        public bool HasNextPage { get; set; }
        public Item[] Items { get; set; }
    }

    public class Item
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public object Description { get; set; }
        public string Type { get; set; }
        public string Filename { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string DownloadUrl { get; set; }
        public Tag[] Tags { get; set; }
    }

    public class Tag
    {
        public string Source { get; set; }
        public string Value { get; set; }
    }
}
