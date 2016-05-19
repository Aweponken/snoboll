using UnityEngine;
using UnityEngine.UI;// we need this namespace in order to access UI elements within our script
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class DidYouKnow : MonoBehaviour {
    public Text b;
    int a;
    string[] v = new string[] {
    "Did you know that the Artic Sea Ice decreases with an avarage of 13.4 procent per decade, according to NASA.",
    "Did you know that the global avarage sea level has risen with an avarage of 3.42 mm per year, according to NASA.",
    "Did you know that 2015 was the warmest year ever recorded according to NOAA, National Oceanic and Atmospheric Administration.",
    "Did you know that the continent of Antarctica has been losing about 134 billion metric tons of ice per year since 2002, according to NASA.",
    "Did you know that the continent of Greenland ice sheet has been losing an has been losing about 287 billion metric tons of ice per year, according to NASA.",
    "Did you know that “Global warming” refers to the long-term warming of the planet.",
    "Did you know that the vast majority of actively publishing climate scientists(97 percent), agree that humans are causing global warming and climate change, according to NASA.",
    "Did you know that January, February, March and April of 2016 all set new reords for highest temperarure, accoring to NASA.",
    "Did you know that 14 of the 15 hottest years on record have occurred since 2000, According to ECMW."};

    string[] l = new string[] {
        "http://climate.nasa.gov/vital-signs/arctic-sea-ice/",
        "http://climate.nasa.gov/vital-signs/sea-level/",
        "http://www.ncdc.noaa.gov/sotc/summary-info/global/201512",
        "http://climate.nasa.gov/vital-signs/land-ice/",
        "http://climate.nasa.gov/vital-signs/land-ice/",
        "http://climate.nasa.gov/faq/",
        "http://climate.nasa.gov/faq/",
        "http://data.giss.nasa.gov/gistemp/",
        "http://www.ecmwf.int/en/about/media-centre/news/2015/ecmwf-releases-global-reanalysis-data-2014-0"};




    // Use this for initialization
    void Start () {
        a = Random.Range(0, 9);
        string v1 = v[a];
        b.text = v1;
       
        }
	
    public void OnClickDid()
    {
        string b1 = l[a];
        Application.OpenURL(b1);
    }
}
