## Overview
This project is SDK for playing video ads and displaying a simple interface for making purchases

## Libraries used in the project
[RSG.C-Sharp-Promise](https://github.com/Real-Serious-Games/C-Sharp-Promise)

[Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json)

# Instalation
You can get the code by cloning the github repository. You can do this in a UI like SourceTree or you can do it from the command line as follows:

```git clone https://github.com/Apanyold/SayolloTestTask.git```

# Video Ads

### Overview:
A tool for playing video ads on the screen, with its caching and automatic closing at the end of the video.

For video playback, the VideoManager prefab is used. With it, you can play videos on any VideoPlayer. The video will be automatically downloaded and cached on the device.

**VideoManager** has the following public methods:
```c#
public PlayVastVideo(string apiUrl, VideoPlayer videoPlayer)
```
>Makes a get request for the specified apiUrl, passing the video download url to the PlayUrlVideo(string videoUrl, VideoPlayer videoPlayer) method
```c#
public PlayUrlVideo(string videoUrl, VideoPlayer videoPlayer)
```
>Downloads and caches video by passing playback to the PlayVideoFile(VideoFile videoFile, VideoPlayer videoPlayer) method
```c#
public PlayVideoFile(VideoFile videoFile, VideoPlayer videoPlayer)
```
>Accesses the VideoController instance that reads the video from the device along the path contained in VideoFile.FileFullPath

Since these methods return the **IPromise**, it is always possible to understand whether the execution of the method was successful or not, without additional checks. For example:
```c#
videoManager.PlayVastVideo(videoApiUrl, videoPlayer)
  .Then(() =>
  {
    //what should happen during successful video playback
  })
  .Catch(exception =>
  {
    // error handling in case of unsuccessful video playback
  });
```
On the demo scene ``VideoAdsDemoScene`` in folder ```Assets/Scenes/VideoAds```, you can see an example of using SDK in the ```DemoAdsVideo``` class

# Purchase
### Overview:
Tool for displaying the purchased product and sending user-filled credit information

The main components that are used in the SKD for making purchases:

 - ```PurchaseRequestSender``` prefab that uses the script of the same name is located along the path ```Assets/Prefabs/Purchase```. 
Used to make API requests.

![image](https://user-images.githubusercontent.com/51063161/175549706-b7f0a603-e036-46b8-8865-d6e0f65b60a5.png)

**PurchaseRequestSender** has the following public methods:

```c# 
OnItemRequested(string json) 
```
> Uses api data from ```PurchaseConfig``` and returns information about the items that should be in the store

```c# 
OnPurchaseRequested(string json) 
```
> Uses api data from ```PurchaseConfig``` and sends user's credit information data in json

```c# 
OnImageRequested(string url) 
```
> Downloads the image from the specified url and returns it as a sprite

This class uses ```PurchaseConfig``` to create api requests to get product information and send payment information.

![image](https://user-images.githubusercontent.com/51063161/175549757-138cf3cc-e897-4da8-b99b-1b269fed1eb0.png)

- ```PurchaseUI``` prefab that uses ```PurchaseUIManager``` script to visualize the buying process. Located in folder ```Assets/Prefabs/Purchase```.

![image](https://user-images.githubusercontent.com/51063161/175550701-800bb76f-3a47-45dd-8213-ac3a32ea9651.png)

For the correct operation of this component, it must first be initialized using the method ```Init(...)```. An example of usage can be seen on the demo scene

This prefab contains the following 3 windows:
1. Purchase start window, which contains a button to start working with the store

![image](https://user-images.githubusercontent.com/51063161/175552550-cd8d1f2d-1365-45fd-8f6c-eb34b2aeb0c3.png)

2. Shop window that contains products

![image](https://user-images.githubusercontent.com/51063161/175552451-98d06722-2158-4033-b698-a24d6a9404f1.png)

3. The purchase window, which contains fields for filling in credit information. This window will not send payment information without filling in all required fields

![image](https://user-images.githubusercontent.com/51063161/175552177-60155903-0975-4830-a244-59c721d83e9e.png)

On the demo scene, you can see an example of the use and interaction between the ```PurchaseUI``` and ```PurchaseConfig``` classes in the ```DemoPurchaseManager``` class. Demo scene ```PurchaseDemoScene``` is in the following path ```Assets/Scenes/Purchase```
