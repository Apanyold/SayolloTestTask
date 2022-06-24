using System;
using UnityEngine;
using System.IO;

namespace Didenko.VideoAds.Cache
{
    public abstract class CachebleFile : ICachebleFile
    {
        public abstract string Subfolder { get; }
        public abstract string Extension { get; }

        public string FileFullPath => Path.Combine(Application.persistentDataPath, Subfolder, fileName + Extension);

        public string FileName { get => fileName; set => fileName = value; }

        public string FileDirectory => Path.Combine(Application.persistentDataPath, Subfolder);


        private string fileName;

        protected CachebleFile(string fileName)
        {
            this.fileName = fileName;
        }
    }
}