using System;
using System.IO;
using Didenko.Extensions;
using RSG;
using UnityEngine;

namespace Didenko.VideoAds.Cache
{
    public class FileCacher
    {
        public IPromise CacheFile(ICachebleFile cachebleFile, byte[] data)
        {
            var promise = new Promise();

            if (!Directory.Exists(cachebleFile.FileDirectory))
                Directory.CreateDirectory(cachebleFile.FileDirectory);
            try
            {
                using (FileStream fstream = new FileStream(cachebleFile.FileFullPath, FileMode.OpenOrCreate))
                {
                    fstream.Write(data, 0, data.Length);
                    promise.Resolve();
                }
            }
            catch (Exception e)
            {
                promise.LogReject(e);
            }

            return promise;
        }
    }
}