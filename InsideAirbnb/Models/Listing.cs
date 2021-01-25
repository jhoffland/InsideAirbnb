using System;
using System.Collections.Generic;

#nullable disable

namespace InsideAirbnb.Models
{
    public partial class Listing
    {
        public Listing()
        {
            Reviews = new HashSet<Review>();
        }

        public int Id { get; set; }
        public string ListingUrl { get; set; }
        public double ScrapeId { get; set; }
        public DateTime LastScraped { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Space { get; set; }
        public string Description { get; set; }
        public string ExperiencesOffered { get; set; }
        public string NeighborhoodOverview { get; set; }
        public string Notes { get; set; }
        public string Transit { get; set; }
        public string Access { get; set; }
        public string Interaction { get; set; }
        public string HouseRules { get; set; }
        public string ThumbnailUrl { get; set; }
        public string MediumUrl { get; set; }
        public string PictureUrl { get; set; }
        public string XlPictureUrl { get; set; }
        public int HostId { get; set; }
        public string HostUrl { get; set; }
        public string HostName { get; set; }
        public DateTime? HostSince { get; set; }
        public string HostLocation { get; set; }
        public string HostAbout { get; set; }
        public string HostResponseTime { get; set; }
        public string HostResponseRate { get; set; }
        public string HostAcceptanceRate { get; set; }
        public string HostIsSuperhost { get; set; }
        public string HostThumbnailUrl { get; set; }
        public string HostPictureUrl { get; set; }
        public string HostNeighbourhood { get; set; }
        public int? HostListingsCount { get; set; }
        public int? HostTotalListingsCount { get; set; }
        public string HostVerifications { get; set; }
        public string HostHasProfilePic { get; set; }
        public string HostIdentityVerified { get; set; }
        public string Street { get; set; }
        public string Neighbourhood { get; set; }
        public string NeighbourhoodCleansed { get; set; }
        public string NeighbourhoodGroupCleansed { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string Market { get; set; }
        public string SmartLocation { get; set; }
        public string CountryCode { get; set; }
        public string Country { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string IsLocationExact { get; set; }
        public string PropertyType { get; set; }
        public string RoomType { get; set; }
        public int? Accommodates { get; set; }
        public double? Bathrooms { get; set; }
        public int? Bedrooms { get; set; }
        public int? Beds { get; set; }
        public string BedType { get; set; }
        public string Amenities { get; set; }
        public string SquareFeet { get; set; }
        public string Price { get; set; }
        public string WeeklyPrice { get; set; }
        public string MonthlyPrice { get; set; }
        public string SecurityDeposit { get; set; }
        public string CleaningFee { get; set; }
        public int? GuestsIncluded { get; set; }
        public string ExtraPeople { get; set; }
        public int? MinimumNights { get; set; }
        public int? MaximumNights { get; set; }
        public string CalendarUpdated { get; set; }
        public string HasAvailability { get; set; }
        public int? Availability30 { get; set; }
        public int? Availability60 { get; set; }
        public int? Availability90 { get; set; }
        public int? Availability365 { get; set; }
        public DateTime? CalendarLastScraped { get; set; }
        public int? NumberOfReviews { get; set; }
        public DateTime? FirstReview { get; set; }
        public DateTime? LastReview { get; set; }
        public int? ReviewScoresRating { get; set; }
        public int? ReviewScoresAccuracy { get; set; }
        public int? ReviewScoresCleanliness { get; set; }
        public int? ReviewScoresCheckin { get; set; }
        public int? ReviewScoresCommunication { get; set; }
        public int? ReviewScoresLocation { get; set; }
        public int? ReviewScoresValue { get; set; }
        public string RequiresLicense { get; set; }
        public string License { get; set; }
        public string JurisdictionNames { get; set; }
        public string InstantBookable { get; set; }
        public string IsBusinessTravelReady { get; set; }
        public string CancellationPolicy { get; set; }
        public string RequireGuestProfilePicture { get; set; }
        public string RequireGuestPhoneVerification { get; set; }
        public int CalculatedHostListingsCount { get; set; }
        public double? ReviewsPerMonth { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
