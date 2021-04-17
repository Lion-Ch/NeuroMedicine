using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroMedicine.BusinessLayer.Logic.Classes
{
    public class DateTimeConverter
    {
        private List<int> _numDays;

        public DateTimeConverter(List<int> numDays)
        {
            _numDays = numDays;
        }

        public DateTimeConverter()
        {
        }

        /// <summary>
        /// Даты работы начиная с текущего на месяц
        /// </summary>
        public List<DateTime> GetWorkDays()
        {
            List<DateTime> result = new List<DateTime>();
            for(int i = 0; i<4;i++)
            {
                foreach (var day in _numDays)
                {
                    var d = day;
                    if (day == 0)
                        d = 7;
                    DateTime dateTime = DateTime.Now.AddDays(7*i);
                    int currDay = (int)DateTime.Now.DayOfWeek;
                    if (day > currDay)
                        dateTime = dateTime.AddDays(day - currDay);
                    else if (day < currDay)
                        dateTime = dateTime.AddDays(day - currDay + 7);
                    result.Add(dateTime);
                }
            }
            result = result.OrderBy(x => x.Date).ToList();
            return result;
        }

        public List<DateTime> GetFreeWorkTimes(DateTime timeStart,DateTime timeFinish,int numPatients,List<DateTime> dateTimes)
        {
            //Считаем количество минут на 1го пацеинта
            //Плюсуем к стартовой дате, пока не пройдем каждого пациента
            //Удаляем похожие даты
            List<DateTime> times = new List<DateTime>() { timeStart };
            int timeWork = (int)(timeFinish - timeStart).TotalMinutes / numPatients;
            DateTime lastTime = timeStart;
            for (int i = 0; i < numPatients - 1; i++)
            {
                lastTime = lastTime.AddMinutes(timeWork);
                times.Add(lastTime);
            }
            List<int> indexes = new List<int>();
            foreach(var time in dateTimes)
            {
                for(int i=0;i<times.Count;i++)
                {
                    if (time.TimeOfDay == times[i].TimeOfDay)
                    {
                        times.RemoveAt(i);
                    }
                }
            }

            return times;
        }
    }
}
