using DataLayer.Models.Classes;
using DataLayer.SqlServer.Tables;
using NeuroMedicine.BusinessLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;

namespace NeuroMedicine.BusinessLayer.Logic
{
    public class DocPrinter
    {
        private Word.Application _app;
        private string _resultFolder = AppDomain.CurrentDomain.BaseDirectory + @"Results\";
        private string _pathToDocs;

        private readonly string nameOrgTag = "{nameOrg}";

        private readonly string fioPatientTag = "{fio}";
        private readonly string fioDocTag = "{fioDoc}";
        private readonly string dateBirthTag = "{dateBirth}";
        private readonly string imageTag = "{image}";
        private readonly string diagnosisTag = "{diagnosis}";
        private readonly string serviceTag = "{service}";
        private readonly string dateTag = "{date}";
        private readonly string soeTag = "{ESR}";
        private readonly string hbTag = "{HB}";
        private readonly string leukocytesTag = "{Leukocytes}";
        private readonly string erythrocytesTag = "{Erythrocytes}";
        private readonly string erythrocyteIndexTag = "{ErythrocyteIndex}";
        private readonly string coagulabilityTag = "{Coagulability}";
        private readonly string plateletsTag = "{Platelets}";
        private readonly string prothrombinIndexTag = "{ProthrombinIndex}";
        private readonly string bTag = "{B}";
        private readonly string eTag = "{E}";
        private readonly string yuTag = "{YU}";
        private readonly string pTag = "{P}";
        private readonly string fromTag = "{FROM}";
        private readonly string lTag = "{L}";
        private readonly string mTag = "{M}";
        private readonly string QuantityTag = "{Quantity}";
        private readonly string TransparencyTag = "{Transparency}";
        private readonly string ColorTag = "{Color}";
        private readonly string SpecificGravityTag = "{SpecificGravity}";
        private readonly string ReactionTag = "{Reaction}";
        private readonly string ProteinTag = "{Protein}";
        private readonly string SugarTag = "{Sugar}";
        private readonly string CylindersTag = "{Cylinders}";
        private readonly string RenalEpitheliumTag = "{RenalEpithelium}";
        private readonly string EpitheliumTag = "{Epithelium}";


        private readonly string addressTag = "{address}";
        private readonly string cityOrgTag = "{cityOrg}";
        private readonly string fioDirectorTag = "{fioDirector}";
        private readonly string passportGetTag = "{passportGet}";
        private readonly string passportTag = "{passport}";
        private readonly string priceTag = "{price}";
        private readonly string durationServiceTag = "{durationService}";
        private readonly string addressOrgTag = "{addressOrg}";
        private readonly string indexOrgTag = "{indexOrg}";
        private readonly string phoneOrgTag = "{phoneOrg}";
        private readonly string innOrgTag = "{innOrg}";
        private readonly string rsOrgTag = "{rsOrg}";
        private readonly string bankOrgTag = "{bankOrg}";
        private readonly string ksOrgTag = "{ksOrg}";
        private readonly string bikOrgTag = "{bikOrg}";
        private readonly string phonePatTag = "{phonePat}";

        private Word.Document AddDocument(string nameDoc)
        {
            try
            {
                _app = new Word.Application();
                if (File.Exists(_pathToDocs + nameDoc))
                {
                    var _fileInfo = GetFileInfo(nameDoc);

                    return _app.Documents.Add(_fileInfo.FullName);
                }
                else
                {
                    AppContainer.Instance.ViewNavigator.NavigateToModal(new ConfirmationModalVM("Файл шаблона не найден!"));
                }
            }
            catch (Exception e)
            {
            }

            return null;
        }

