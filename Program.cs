using System.Diagnostics.Metrics;

namespace CalculateVacations
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random(); //инициализируем объект рандомизатор
            var employees = new List<Employee>() //инициализируем сотрудников
            {
                new Employee("Иванов Иван Иванович"),
                new Employee("Петров Петр Петрович"),
                new Employee("Юлина Юлия Юлиановна"),
                new Employee("Сидоров Сидор Сидорович"),
                new Employee("Павлов Павел Павлович"),
                new Employee("Георгиев Георг Георгиевич")
            };
            foreach (var employee in employees)
            {
                SetEmployeeVacationSchedule(employee, random); //заполняем график отпусков сотрудника
            }
            foreach (var employee in employees)
            {
                Console.WriteLine($"График отпусков сотрудника: {employee.Name}\n\t" +
                    $"{string.Join("\n\t", employee.VacationDays)}"); //выводим график
            }
        }

        static void SetEmployeeVacationSchedule(Employee employee, Random random)
        {
            while (employee.NumberOfVacationDays > 0)
            {
                var vacationLength = 0 == random.Next(2) ? 7 : 14; //случайным образом получаем количество дней отпуска
                if (employee.NumberOfVacationDays <= 7) vacationLength = 7;
                var startVacationDate = GetRandomWorkingDate(random); //получаем дату начала отпуска
                var endVacationDate = startVacationDate.AddDays(vacationLength); //получаем дату конца отпуска
                if (VacationMeetsConditions(employee, startVacationDate, endVacationDate))
                {
                    for (var day = startVacationDate; day < endVacationDate; day = day.AddDays(1)) //заполняем график согласно количеству дней отпуска
                    {
                        employee.VacationDays.Add(day);
                    }
                    employee.NumberOfVacationDays -= vacationLength;
                }
            }
        }

        static bool VacationMeetsConditions(Employee employee, DateTime startDate, DateTime endDate)
        {
            var existStart = employee.VacationDays.Any(element => element.AddMonths(-1) <= startDate && startDate <= element.AddMonths(1));
            var existEnd = employee.VacationDays.Any(element => element.AddMonths(-1) <= endDate && endDate <= element.AddMonths(1));
            //условие, что между отпусками сотрудника был промежуток минимум в 1 месяц
            return !(existStart || existEnd);
        }

        static DateTime GetRandomWorkingDate(Random random)
        {
            var firstDateOfYear = new DateTime(DateTime.Now.Year, 1, 1);
            var lastDateOfYear = new DateTime(DateTime.Now.Year, 12, 31);
            var workingDate = firstDateOfYear.AddDays(random.Next((lastDateOfYear - firstDateOfYear).Days));
            if (workingDate.DayOfWeek != DayOfWeek.Saturday && workingDate.DayOfWeek != DayOfWeek.Sunday) //является ли день будним
                return workingDate;
            else return GetRandomWorkingDate(random); //если нет рекурсивно ищем новую дату
        }
    }
}