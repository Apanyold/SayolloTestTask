using Didenko.Extensions;
using RSG;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.Video;

namespace Didenko.VideoAds.Video
{
    public class VideoController
    {
        private VideoPlayer videoPlayer;

        public IPromise PlayVideo(VideoFile videoFile, VideoPlayer player)
        {
            videoPlayer = player;

            Init();

            var promise = new Promise();

            if (!File.Exists(videoFile.FileFullPath))
            {
                promise.LogReject(new Exception("File dosen't exists"));
                return promise;
            }

            videoPlayer.url = videoFile.FileFullPath;
            videoPlayer.Prepare();

            SubscribePromise(promise);

            return promise;

            void SubscribePromise(Promise _promise)
            {
                VideoPlayer.ErrorEventHandler errorHandler = null;
                errorHandler = (source, message) =>
                {
                    _promise.LogReject(new Exception(message));
                    source.errorReceived -= errorHandler;
                };

                videoPlayer.errorReceived += errorHandler;

                VideoPlayer.EventHandler startHandler = null;
                startHandler = (source) =>
                {
                    source.started -= startHandler;
                    _promise.Resolve();
                };

                videoPlayer.started += startHandler;
            }
        }

        private void Init()
        {
            videoPlayer.isLooping = true;

            Subscribe();
        }

        private void Subscribe()
        {
            videoPlayer.prepareCompleted += VideoPlayer_prepareCompleted;
            videoPlayer.loopPointReached += VideoPlayer_loopPointReached;
        }

        private void Unsubscribe()
        {
            videoPlayer.prepareCompleted -= VideoPlayer_prepareCompleted;
            videoPlayer.loopPointReached -= VideoPlayer_loopPointReached;
        }

        private void VideoPlayer_loopPointReached(VideoPlayer source)
        {
            source.Stop();

            Unsubscribe();
        }

        private void VideoPlayer_prepareCompleted(VideoPlayer source)
        {
            source.Play();

            Debug.Log("Video playing");
        }
    }
}