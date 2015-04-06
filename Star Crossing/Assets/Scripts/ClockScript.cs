using UnityEngine;
using System.Collections;

public class ClockScript : MonoBehaviour {

	private int hour = 0;
	private int minute = 0;

	void Start () {

	}

	public int getHour()
	{
		return hour;
	}

	public int getMinute()
	{
		return minute;
	}

	public void SetTime (int hr, int min) {
		hour = hr % 24;
		minute = min % 60;
	}

	public void IncrementTime (int hr, int min) {
		minute += min;
		if (minute >= 60) {
			minute -= 60;
			hour++;
		}
		hour += hr;
		if (hour >= 24) {
			hour -= 24;
		}
	}

	public string GetTimeAsString () {
		string hr;
		string min;
		string ampm;
		// Format hour
		if (hour == 0) {
			hr = "12";
			ampm = "A.M.";
		} else if (hour < 12) {
			hr = hour.ToString ();
			ampm = "A.M.";
		} else if (hour == 12) {
			hr = "12";
			ampm = "P.M.";
		} else {
			hr = (hour - 12).ToString ();
			ampm = "P.M.";
		}
		// Format minute
		if (minute < 10) {
			min = "0" + minute.ToString ();
		} else {
			min = minute.ToString ();
		}
		// Put the string together
		return hr + ":" + min + " " + ampm;
	}
}
