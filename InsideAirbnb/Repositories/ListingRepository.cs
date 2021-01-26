using InsideAirbnb.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace InsideAirbnb.Repositories
{
    public class ListingRepository : IRepository<Listing>
    {
        public IQueryable<Listing> Filter(Filter filter)
        {
            throw new NotImplementedException();
        }

        public Task<Listing> Get(int id)
        {
            throw new NotImplementedException();
        }

        public static double? AmsterdamDBLatitude(double? dbLatitude)
        {
            if (dbLatitude == null) return null;

            string latitudeString = dbLatitude.ToString();

            double first = double.Parse($"{latitudeString[0]}{latitudeString[1]}");
            double decimals = double.Parse($"0.{latitudeString.Substring(2, 6)}", CultureInfo.InvariantCulture);

            return first + decimals;
        }

        public static double? AmsterdamDBLongitude(double? dbLongitude)
        {
            if (dbLongitude == null) return null;

            string longitudeString = dbLongitude.ToString();

            double first = double.Parse($"{longitudeString[0]}");
            double decimals = double.Parse($"0.{longitudeString.Substring(1, 6)}", CultureInfo.InvariantCulture);

            return first + decimals;
        }
    }
}
