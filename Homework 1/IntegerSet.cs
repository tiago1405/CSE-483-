﻿/////////////////////////////////////////////////////////////////////////////////////////////////////
// CSE483 Set Class
// Author - Graduate Student
//
// Simple Set class

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// The IntegerSet class contains members and methods which encapsulate an actual mathematical set.
/// </summary>
/// 

namespace MyIntegerSet
{
    public class IntegerSet
    {
        /// <summary>
        /// The private set of bools which make up the integer set.
        /// </summary>
        private bool[] _set;

        /// <summary>
        /// An public accessor for the private set of bools.
        /// </summary>
        public bool[] Set
        {
            get { return _set; }
            set { _set = value; }
        }


        /// <summary>
        /// The constructor for the IntegerSet class.
        /// </summary>
        public IntegerSet()
        {
            Console.Write("\n  IntegerSet Constructed");
            _set = new bool[101];
            for (int i = 0; i < 101; i++)
            {
                _set[i] = false;
            }
        }

        public IntegerSet(bool[] val)
            : this()
        {
            Console.Write("\n   IntegerSet Constructed with bool[]");
            int size = Math.Min(val.Length, 101);
            for (int i = 0; i < size; i++)
            {
                _set[i] = val[i];
            }
        }


        /// <summary>
        /// This function finds the union between two IntegerSet objects and returns the set.
        /// </summary>
        /// <param name="otherSet">The other IntegerSet object.</param>
        /// <returns>The resultant set which contains the union of the two sets.</returns>
        public IntegerSet Union(IntegerSet otherSet)
        {
            IntegerSet resultSet = new IntegerSet();
            int counter = 0;

            foreach (bool value in otherSet.Set)
            {
                if (value)
                    resultSet.Set[counter] = true;
                else if (_set[counter])
                    resultSet.Set[counter] = true;
                counter++;
            }

            return resultSet;
        }

        /// <summary>
        /// This function finds the intersection between two IntegerSet objects and returns the set.
        /// </summary>
        /// <param name="otherSet">The other IntegerSet object.</param>
        /// <returns>The resultant set which contains the intersection of the two sets.</returns>
        public IntegerSet Intersection(IntegerSet otherSet)
        {
            IntegerSet resultSet = new IntegerSet();
            int counter;

            for (counter = 0; counter < 101; counter++)
            {
                resultSet.Set[counter] = true;
            }

            counter = 0;
            foreach (bool value in otherSet.Set)
            {
                if (!value)
                    resultSet.Set[counter] = false;
                else if (!_set[counter])
                    resultSet.Set[counter] = false;
                counter++;
            }

            return resultSet;
        }

        /// <summary>
        /// This function inserts an element into an IntegerSet object by setting it to true.
        /// </summary>
        /// <param name="k">The element to be added.</param>
        public void InsertElement(int k)
        {
            try
            {
                _set[k] = true;
            }
            catch (Exception)
            {
                Console.WriteLine("The number " + k + " is an invalid number.\n Try again.");
            }
        }

        /// <summary>
        /// This function deletes an element from an IntegerSet object by setting it to false.
        /// </summary>
        /// <param name="k">The element to be deleted.</param>
        public void DeleteElement(int k)
        {
            try
            {
                _set[k] = false;
            }
            catch (Exception ex)
            {
                throw new Exception("The number " + k + " is an invalid number.\n Try again.", ex);
            }
        }

        /// <summary>
        /// This function generates a string which contains all of the values contained in an IntegerSet object
        /// and returns it.
        /// </summary>
        /// <returns>A string which contains all the values contained in an IntegerSet object.</returns>
        public override string ToString()
        {
            string resultString = string.Empty;

            for (int i = 0; i < _set.Length; i++)
            {
                if (_set[i])
                {
                    resultString = resultString + i + ", ";
                }
            }

            return resultString;
        }

        /// <summary>
        /// This function compares to IntegerSet objects to see if they are equal.
        /// </summary>
        /// <param name="otherSet">The other IntegerSet object.</param>
        /// <returns>The result of the comparison.</returns>
        public bool IsEqualTo(IntegerSet otherSet)
        {
            bool result = true;
            int counter = 0;

            foreach (bool value in otherSet.Set)
            {
                if (value != _set[counter])
                    result = false;
                counter++;
            }

            return result;
        }

        /// <summary>
        /// This function empties the set
        /// </summary>
        /// <returns> nothing
        public void Clear()
        {
            for (int i = 0; i < _set.Length; i++)
            {
                _set[i] = false;
            }
        }
    }
}
