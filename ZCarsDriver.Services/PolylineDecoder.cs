namespace ZCarsDriver.Services
{
    public class PolylineDecoder
    {
        public static List<(double Latitude, double Longitude)> DecodePolyline(string encoded)
        {
            if (string.IsNullOrEmpty(encoded)) return null;

            var polyline = new List<(double Latitude, double Longitude)>();
            int index = 0, len = encoded.Length;
            int lat = 0, lng = 0;

            while (index < len)
            {
                int result = 0, shift = 0;
                int b;
                do
                {
                    b = encoded[index++] - 63;
                    result |= (b & 0x1F) << shift;
                    shift += 5;
                } while (b >= 0x20);

                int dlat = (result & 1) != 0 ? ~(result >> 1) : (result >> 1);
                lat += dlat;

                result = 0;
                shift = 0;
                do
                {
                    b = encoded[index++] - 63;
                    result |= (b & 0x1F) << shift;
                    shift += 5;
                } while (b >= 0x20);

                int dlng = (result & 1) != 0 ? ~(result >> 1) : (result >> 1);
                lng += dlng;

                polyline.Add((lat / 1e5, lng / 1e5));
            }

            return polyline;
        }
    }
}
