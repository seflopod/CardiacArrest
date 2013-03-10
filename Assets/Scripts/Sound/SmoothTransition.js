#pragma strict

function Crossfade ( a1 : AudioSource, a2 : AudioSource, duration : float )
{
    var startTime = Time.time;
    var endTime = startTime + duration;

    while (Time.time < endTime) {

        var i = (Time.time - startTime) / duration;

        a1.volume = (1-i);
        a2.volume = i;

       yield;

    }
}