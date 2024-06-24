using Domain.Primitives;

namespace Domain.ValueObjects
{
    public class Location : IEquatable<Location>, IValueObjectNonBinary
    {
        /// <summary>
        /// Gets or sets the latitude coordinate of this location.
        /// </summary>
        public double? Latitude { get; private set; }
        /// <summary>
        /// Gets or sets the longitude coordinate of this location.
        /// </summary>
        public double? Longitude { get; private set; }
        
		/// <summary>
		/// Gets or sets the timestamp of the location in UTC.
		/// </summary>
		public DateTimeOffset? Timestamp { get; private set; }

		/// <summary>
		/// Gets the altitude in meters (if available) in a reference system which is specified by <see cref="AltitudeReferenceSystem"/>.
		/// </summary>
		/// <remarks>Returns 0 or <see langword="null"/> if not available.</remarks>
		public double? Altitude { get; private set; }

		/// <summary>
		/// Gets or sets the horizontal accuracy (in meters) of the location.
		/// </summary>
		public double? Accuracy { get; private set; }

		/// <summary>
		/// Gets or sets the vertical accuracy (in meters) of the location.
		/// </summary>
		public double? VerticalAccuracy { get; private set; }

		/// <summary>
		/// Gets or sets the current speed in meters per second at the time when this location was determined.
		/// </summary>
		/// <remarks>
		/// <para>Returns 0 or <see langword="null"/> if not available. Otherwise the value will range between 0-360.</para>
		/// <para>Requires <see cref="Accuracy"/> to be <see cref="GeolocationAccuracy.High"/> or better
		/// and may not be returned when calling <see cref="Geolocation.GetLastKnownLocationAsync"/>.</para>
		/// </remarks>
		public double? Speed { get; private set; }

		/// <summary>
		/// Gets or sets the current degrees relative to true north at the time when this location was determined.
		/// </summary>
		/// <remarks>Returns 0 or <see langword="null"/> if not available.</remarks>
		public double? Course { get; private set; }

		/// <summary>
		/// Specifies the reference system in which the <see cref="Altitude"/> value is expressed.
		/// </summary>
		public string? GeoReferenceSystem { get; private set; }

        private Location()
        {

        }
        private Location(
            double latitude,
            double longitude,
            DateTimeOffset timestamp,
            double? altitude = null,
            double? accuracy = null,
            double? verticalAccuracy = null,
            double? speed = null,
            double? course = null,
            string geoReferenceSystem = "")
        {
            Latitude = latitude;
            Longitude = longitude;
            Timestamp = timestamp;
            Altitude = altitude;
            Accuracy = accuracy;
            VerticalAccuracy = verticalAccuracy;
            Speed = speed;
            Course = course;
            GeoReferenceSystem = geoReferenceSystem;
        }

        public static Location Parse(
            double latitude,
            double longitude,
            DateTimeOffset timestamp,
            double? altitude = null,
            double? accuracy = null,
            double? verticalAccuracy = null,
            bool reducedAccuracy = false,
            double? speed = null,
            double? course = null,
            bool isFromMockProvider = false,
            string geoReferenceSystem = "")
        {
            return new Location(
                latitude,
                longitude,
                timestamp,
                altitude,
                accuracy,
                verticalAccuracy,
                speed,
                course,
                geoReferenceSystem);
        }

        /// <summary>
        /// Returns a string representation of the current values of <see cref="Location"/>.
        /// </summary>
        /// <returns>A string representation of this instance in the format of <c>Latitude: {value}, Longitude: {value}, Altitude: {value}, Accuracy: {value}, VerticalAccuracy: {value}, Speed: {value}, Course: {value}, Timestamp: {value}</c>.</returns>
        public override string ToString() =>
            $"{nameof(Latitude)}: {Latitude}, " +
            $"{nameof(Longitude)}: {Longitude}, " +
            $"{nameof(Altitude)}: {Altitude}, " +
            $"{nameof(Accuracy)}: {Accuracy}, " +
            $"{nameof(VerticalAccuracy)}: {VerticalAccuracy}, " +
            $"{nameof(Speed)}: {Speed}, " +
            $"{nameof(Course)}: {Course}, " +
            $"{nameof(Timestamp)}: {Timestamp}";

        public static implicit operator string(Location location) => location.ToString();

        public static bool operator !=(Location locationLeft, Location locationRight)
        {
            if (ReferenceEquals(locationLeft, null) && ReferenceEquals(locationRight, null)) return true;
            if (ReferenceEquals(locationLeft, null) || ReferenceEquals(locationRight, null)) return false;

            return locationLeft.Equals(locationRight);
        }

        public static bool operator ==(Location locationLeft, Location locationRight)
        {
            return (locationLeft != locationRight);
        }

        public bool Equals(Location? other)
        {
            if (ReferenceEquals(other, null)) return false;
            return this.Latitude == other.Latitude && this.Longitude == other.Longitude;
        }
    }
    public static class LocationExtension
    {
        const double degreesToRadians = Math.PI / 180.0;
        const double meanEarthRadiusInKilometers = 6371.0;
        const double milesToKilometers = 1.609344;
        const double kilometersToMiles = 1.0 / milesToKilometers;

        /// <summary>
        /// Calculates the distance between two coordinates in miles.
        /// </summary>
        /// <param name="location1">First Location.</param>
        /// <param name="location2">Second Location.</param>
        /// <returns>The distance in miles.</returns>
        public static double CoordinatesToMiles(this Location location1, Location location2) =>
            KilometersToMiles(CoordinatesToKilometers(location1,location2));

        /// <summary>
        /// Calculates the distance between two coordinates in kilometers.
        /// </summary>
        /// <param name="location1">First Location.</param>
        /// <param name="location2">Second Location.</param>
        /// <returns>The distance in kilometers.</returns>
        public static double CoordinatesToKilometers(this Location location1, Location location2)
        {
            if ((location2.Latitude is null || location2.Longitude is null) || location1.Latitude is null || location1.Longitude is null)
            {
                throw new NullReferenceException();
            }

            double lat1 = location1.Latitude.Value;
            double lon1 = location1.Longitude.Value;
            double lat2 = location2.Latitude.Value;
            double lon2 = location2.Longitude.Value;

            if (lat1 == lat2 && lon1 == lon2)
                return 0;

            var dLat = DegreesToRadians(lat2 - lat1);
            var dLon = DegreesToRadians(lon2 - lon1);

            lat1 = DegreesToRadians(lat1);
            lat2 = DegreesToRadians(lat2);

            var dLat2 = Math.Sin(dLat / 2) * Math.Sin(dLat / 2);
            var dLon2 = Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var a = dLat2 + dLon2 * Math.Cos(lat1) * Math.Cos(lat2);
            var c = 2 * Math.Asin(Math.Sqrt(a));

            return meanEarthRadiusInKilometers * c;
        }

        /// <summary>
        /// Converts degrees to radian.
        /// </summary>
        /// <param name="degrees">The value in degrees to convert.</param>
        /// <returns>The value from <paramref name="degrees"/> in radian.</returns>
        public static double DegreesToRadians(double degrees) =>
            degrees * degreesToRadians;

        /// <summary>
        /// Converts distances from kilometers to miles.
        /// </summary>
        /// <param name="kilometers">The value in kilometers to convert.</param>
        /// <returns>The value from <paramref name="kilometers"/> in miles.</returns>
        public static double KilometersToMiles(double kilometers) =>
            kilometers * kilometersToMiles;
    }
}
