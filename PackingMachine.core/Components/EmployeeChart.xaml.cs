using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Media;

namespace PackingMachine.core.Components
{
    /// <summary>
    /// Interaction logic for EmployeeChart.xaml
    /// </summary>
    public partial class EmployeeChart: UserControl, INotifyPropertyChanged
    {

        public Func<Double,string> yFormatter { set; get; }
        private SeriesCollection seriesCollection;
        public SeriesCollection SeriesCollection { get => seriesCollection; set { seriesCollection=value; OnPropertyChanged( ); } }
        private List<string> labels;
        public List<string> Labels { get => labels; set { labels=value; OnPropertyChanged( ); } }
        public string Title { get => title; set { title=value; OnPropertyChanged( ); } }

        private string title;

        private ChartValues<Double> timeStandard;
        public ChartValues<Double> TimeStandard { get => timeStandard; set { timeStandard=value; OnPropertyChanged( ); } }
        private ChartValues<Double> executionTime;
        public ChartValues<Double> ExecutionTime { get => executionTime; set { executionTime=value; OnPropertyChanged( ); } }

        LineSeries LineSpeed = new LineSeries( )
        {
            Title="Thời gian chuẩn",
            PointGeometry=DefaultGeometries.Circle,
            PointForeground=Brushes.DeepSkyBlue,
            PointGeometrySize=8
        };
        LineSeries LineData =
        new LineSeries( )
        {
            Title="Thời gian đóng gói 1 sản phẩm",
            PointGeometry=DefaultGeometries.Circle,
            PointForeground=Brushes.SkyBlue,
            PointGeometrySize=8
        };
        public EmployeeChart ( )
        {
            InitializeComponent( );
            Title="Công nhân chuyền 1";
            SeriesCollection=new SeriesCollection( );
            ExecutionTime=new ChartValues<Double>( );
            TimeStandard=new ChartValues<Double>( );

            Labels=new List<string>( );
            //    yFormatter = value => value.ToString();
            chart( );
            this.DataContext=this;
        }
        public void chart ( )
        {

            //for ( int i = 0;i<5;i++ )
            //{
            //    ExecutionTime.Add(30);
            //    Labels.Add("abc");
            //}
            LineData.Values=ExecutionTime;
            LineSpeed.Values=TimeStandard;
            SeriesCollection.Add(LineData);
            SeriesCollection.Add(LineSpeed);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged ([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(propertyName));
        }
    }
}
