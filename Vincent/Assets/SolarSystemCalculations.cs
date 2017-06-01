using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;



public class SolarSystemCalculations : MonoBehaviour {
    // Time stuff
    public Text t;
    public Transform mainCam;
    public InputField yearIn;
    public InputField monthIn;
    public InputField dayIn;
    public InputField timeIn;
    public Button confirmDate;
    public Button realTime;

    // Simulations stuff
    public float simSpeedH = 0;
    public float simSpeedD = 0;
    public Button stopSim;
    public Button advHourSim;
    public Button advDaySim;
    public Button rewHourSim;
    public Button rewDaySim;
    public Text simStatus;

    // Location stuff
    public double devLon = 15;  // Current device longitude
    public double devLat = 60;  // Current device latitude
    public bool topocentric = false; // If the app is in topocentric mode or not 
    public InputField longIn;
    public InputField latIn;
    public Button topoBtn;
    public Button geoBtn;
    public Text currLocTxt;


    // Parent objects
    public GameObject sunObj;
    public GameObject moonObj;
    public GameObject mercuryObj;
    public GameObject venusObj;
    public GameObject marsObj;
    public GameObject jupiterObj;
    public GameObject saturnObj;
    public GameObject uranusObj;
    public GameObject neptuneObj;

    // Child image
    public Transform sunTr;
    public Transform moonTr;
    public Transform mercuryTr;
    public Transform venusTr;
    public Transform marsTr;
    public Transform jupiterTr;
    public Transform saturnTr;
    public Transform uranusTr;
    public Transform neptuneTr;
    
    // Child shadow
    public Transform mercurySha;
    public Transform venusSha;
    public Transform marsSha;
    public Transform jupiterSha;
    public Transform saturnSha;
    public Transform uranusSha;
    public Transform neptuneSha;

    public Text currentTimeTxt;
    public DateTime currentTime;
    

    public double currentD; // Current day number

    // Use this for initialization
    void Start () {


        currentTime = System.DateTime.UtcNow;
        currentD = dayNumber(System.DateTime.UtcNow.Year, System.DateTime.UtcNow.Month, System.DateTime.UtcNow.Day, System.DateTime.UtcNow.TimeOfDay.TotalHours);
        //d = dayNumber(2017, 5, 24, 23);


        instantiateSolarSystem(currentD);
       // InvokeRepeating("advanceHour", 0.0f, 1.0f);


        Button conbtn = confirmDate.GetComponent<Button>();
        conbtn.onClick.AddListener(setNewTime);

        Button realbtn = realTime.GetComponent<Button>();
        realbtn.onClick.AddListener(delegate { instantiateSolarSystem(dayNumber(System.DateTime.UtcNow.Year, System.DateTime.UtcNow.Month, System.DateTime.UtcNow.Day, System.DateTime.UtcNow.TimeOfDay.TotalHours)); });
        realbtn.onClick.AddListener(realCurrTime);

        Button stopbtn = stopSim.GetComponent<Button>();
        stopbtn.onClick.AddListener(stopTime);

        Button adv1hbtn = advHourSim.GetComponent<Button>();
        adv1hbtn.onClick.AddListener(advanceHourInv);

        Button adv1dbtn = advDaySim.GetComponent<Button>();
        adv1dbtn.onClick.AddListener(advanceDayInv);

        Button rew1hbtn = rewHourSim.GetComponent<Button>();
        rew1hbtn.onClick.AddListener(rewindHourInv);

        Button rew1dbtn = rewDaySim.GetComponent<Button>();
        rew1dbtn.onClick.AddListener(rewindDayInv);

        Button geocbtn = geoBtn.GetComponent<Button>();
        geocbtn.onClick.AddListener(geocentricMode);

        Button topobtn = topoBtn.GetComponent<Button>();
        topobtn.onClick.AddListener(topocentricMode);
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

        public double dist; // in astronomical unit
        public double dia;  // Apparent diameter

        public double lon;
        public double meanL; // mean longitude


        public Sun (double d)
        {
            double w = 282.9404 + 4.70935 * Mathf.Pow(10, -5F) * d; // longitude of perihelion
            double a = 1.0;                                         // mean distance
            double e = 0.016709 - 1.151 * Mathf.Pow(10, -9F) * d;   // eccentricity
            double m = Rev(356.0470 + 0.9856002585 * d);            // mean anomaly
            double o = 23.4393 - 3.563 * Mathf.Pow(10, -7F) * d;    // obliquity of the ecliptic
            meanL = w + m;                                       // mean longitude
            double eAnom = EAnomIteration(e, m);                    // eccentric anomaly
            double xEcl = (Mathf.Cos((float) D2R(eAnom)) - e);
            double yEcl = (Mathf.Sin((float) D2R(eAnom)) * Mathf.Sqrt((float)(1 - e * e)));
            double r = Mathf.Sqrt((float)(xEcl * xEcl + yEcl * yEcl));
            double v = R2D(Mathf.Atan2((float)yEcl, (float)xEcl));
           

            lon = Rev(v + w);
            x = r * Mathf.Cos((float)(D2R(lon)));
            y = r * Mathf.Sin((float)(D2R(lon)));
            z = 0.0;
            double xEq = x;
            double yEq = y * Mathf.Cos((float) D2R(o)) - z * Mathf.Sin((float) D2R(o));
            double zEq = y * Mathf.Sin((float) D2R(o)) + z * Mathf.Cos((float) D2R(o));
            double ra = R2D(Mathf.Atan2((float)yEq, (float)xEq));
            double dec = R2D(Mathf.Atan2((float)zEq, Mathf.Sqrt((float)(xEq * xEq + yEq * yEq))));
            dist = r;
            dia = 1919.26 / dist;
            dia = Mathf.Log((float)dia);
            coords = new Coords(ra, dec);
        }
    }

