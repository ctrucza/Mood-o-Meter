﻿window.onload = function () {

    var goodButton = document.getElementById("good-button");
    goodButton.onclick = good;

    var badbutton = document.getElementById("bad-button");
    badbutton.onclick = bad;


    var moodHub = $.connection.moodHub;
    moodHub.client.hello = function () {
        showMoodCloud();
    }

    $.connection.hub.start().done(function () {
        moodHub.server.hello();
    });

};

function good() {
    sendMood("goood");
}

function bad() {
    sendMood("badie");
}

function sendMood(moood) {
    $.ajax({
        url: "/Mood/Create",
        type: "POST",
        data: {
            moood: moood
        }
    });
}

function reloadMoods() {
    $.ajax({
        url: "/Mood/GetMoods",
        type: "GET",
        success: function (result) {
            showMoods(result);
        }
    });
}

function showMoodCloud() {
    $.ajax({
        url: "/Mood/GetMoodCloud",
        type: "GET",
        success: function (result) {
            showMoods(result);
        }
    });
}

function showMoods(moods) {
    var cloud = new MoodsCloud("#mood-cloud", moods);
    cloud.update();
}