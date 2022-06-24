using Didenko.Networking;
using Didenko.VideoAds.Cache;
using Didenko.VideoAds.Networking.Data;
using RSG;
using System.IO;
using UnityEngine;
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

        public IPromise PlayVastVideo(string vastXmlUrl, VideoPlayer videoPlayer)
        {
            var promise = new Promise();

            webRequestSender.GetXmlRequest<VAST>(vastXmlUrl)
                .Then(result =>
                {
                    promise = (Promise)PlayUrlVideo(result.Ad.InLine.Creatives.Creative.Linear.MediaFiles.MediaFile, videoPlayer);
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
                webRequestSender.GetRequest(videoUrl)
                    .Then(handler =>
                    {
                        fileCacher.CacheFile(videoFile, handler.data)
                        .Then(() =>
                        {
                            Debug.Log("File cached");
                            promise = (Promise)PlayVideoFile(videoFile, videoPlayer);
                        })
                        .Catch(promise.Reject);
                    })
                    .Catch(promise.Reject);
            }

            return promise;
        }

        public IPromise PlayVideoFile(VideoFile videoFile, VideoPlayer videoPlayer)
        {
            return videoController.PlayVideo(videoFile, videoPlayer);
        }
    }
}
