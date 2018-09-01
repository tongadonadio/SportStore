using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using SportStore.BusinessLogic;
using SportStore.BusinessLogic.V1.Log;
using SportStore.Log.Events;

namespace SportStore.App.Management.Components
{
    /// <summary>
    /// Lógica de interacción para LogUserControl.xaml
    /// </summary>
    public partial class LogsUserControl : UserControl
    {
        private ISportStoreBusinessLogic businessLogic;

        public LogsUserControl(ISportStoreBusinessLogic businessLogic)
        {
            InitializeComponent();

            this.businessLogic = businessLogic;

            this.Loaded += LogsUserControl_Loaded;
        }

        private void LogsUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LogsListView.ItemsSource = SportStoreLog.Instance.FindEvents(le => true);
        }

        private void StartDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LogsListView.ItemsSource = FilteredSportStoreLogs();
        }

        private void EndDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LogsListView.ItemsSource = FilteredSportStoreLogs();
        }

        private IEnumerable<LogEvent> FilteredSportStoreLogs()
        {
            var startDate = StartDatePicker.SelectedDate.HasValue ? StartDatePicker.SelectedDate : null;
            var endDate = EndDatePicker.SelectedDate.HasValue ? new DateTime?(EndDatePicker.SelectedDate.Value.AddHours(23).AddMinutes(59).AddSeconds(59)) : null;

            return SportStoreLog.Instance.FindEvents(le => (startDate == null || startDate <= le.DateTime) && (endDate == null || endDate >= le.DateTime));
        }
    }
}
