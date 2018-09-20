﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Lec48_CustomStringBuilder
{
    /*
    # 데코레이터 패턴
     - 클래스의 변형 없이 기능을 추가하고 싶을 때 사용
     - seal 클래스와 같이 상속이 불가능한 경우 사용
    */
    public class CodeBuilder // : StringBuilder (seal 클래스라 상속 불가)
    {
        private StringBuilder builder = new StringBuilder();

        public override string ToString()
        {
            return builder.ToString();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ((ISerializable)builder).GetObjectData(info, context);
        }

        public int EnsureCapacity(int capacity)
        {
            return builder.EnsureCapacity(capacity);
        }

        public string ToString(int startIndex, int length)
        {
            return builder.ToString(startIndex, length);
        }

        public CodeBuilder Clear()
        {
            builder.Clear();
            return this;
        }

        public CodeBuilder Append(char value, int repeatCount)
        {
            builder.Append(value, repeatCount);
            return this;
        }

        public CodeBuilder Append(char[] value, int startIndex, int charCount)
        {
            builder.Append(value, startIndex, charCount);
            return this;
        }

        public CodeBuilder Append(string value)
        {
            builder.Append(value);
            return this;
        }

        public CodeBuilder Append(string value, int startIndex, int count)
        {
            builder.Append(value, startIndex, count);
            return this;
        }

        public CodeBuilder AppendLine()
        {
            builder.AppendLine();
            return this;
        }

        public CodeBuilder AppendLine(string value)
        {
            builder.AppendLine(value);
            return this;
        }

        public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
        {
            builder.CopyTo(sourceIndex, destination, destinationIndex, count);
        }

        public CodeBuilder Insert(int index, string value, int count)
        {
            builder.Insert(index, value, count);
            return this;
        }

        public CodeBuilder Remove(int startIndex, int length)
        {
            builder.Remove(startIndex, length);
            return this;
        }

        public CodeBuilder Append(bool value)
        {
            builder.Append(value);
            return this;
        }

        public CodeBuilder Append(sbyte value)
        {
            builder.Append(value);
            return this;
        }

        public CodeBuilder Append(byte value)
        {
            builder.Append(value);
            return this;
        }

        public CodeBuilder Append(char value)
        {
            builder.Append(value);
            return this;
        }

        public CodeBuilder Append(short value)
        {
            builder.Append(value);
            return this;
        }

        public CodeBuilder Append(int value)
        {
            builder.Append(value);
            return this;
        }

        public CodeBuilder Append(long value)
        {
            builder.Append(value);
            return this;
        }

        public CodeBuilder Append(float value)
        {
            builder.Append(value);
            return this;
        }

        public CodeBuilder Append(double value)
        {
            builder.Append(value);
            return this;
        }

        public CodeBuilder Append(decimal value)
        {
            builder.Append(value);
            return this;
        }

        public CodeBuilder Append(ushort value)
        {
            builder.Append(value);
            return this;
        }

        public CodeBuilder Append(uint value)
        {
            builder.Append(value);
            return this;
        }

        public CodeBuilder Append(ulong value)
        {
            builder.Append(value);
            return this;
        }

        public CodeBuilder Append(object value)
        {
            builder.Append(value);
            return this;
        }

        public CodeBuilder Append(char[] value)
        {
            builder.Append(value);
            return this;
        }

        public CodeBuilder Insert(int index, string value)
        {
            builder.Insert(index, value);
            return this;
        }

        public CodeBuilder Insert(int index, bool value)
        {
            builder.Insert(index, value);
            return this;
        }

        public CodeBuilder Insert(int index, sbyte value)
        {
            builder.Insert(index, value);
            return this;
        }

        public CodeBuilder Insert(int index, byte value)
        {
            builder.Insert(index, value);
            return this;
        }

        public CodeBuilder Insert(int index, short value)
        {
            builder.Insert(index, value);
            return this;
        }

        public CodeBuilder Insert(int index, char value)
        {
            builder.Insert(index, value);
            return this;
        }

        public CodeBuilder Insert(int index, char[] value)
        {
            builder.Insert(index, value);
            return this;
        }

        public CodeBuilder Insert(int index, char[] value, int startIndex, int charCount)
        {
            builder.Insert(index, value, startIndex, charCount);
            return this;
        }

        public CodeBuilder Insert(int index, int value)
        {
            builder.Insert(index, value);
            return this;
        }

        public CodeBuilder Insert(int index, long value)
        {
            builder.Insert(index, value);
            return this;
        }

        public CodeBuilder Insert(int index, float value)
        {
            builder.Insert(index, value);
            return this;
        }

        public CodeBuilder Insert(int index, double value)
        {
            builder.Insert(index, value);
            return this;
        }

        public CodeBuilder Insert(int index, decimal value)
        {
            builder.Insert(index, value);
            return this;
        }

        public CodeBuilder Insert(int index, ushort value)
        {
            builder.Insert(index, value);
            return this;
        }

        public CodeBuilder Insert(int index, uint value)
        {
            builder.Insert(index, value);
            return this;
        }

        public CodeBuilder Insert(int index, ulong value)
        {
            builder.Insert(index, value);
            return this;
        }

        public CodeBuilder Insert(int index, object value)
        {
            builder.Insert(index, value);
            return this;
        }

        public CodeBuilder AppendFormat(string format, object arg0)
        {
            builder.AppendFormat(format, arg0);
            return this;
        }

        public CodeBuilder AppendFormat(string format, object arg0, object arg1)
        {
            builder.AppendFormat(format, arg0, arg1);
            return this;
        }

        public CodeBuilder AppendFormat(string format, object arg0, object arg1, object arg2)
        {
            builder.AppendFormat(format, arg0, arg1, arg2);
            return this;
        }

        public CodeBuilder AppendFormat(string format, params object[] args)
        {
            builder.AppendFormat(format, args);
            return this;
        }

        public CodeBuilder AppendFormat(IFormatProvider provider, string format, object arg0)
        {
            builder.AppendFormat(provider, format, arg0);
            return this;
        }

        public CodeBuilder AppendFormat(IFormatProvider provider, string format, object arg0, object arg1)
        {
            builder.AppendFormat(provider, format, arg0, arg1);
            return this;
        }

        public CodeBuilder AppendFormat(IFormatProvider provider, string format, object arg0, object arg1, object arg2)
        {
            builder.AppendFormat(provider, format, arg0, arg1, arg2);
            return this;
        }

        public CodeBuilder AppendFormat(IFormatProvider provider, string format, params object[] args)
        {
            builder.AppendFormat(provider, format, args);
            return this;
        }

        public CodeBuilder Replace(string oldValue, string newValue)
        {
            builder.Replace(oldValue, newValue);
            return this;
        }

        public bool Equals(CodeBuilder sb)
        {
            return builder.Equals(sb);
        }

        public CodeBuilder Replace(string oldValue, string newValue, int startIndex, int count)
        {
            builder.Replace(oldValue, newValue, startIndex, count);
            return this;
        }

        public CodeBuilder Replace(char oldChar, char newChar)
        {
            builder.Replace(oldChar, newChar);
            return this;
        }

        public CodeBuilder Replace(char oldChar, char newChar, int startIndex, int count)
        {
            builder.Replace(oldChar, newChar, startIndex, count);
            return this;
        }

        public int Capacity
        {
            get => builder.Capacity;
            set => builder.Capacity = value;
        }

        public int MaxCapacity => builder.MaxCapacity;

        public int Length
        {
            get => builder.Length;
            set => builder.Length = value;
        }

        public char this[int index]
        {
            get => builder[index];
            set => builder[index] = value;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var cb = new CodeBuilder();
            cb.AppendLine("class Foo")
                .AppendLine("{")
                .AppendLine("}");

            Console.WriteLine(cb);
            Console.WriteLine();
            Console.WriteLine("asdf");
        }
    }
}