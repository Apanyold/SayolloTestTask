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

            SubscribeToVideoEventHandlers(promise, player);

            return promise;
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

        private void SubscribeToVideoEventHandlers(Promise promise, VideoPlayer player)
        {
            ErrorHandlerSubscription(promise, player);
            StartHandlerSubscription(promise, player);
        }

        private void StartHandlerSubscription(Promise promise, VideoPlayer player)
        {
            VideoPlayer.EventHandler startHandler = null;
            startHandler = (source) =>
            {
                promise.Resolve();
                source.started -= startHandler;
            };

            player.started += startHandler;
        }

        private void ErrorHandlerSubscription(Promise promise, VideoPlayer player)
        {
            VideoPlayer.ErrorEventHandler errorHandler = null;
            errorHandler = (source, message) =>
            {
                promise.LogReject(new Exception(message));
                source.errorReceived -= errorHandler;
            };

            player.errorReceived += errorHandler;
        }
    }
}