using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;



public class SolarSystemCalculations : MonoBehaviour {

    public Text t;
    public Transform mainCam;

    public GameObject sunObj;
    public GameObject moonObj;
    public GameObject mercuryObj;
    public GameObject venusObj;
    public GameObject marsObj;
    public GameObject jupiterObj;
    public GameObject saturnObj;
    public GameObject uranusObj;
    public GameObject neptuneObj;

    // Use this for initialization
    void Start () {

        double d = dayNumber(2012,5,20,23.8);
        Debug.Log(System.DateTime.UtcNow.TimeOfDay.TotalHours);
        Sun sun = new Sun(d);
        Moon moon = new Moon(d);

        Planet mercury = new Planet(
            "Mercury",
            48.3313, 3.24587 * Mathf.Pow(10, -5F),
            7.0047, 5.00 * Mathf.Pow(10, -8F),
            29.1241, 1.01444 * Mathf.Pow(10, -5F),
            0.387098,
            0.205635, 5.59 * Mathf.Pow(10, -10F),
            168.6562, 4.0923344368,
            sun,
            d);

        Planet venus = new Planet(
           "Venus",
           76.6799, 2.46590 * Mathf.Pow(10, -5F),
           3.3946, 2.75 * Mathf.Pow(10, -8F),
           54.8910, 1.38374 * Mathf.Pow(10, -5F),
           0.723330,
           0.006773, -1.302 * Mathf.Pow(10, -9F),
           48.0052, 1.6021302244,
           sun,
           d);

        Planet mars = new Planet(
          "Mars",
          49.5574, 2.11081 * Mathf.Pow(10, -5F),
          1.8497, -1.78 * Mathf.Pow(10, -8F),
          286.5016, 2.92961 * Mathf.Pow(10, -5F),
          1.523688,
          0.093405, 2.516 * Mathf.Pow(10, -9F),
          18.6021, 0.5240207766,
          sun,
          d);

        Planet jupiter = new Planet(
            "Jupiter",
            100.4542, 2.76854 * Mathf.Pow(10, -5F),
            1.3030, -1.557 * Mathf.Pow(10, -7F),
            273.8777, 1.6450 * Mathf.Pow(10, -5F),
            5.20256,
            0.048498, 4.469 * Mathf.Pow(10, -9F),
            19.8950, 0.0830853001,
            sun,
            d);

        Planet saturn = new Planet(
            "Saturn",
            113.6634, 2.38980 * Mathf.Pow(10, -5F),
            2.4886, -1.081 * Mathf.Pow(10, -7F),
            339.3939, 2.97661 * Mathf.Pow(10, -5F),
            9.55475,
            0.055546, -9.499 * Mathf.Pow(10, -9F),
            316.9670, 0.0334442282,
            sun,
            d);

        Planet uranus = new Planet(
            "Uranus",
            74.0005, 1.3978 * Mathf.Pow(10, -5F),
            0.7733, 1.9 * Mathf.Pow(10, -8F),
            96.6612, 3.0565 * Mathf.Pow(10, -5F),
            19.18171,
            0.047318, 7.45 * Mathf.Pow(10, -9F),
            142.5905, 0.011725806,
            sun,
            d);

        Planet neptune = new Planet(
         "Neptune",
         131.7806, 3.0173 * Mathf.Pow(10, -5F),
         1.7700, -2.55 * Mathf.Pow(10, -7F),
         272.8461, -6.027 * Mathf.Pow(10, -6F),
         30.05826,
         0.008606, 2.15 * Mathf.Pow(10, -9F),
         260.2471, 0.005995147,
         sun,
         d);
       // t.text = (sun.coords.ra).ToString();

        for (int i = 0; i < 8; i++)
        {

        }
        
        sunObj.transform.position     = sph2Cart(sun.coords.ra, sun.coords.dec, 499 + sun.dist/100);
        moonObj.transform.position    = sph2Cart(moon.coords.ra, moon.coords.dec, 499);
        mercuryObj.transform.position = sph2Cart(mercury.coords.ra, mercury.coords.dec, 499 + mercury.dist/100);
        venusObj.transform.position   = sph2Cart(venus.coords.ra, venus.coords.dec, 499 + venus.dist / 100);
        marsObj.transform.position    = sph2Cart(mars.coords.ra, mars.coords.dec, 499 + mars.dist / 100);
        jupiterObj.transform.position = sph2Cart(jupiter.coords.ra, jupiter.coords.dec, 499 + jupiter.dist / 100);
        saturnObj.transform.position  = sph2Cart(saturn.coords.ra, saturn.coords.dec, 499 + saturn.dist / 100);
        uranusObj.transform.position  = sph2Cart(uranus.coords.ra, uranus.coords.dec, 499 + uranus.dist / 100);
        neptuneObj.transform.position = sph2Cart(neptune.coords.ra, neptune.coords.dec, 499 + neptune.dist / 100);

        sunObj.transform.LookAt(mainCam);
        moonObj.transform.LookAt(mainCam);
        mercuryObj.transform.LookAt(mainCam);
        venusObj.transform.LookAt(mainCam);
        marsObj.transform.LookAt(mainCam);
        jupiterObj.transform.LookAt(mainCam);
        saturnObj.transform.LookAt(mainCam);
        uranusObj.transform.LookAt(mainCam);
        neptuneObj.transform.LookAt(mainCam);
        

    }

