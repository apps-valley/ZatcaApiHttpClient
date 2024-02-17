using System.ComponentModel.DataAnnotations;

namespace ZatcaApiHttpClient.Models.Invoices
{
    public class PartyPostalAddress
    {

        /// <summary>
        /// The street name of the address.
        /// </summary>
        [Length(maximumLength: 127, minimumLength: 1, ErrorMessage = "عنوان المشتري – الشارع يجب أن يحتوي على حرف واحد على الأقل ولا يزيد عن 127 حرف.")]
        public string StreetName { get; set; }

        /// <summary>
        /// Any additional information about the street name.
        /// </summary>
        public string AdditionalStreetName { get; set; }

        /// <summary>
        /// The building number of the address.
        /// </summary>
        public string BuildingNumber { get; set; }

        /// <summary>
        /// The plot identification number of the address.
        /// </summary>
        public string PlotIdentification { get; set; }

        /// <summary>
        /// The subdivision name of the city for the address.
        /// </summary>
        public string CitySubdivisionName { get; set; }

        /// <summary>
        /// The city name of the address.
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// The postal zone (or zip code) of the address.
        /// </summary>
        [MaxLength(5)]
        public string PostalZone { get; set; }

        /// <summary>
        /// The subentity of the country for the address.
        /// </summary>
        public string CountrySubentity { get; set; }

        /// <summary>
        /// The country of the address.
        /// </summary>
        [MaxLength(2)]
        public string Country { get; set; }
    }
}