        private void FillParameters(Dictionary<string, string> keyValues)
        {
            if (_app != null)
            {
                Word.Find find = _app.Selection.Find;
                foreach (var item in keyValues)
                {
                    //Сделать цикл для словаря
                    find.Text = item.Key;
                    find.Replacement.Text = item.Value;

                    Object wrap = Word.WdFindWrap.wdFindContinue;
                    Object replace = Word.WdReplace.wdReplaceAll;
                    Object missing = Type.Missing;

                    find.Execute(FindText: Type.Missing,
                                MatchCase: false,
                                MatchWholeWord: false,
                                MatchWildcards: false,
                                MatchSoundsLike: missing,
                                MatchAllWordForms: false,
                                Forward: true,
                                Wrap: wrap,
                                Format: false,
                                ReplaceWith: missing,
                                Replace: replace);
                }
            }
            else
            {
                AppContainer.Instance.ViewNavigator.NavigateToModal(new ConfirmationModalVM("Файл шаблона не найден!"));
            }
        }
        private FileInfo GetFileInfo(string nameFile)
        {
            if (File.Exists(_pathToDocs + nameFile))
            {
                return new FileInfo(_pathToDocs + nameFile);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Замена нужного параметра на текст
        /// </summary>
        private void ReplaceStub(string stubToReplace, string text, Word.Document worldDocument)
        {
            var range = worldDocument.Content;
            range.Find.ClearFormatting();
            object wdReplaceAll = Word.WdReplace.wdReplaceAll;
            range.Find.Execute(FindText: stubToReplace, ReplaceWith: text, Replace: wdReplaceAll);
        }
        public void PrintBloodTest(Patient patient, RefBloodTest bloodTest)
        {
            try
            {
                AddDocument("Анализ крови.docx");
                Dictionary<string, string> values = new Dictionary<string, string>()
                {
                    {nameOrgTag,AppContainer.Instance.Settings.NameOrganization },
                    {fioPatientTag,patient.FullName },
                    {dateTag, bloodTest.Date.ToString("dd.MM.yyyy") },
                    {soeTag,bloodTest.ESR.ToString()},
                    {hbTag,bloodTest.HB.ToString() },
                    {leukocytesTag,bloodTest.Leukocytes.ToString() },
                    {erythrocytesTag,bloodTest.Erythrocytes.ToString() },
                    {erythrocyteIndexTag,bloodTest.ErythrocyteIndex.ToString() },
                    {coagulabilityTag,bloodTest.Coagulability.ToString() },
                    {plateletsTag,bloodTest.Platelets.ToString() },
                    {prothrombinIndexTag,bloodTest.ProthrombinIndex.ToString() },
                    {bTag,bloodTest.B.ToString() },
                    {eTag,bloodTest.E.ToString() },
                    {yuTag,bloodTest.YU.ToString() },
                    {pTag,bloodTest.P.ToString() },
                    {fromTag,bloodTest.FROM.ToString() },
                    {lTag,bloodTest.L.ToString() },
                    {mTag,bloodTest.M.ToString() }
                };
                FillParameters(values);
                _app.Visible = true;
            }
            catch { }
            finally
            {
                if (_app != null)
                    _app.Quit();
            }
            
        }
        public void PrintUrineTest(Patient patient, RefUrineTest refUrineTest)
        {
            try
            {
                AddDocument("Анализ мочи.docx");
                Dictionary<string, string> values = new Dictionary<string, string>()
            {
                {nameOrgTag,AppContainer.Instance.Settings.NameOrganization },
                {fioPatientTag,patient.FullName },
                {dateTag, refUrineTest.Date.ToString("dd.MM.yyyy") },
                {QuantityTag,refUrineTest.Quantity.ToString()},
                {ColorTag,refUrineTest.Color.ToString() },
                {TransparencyTag,refUrineTest.Transparency.ToString() },
                {SpecificGravityTag,refUrineTest.SpecificGravity.ToString() },
                {ReactionTag,refUrineTest.Reaction.ToString() },
                {ProteinTag,refUrineTest.Protein.ToString() },
                {SugarTag,refUrineTest.Sugar.ToString() },
                {CylindersTag,refUrineTest.Cylinders.ToString() },
                {RenalEpitheliumTag,refUrineTest.RenalEpithelium.ToString() },
                {erythrocytesTag,refUrineTest.Erythrocytes.ToString() },
                {leukocytesTag,refUrineTest.Leukocytes.ToString() },
                {EpitheliumTag,refUrineTest.Epithelium.ToString() }
            };
                FillParameters(values);
                _app.Visible = true;
            }
            catch { }
        }

        public void PrintTalon(string fioDoc, DateTime date, string fioPatient, string nameService)
        {
            try
            {
                AddDocument("Талон.docx");
                Dictionary<string, string> values = new Dictionary<string, string>()
                {
                    {nameOrgTag,AppContainer.Instance.Settings.NameOrganization },
                    {fioPatientTag,fioPatient },
                    {fioDocTag, fioDoc },
                    {serviceTag, nameService },
                    {dateTag, date.ToString("HH:mm dd.MM.yyyy") }
                };
                FillParameters(values);
                _app.Visible = true;
            }
            catch { }
            finally
            {
                if (_app != null)
                    _app.Quit();
            }
        }
        public void PrintLungs(string nameService, List<PatientDiagnosis> patientDiagnoses)
        {
            try
            {
                var doc = AddDocument("Флюорография.docx");
                doc.Range().Copy();
                for(int i=0;i<patientDiagnoses.Count;i++)
                {
                    var p = patientDiagnoses[i];
                    Dictionary<string, string> values = new Dictionary<string, string>()
                    {
                        {nameOrgTag,AppContainer.Instance.Settings.NameOrganization },
                        {fioPatientTag, p.RefPatient.FullName },
                        {fioDocTag, AppContainer.Instance.CurrentUser.FullName},
                        {serviceTag, nameService },
                        {dateTag, p.Date.ToString("HH:mm dd.MM.yyyy")},
                        {dateBirthTag, p.RefPatient.DateBirth.ToString("dd.MM.yyyy") },
                        {diagnosisTag, p.Conclusion }
                    };
                    FillParameters(values);
                    var sel = _app.Selection;
                    sel.Find.Text = "{image0}";
                    sel.Range.Select();
                    sel.Find.Execute(Replace: Word.WdReplace.wdReplaceNone);
                    sel.InlineShapes.AddPicture(p.PhotoUrl);

                    Word.Range endRange = doc.GoTo(Word.WdGoToItem.wdGoToLine, Word.WdGoToDirection.wdGoToLast, Missing.Value, Missing.Value);

                    if(i != patientDiagnoses.Count - 1)
                        endRange.Paste();
                    _app.Visible = true;
                }
            }
            catch { }
            finally
            {
                if (_app != null)
                    _app.Quit();
            }
        }
        public void PrintContract(Patient patient, Service service, User user)
        {
            try
            {
                var doc = AddDocument("Договор.docx");
                var set = AppContainer.Instance.Settings;
                Dictionary<string, string> values = new Dictionary<string, string>()
                {
                    {nameOrgTag, AppContainer.Instance.Settings.NameOrganization },
                    {fioDirectorTag,set.fioDirector },
                    {cityOrgTag, set.cityOrgTag},
                    {addressOrgTag, set.addressOrgTag },
                    {indexOrgTag,set.indexOrgTag },
                    {phoneOrgTag,set.phoneOrgTag },
                    {innOrgTag,set.innOrgTag },
                    {rsOrgTag, set.rsOrgTag },
                    {bankOrgTag, set.bankOrgTag  },
                    {ksOrgTag, set.ksOrgTag},
                    {bikOrgTag, set.bikOrgTag },
                    {dateTag,DateTime.Now.ToString("dd.MM.yyyy") },
                    {fioPatientTag, patient.FullName},
                    {passportTag, patient.Passport },
                    {passportGetTag, patient.PassportInfo },
                    {addressTag, patient.Address },
                    {priceTag, service.Price.ToString() },
                    {serviceTag, service.Name },
                    {durationServiceTag, service.Duration.ToString()  },
                    {fioDocTag, user.FullName },
                    {phonePatTag,patient.Mobile }
                };
                FillParameters(values);
                _app.Visible = true;

            }
            catch { }
            finally
            {
                if (_app != null)
                    _app.Quit();
            }
        }
        private void CreateFolders()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(_resultFolder);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
        }
        public DocPrinter()
        {
            _pathToDocs = AppDomain.CurrentDomain.BaseDirectory + @"PrintDocs\";
            CreateFolders();
        }
    }
}
