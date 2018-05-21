/*Author: Tiago Zanaga Da Costa*/
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

namespace Homework1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Model myModel;
        public MainWindow()
        {
            InitializeComponent();
            myModel = new Model();
            this.DataContext = myModel;
        }

        private void updateBut_Click(object sender, RoutedEventArgs e)
        {
            myModel.doUnion();
            myModel.doInter();
        }
    }
}
