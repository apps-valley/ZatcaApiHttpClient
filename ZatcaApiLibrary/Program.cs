using System.Text.Json;
using ZatcaApiHttpClient;
using ZatcaApiHttpClient.Enums;
using ZatcaApiHttpClient.Extensions;
using ZatcaApiHttpClient.Models.Invoices;
using ZatcaApiHttpClient.Models.Login;

namespace ZatcaApiLibrary
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            /// <summary>
            /// Fetches a new access token.
            /// </summary>
            /// <remarks>
            /// The access token must be cached and used in every request. It is valid for 10 hours.
            /// Before the 10 hours end, a new access token must be generated.
            /// </remarks>
            Console.WriteLine("__________________________Fetches a new access token___________________________");
            var apiResponse = await new UserAuthenticationClient().Login(new LoginCommand()
            {
                ClientId = "78bbb107-fe64-471d-9c5c-7665a0bdf964",
                ClientSecret = "Mahmoud@1512"
            });

            // If the login was not successful, terminate the program.
            if (!apiResponse.IsSuccess)
            {
                Console.WriteLine(string.Join(Environment.NewLine, apiResponse.Errors));
                return;
            }
            // Print the access token to the console.
            Console.WriteLine(apiResponse.Data.access_token);



            // Create a new InvoiceClient using the access token.
            using var InvocieClient = new InvoiceClient(apiResponse.Data.access_token);

            // Submit an invoice.
            Console.WriteLine("__________________________SubmitInvoice___________________________");
            var submitInvoiceResponse = await InvocieClient.SubmitInvoice(new SubmitInvoiceCommand
            {
                DocumentId = 8001,
                DocumentType = InvoiceKind.TaxInvoice,
                Currency = Currency.SAR,
                DocumentIssueDateTime = DateTime.Now.ToKsaDateTime(),
                SupplyDate = DateTime.Now.ToKsaDateTime(),
                Buyer = new Buyer
                {
                    BuyerName = "Taj Al Mulook General Trading LLC",
                    BuyerVatId = "310285784400003",
                    BuyerAddress = new PartyPostalAddress
                    {
                        CityName = "الرياض",
                        CitySubdivisionName = "الرياض",
                        Country = "SA",
                        BuildingNumber = "1112",
                        PostalZone = "12345",
                        StreetName = "شارع الرياض"

                    }
                },
                ReferenceId = null,
                PaymentMeans = null,
                //DiscountOnDocumentTotal = 10,
                DocumentLineItems =
                [
                    new() {
                        LineItemName = "POLYETHYLENE HDPE HHM TR - 131",
                        LineItemPrice = 10,
                        LineItemQty = 1m,
                        DiscountOnLineItem = 0,
                        TaxCategoryCode= TaxCategories.S,
                        TaxReasonDescription = null,
                        VatRateOnLineItem= 15,
                        //TaxReasonCode = TaxReasonCode.VATEX_SA_32
                    }
                ],
                InvoiceIndicator = InvoiceIndicator.None
            });
            // If the invoice submission was not successful, print the errors to the console.
            if (!submitInvoiceResponse.IsSuccess)
            {
                Console.WriteLine(string.Join(Environment.NewLine, submitInvoiceResponse.Errors));
                return;
            }
            // Print the response data to the console.
            Console.WriteLine(JsonSerializer.Serialize(submitInvoiceResponse.Data));


            Console.WriteLine("__________________________GetInvoiceStatus___________________________");
            //Get Invocie Status
            var getInvoiceStatusResponse = await InvocieClient.GetInvoiceStatus(submitInvoiceResponse.Data.UUID);
            if (!getInvoiceStatusResponse.IsSuccess)
            {
                Console.WriteLine(string.Join(Environment.NewLine, getInvoiceStatusResponse.Errors));
                return;
            }
            Console.WriteLine(JsonSerializer.Serialize(getInvoiceStatusResponse.Data));

            Console.WriteLine("__________________________PrintA3Invoice___________________________");
            //Get Invoice PDF/A3
            var getPdfA3InvoiceResponse = await InvocieClient.PrintA3Invoice(submitInvoiceResponse.Data.UUID);
            // If the PDF/A3 retrieval was not successful, print the errors to the console.
            if (!getPdfA3InvoiceResponse.IsSuccess)
            {
                Console.WriteLine(string.Join(Environment.NewLine, getPdfA3InvoiceResponse.Errors));
                return;
            }
            // Save the PDF/A3 as a file with a random name.
            var pdfA3ByteArray = getPdfA3InvoiceResponse.Data;
            var fileName = Path.GetRandomFileName() + ".pdf";
            await File.WriteAllBytesAsync(fileName, pdfA3ByteArray);

            // Print the file name to the console.
            Console.WriteLine($"PDF/A3 invoice saved as {fileName}");


        }
    }
}