    public struct Coords
    {
        public double ra; // Right ascension
        public double dec; // Declination

        public Coords(double ra, double dec)
        {
            this.ra = ra;
            this.dec = dec;
        }
    }

    public struct Sun
    {
        public Coords coords; // RA and dec

        // coords in x y z
        public double x;    
        public double y;
        public double z;
        public double dist;


        public Sun (double d)
        {
            double w = 282.9404 + 4.70935 * Mathf.Pow(10, -5F) * d; // longitude of perihelion
            double a = 1.0;                                         // mean distance
            double e = 0.016709 - 1.151 * Mathf.Pow(10, -9F) * d;   // eccentricity
            double m = Rev(356.0470 + 0.9856002585 * d);            // mean anomaly
            double o = 23.4393 - 3.563 * Mathf.Pow(10, -7F) * d;    // obliquity of the ecliptic
            double l = w + m;                                       // mean longitude
            double eAnom = EAnomIteration(e, m);                    // eccentric anomaly
            double xEcl = (Mathf.Cos((float) D2R(eAnom)) - e);
            double yEcl = (Mathf.Sin((float) D2R(eAnom)) * Mathf.Sqrt((float)(1 - e * e)));
            double r = Mathf.Sqrt((float)(xEcl * xEcl + yEcl * yEcl));
            double v = R2D(Mathf.Atan2((float)yEcl, (float)xEcl));

           // double cor = precessCorrection(2000, d);

            double lon = Rev(v + w);
            x = r * Mathf.Cos((float)(D2R(lon)));
            y = r * Mathf.Sin((float)(D2R(lon)));
            z = 0.0;
            double xEq = x;
            double yEq = y * Mathf.Cos((float) D2R(o)) - z * Mathf.Sin((float) D2R(o));
            double zEq = y * Mathf.Sin((float) D2R(o)) + z * Mathf.Cos((float) D2R(o));
            double ra = R2D(Mathf.Atan2((float)yEq, (float)xEq));
            double dec = R2D(Mathf.Atan2((float)zEq, Mathf.Sqrt((float)(xEq * xEq + yEq * yEq))));
            dist = r;
            coords = new Coords(ra, dec);
        }
    }

    public struct Moon
    {
        public Coords coords;

        // in degrees
        public double n;      // Long of asc. node
        public double i;      // Inclination 
        public double w;      // Argument of perihelion
        public double a;      // Semi-major axis
        public double e;      // Eccentricity
        public double M;      // Mean anomoaly



