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

	console.log(`Play: ${resourceLocation}.`);

	await player.Element.pause();
	player.Source.src = resourceLocation;

	if (IsFirstVideo) {

		var videoElement = GetVideoElement();

		videoElement.addEventListener("timeupdate", function (event) {
			console.log(this.currentTime);

			page.instace.invokeMethodAsync("OnUpdatePosition", this.currentTime);
		});

		videoElement.onloadedmetadata = function () {
			console.log('metadata loaded!');
			page.instace.invokeMethodAsync("OnUpdateDuration", this.duration);
		};

		IsFirstVideo = false;
	}

	player.Element.load();
	await player.Element.play();
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