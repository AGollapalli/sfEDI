using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sfEDI
{
    class Element
    {
        public string Value { get; private set; }

        private Type ElementType { get; set; }
        private int? Min { get; set; }
        private int? Max { get; set; }
        private int? DecimalPlace { get; set; }

        public void AddValue(object val)
        {
            try
            {
                Value = Validate(val);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Value = "";
            }
            
        }

        private string Validate(object val)
        {
            if (val == null)
            {
                throw new Exception("Value is null.");
            }

            string v = val.ToString();
            if (v != " ") v = v.Trim();

            if (v == "") { return v; }

            if (val.GetType() == ElementType)
            {
                if (DecimalPlace != null)
                {                 
                    v = Decimal.ToInt32((Decimal)val * (Decimal)Math.Pow(10, (int)DecimalPlace)).ToString();
                }
                if (Max != null)
                {
                    if (v.Length <= Max)
                    {
                        if (Min != null)
                        {
                            if (v.Length >= (int)Min)
                            {
                                return v;
                            }
                            else { throw new Exception("Value is too short."); }
                        }
                        else
                        {
                            if (v.Length < Max)
                            {
                                if (ElementType == typeof(string))
                                {
                                    v = v.PadRight((int)Max);
                                    return v;
                                }
                                else
                                {
                                    if (ElementType == typeof(int)
                                        || ElementType == typeof(decimal)
                                        || ElementType == typeof(double))
                                    {
                                        v = v.PadLeft((int)Max, '0');
                                        return v;
                                    }
                                    else { throw new Exception("ElementType must be int, string, decimal, or double"); }
                                }

                            }
                            else
                            {
                                return v;
                            }

                        }
                    }
                    else
                    {
                        if (ElementType == typeof(string) && Max >= 30)
                        {
                            Console.WriteLine($"Value is greater than {Max} characters. String truncated");
                            return v.Substring(0, (int)Max);
                        }
                        else throw new Exception("Value is too long");
                    }
                }
                else
                {
                    return v;
                }
            }
            else throw new Exception($"Value of {val} must be of type {ElementType}");
            
        }

        public Element(Type elementType)
        {
            ElementType = elementType;
            Value = "";
        }
        public Element(Type elementType, int max)
        {
            ElementType = elementType;
            Max = max;
            Value = "";
        }
        public Element(Type elementType, int min, int max)
        {
            ElementType = elementType;
            Max = max;
            Min = min;
            Value = "";
        }
        public Element(Type elementType, int min, int max, int decimalPlace)
        {
            ElementType = elementType;
            Max = max;
            Min = min;
            DecimalPlace = decimalPlace;
            Value = "";
        }
    }
}