        public Moon (double d)
        {
            n = Rev(125.1228 - 0.0529538083 * d);
            i = Rev(5.1454);
            w = Rev(318.0634 + 0.1643573223 * d);
            a = 60.2666;
            e = 0.054900;
            M = Rev(115.3654 + 13.0649929509 * d);

            double eAnom = EAnomIteration(e, M);

            // Lunar orbit coords
            double xEq = a * (Mathf.Cos((float)D2R(eAnom)) - e);
            double yEq = a * (Mathf.Sin((float)D2R(eAnom)) * Mathf.Sqrt((float)(1 - e * e)));

            double r = Mathf.Sqrt((float)(xEq * xEq + yEq * yEq)); // distance
            double v = R2D(Mathf.Atan2((float)yEq, (float)xEq));   // True anomaly

            double xEcl = r * (Mathf.Cos((float)D2R(n)) * Mathf.Cos((float)D2R((v + w))) - Mathf.Sin((float)D2R(n))
                * Mathf.Sin((float)D2R((v + w))) * Mathf.Cos((float)D2R(i)));
            double yEcl = r * (Mathf.Sin((float)D2R(n)) * Mathf.Cos((float)D2R((v + w))) + Mathf.Cos((float)D2R(n))
                * Mathf.Sin((float)D2R((v + w))) * Mathf.Cos((float)D2R(i)));
            double zEcl = r * Mathf.Sin((float)D2R((v + w))) * Mathf.Sin((float)D2R((i)));


            double lon = Rev(R2D(Mathf.Atan2((float)yEcl, (float)xEcl)));
            double lat = Rev(R2D(Mathf.Atan2((float)zEcl, Mathf.Sqrt((float)(xEcl * xEcl + yEcl * yEcl)))));

            double lS = Rev((282.9404 + 4.70935 * Mathf.Pow(10, -5F) * d) + Rev(356.0470 + 0.9856002585 * d)); // Sun mean longitude
            double lM = Rev(n + w + M); // Moon mean longitude
            double mS = Rev(356.0470 + 0.9856002585 * d); // Sun mean anomaly
            double elong = lM - lS; // Moon mean elongation
            double f = lM - n; // Moon argument of latitude

            // Pertubations
            
            lon += -1.274 * Mathf.Sin((float)D2R(M - 2 * elong))
                + 0.658 * Mathf.Sin((float)D2R(2 * elong))
                - 0.186 * Mathf.Sin((float)D2R(mS))
                - 0.059 * Mathf.Sin((float)D2R(2 * M - 2 * elong))
                - 0.057 * Mathf.Sin((float)D2R(M - 2 * elong + mS))
                + 0.053 * Mathf.Sin((float)D2R(M + 2 * elong))
                + 0.046 * Mathf.Sin((float)D2R(2 * elong - mS))
                + 0.041 * Mathf.Sin((float)D2R(M - mS))
                - 0.035 * Mathf.Sin((float)D2R(elong))
                - 0.031 * Mathf.Sin((float)D2R(M + mS))
                - 0.015 * Mathf.Sin((float)D2R(2 * f - 2 * elong))
                + 0.011 * Mathf.Sin((float)D2R(M - 4 * elong));

            lat += -0.173 * Mathf.Sin((float)D2R(f - 2 * elong))
                - 0.055 * Mathf.Sin((float)D2R(M - f - 2 * elong))
                - 0.046 * Mathf.Sin((float)D2R(M + f - 2 * elong))
                + 0.033 * Mathf.Sin((float)D2R(f + 2 * elong))
                + 0.017 * Mathf.Sin((float)D2R(2 * M + f));

            r += -0.58 * Mathf.Cos((float)D2R(M - 2 * elong))
                - 0.46 * Mathf.Cos((float)D2R(2 * elong));

            xEcl = r * Mathf.Cos((float)D2R(lon)) * Mathf.Cos((float)D2R(lat));
            yEcl = r * Mathf.Sin((float)D2R(lon)) * Mathf.Cos((float)D2R(lat));
            zEcl = r * Mathf.Sin((float)D2R(lat));
            
            double oblec = 23.4406;
            double xGeoRot = xEcl;
            double yGeoRot = yEcl * Mathf.Cos((float)D2R(oblec)) - zEcl * Mathf.Sin((float)D2R(oblec));
            double zGeoRot = yEcl * Mathf.Sin((float)D2R(oblec)) + zEcl * Mathf.Cos((float)D2R(oblec));


            double ra = Rev(R2D(Mathf.Atan2((float)yGeoRot, (float)xGeoRot)));
            double dec = R2D(Mathf.Atan2((float)zGeoRot, Mathf.Sqrt((float)(xGeoRot * xGeoRot + yGeoRot * yGeoRot))));
            double dist = Mathf.Sqrt((float)(xGeoRot * xGeoRot + yGeoRot * yGeoRot + zGeoRot * zGeoRot));

            

            coords = new Coords(ra, dec);
        }

    }


