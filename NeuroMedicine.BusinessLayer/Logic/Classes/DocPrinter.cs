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

        private readonly string fioPatientTag = "{fio}";

        public void PrintBloodTest(Patient patient, RefBloodTest bloodTest)
        {
            Word.Application app = null;
            try
            {
                if(File.Exists(_pathToDocs + "Анализ крови.docx"))
                {

                    app = new Word.Application();

                    //var wordDoc = app.Documents.Add(_pathToDocs + "Анализ крови.docx");//Открываем шаблон
                    //ReplaceStub(fioPatientTag, patient.FullName, wordDoc);//Заменяем метку на данные из формы(здесь конкретно из текстбокса с именем textBox_fio)
                    //app.ActiveDocument.Prin
                    //wordDoc.SaveAs2(wordDoc.);
                    ///Может быть много таких меток
                    var _fileInfo = GetFileInfo("Анализ крови.docx");
                    app = new Word.Application();
                    Object missing = Type.Missing;

                    app.Documents.Open(_fileInfo.FullName);

                    Word.Find find = app.Selection.Find;
                    //Сделать цикл для словаря
                    find.Text = fioPatientTag;
                    find.Replacement.Text = patient.FullName;

                    Object wrap = Word.WdFindWrap.wdFindContinue;
                    Object replace = Word.WdReplace.wdReplaceAll;

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
                    app.Visible = true;
                    Object newFileName = Path.Combine(_fileInfo.DirectoryName, _fileInfo.Name + $"_{patient.FullName}_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.docx");
                    app.ActiveDocument.SaveAs2(newFileName);
                    //app.ActiveDocument.Close();
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
            finally
            {
                //if(app!= null)
                //{
                //    app.Quit();
                //}    
            }
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
