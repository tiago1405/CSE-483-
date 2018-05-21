/*Author: Tiago Zanaga Da Costa*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Media;
using MyIntegerSet;

namespace Homework1
{
    class Model : INotifyPropertyChanged
    {
        private String _set1;
        private String _set2;
        private String _unionS;
        private String _interS;
        private String _status;

        public String status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged("status");
            }
        }

        public String set1
        {
            get { return _set1; }
            set
            {
                _set1 = value;
                OnPropertyChanged("set1");
            }
        }

        public String set2
        {
            get { return _set2; }
            set
            {
                _set2 = value;
                OnPropertyChanged("set2");
            }
        }

        public String unionS
        {
            get { return _unionS; }
            set
            {
                _unionS = value;
                OnPropertyChanged("unionS");
            }
        }

        public String interS
        {
            get { return _interS; }
            set
            {
                _interS = value;
                OnPropertyChanged("interS");
            }
        }

        public void doUnion()
        {
            IntegerSet s1 = new IntegerSet();
            IntegerSet s2 = new IntegerSet();
            IntegerSet uSet = new IntegerSet();
            s1 = StringToSet(_set1);
            s2 = StringToSet(_set2);

            uSet = s1.Union(s2);

            unionS = uSet.ToString();
            status = "\nUnion Done";
        }

        public void doInter()
        {
            IntegerSet s1 = new IntegerSet();
            IntegerSet s2 = new IntegerSet();
            IntegerSet iSet = new IntegerSet();
            s1 = StringToSet(_set1);
            s2 = StringToSet(_set2);

            iSet = s1.Intersection(s2);

            interS = iSet.ToString();
            status = "\nUnion Done. \nIntersection done.\nAll Done!";
        }

        private IntegerSet StringToSet(String strSet)
        {
            IntegerSet returnSet = new IntegerSet();
            try {
                foreach (String s in strSet.Split(','))
                {
                    try
                    {
                        int num;
                        String s2 = s;
                        num = Convert.ToInt32(s2);
                        returnSet.InsertElement(num);
                    }
                    catch (OverflowException)
                    {
                        status = "One value entered is not within converting range.";
                    }
                    catch (FormatException)
                    {
                        status = "One or more values entered is/are unrecognizable characters for converting.";
                    }
                    catch (Exception)
                    {
                        status = "Number is out of range of the Set.";
                    }

                }
            }
            catch (IndexOutOfRangeException)
            {
                status = "You did not enter any values.";
            }
        
            return returnSet;
        }

        public event PropertyChangedEventHandler PropertyChanged;


        public Model()
        {
            status = "\nReady to go!";
        }
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

            }
        }
    }
}
