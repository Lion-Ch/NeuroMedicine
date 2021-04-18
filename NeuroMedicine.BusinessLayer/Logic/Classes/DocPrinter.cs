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
                AppContainer.Instance.ViewNavigator.NavigateToModal(new ConfirmationModalVM(e.Message));
            }

            return null;
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
            var app = GetApplication("Анализ крови.docx");

            Word.Find find = app.Selection.Find;
            foreach(var item in values)
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
                    //Object newFileName = Path.Combine(_fileInfo.DirectoryName, _fileInfo.Name + $"_{patient.FullName}_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.docx");
                    //app.ActiveDocument.SaveAs2(newFileName);
                    //app.ActiveDocument.Close();
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
        public DocPrinter()
        {
            _pathToDocs = AppDomain.CurrentDomain.BaseDirectory + @"PrintDocs\";
        }
    }
}
