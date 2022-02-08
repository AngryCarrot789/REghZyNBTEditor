using System.IO;
using REghZyIOWrapperV2.Utils;

namespace REghZyIOWrapperV2.Streams {
    /// <summary>
    /// A class for writing primitive objects to a stream
    /// <para>
    /// Most method have repeated code for speed reasons...
    /// </para>
    /// </summary>
    public class DataOutputOutput : IDataOutput {
        private Stream stream;

        /// <summary>
        /// A temporary buffer used for writing
        /// </summary>
        private readonly byte[] buffer = new byte[8];

        public Stream Stream {
            get => this.stream;
            set => this.stream = value;
        }

        public DataOutputOutput(Stream stream) {
            this.stream = stream;
        }

        public void Flush() {
            this.stream.Flush();
        }

        public void Close() {
            this.stream.Flush();
        }

        public void Write(byte[] buf, int offset, int count) {
            this.stream.Write(buf, offset, count);
        }

        public void WriteBoolean(bool val) {
            this.buffer[0] = (byte) (val ? 1 : 0);
            this.stream.Write(this.buffer, 0, 1);
        }

        void IDataOutput.WriteEnum8<TEnum>(TEnum value) {
            WriteByte(EnumConversion<TEnum>.ToByte(value));
        }

        void IDataOutput.WriteEnum16<TEnum>(TEnum value) {
            WriteShort(EnumConversion<TEnum>.ToUInt16(value));
        }

        void IDataOutput.WriteEnum32<TEnum>(TEnum value) {
            WriteInt(EnumConversion<TEnum>.ToUInt32(value));
        }

        void IDataOutput.WriteEnum64<TEnum>(TEnum value) {
            WriteLong(EnumConversion<TEnum>.ToUInt64(value));
        }

        public void WriteByte(byte value) {
            this.buffer[0] = value;
            this.stream.Write(this.buffer, 0, 1);
        }

        public void WriteShort(ushort value) {
            byte[] b = this.buffer;
            b[0] = (byte) ((value >> 8) & 255);
            b[1] = (byte) ((value >> 0) & 255);
            this.stream.Write(b, 0, 2);
        }

        public void WriteInt(uint value) {
            byte[] b = this.buffer;
            b[0] = (byte) ((value >> 24) & 255);
            b[1] = (byte) ((value >> 16) & 255);
            b[2] = (byte) ((value >> 8) & 255);
            b[3] = (byte) ((value >> 0) & 255);
            this.stream.Write(b, 0, 4);
        }

        public void WriteLong(ulong value) {
            byte[] b = this.buffer;
            b[0] = (byte) ((value >> 56) & 255);
            b[1] = (byte) ((value >> 48) & 255);
            b[2] = (byte) ((value >> 40) & 255);
            b[3] = (byte) ((value >> 32) & 255);
            b[4] = (byte) ((value >> 24) & 255);
            b[5] = (byte) ((value >> 16) & 255);
            b[6] = (byte) ((value >> 8) & 255);
            b[7] = (byte) ((value >> 0) & 255);
            this.stream.Write(b, 0, 8);
        }

        public void WriteFloat(float value) {
            unsafe {
                uint bits = *(uint*) &value;
                byte[] b = this.buffer;
                b[0] = (byte) ((bits >> 24) & 255);
                b[1] = (byte) ((bits >> 16) & 255);
                b[2] = (byte) ((bits >> 8) & 255);
                b[3] = (byte) ((bits >> 0) & 255);
                this.stream.Write(b, 0, 4);
            }

        }

        public void WriteDouble(double value) {
            unsafe {
                ulong bits = *(ulong*) &value;
                byte[] b = this.buffer;
                b[0] = (byte) ((bits >> 56) & 255);
                b[1] = (byte) ((bits >> 48) & 255);
                b[2] = (byte) ((bits >> 40) & 255);
                b[3] = (byte) ((bits >> 32) & 255);
                b[4] = (byte) ((bits >> 24) & 255);
                b[5] = (byte) ((bits >> 16) & 255);
                b[6] = (byte) ((bits >> 8) & 255);
                b[7] = (byte) ((bits >> 0) & 255);
                this.stream.Write(b, 0, 8);
            }
        }

        public void WriteChar(char value) {
            byte[] b = this.buffer;
            b[0] = (byte) ((value >> 8) & 255);
            b[1] = (byte) ((value >> 0) & 255);
            this.stream.Write(b, 0, 2);
        }

