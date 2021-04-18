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
            var word = new Word.Application();
            try
            {
                if(File.Exists(_pathToDocs + "Анализ крови.docx"))
                {
                    var wordDoc = word.Documents.Add(_pathToDocs + "Анализ крови.docx");//Открываем шаблон
                    ReplaceStub(fioPatientTag, patient.FullName, wordDoc);//Заменяем метку на данные из формы(здесь конкретно из текстбокса с именем textBox_fio)
                    wordDoc.SaveAs2("ФЫафа");
                    ///Может быть много таких меток
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
