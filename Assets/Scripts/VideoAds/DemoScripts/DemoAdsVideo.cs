using Didenko.Extensions;
using Didenko.VideoAds.Networking.Data;
using Didenko.VideoAds.Video;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Video;

namespace Didenko.VideoAds.Demo
{
    public class DemoAdsVideo : MonoBehaviour
    {
        [SerializeField]
        private VideoManager videoManager;

        [Header("Api url")]
        [SerializeField]
        private string videoApiUrl;

        [Space]
        [SerializeField]
        private VideoPlayer videoPlayer;

        public void PlayVideoButtonPressed()
        {
            videoManager.PlayVastVideo(videoApiUrl, videoPlayer);
        }
    }
}