    public struct Moon
    {
        public Coords coords;
        public Coords topoCoords; // Topocentric coordinates

        // in degrees
        public double n;      // Long of asc. node
        public double i;      // Inclination 
        public double w;      // Argument of perihelion
        public double a;      // Semi-major axis
        public double e;      // Eccentricity
        public double M;      // Mean anomoaly
        public double dia;    // Apparent diameter
        public double phase;  // Phase
        public double elon;   // Elongation



        public Moon (double d, Sun sun, double lata, double longa, double ut) // day number, sun, lat and longitude, universal time ut
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
            double dist = Mathf.Sqrt((float)(xGeoRot * xGeoRot + yGeoRot * yGeoRot + zGeoRot * zGeoRot)); // Distance in Earth radii

            dia = 1873.7 * 60 / dist;
            dia = Mathf.Log((float)dia);

            elon = R2D(Mathf.Acos(Mathf.Cos((float) D2R(sun.lon * lon)) * Mathf.Cos((float)D2R(lat))));
            double pAngle = 180 - elon; // Phase angle
            phase = (1 + Mathf.Cos((float)D2R(pAngle))) / 2;

            // Topocentric calculations
            double mpar = R2D(Mathf.Asin((float) (1 / dist)));

            double gclat = lata - 0.1924 * Mathf.Sin((float) (2 * D2R(lata)));
            double rho = 0.99833 + 0.00167 * Mathf.Cos((float)(2 * D2R((lata))));

            double gmst0 = Rev((sun.meanL + 180)) / 15;
            double lst = gmst0 + ut + longa / 15; // Local sidereal time
            if (lst > 24)
            {
                lst -= 24;
            }
            else if (lst < 0)
            {
                lst += 24;
            }
            double hourAngle = Rev(lst*15 - ra);
            double auxg = R2D(Mathf.Atan(Mathf.Tan((float)D2R(gclat)) / Mathf.Cos((float)D2R(hourAngle))));

            double topRA = ra - mpar * rho * Mathf.Cos((float)D2R(gclat)) * Mathf.Sin((float)D2R(hourAngle)) / Mathf.Cos((float)D2R(dec));
            double topDec = dec - mpar * rho * Mathf.Sin((float)D2R(gclat)) * Mathf.Sin((float)D2R(auxg - dec)) / Mathf.Sin((float)D2R(auxg));
 

            topoCoords = new Coords(topRA, topDec);
            coords = new Coords(ra, dec);
       //     Debug.Log(topRA);
         //   Debug.Log(topDec);
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
        public double dist;   // Distance from Earth, in astronomical unit
        public double diaE;   // Apparent equatorial diameter, in astronomical unit
        public double diaP;   // Apparent polar diameter
        public double phase;  // Current phase
        public double elon;   // Elongation 
        public double mag;    // Magnitude

