using Didenko.VideoAds.Cache;
using Didenko.Extensions;

namespace Didenko.VideoAds.Video
{
    public class VideoFile : CachebleFile
    {
        public VideoFile(string fileName) : base(fileName)
        {
        }

        public override string Subfolder => "Video";

        public override string Extension => ".webm";

        public static string GenerateName(string videoUrl) => videoUrl.GetHashString();
    }
}