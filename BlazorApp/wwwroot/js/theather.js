"use strict";

var IsFirstVideo = true;

function GetVideoElement() {

	return document.getElementById("video");
}

function GetSourceElement() {

	return document.getElementById("video-source");
}

function GetVideoPlayer() {

	return {
		Element: GetVideoElement(),
		Source: GetSourceElement(),
	};
}

document.addEventListener("DOMContentLoaded", function (event) {

});

async function OnUpdateVideoSource(resourceLocation) {

	var player = GetVideoPlayer();

	await player.Element.pause();
	player.Source.src = resourceLocation;
	player.Element.load();
	await player.Element.play();

	if (IsFirstVideo) {
		var videoElement = GetVideoElement();

		videoElement.addEventListener("timeupdate", function (event) {
			console.log(videoElement.currentTime);

			page.instace.invokeMethodAsync("OnUpdatePosition", videoElement.currentTime);
		});

		IsFirstVideo = false;
	}
}

async function SetPlaystate(state) {

	var player = GetVideoPlayer();

	switch (state) {
		case 1: await player.Element.play(); break;
		case 2: await player.Element.pause(); break;
		default:
			console.log(`PlayState ${state} not found!`);
	}
}

function SetPosition(value) {

	var player = GetVideoPlayer();

	player.Element.currentTime = value;
}