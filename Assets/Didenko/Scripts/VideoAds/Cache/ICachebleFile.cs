namespace Didenko.VideoAds.Cache
{
    public interface ICachebleFile
    {
        string FileName { get; set; }

        string Subfolder { get; }

        string FileFullPath { get; }

        string FileDirectory { get; }

        string Extension { get; }
    }
}