        public void WriteStringUTF16(string value) {
            byte[] b = this.buffer;
            int len = value.Length;
            if (len == 1) {
                char c = value[0];
                b[0] = (byte) ((c >> 8) & 255);
                b[1] = (byte) ((c >> 0) & 255);
                this.stream.Write(b, 0, 2);
            }
            else if (len == 2) {
                char c1 = value[0];
                char c2 = value[1];
                b[0] = (byte) ((c1 >> 8) & 255);
                b[1] = (byte) ((c1 >> 0) & 255);
                b[2] = (byte) ((c2 >> 8) & 255);
                b[3] = (byte) ((c2 >> 0) & 255);
                this.stream.Write(b, 0, 4);
            }
            else if (len == 3) {
                char c1 = value[0];
                char c2 = value[1];
                char c3 = value[2];
                b[0] = (byte) ((c1 >> 8) & 255);
                b[1] = (byte) ((c1 >> 0) & 255);
                b[2] = (byte) ((c2 >> 8) & 255);
                b[3] = (byte) ((c2 >> 0) & 255);
                b[4] = (byte) ((c3 >> 8) & 255);
                b[5] = (byte) ((c3 >> 0) & 255);
                this.stream.Write(b, 0, 6);
            }
            else {
                unsafe {
                    fixed (char* ptr = value) {
                        b[0] = (byte) ((ptr[0] >> 8) & 255);
                        b[1] = (byte) ((ptr[0] >> 0) & 255);
                        b[2] = (byte) ((ptr[1] >> 8) & 255);
                        b[3] = (byte) ((ptr[1] >> 0) & 255);
                        b[4] = (byte) ((ptr[2] >> 8) & 255);
                        b[5] = (byte) ((ptr[2] >> 0) & 255);
                        b[6] = (byte) ((ptr[3] >> 8) & 255);
                        b[7] = (byte) ((ptr[3] >> 0) & 255);
                        this.stream.Write(b, 0, 8);
                        WritePtrUTF16(ptr, 4, len - 4);
                    }
                }
            }
        }

        public void WriteStringUTF8(string value) {
            byte[] b = this.buffer;
            int len = value.Length;
            if (len == 1) {
                b[0] = (byte) (value[0] & 255);
                this.stream.Write(b, 0, 1);
            }
            else if (len == 2) {
                b[0] = (byte) (value[0] & 255);
                b[1] = (byte) (value[1] & 255);
                this.stream.Write(b, 0, 2);
            }
            else if (len == 3) {
                b[0] = (byte) (value[0] & 255);
                b[1] = (byte) (value[1] & 255);
                b[2] = (byte) (value[2] & 255);
                this.stream.Write(b, 0, 3);
            }
            else {
                unsafe {
                    fixed (char* ptr = value) {
                        b[0] = (byte) (ptr[0] & 255);
                        b[1] = (byte) (ptr[1] & 255);
                        b[2] = (byte) (ptr[2] & 255);
                        b[3] = (byte) (ptr[3] & 255);
                        this.stream.Write(b, 0, 4);
                        WritePtrUTF8(ptr, 4, len - 4);
                    }
                }
            }
        }

        public void WriteCharsUTF16(char[] chars) {
            byte[] b = this.buffer;
            int len = chars.Length;
            if (len == 1) {
                char c = chars[0];
                b[0] = (byte) ((c >> 8) & 255);
                b[1] = (byte) ((c >> 0) & 255);
                this.stream.Write(b, 0, 2);
            }
            else if (len == 2) {
                char c1 = chars[0];
                char c2 = chars[1];
                b[0] = (byte) ((c1 >> 8) & 255);
                b[1] = (byte) ((c1 >> 0) & 255);
                b[2] = (byte) ((c2 >> 8) & 255);
                b[3] = (byte) ((c2 >> 0) & 255);
                this.stream.Write(b, 0, 4);
            }
            else if (len == 3) {
                char c1 = chars[0];
                char c2 = chars[1];
                char c3 = chars[2];
                b[0] = (byte) ((c1 >> 8) & 255);
                b[1] = (byte) ((c1 >> 0) & 255);
                b[2] = (byte) ((c2 >> 8) & 255);
                b[3] = (byte) ((c2 >> 0) & 255);
                b[4] = (byte) ((c3 >> 8) & 255);
                b[5] = (byte) ((c3 >> 0) & 255);
                this.stream.Write(b, 0, 6);
            }
            else {
                unsafe {
                    fixed (char* ptr = chars) {
                        b[0] = (byte) ((ptr[0] >> 8) & 255);
                        b[1] = (byte) ((ptr[0] >> 0) & 255);
                        b[2] = (byte) ((ptr[1] >> 8) & 255);
                        b[3] = (byte) ((ptr[1] >> 0) & 255);
                        b[4] = (byte) ((ptr[2] >> 8) & 255);
                        b[5] = (byte) ((ptr[2] >> 0) & 255);
                        b[6] = (byte) ((ptr[3] >> 8) & 255);
                        b[7] = (byte) ((ptr[3] >> 0) & 255);
                        this.stream.Write(b, 0, 8);
                        WritePtrUTF16(ptr, 4, len - 4);
                    }
                }
            }
        }