        public Planet(
            string name,
            double n1, double n2,
            double i1, double i2,
            double w1, double w2,
            double a1,
            double e1, double e2,
            double M1, double M2,
            Sun sun,
            double d,
            double d0E, double d0P,
            double ma1, double ma2)
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

            lon += precessCorrection(2000, d);


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

            double s = sun.dist;
            double pAngle = R2D(Mathf.Acos((float)((r * r + dist * dist - s * s) / (2 * s * dist))));
            phase = (1 + Mathf.Cos((float) D2R(pAngle))) / 2;
            elon = R2D(Mathf.Acos((float) ((s * s + dist * dist - r * r) / (2 * s * dist))));
            diaE = d0E / dist;
            diaP = d0P / dist;
            diaE = Mathf.Log10((float)diaE);
            diaP = Mathf.Log10((float)diaP);

            mag = ma1 + 5 * Mathf.Log10((float) (r * dist)) + ma2 * pAngle;
            
            coords = new Coords(ra, dec);
        }
        
    }

    void instantiateSolarSystem(double d)
    {
        currentD = d;
        Sun sun = new Sun(d);
        Moon moon = new Moon(d, sun, devLat, devLon, currentTime.TimeOfDay.TotalHours);

        Planet mercury = new Planet(
            "Mercury",
            48.3313, 3.24587 * Mathf.Pow(10, -5F),
            7.0047, 5.00 * Mathf.Pow(10, -8F),
            29.1241, 1.01444 * Mathf.Pow(10, -5F),
            0.387098,
            0.205635, 5.59 * Mathf.Pow(10, -10F),
            168.6562, 4.0923344368,
            sun,
            d,
            6.74, 6.74,
            -0.36, 0.027);

        Planet venus = new Planet(
           "Venus",
           76.6799, 2.46590 * Mathf.Pow(10, -5F),
           3.3946, 2.75 * Mathf.Pow(10, -8F),
           54.8910, 1.38374 * Mathf.Pow(10, -5F),
           0.723330,
           0.006773, -1.302 * Mathf.Pow(10, -9F),
           48.0052, 1.6021302244,
           sun,
           d,
           16.92, 16.92,
           -4.34, 0.013);

        Planet mars = new Planet(
          "Mars",
          49.5574, 2.11081 * Mathf.Pow(10, -5F),
          1.8497, -1.78 * Mathf.Pow(10, -8F),
          286.5016, 2.92961 * Mathf.Pow(10, -5F),
          1.523688,
          0.093405, 2.516 * Mathf.Pow(10, -9F),
          18.6021, 0.5240207766,
          sun,
          d,
          9.36, 9.28,
          -1.51, 0.016);

        Planet jupiter = new Planet(
            "Jupiter",
            100.4542, 2.76854 * Mathf.Pow(10, -5F),
            1.3030, -1.557 * Mathf.Pow(10, -7F),
            273.8777, 1.6450 * Mathf.Pow(10, -5F),
            5.20256,
            0.048498, 4.469 * Mathf.Pow(10, -9F),
            19.8950, 0.0830853001,
            sun,
            d,
            196.94, 185.08,
            -9.25, 0.014);

        Planet saturn = new Planet(
            "Saturn",
            113.6634, 2.38980 * Mathf.Pow(10, -5F),
            2.4886, -1.081 * Mathf.Pow(10, -7F),
            339.3939, 2.97661 * Mathf.Pow(10, -5F),
            9.55475,
            0.055546, -9.499 * Mathf.Pow(10, -9F),
            316.9670, 0.0334442282,
            sun,
            d,
            165.6, 150.8,
            -9, 0.044);

        Planet uranus = new Planet(
            "Uranus",
            74.0005, 1.3978 * Mathf.Pow(10, -5F),
            0.7733, 1.9 * Mathf.Pow(10, -8F),
            96.6612, 3.0565 * Mathf.Pow(10, -5F),
            19.18171,
            0.047318, 7.45 * Mathf.Pow(10, -9F),
            142.5905, 0.011725806,
            sun,
            d,
            65.8, 62.1,
            -7.15, 0.001);

        Planet neptune = new Planet(
         "Neptune",
         131.7806, 3.0173 * Mathf.Pow(10, -5F),
         1.7700, -2.55 * Mathf.Pow(10, -7F),
         272.8461, -6.027 * Mathf.Pow(10, -6F),
         30.05826,
         0.008606, 2.15 * Mathf.Pow(10, -9F),
         260.2471, 0.005995147,
         sun,
         d,
         62.2, 60.9,
         -6.9, 0.001);
        // t.text = (sun.coords.ra).ToString();



        // Positioning
        sunObj.transform.localPosition = sph2Cart(sun.coords.ra, sun.coords.dec, 499 + (sun.dist / 20));
        if (topocentric)
        {
            moonObj.transform.localPosition = sph2Cart(moon.topoCoords.ra, moon.topoCoords.dec, 498);

            Debug.Log(sph2Cart(moon.topoCoords.ra, moon.topoCoords.dec, 498).x);
            Debug.Log(sph2Cart(moon.topoCoords.ra, moon.topoCoords.dec, 498).y);
            Debug.Log(sph2Cart(moon.topoCoords.ra, moon.topoCoords.dec, 498).z);
        }
        else
        {
            moonObj.transform.localPosition = sph2Cart(moon.coords.ra, moon.coords.dec, 498);
            Debug.Log(sph2Cart(moon.coords.ra, moon.coords.dec, 498).x);
            Debug.Log(sph2Cart(moon.coords.ra, moon.coords.dec, 498).y);
            Debug.Log(sph2Cart(moon.coords.ra, moon.coords.dec, 498).z);
        }
        mercuryObj.transform.localPosition = sph2Cart(mercury.coords.ra, mercury.coords.dec, 499 + mercury.dist / 20);
        venusObj.transform.localPosition = sph2Cart(venus.coords.ra, venus.coords.dec, 499 + venus.dist / 20);
        marsObj.transform.localPosition = sph2Cart(mars.coords.ra, mars.coords.dec, 499 + mars.dist / 20);
        jupiterObj.transform.localPosition = sph2Cart(jupiter.coords.ra, jupiter.coords.dec, 499 + jupiter.dist / 20);
        saturnObj.transform.localPosition = sph2Cart(saturn.coords.ra, saturn.coords.dec, 499 + saturn.dist / 20);
        uranusObj.transform.localPosition = sph2Cart(uranus.coords.ra, uranus.coords.dec, 499 + uranus.dist / 20);
        neptuneObj.transform.localPosition = sph2Cart(neptune.coords.ra, neptune.coords.dec, 499 + neptune.dist / 20);

        


        //moonShadow.localScale = new Vector3(0, 0);
        /*
        Debug.Log(sun.dia);
        Debug.Log(moon.elon);
        Debug.Log(mercury.elon);
        Debug.Log(venus.elon);
        Debug.Log(mars.elon);
        Debug.Log(jupiter.elon);
        Debug.Log(uranus.elon);
        Debug.Log(neptune.elon);*/



        // Apparaent diameters
        sunTr.localScale = new Vector3((float) (sun.dia / 1.8), (float) (sun.dia/1.8) );
        moonTr.localScale = new Vector3((float)moon.dia / 3, (float)moon.dia / 3);
        mercuryTr.localScale = new Vector3((float)mercury.diaE , (float)mercury.diaP );
        venusTr.localScale = new Vector3((float)venus.diaE , (float)venus.diaP);
        marsTr.localScale = new Vector3((float)mars.diaE , (float)mars.diaP);
        jupiterTr.localScale = new Vector3((float)jupiter.diaE , (float)jupiter.diaP);
        saturnTr.localScale = new Vector3((float)saturn.diaE , (float)saturn.diaP);
        uranusTr.localScale = new Vector3((float)uranus.diaE, (float)uranus.diaP);
        neptuneTr.localScale = new Vector3((float)neptune.diaE, (float)neptune.diaP);

        mercurySha.localScale = new Vector3((float)mercury.diaE, (float)mercury.diaP);
        venusSha.localScale = new Vector3((float)venus.diaE, (float)venus.diaP);
        marsSha.localScale = new Vector3((float)mars.diaE, (float)mars.diaP);
        jupiterSha.localScale = new Vector3((float)jupiter.diaE, (float)jupiter.diaP);
        saturnSha.localScale = new Vector3((float)saturn.diaE, (float)saturn.diaP);
        uranusSha.localScale = new Vector3((float)uranus.diaE, (float)uranus.diaP);
        neptuneSha.localScale = new Vector3((float)neptune.diaE, (float)neptune.diaP);


        // Billboarding
        sunObj.transform.LookAt(mainCam);
        moonObj.transform.LookAt(mainCam);
        mercuryObj.transform.LookAt(mainCam);
        venusObj.transform.LookAt(mainCam);
        marsObj.transform.LookAt(mainCam);
        jupiterObj.transform.LookAt(mainCam);
        saturnObj.transform.LookAt(mainCam);
        uranusObj.transform.LookAt(mainCam);
        neptuneObj.transform.LookAt(mainCam);

        


        // Elongation
        Color color = mercurySha.GetComponent<Renderer>().material.color;
        color.a = (float) (1 - mercury.elon / 100);
        mercurySha.GetComponent<Renderer>().material.color = color;

        color = venusSha.GetComponent<Renderer>().material.color;
        color.a = (float)(1 - venus.elon / 100);
        venusSha.GetComponent<Renderer>().material.color = color;

        color = marsSha.GetComponent<Renderer>().material.color;
        color.a = (float)(1 - mars.elon / 100);
        marsSha.GetComponent<Renderer>().material.color = color;

        color = jupiterSha.GetComponent<Renderer>().material.color;
        color.a = (float)(1 - jupiter.elon / 100);
        jupiterSha.GetComponent<Renderer>().material.color = color;

        color = saturnSha.GetComponent<Renderer>().material.color;
        color.a = (float)(1 - saturn.elon / 100);
        saturnSha.GetComponent<Renderer>().material.color = color;

        color = saturnSha.GetComponent<Renderer>().material.color;
        color.a = (float)(1 - saturn.elon / 100);
        saturnSha.GetComponent<Renderer>().material.color = color;

        color = saturnSha.GetComponent<Renderer>().material.color;
        color.a = (float)(1 - saturn.elon / 100);
        saturnSha.GetComponent<Renderer>().material.color = color;

        color = saturnSha.GetComponent<Renderer>().material.color;
        color.a = (float)(1 - saturn.elon / 120);
        saturnSha.GetComponent<Renderer>().material.color = color;

        color = uranusSha.GetComponent<Renderer>().material.color;
        color.a = (float)(1 - uranus.elon / 120);
        uranusSha.GetComponent<Renderer>().material.color = color;

        color = neptuneSha.GetComponent<Renderer>().material.color;
        color.a = (float)(1 - neptune.elon / 120);
        neptuneSha.GetComponent<Renderer>().material.color = color;


    }

    // Methods for location

    void geocentricMode()
    {
        topocentric = false;
        instantiateSolarSystem(currentD);
    }

    void topocentricMode()
    {
        topocentric = true;
        devLat = Convert.ToDouble(latIn.text);
        devLon = Convert.ToDouble(longIn.text);
        instantiateSolarSystem(currentD);
    }










    // Methods for time simulation
    void setNewTime() // Updates solar system to inputted time
    {
        double d = dayNumber(Convert.ToInt32(yearIn.text), Convert.ToInt32(monthIn.text), Convert.ToInt32(dayIn.text), Convert.ToDouble(timeIn.text));
     //   Debug.Log((Int32)((Convert.ToDouble(timeIn.text) % 1) * 60));
        //currentTime.Year = yearIn.text;
        currentTime = new DateTime(Convert.ToInt32(yearIn.text), Convert.ToInt32(monthIn.text), Convert.ToInt32(dayIn.text),
            (Int32)Convert.ToDouble(timeIn.text),(Int32) ((Convert.ToDouble(timeIn.text) % 1) * 60), 0);
        currentD = d;
        instantiateSolarSystem(d);
    }

    void realCurrTime()
    {
        currentTime = System.DateTime.UtcNow;
    }
    

    void advanceDayInv()
    {
        simSpeedH += 24;
        //simSpeedD++;
        simStatus.text = "+" + simSpeedH + " h/s";
        CancelInvoke();
        if (simSpeedH > 0)
        {
            simStatus.text = "+" + simSpeedH + " h/s";
            InvokeRepeating("advanceHour", 0.0f, (1 / simSpeedH));
        }
        else if (simSpeedH == 0)
        {
            simStatus.text = "Stopped";
        }
        else
        {
            simStatus.text = simSpeedH + " h/s";
            InvokeRepeating("rewindHour", 0.0f, Mathf.Abs((1 / simSpeedH)));
        }
       //     InvokeRepeating("advanceHour", 0.0f, (1 / simSpeedH));
    }


    void advanceHour()
    {
        currentTime = currentTime.AddHours(1);
        instantiateSolarSystem(currentD + 0.04166666667);
    }

    void advanceHourInv()
    {
        simSpeedD = 0;
        simSpeedH++;
     //   simStatus.text = "+" + simSpeedH + " h/s";
        CancelInvoke();
        if (simSpeedH > 0)
        {
            simStatus.text = "+" + simSpeedH + " h/s";
            InvokeRepeating("advanceHour", 0.0f, (1 / simSpeedH));
        }
        else if (simSpeedH == 0)
        {
            simStatus.text = "Stopped";
        }
        else
        {
            simStatus.text = simSpeedH + " h/s";
            InvokeRepeating("rewindHour", 0.0f, Mathf.Abs((1 / simSpeedH)));
        }
       // InvokeRepeating("advanceHour", 0.0f, (1 / simSpeedH));
    }

    void rewindHour()
    {
        currentTime = currentTime.AddHours(-1);
        instantiateSolarSystem(currentD - 0.04166666667);
    }

    void rewindHourInv()
    {
        simSpeedH--;
        CancelInvoke();
        if (simSpeedH > 0)
        {
            simStatus.text = "+" + simSpeedH + " h/s";
            InvokeRepeating("advanceHour", 0.0f, (1 / simSpeedH));
        }
        else if(simSpeedH == 0)
        {
            simStatus.text = "Stopped";
        }
        else
        {
            simStatus.text = simSpeedH + " h/s";
            InvokeRepeating("rewindHour", 0.0f, Mathf.Abs((1 / simSpeedH)));
        }
        
      //  InvokeRepeating("advanceHour", 0.0f, (1 / simSpeedH));
    }

    void rewindDayInv()
    {
        simSpeedH -= 24;
        //simSpeedD++;
        simStatus.text = "+" + simSpeedH + " h/s";
        CancelInvoke();
        if (simSpeedH > 0)
        {
            simStatus.text = "+" + simSpeedH + " h/s";
            InvokeRepeating("rewindHour", 0.0f, (1 / simSpeedH));
        }
        else if (simSpeedH == 0)
        {
            simStatus.text = "Stopped";
        }
        else
        {
            simStatus.text = simSpeedH + " h/s";
            InvokeRepeating("rewindHour", 0.0f, Mathf.Abs((1 / simSpeedH)));
        }
        //     InvokeRepeating("advanceHour", 0.0f, (1 / simSpeedH));
    }

    void stopTime()
    {
        simStatus.text = "Stopped";
        simSpeedH = 0;
        simSpeedD = 0;
        CancelInvoke();
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
    
    static Vector3 sph2Cart (double ra, double dec, double r) // takes angular ra and dec and returns a cartesian vector
    {
        double x = r * Mathf.Cos((float)D2R(ra)) * Mathf.Cos((float)D2R(dec));
        double z = r * Mathf.Sin((float)D2R(ra)) * Mathf.Cos((float)D2R(dec));
        double y = r * Mathf.Sin((float)D2R(dec));
        return new Vector3((float)x, (float)y, (float)z);
    }


    void Update()
    {
        currentTimeTxt.text = currentTime.Day + "/" + currentTime.Month + "/" + currentTime.Year + "  " + currentTime.Hour.ToString("D2") + ":" + currentTime.Minute.ToString("D2");

        if (topocentric)
        {
            currLocTxt.text = "Lon: " + devLon.ToString("F3") + "  Lat: " + devLat.ToString("F3");

        }
        else
        {
            currLocTxt.text = "Geocentric";
        }
    }

}
