using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;
using System.Threading;
using System.Text;
using ConvertSheetToPDF.Data;

namespace SECDWebPage.Services
{
    public class GoogleSheetsService
    {
        static string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        static string ApplicationName = "Google Sheets API .NET Quickstart";
        private SheetsService service;

        public GoogleSheetsService()
        {

        }
        public Module GetModuleData()
        {
            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Sheets API service.
            service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });


            // Define request parameters.
            String spreadsheetId = "1ur6ahoiBFPEUqX4WLwwycNgApAQMIeEw2UfCUK9MvwI";
            var module = new Module();
            module.Sections = new List<Section>();
            module.Header
            for (int i = 1; i <= 1; i++)
            {
                module.Sections.Add(GetSection(spreadsheetId, $"Topic {i}"));
            }

            return module;
        }

        public Dictionary<string, string> GetFromSheet(string spreadsheetId, string tabName, string range)
        {
            String rangeString = $"{tabName}!{range}";//range that contains section data
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(spreadsheetId, rangeString);
            //https://docs.google.com/spreadsheets/d/1ur6ahoiBFPEUqX4WLwwycNgApAQMIeEw2UfCUK9MvwI/edit#gid=1910286277
            // Prints the names and majors of students in a sample spreadsheet:
            // https://docs.google.com/spreadsheets/d/1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms/edit
            ValueRange response = request.Execute();
            //IList<IList<Object>> values = response.Values;
            Dictionary<string, string> values = new Dictionary<string, string>();
            foreach (var value in response.Values)
            {
                values.Add((string)value.ElementAtOrDefault(0), (string)value.ElementAtOrDefault(6));

            }
            return values;
        }
        public Section GetSection(string spreadsheetId, string tabName)
        {
            var sectionDictionary = GetFromSheet(spreadsheetId, tabName, "A2:G27");

            var section = new Section()
            {
                Name = sectionDictionary.GetValueOrDefault("Section Name:"),
                Description = sectionDictionary.GetValueOrDefault("Section Description:"),
                Display = sectionDictionary.GetValueOrDefault("Display Section?") == "Yes",
                ID = sectionDictionary.GetValueOrDefault("Section ID:"),
                Image = sectionDictionary.GetValueOrDefault("Section Image:"),
                TemplateTabName = sectionDictionary.GetValueOrDefault("Template Tab Name:"),
                Paragraphs = new List<Paragraph>()
            };

            for (int i = 1; i <= 10; i++)
            {
                if (sectionDictionary.GetValueOrDefault($"Section Paragraph {i}:") != null)
                {
                    section.Paragraphs.Add(new Paragraph()
                    {
                        Text = sectionDictionary.GetValueOrDefault($"Section Paragraph {i}:"),
                        Image = sectionDictionary.GetValueOrDefault($"Section Image {i}:")
                    });
                }
            }
            section.Dialogue = GetDialogue(spreadsheetId, tabName, "A28:G42");
            section.Assessment = GetAssessment(spreadsheetId, tabName, "A43:G134");
            section.References = new List<Reference>();
            section.References.Add(GetReference(spreadsheetId, tabName, "A135:G147"));
            section.References.Add(GetReference(spreadsheetId, tabName, "A148:G160"));

            return section;

        }

        private Reference GetReference(string spreadsheetId, string tabName, string range)
        {
            var refereneceDictionary = GetFromSheet(spreadsheetId, tabName, range);

            var reference = new Reference()
            {
                Name = refereneceDictionary.GetValueOrDefault($"Section Name:"),
                Description = refereneceDictionary.GetValueOrDefault($"Section Description:"),
                Display = refereneceDictionary.GetValueOrDefault("Display Section?") == "Yes",
                Resources = new List<Resource>()
            };

            for (int i = 1; i <= 5; i++)
            {
                if (refereneceDictionary.GetValueOrDefault($"Reference/Resource {i.ToString("00")} Text:") != null)
                {
                    reference.Resources.Add(new Resource()
                    {
                        Text = refereneceDictionary.GetValueOrDefault($"Reference/Resource {i.ToString("00")} Text:"),
                        Link = refereneceDictionary.GetValueOrDefault($"Reference/Resource {i.ToString("00")} Link:"),
                    });
                }
            }
            return reference;
        }

        private Assessment GetAssessment(string spreadsheetId, string tabName, string range)
        {
            var assessmentDictionary = GetFromSheet(spreadsheetId, tabName, range);
            var assessment = new Assessment()
            {
                Name = assessmentDictionary.GetValueOrDefault($"Section Name:"),
                Description = assessmentDictionary.GetValueOrDefault($"Section Description:"),
                Questions = new List<Question>()
            };
            for (int i = 1; i <= 10; i++)
            {
                if (assessmentDictionary.GetValueOrDefault($"Question {i.ToString("00")}:") != null)
                {
                    assessment.Questions.Add(new Question()
                    {
                        Text = assessmentDictionary.GetValueOrDefault($"Question {i.ToString("00")}:"),
                        Answers = new List<Answer>()
                        {
                            new Answer()
                            {
                                 Text = assessmentDictionary.GetValueOrDefault($"Question {i.ToString("00")} Answer A:"),
                                 Detail = assessmentDictionary.GetValueOrDefault($"Question {i.ToString("00")} Answer A Detail:")
                            },
                            new Answer()
                            {
                                 Text = assessmentDictionary.GetValueOrDefault($"Question {i.ToString("00")} Answer B:"),
                                 Detail = assessmentDictionary.GetValueOrDefault($"Question {i.ToString("00")} Answer B Detail:")
                            },
                            new Answer()
                            {
                                 Text = assessmentDictionary.GetValueOrDefault($"Question {i.ToString("00")} Answer C:"),
                                 Detail = assessmentDictionary.GetValueOrDefault($"Question {i.ToString("00")} Answer C Detail:")
                            },
                            new Answer()
                            {
                                 Text = assessmentDictionary.GetValueOrDefault($"Question {i.ToString("00")} Answer D:"),
                                 Detail = assessmentDictionary.GetValueOrDefault($"Question {i.ToString("00")} Answer D Detail:")
                            }
                        }
                    });
                }
            }
            return assessment;
        }

        private Dialogue GetDialogue(string spreadsheetId, string tabName, string range)
        {
            var dialogueDictionary1 = GetFromSheet(spreadsheetId, tabName, range);
            var dialogue = new Dialogue()
            {
                Name = dialogueDictionary1.GetValueOrDefault($"Section Name:"),
                Description = dialogueDictionary1.GetValueOrDefault($"Section Description:"),
                Display = dialogueDictionary1.GetValueOrDefault("Display Section?") == "Yes",
                Setup = dialogueDictionary1.GetValueOrDefault($"Dialogue Setup:"),
                Image = dialogueDictionary1.GetValueOrDefault($"Section Image:"),
                Lines = new List<string>()
            };
            for (int i = 1; i <= 10; i++)
            {
                if (dialogueDictionary1.GetValueOrDefault($"Dialogue Line {i.ToString("00")}:") != null)
                {
                    dialogue.Lines.Add(dialogueDictionary1.GetValueOrDefault($"Dialogue Line {i.ToString("00")}:"));
                }
            }
            return dialogue;
        }
    }
}
