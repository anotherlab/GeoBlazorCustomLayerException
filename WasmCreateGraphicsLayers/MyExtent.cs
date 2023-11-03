namespace WasmCreateGraphicsLayers;

public struct CoordinateEntity
{
    public double Latitude;
    public double Longitude;
    public CoordinateEntity(double x, double y)
    {
        Latitude = x;
        Longitude = y;
    }
}
public class MyExtent
{
    public double MinLatitude { get; set; }
    public double MinLongitude { get; set; }
    public double MaxLatitude { get; set; }
    public double MaxLongitude { get; set; }

    private bool IsFirstPoint = true;

    public void Clear()
    {
        MinLongitude = MaxLongitude = MinLatitude = MaxLatitude = 0.0;
        IsFirstPoint = true;
    }

    public void ReadLatLon(CoordinateEntity position)
    {
        ReadLatLon(position.Latitude, position.Longitude);
    }
    public void ReadLonLat(double lon, double lat)
    {
        ReadLatLon(lat, lon);
    }

    public void ReadLatLon(double lat, double lon)
    {
        if (IsFirstPoint)
        {
            MinLongitude = lon;
            MaxLongitude = lon;
            MinLatitude = lat;
            MaxLatitude = lat;

            IsFirstPoint = false;
        }
        else
        {
            if (lon < MinLongitude)
                MinLongitude = lon;
            if (lat < MinLatitude)
                MinLatitude = lat;
            if (lon > MaxLongitude)
                MaxLongitude = lon;
            if (lat > MaxLatitude)
                MaxLatitude = lat;
        }
    }
    public MyExtent() { }

    public MyExtent(double latitude, double longitude)
    {
        ReadLatLon(latitude, longitude);
    }
    public MyExtent(double latitude, double longitude, double otherLatitude, double otherLongitude) : this(latitude, longitude)
    {
        ReadLatLon(otherLatitude, otherLongitude);
    }

    #region Stuff we are not using
    // Simple way of getting center, reasonably accurate for short distances
    public CoordinateEntity GetCenter()
    {
        var Latitude = (MaxLatitude + MinLatitude) / 2.0;
        var Longitude = (MaxLongitude + MinLongitude) / 2.0;

        return new CoordinateEntity(Latitude, Longitude);
    }

    public static double DegreeBearing(
        double lat1, double lon1,
        double lat2, double lon2)
    {
        var dLon = ToRad(lon2 - lon1);
        var dPhi = Math.Log(
            Math.Tan(ToRad(lat2) / 2 + Math.PI / 4) / Math.Tan(ToRad(lat1) / 2 + Math.PI / 4));
        if (Math.Abs(dLon) > Math.PI)
            dLon = dLon > 0 ? -(2 * Math.PI - dLon) : (2 * Math.PI + dLon);
        return ToBearing(Math.Atan2(dLon, dPhi));
    }

    public static double ToRad(double degrees)
    {
        return degrees * (Math.PI / 180);
    }

    public static double ToDegrees(double radians)
    {
        return radians * 180 / Math.PI;
    }

    public static double ToBearing(double radians)
    {
        // convert radians to degrees (as bearing: 0...360)
        return (ToDegrees(radians) + 360) % 360;
    }
    #endregion

    /// <summary>
    /// Return a new copy of current extent
    /// </summary>
    /// <returns></returns>
    public MyExtent Clone()
    {
        return (new MyExtent(MaxLatitude, MaxLongitude, MinLatitude, MinLongitude));
    }

    /// <summary>
    /// Increase the size of the extent inplace by a multiplier value
    /// Technically it's hideously inaccurate and will make GIS professionals weep
    /// But it's accurate enough
    /// Will change the extent inplace. Use Clone() first to make a new copy before calling
    /// Expand() if you need to keep the original Extent
    /// </summary>
    /// <param name="factor"></param>
    /// <returns></returns>
    public MyExtent Expand(double factor)
    {
        double dLon = Math.Abs(MaxLongitude - MinLongitude) * factor;
        double dLat = Math.Abs(MaxLatitude - MinLatitude) * factor;

        MaxLongitude -= dLon;
        MaxLatitude -= dLat;

        MinLongitude += dLon;
        MinLatitude += dLat;

        return this;
    }
}