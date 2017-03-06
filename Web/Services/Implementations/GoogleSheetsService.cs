using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using Web.Services.Interfaces;

namespace Web.Services.Implementations
{
    public class GoogleSheetsService: IGoogleSheetsService
    {
        private string applicationName = "LinCollect";

        ServiceAccountCredential serviceAccountCredential { get; set; }
        public GoogleSheetsService()
        {
            string credentialPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content");
            credentialPath = Path.Combine(credentialPath, "LinCollect-7a500f9e0d6a.p12");

            var certificate = new X509Certificate2(credentialPath, "notasecret", X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.Exportable);
            serviceAccountCredential = new ServiceAccountCredential(new ServiceAccountCredential.Initializer("linc-809@winter-sensor-149718.iam.gserviceaccount.com")
            {
                Scopes = new string[]{ SheetsService.Scope.SpreadsheetsReadonly }
            }.FromCertificate(certificate));
        }

        public List<string> GetCompanies(string fileId, ref string error)
        {
            var service = new SheetsService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = serviceAccountCredential,
                ApplicationName = applicationName
            });

            try
            {
                var buff = service.Spreadsheets.Get(fileId);
                buff.IncludeGridData = true;
                var copy = buff.Execute();

                return copy.Sheets.Skip(1).FirstOrDefault().Data.FirstOrDefault()
                    .RowData.Select(x => x.Values.FirstOrDefault().FormattedValue).Skip(1).ToList();
            }
            catch (Exception ex)
            {
                error = "Not have access to the file '{0}' or is not 'Node List' sheet. Open <a target='_blank' href='https://docs.google.com/spreadsheets/d/{1}'>THIS LINK</a> to fix problem.";
                return new List<string>();
            }
        }

        public Task<List<string[]>> GetEnumerators(string fileId, ref string error)
        {
            var service = new SheetsService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = serviceAccountCredential,
                ApplicationName = applicationName
            });

            var userList = new List<string[]>();

            try
            {
                var sheet = service.Spreadsheets.Values.Get(fileId, "Mailing list!A2:B");

                var rows = sheet.Execute();

                foreach (var rowsValue in rows.Values)
                {
                    if (rowsValue.FirstOrDefault() != null)
                    {
                        userList.Add(new[] {rowsValue[0].ToString(), rowsValue[1].ToString()});
                    }
                }

                return Task.FromResult(userList);
            }
            catch (Exception e)
            {
                error = "Not have access to the file '{0}' or is not 'Node List' sheet. Open <a target='_blank' href='https://docs.google.com/spreadsheets/d/{1}'>THIS LINK</a> to fix problem.";
                return Task.FromResult(userList);
            }
        }
    }
}