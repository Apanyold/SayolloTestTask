﻿using Didenko.Extensions;
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

        [SerializeField]
        private string XmlVastUrl;

        [SerializeField]
        private VideoPlayer videoPlayer;

        public void PlayVideoButtonPressed()
        {
            videoManager.PlayVastVideo(XmlVastUrl, videoPlayer);
        }
    }
}