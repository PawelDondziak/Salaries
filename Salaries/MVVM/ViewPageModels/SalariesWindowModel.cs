using CsvHelper;
using Salaries.Helpers;
using Salaries.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Xml.Linq;

namespace Salaries.MVVM.ViewPageModels
{
    public class SalariesWindowModel : INotifyPropertyChanged
    {
        private readonly string _noDataString = "--";
        public ObservableCollection<Employee> Employees { get; set; } = new ObservableCollection<Employee>();
        public string NumberOfPeopleNamedSmith
        {
            get
            {
                string lastName = "Smith";
                if (!Employees.Any(X => X.LastName == lastName))
                    return _noDataString;
                return Employees.Count(X => X.LastName == lastName).ToString();
            }
        }
        public string SmithWithBiggestSalary
        {
            get
            {
                string lastName = "Smith";
                if (!Employees.Any(X => X.LastName == lastName))
                    return _noDataString;
                return Employees.Where(X => X.LastName == lastName).Max(X => X.Salary).ToString();
            }
        }
        public string DepartmentWithBiggestEarnings
        {
            get
            {
                if (!Employees.Any())
                    return _noDataString;

                var highestTotalSalaryDepartment = Employees.GroupBy(x => x.Department)
                    .Select(x => new
                    {
                        Department = x.Key,
                        TotalSalary = x.Sum(y => y.Salary)
                    })
                    .OrderByDescending(x => x.TotalSalary).FirstOrDefault();

                try
                {
                    return highestTotalSalaryDepartment.TotalSalary.ToString();
                }
                // here I didn't catch the exception, because I don't have any service to deal with it in this apllication
                catch { return _noDataString; }
            }
        }
        public string DepartmentWithMostEmployees
        {
            get
            {
                if (!Employees.Any())
                    return _noDataString;

                string? nameOfDepartment = Employees.GroupBy(x => x.Department)
                    .OrderByDescending(x => x.Count())
                    .Select(x => x.Key).FirstOrDefault();

                return string.IsNullOrEmpty(nameOfDepartment) ? _noDataString : nameOfDepartment;
            }
        }

        public bool OrderByLastNames;

        public event PropertyChangedEventHandler? PropertyChanged;

        public SalariesWindowModel()
        {
            LoadDataFromFile();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void LoadDataFromFile()
        {
            try
            {
                using (var reader = new StreamReader(@"Data\salaries.csv"))
                {
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        Employees = new ObservableCollection<Employee>(csv.GetRecords<Employee>());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public ICommand OrderByLastNamesCommand => new Command(() =>
        {
            OrderByLastNames = !OrderByLastNames;

            if (OrderByLastNames)
                Employees = new ObservableCollection<Employee>(Employees.OrderBy(X => X.LastName));
            else
                LoadDataFromFile();

            OnPropertyChanged(nameof(Employees));
        });
    }
}
