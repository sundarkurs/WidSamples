using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontifyApiConsumer.Models
{
    public class FrontifyCreateAsset
    {
        public CreateAssetData Data { get; set; }
        public Createasset CreateAsset { get; set; }
    }

    public class CreateAssetData
    {
        public Createasset CreateAsset { get; set; }
    }

    public class Createasset
    {
        public Job Job { get; set; }
    }

    public class Job
    {
        public string AssetId { get; set; }
    }
}
