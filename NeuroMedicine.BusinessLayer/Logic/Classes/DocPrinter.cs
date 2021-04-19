using DataLayer.Models.Classes;
using DataLayer.SqlServer.Tables;
using NeuroMedicine.BusinessLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;

namespace NeuroMedicine.BusinessLayer.Logic
{
    public class DocPrinter
    {
        private string _pathToDocs;

        private readonly string nameOrgTag = "{nameOrg}";

        private readonly string fioPatientTag = "{fio}";
        private readonly string fioDocTag= "{fioDoc}";
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

        private Word.Application GetApplication(string nameDoc)
        {
            Word.Application app = null;
            try
            {
                if (File.Exists(_pathToDocs + nameDoc))
                {

                    app = new Word.Application();
                    var _fileInfo = GetFileInfo(nameDoc);
                    app = new Word.Application();

                    app.Documents.Open(_fileInfo.FullName);
                    return app;
                }
                else
                {
                    AppContainer.Instance.ViewNavigator.NavigateToModal(new ConfirmationModalVM("Файл шаблона не найден!"));
                }
            }
            catch(Exception e)
            {
            }

            return null;
        }

        private Word.Application PrintDoc(string nameDoc, Dictionary<string,string> keyValues)
        {
            var app = GetApplication(nameDoc);

            if (app != null)
            {
                Word.Find find = app.Selection.Find;
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

                app.Visible = true;
            }
            else
            {
                AppContainer.Instance.ViewNavigator.NavigateToModal(new ConfirmationModalVM("Файл шаблона не найден!"));
            }

            return app;
        }
        private FileInfo GetFileInfo(string nameFile)
        {
            if(File.Exists(_pathToDocs + nameFile))
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
            PrintDoc("Анализ крови.docx", values);
        }
        public void PrintUrineTest(Patient patient, RefUrineTest refUrineTest)
        {
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
            PrintDoc("Анализ мочи.docx", values);
        }

        public void PrintTalon(string fioDoc, DateTime date, string fioPatient, string nameService)
        {
            Dictionary<string, string> values = new Dictionary<string, string>()
            {
                {nameOrgTag,AppContainer.Instance.Settings.NameOrganization },
                {fioPatientTag,fioPatient },
                {fioDocTag, fioDoc },
                {serviceTag, nameService },
                {dateTag, date.ToString("HH:mm dd.MM.yyyy") }
            };
            PrintDoc("Талон.docx", values);
        }
        public void PrintImg(Patient patient, string imgPath, string nameService, string diagnosis, User doctor)
        {
            //Dictionary<string, string> values = new Dictionary<string, string>()
            //{
            //    {nameOrgTag,AppContainer.Instance.Settings.NameOrganization },
            //    {fioPatientTag,fioPatient },
            //    {fioDocTag, fioDoc },
            //    {serviceTag, nameService },
            //    {dateTag, date.ToString("HH:mm dd.MM.yyyy") }
            //};
            //var sel = app.Selection;
            //sel.Find.Text = "{image}";
            //sel.Find.Execute(Replace: Word.WdReplace.wdReplaceNone);
            //sel.Range.Select();
            //sel.InlineShapes.AddPicture(@"C:\Users\levac\OneDrive\Рабочий стол\Учеба\Диплом\Dataset\test\NORMAL\IM-0001-0001.jpeg");
            //app.Visible = true;
        }
        public DocPrinter()
        {
            _pathToDocs = AppDomain.CurrentDomain.BaseDirectory + @"PrintDocs\";
        }
    }
}