        public void WriteCharsUTF8(char[] chars) {
            byte[] b = this.buffer;
            int len = chars.Length;
            if (len == 1) {
                b[0] = (byte) (chars[0] & 255);
                this.stream.Write(b, 0, 1);
            }
            else if (len == 2) {
                b[0] = (byte) (chars[0] & 255);
                b[1] = (byte) (chars[1] & 255);
                this.stream.Write(b, 0, 2);
            }
            else if (len == 3) {
                b[0] = (byte) (chars[0] & 255);
                b[1] = (byte) (chars[1] & 255);
                b[2] = (byte) (chars[2] & 255);
                this.stream.Write(b, 0, 3);
            }
            else {
                unsafe {
                    fixed (char* ptr = chars) {
                        b[0] = (byte) (ptr[0] & 255);
                        b[1] = (byte) (ptr[1] & 255);
                        b[2] = (byte) (ptr[2] & 255);
                        b[3] = (byte) (ptr[3] & 255);
                        this.stream.Write(b, 0, 4);
                        WritePtrUTF8(ptr, 4, len - 4);
                    }
                }
            }
        }

        public unsafe void WritePtrUTF16(char* ptr, int offset, int length) {
            byte[] b = this.buffer;
            Stream s = this.stream;
            while (true) {
                if (length > 3) {
                    b[0] = (byte) ((ptr[offset + 0] >> 8) & 255);
                    b[1] = (byte) ((ptr[offset + 0] >> 0) & 255);
                    b[2] = (byte) ((ptr[offset + 1] >> 8) & 255);
                    b[3] = (byte) ((ptr[offset + 1] >> 0) & 255);
                    b[4] = (byte) ((ptr[offset + 2] >> 8) & 255);
                    b[5] = (byte) ((ptr[offset + 2] >> 0) & 255);
                    b[6] = (byte) ((ptr[offset + 3] >> 8) & 255);
                    b[7] = (byte) ((ptr[offset + 3] >> 0) & 255);
                    offset += 4;
                    length -= 4;
                    s.Write(b, 0, 8);
                    continue;
                }
                else if (length == 3) {
                    b[0] = (byte) ((ptr[offset + 0] >> 8) & 255);
                    b[1] = (byte) ((ptr[offset + 0] >> 0) & 255);
                    b[2] = (byte) ((ptr[offset + 1] >> 8) & 255);
                    b[3] = (byte) ((ptr[offset + 1] >> 0) & 255);
                    b[4] = (byte) ((ptr[offset + 2] >> 8) & 255);
                    b[5] = (byte) ((ptr[offset + 2] >> 0) & 255);
                    s.Write(b, 0, 6);
                    return;
                }
                else if (length == 2) {
                    b[0] = (byte) ((ptr[offset + 0] >> 8) & 255);
                    b[1] = (byte) ((ptr[offset + 0] >> 0) & 255);
                    b[2] = (byte) ((ptr[offset + 1] >> 8) & 255);
                    b[3] = (byte) ((ptr[offset + 1] >> 0) & 255);
                    s.Write(b, 0, 4);
                    return;
                }
                else if (length == 1) {
                    b[0] = (byte) ((ptr[offset] >> 8) & 255);
                    b[1] = (byte) ((ptr[offset] >> 0) & 255);
                    s.Write(b, 0, 2);
                    return;
                }
                else {
                    return;
                }
            }
        }

        public unsafe void WritePtrUTF8(char* ptr, int offset, int length) {
            byte[] b = this.buffer;
            Stream s = this.stream;
            while (true) {
                if (length > 7) {
                    b[0] = (byte) (ptr[offset + 0] & 255);
                    b[1] = (byte) (ptr[offset + 1] & 255);
                    b[2] = (byte) (ptr[offset + 2] & 255);
                    b[3] = (byte) (ptr[offset + 3] & 255);
                    b[4] = (byte) (ptr[offset + 4] & 255);
                    b[5] = (byte) (ptr[offset + 5] & 255);
                    b[6] = (byte) (ptr[offset + 6] & 255);
                    b[7] = (byte) (ptr[offset + 7] & 255);
                    offset += 8;
                    length -= 8;
                    s.Write(b, 0, 8);
                    continue;
                }
                else if (length > 3) {
                    b[0] = (byte) (ptr[offset + 0] & 255);
                    b[1] = (byte) (ptr[offset + 1] & 255);
                    b[2] = (byte) (ptr[offset + 2] & 255);
                    b[3] = (byte) (ptr[offset + 3] & 255);
                    offset += 4;
                    length -= 4;
                    s.Write(b, 0, 4);
                    continue;
                }
                else if (length == 3) {
                    b[0] = (byte) (ptr[offset + 0] & 255);
                    b[1] = (byte) (ptr[offset + 1] & 255);
                    b[2] = (byte) (ptr[offset + 2] & 255);
                    s.Write(b, 0, 3);
                    return;
                }
                else if (length == 2) {
                    b[0] = (byte) (ptr[offset + 0] & 255);
                    b[1] = (byte) (ptr[offset + 1] & 255);
                    s.Write(b, 0, 2);
                    return;
                }
                else if (length == 1) {
                    b[0] = (byte) (ptr[offset + 0] & 255);
                    s.Write(b, 0, 1);
                    return;
                }
                else {
                    return;
                }
            }
        }
    }
}