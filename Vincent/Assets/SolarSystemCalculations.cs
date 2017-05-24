using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SolarSystemCalculations : MonoBehaviour {

    public Text t;

	// Use this for initialization
	void Start () {

        int d = dayNumber(2017,5,23);
        double merN = (48.3313 + 3.24587*Mathf.Pow(10,-5F) * d) * Mathf.Deg2Rad;
        double merI = (7.0047 + 5.00 * Mathf.Pow(10, -8F) * d) * Mathf.Deg2Rad;
        double merW = (29.1241 + 1.01444 * Mathf.Pow(10, -5F) * d) * Mathf.Deg2Rad;
        double merA = 0.387098;
        double merE = 0.205635 + 5.59 * Mathf.Pow(10, -10F) * d;
        double merM = Rev(168.6562 + 4.0923344368 * d);
        double merEAnom = EAnomIteration(merE, merM);
        double merX = merA * (Mathf.Cos((float) merEAnom * Mathf.Deg2Rad) - merE);
        double merY = merA * Mathf.Sqrt((float)(1 - merE * merE)) * Mathf.Sin((float)merEAnom * Mathf.Deg2Rad);
        double merR = Mathf.Sqrt((float)(merX * merX + merY * merY));
        double merV = Mathf.Atan2((float)merY, (float)merX);
        double merEX = merR * (Mathf.Cos((float)merN) * Mathf.Cos((float)(merV + merW)) - Mathf.Sin((float)merN) * Mathf.Sin((float)(merV + merW)) * Mathf.Cos((float)merI));
        double merEY = merR * (Mathf.Sin((float)merN) * Mathf.Cos((float)(merV + merW)) + Mathf.Cos((float)merN) * Mathf.Sin((float)(merV + merW)) * Mathf.Cos((float)merI));
        double merEZ = merR * Mathf.Sin((float)(merV + merW)) * (Mathf.Sin((float)merI));
        double merLon = Mathf.Atan2((float)merEY, (float)merEX);
        double merRsphe = Mathf.Sqrt((float)(merEX * merEX + merEY * merEY + merEZ * merEZ));
        double merLat = Mathf.Atan2((float)merEZ, Mathf.Sqrt((float)(merEX * merEX + merEY * merEY)));
        Sun sun = new Sun(d);
        Planet mer = new Planet(
            "Mercury",
            48.3313, 3.24587 * Mathf.Pow(10, -5F),
            7.0047, 5.00 * Mathf.Pow(10, -8F),
            29.1241, 1.01444 * Mathf.Pow(10, -5F),
            0.387098,
            0.205635, 5.59 * Mathf.Pow(10, -10F),
            168.6562, 4.0923344368,
            sun,
            d);
        t.text = (mer.coords.ra).ToString();
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

        public Sun (int d)
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

        public Planet(
            string name,
            double n1, double n2,
            double i1, double i2,
            double w1, double w2,
            double a1,
            double e1, double e2,
            double M1, double M2,
            Sun sun,
            int d)
        {
            this.name = name;
            n = (n1 + n2 * d);
            i = i1 + i2 * d;
            w = w1 + w2 * d;
            a = a1;
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
            double lon = R2D(Mathf.Atan2((float)yEcl, (float)xEcl));
            double lat = R2D(Mathf.Atan2((float)zEcl, Mathf.Sqrt((float)(xEcl * xEcl + yEcl * yEcl))));


            // Geocentric coords
            double xGeo = xEcl + sun.x;
            double yGeo = yEcl + sun.y;
            double zGeo = zEcl + sun.z;
        
            // y and z rotated around x to get equatorial geocentric
            double oblec = 23.4406;
            double yGeoRot = yGeo * Mathf.Cos((float) D2R(oblec)) - zGeo * Mathf.Sin((float) D2R(oblec));
            double zGeoRot = yGeo * Mathf.Sin((float) D2R(oblec)) + zGeo * Mathf.Cos((float) D2R(oblec));
            

            // Geocentric spherical coords
            double ra  = R2D(Mathf.Atan2((float)yGeoRot, (float)xGeo));
            double dec = R2D(Mathf.Atan2((float)zGeoRot, Mathf.Sqrt((float)(xGeo * xGeo + yGeoRot * yGeoRot))));
            double dist = Mathf.Sqrt((float) (xGeo * xGeo + yGeoRot * yGeoRot + zGeoRot * zGeoRot));

            coords = new Coords(ra, dec);
        }
        
    }

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

    static int dayNumber (int year, int month, int day)
    {
        return 367 * year - (7 * (year + ((month + 9) / 12))) / 4 + (275 * month) / 9 + day - 730530;
    }

    static double precessCorrection (double epoch, int d)
    {
        return 3.82394 * Mathf.Pow(10, -5) * (365.2422 * (epoch - 2000.0) - d);
    }
    

	// Update is called once per frame
	void Update () {
		
	}
}
