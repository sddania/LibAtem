using System;
using System.Linq;
using System.Reflection;

namespace LibAtem.Serialization
{
    public class ByteArrayAttribute : SerializableAttributeBase, IRandomGeneratorAttribute
    {
        private readonly int _length;

        public ByteArrayAttribute(int length = 0)
        {
            _length = length;
        }

        public override void Serialize(byte[] data, uint start, object val)
        {
            byte[] arr = (byte[])val;
            int len = _length > 0 ? _length : arr.Length;
            arr.Take(len).ToArray().CopyTo(data, (int) start);
        }

        public override object Deserialize(byte[] data, uint start, PropertyInfo prop)
        {
            if (_length <= 0)
                return null; // Note: not supported

            return data.Skip((int) start).Take(_length).ToArray();
        }

        public override bool AreEqual(object val1, object val2)
        {
            return ((byte[]) val1).SequenceEqual((byte[]) val2);
        }

        public object GetRandom(Random random)
        {
            if (_length == 0)
                throw new ArgumentException("Cannot generate random data for a byte array with no length");

            var res = new byte[_length];
            random.NextBytes(res);
            return res;
        }

        public bool IsValid(object obj)
        {
            if (_length == 0)
                return true;

            byte[] arr = (byte[])obj;
            return arr.Length == _length;
        }
    }
}