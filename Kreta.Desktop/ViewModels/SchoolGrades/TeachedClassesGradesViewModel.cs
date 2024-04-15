using CommunityToolkit.Mvvm.ComponentModel;
using Kreta.Desktop.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kreta.Desktop.ViewModels.SchoolGrades
{
    public partial class TeachedClassesGradesViewModel : BaseViewModel
    {
        [ObservableProperty]
        public string _title = "Tanított osztályok jegyei";
    }
}