    public struct Planet
    {
        public string name;   // name of the planet

        // in degrees
        public double n;      // Long of asc. node
        public double i;      // Inclination 
        public double w;      // Argument of perihelion
        public double a;      // Semi-major axis
        public double e;      // Eccentricity
        public double M;      // Mean anomoaly
        public Coords coords; // RA and Dec
        public double dist;   // Distance from Earth

        public Planet(
            string name,
            double n1, double n2,
            double i1, double i2,
            double w1, double w2,
            double a1,
            double e1, double e2,
            double M1, double M2,
            Sun sun,
            double d)
        {
            this.name = name;
            n = (n1 + n2 * d);
            i = i1 + i2 * d;
            w = w1 + w2 * d;
            a = a1;
            if (name == "Uranus")
            {
                a += -1.55 * Mathf.Pow(10,-8) * d;
            }
            if (name == "Neptune")
            {
                a += 3.313 * Mathf.Pow(10, -8) * d;
            }
            e = e1 + e2 * d;
            M = M1 + M2 * d;
            double eAnom = EAnomIteration(e, M);  // Eccentric anomaly

            // Eqautorial coords
            double xEq = a * (Mathf.Cos((float) D2R(eAnom)) - e);
            double yEq = a * (Mathf.Sin((float) D2R(eAnom)) * Mathf.Sqrt((float)(1 - e * e)));

            double r = Mathf.Sqrt((float)(xEq * xEq + yEq * yEq)); // Heliocentric distance
            double v = R2D(Mathf.Atan2((float)yEq, (float)xEq));   // True anomaly

            // Heliocentric ecliptic coords
            double xEcl = r * (Mathf.Cos((float)D2R(n)) * Mathf.Cos((float)D2R((v + w))) - Mathf.Sin((float)D2R(n))
                * Mathf.Sin((float)D2R((v + w))) * Mathf.Cos((float)D2R(i)));
            double yEcl = r * (Mathf.Sin((float)D2R(n)) * Mathf.Cos((float)D2R((v + w))) + Mathf.Cos((float)D2R(n))
                * Mathf.Sin((float)D2R((v + w))) * Mathf.Cos((float)D2R(i)));
            double zEcl = r * Mathf.Sin((float)D2R((v + w))) * Mathf.Sin((float)D2R((i)));


            // Heliocentric spherical coords
            double lon = Rev(R2D(Mathf.Atan2((float)yEcl, (float)xEcl)));
            double lat = Rev(R2D(Mathf.Atan2((float)zEcl, Mathf.Sqrt((float)(xEcl * xEcl + yEcl * yEcl)))));
            
            // Pertubations calculations for Jupiter, Saturn & Uranus
            double mJ = Rev(19.8950 + 0.0830853001 * d);   // Mean anomalies
            double mS = Rev(316.9670 + 0.0334442282 * d);
            double mU = Rev(142.5905 + 0.011725806 * d);
            if (name == "Jupiter")
            {
                lon += -0.332 * Mathf.Sin((float)D2R(2 * mJ - 5 * mS - 67.6))
                    - 0.056 * Mathf.Sin((float)D2R(2 * mJ - 2 * mS + 21))
                    + 0.042 * Mathf.Sin((float)D2R(3 * mJ - 5 * mS + 21))
                    - 0.036 * Mathf.Sin((float)D2R(mJ - 2 * mS))
                    + 0.022 * Mathf.Cos((float)D2R(mJ - mS))
                    + 0.023 * Mathf.Sin((float)D2R(2 * mJ - 3 * mS + 52))
                    - 0.016 * Mathf.Sin((float)D2R(mJ - 5 * mS - 69));
            }
            if (name == "Saturn")
            {
                lon += 0.812 * Mathf.Sin((float)D2R(2 * mJ - 5 * mS - 67.6))
                    - 0.229 * Mathf.Cos((float)D2R(2 * mJ - 4 * mS - 2))
                    + 0.119 * Mathf.Sin((float)D2R(mJ - 2 * mS - 3))
                    + 0.046 * Mathf.Sin((float)D2R(2 * mJ - 6 * mS - 69))
                    + 0.014 * Mathf.Sin((float)D2R(mJ - 3 * mS + 32));

                lat += -0.020 * Mathf.Cos((float)D2R(2 * mJ - 4 * mS - 2))
                    +0.018 * Mathf.Sin((float)D2R(2 * mJ - 6 * mS - 49));
                
            }
            if (name == "Uranus")
            {
                lon += 0.040 * Mathf.Sin((float)D2R(mS - 2 * mU + 6))
                    + 0.035 * Mathf.Sin((float)D2R(mS - 3 * mU + 33))
                    - 0.015 * Mathf.Sin((float)D2R(mJ - mU + 20));
            }
            
            xEcl = r * Mathf.Cos((float)D2R(lon)) * Mathf.Cos((float)D2R(lat));
            yEcl = r * Mathf.Sin((float)D2R(lon)) * Mathf.Cos((float)D2R(lat));
            zEcl = r * Mathf.Sin((float)D2R(lat));

            // Geocentric coords
            double xGeo = xEcl + sun.x;
            double yGeo = yEcl + sun.y;
            double zGeo = zEcl + sun.z;
        
            // y and z rotated around x to get equatorial geocentric
            double oblec = 23.4406;
            double yGeoRot = yGeo * Mathf.Cos((float) D2R(oblec)) - zGeo * Mathf.Sin((float) D2R(oblec));
            double zGeoRot = yGeo * Mathf.Sin((float) D2R(oblec)) + zGeo * Mathf.Cos((float) D2R(oblec));
            
            // Geocentric spherical coords
            double ra  = Rev(R2D(Mathf.Atan2((float)yGeoRot, (float)xGeo)));
            double dec = R2D(Mathf.Atan2((float)zGeoRot, Mathf.Sqrt((float)(xGeo * xGeo + yGeoRot * yGeoRot))));
            dist = Mathf.Sqrt((float) (xGeo * xGeo + yGeoRot * yGeoRot + zGeoRot * zGeoRot));

            
            coords = new Coords(ra, dec);
        }
        
    }


