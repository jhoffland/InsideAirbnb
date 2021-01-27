namespace InsideAirbnb
{
    public class Filter
    {
        private int? _PriceMin;
        public int? PriceMin
        {
            get => _PriceMin;
        }

        private int? _PriceMax;
        public int? PriceMax
        {
            get => _PriceMax;
        }

        private string? _Neighbourhood;
        public string? Neighbourhood
        {
            get => _Neighbourhood;
        }

        private int? _RatingMin;
        public int? RatingMin
        {
            get => _RatingMin;
        }

        private int? _RatingMax;
        public int? RatingMax
        {
            get => _RatingMax;
        }

        public string CacheKey
        {
            get => $"{PriceMin}_{PriceMax}_{Neighbourhood}_{RatingMin}_{RatingMax}";
        }

        public Filter(string priceMin, string priceMax, string neighbourhood, string ratingMin, string ratingMax)
        {
            int priceMinInt;
            if (priceMin != null && priceMin.Length > 0 && int.TryParse(priceMin, out priceMinInt))
            {
                _PriceMin = priceMinInt;
            } else
            {
                _PriceMin = null;
            }

            int priceMaxInt;
            if (priceMax != null && priceMax.Length > 0 && int.TryParse(priceMax, out priceMaxInt))
            {
                _PriceMax = priceMaxInt;
            } else
            {
                _PriceMax = null;
            }

            if (neighbourhood != null && neighbourhood.Length > 0)
            {
                _Neighbourhood = neighbourhood;
            } else
            {
                _Neighbourhood = null;
            }

            int ratingMinInt;
            if (ratingMin != null && ratingMin.Length > 0 && int.TryParse(ratingMin, out ratingMinInt))
            {
                _RatingMin = ratingMinInt;
            } else
            {
                _RatingMin = null;
            }

            int ratingMaxInt;
            if (ratingMax != null && ratingMax.Length > 0 && int.TryParse(ratingMax, out ratingMaxInt))
            {
                _RatingMax = ratingMaxInt;
            } else
            {
                _RatingMax = null;
            }
        }
    }
}
