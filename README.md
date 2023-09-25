# Derpibooru Wallpaper
A simple wallpaper changer program for Windows (Desktop)

## Description
~~This description is partially written by ChatGPT because I don't know how to start a sentence...~~

I made this program back in March 2023 because I wanted to create a super easy way to have a fresh and unique wallpaper experience. No longer will I have to spend hours searching & downloading pony wallpapers by hand, and get bored and disappointed once a downloaded collection runs out of fresh ones I haven't used before. With this program, you and I can enjoy a constant stream of beautiful pony wallpapers without ever having to lift a finger!

## Features
- Random Wallpaper Fetching: 
Within the search result, the program will choose a random one to download and set as wallpaper.

- Interval Settings: 
You can choose how often the program updates your wallpaper. By days, hours, or minutes.

- System Startup Launch: 
This seems like an obvious feature to include for a wallpaper app...

- Change Wallpaper "Right Now":
Sometimes you may be bored and don't want to wait, or you just don't like your current wallpaper, you can just choose to change one right away. Doing so will also reset the interval timer just like how it normally does per change.
(Right-click on the taskbar icon -> `Change Wallpaper`)
> [!NOTE]
> I did not make a progress bar for this, so you may need to use the Task Manager if you want to know if the download is still running...

## Installation
Download the [latest release](https://github.com/wd357dui/Derpibooru-Wallpaper/releases/latest/download/DerpibooruWallpaper.zip), and unzip it to a folder you prefer.
> [!IMPORTANT]
> The program needs read & write access to its own folder to save settings and image files, so don't put it inside `C:/` or `Program Files` unless you are ready to set administrator privilege for it, which can still be troublesome and may break the launch-on-system-startup feature.

## How to use
- Set your API Key

Although the program **CAN** work without an API Key, it is highly recommended to use one. (Every Derpibooru account has an API key)
Once you set your API Key, the searches will apply the filter that your account is currently using.
Login on Derpibooru to [see your API key](https://derpibooru.org/registrations/edit), copy and paste the key in the program settings and click `Save`.
If you somehow need to manually type your API key (and/or if you need to see it), double-click on the text box to show the key as plain text.
> [!WARNING]
> Do not share your API key with anyone. It's just as important as your password, and Derpibooru didn't offer any option to change it according to my knowledge.

- Set search parameters

The search text works exactly the same as the one on the website. Please refer to the [Search Syntax](https://derpibooru.org/pages/search_syntax) page on Derpibooru for details.
It is recommended to add `(aspect_ratio.gte:1.6, aspect_ratio.lte:1.7)` and `(width.gte:1920, height.gte:1080)` in your search text. It is not recommended to use the `wallpaper` tag since not all wallpaper-worthy pictures have the tag `wallpaper`, it is possible that you miss out on some wonderful art if you use this tag.
Don't forget to add the `safe` tag if you don't want to include NSFW pictures as your possible wallpaper...
Type in your search text and click `Save` before closing the settings window.
> [!NOTE]
> This program changes your wallpaper, but it's not a [Wallpaper Engine](https://wikipedia.org/wiki/Wallpaper_Engine) (yet).
> So if your search result matched a video or animated GIF things will **NOT** work.
> That's why I usually add `-video`, `-sound`, and `-animated` in the search text to avoid those altogether.
> However, the thought crossed my mind to make this app Wallpaper-Engine-Capable. I have the skill to make it capable of displaying animated/video wallpapers (since I spent so many years learning DirectX and everything), but I just didn't have the time & will to do it

## Links
[Derpibooru API](https://derpibooru.org/pages/api)

## ~~Task Lists~~
> - [ ] ~~Make it a Wallpaper Engine?~~