    //Utils

    static double D2R (double x) // convert x in degress to radians
    {
        return x * Mathf.Deg2Rad;
    }

    static double R2D (double x) // convert x in radians to degrees 
    {
        return x * Mathf.Rad2Deg;
    }
	
    static double Rev (double x) // angle x, return angle with degree between 0 and 360 
    {
        return x - Mathf.Floor(((float)x)/360)*360.0;
    }


    static double EAnomIteration (double e, double m) // eccentricity e, mean anomaly m in degrees, returns Eccentric Anomaly
    {
        double minDiff = 0.001;
        double radM = R2D(m);
        double eAnomCurr = (m) + (180 / Mathf.PI) * e * Mathf.Sin((float)radM) * (1 + e * Mathf.Cos((float)radM));
        double eAnomNext = eAnomCurr - (eAnomCurr - (180/Mathf.PI) * e * Mathf.Sin((float) D2R(eAnomCurr)) - m) / (1 - e * Mathf.Cos((float) D2R(eAnomCurr)));
        
        while (Mathf.Abs((float) (eAnomCurr - eAnomNext)) > minDiff)
        {
            eAnomCurr = eAnomNext;
            eAnomNext = eAnomCurr - (eAnomCurr - (180/Mathf.PI) * e * Mathf.Sin((float) D2R(eAnomCurr)) - m) / (1 - e * Mathf.Cos((float)D2R(eAnomCurr)));
        }
        
        return eAnomNext;
    }

    static double dayNumber (int year, int month, int day, double time) // time in UTC
    {
        return 367 * year - (7 * (year + ((month + 9) / 12))) / 4 + (275 * month) / 9 + day + time/24 - 730530;
    }

    static double precessCorrection (double epoch, double d)
    {
        return 3.82394 * Mathf.Pow(10, -5) * (365.2422 * (epoch - 2000.0) - d);
    }
    
    static Vector3 sph2Cart (double ra, double dec, double r) // takes ra and dec and returns a cartesian vector
    {
        double z = r * Mathf.Cos((float)D2R(ra)) * Mathf.Cos((float)D2R(dec));
        double x = r * Mathf.Sin((float)D2R(ra)) * Mathf.Cos((float)D2R(dec));
        double y = r * Mathf.Sin((float)D2R(dec));
        return new Vector3((float)x, (float)y, (float)z);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
