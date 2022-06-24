using Didenko.Networking;
using Didenko.VideoAds.Cache;
using Didenko.VideoAds.Networking.Data;
using RSG;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Video;

namespace Didenko.VideoAds.Video
{
    public class VideoManager : MonoBehaviour
    {
        private VideoController videoController;
        private WebRequestSender webRequestSender;
        private FileCacher fileCacher;

        private void Awake()
        {
            videoController = new VideoController();
            webRequestSender = new WebRequestSender();
            fileCacher = new FileCacher();
        }

        public IPromise PlayVastVideo(string apiUrl, VideoPlayer videoPlayer)
        {
            var promise = new Promise();

            webRequestSender.GetXmlRequest<VAST>(apiUrl)
                .Then(result =>
                {
                    var mediaFile = result.Ad.InLine.Creatives.Creative.Linear.MediaFiles.MediaFile;
                    promise = (Promise)PlayUrlVideo(mediaFile, videoPlayer);
                })
                .Catch(promise.Reject);

            return promise;
        }

        public IPromise PlayUrlVideo(string videoUrl, VideoPlayer videoPlayer)
        {
            var promise = new Promise();
            var videoFile = new VideoFile(VideoFile.GenerateName(videoUrl));

            if (File.Exists(videoFile.FileFullPath))
                promise = (Promise)PlayVideoFile(videoFile, videoPlayer);
            else
            {
                DownloadVideo(videoUrl, videoFile)
                    .Then(() => 
                    {
                        promise = (Promise)PlayVideoFile(videoFile, videoPlayer);
                    })
                    .Catch(promise.Reject);
            }

            return promise;
        }

        public IPromise PlayVideoFile(VideoFile videoFile, VideoPlayer videoPlayer)
        {
            return videoController.PlayVideo(videoFile, videoPlayer);
        }

        private IPromise DownloadVideo(string videoUrl, VideoFile videoFile)
        {
            var promise = new Promise();

            webRequestSender.GetRequest(videoUrl)
                .Then(handler =>
                {
                    CacheVideo(videoFile, handler);
                })
                .Catch(promise.Reject);

            return promise;
        }

        private IPromise CacheVideo(VideoFile videoFile, DownloadHandler handler)
        {
            return fileCacher.CacheFile(videoFile, handler.data);
        }
    }
}
