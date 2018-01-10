// Decompiled with JetBrains decompiler
// Type: Microsoft.Dynamics.Nav.Model.Tools.IO.Rc4
// Assembly: Microsoft.Dynamics.Nav.Model.Tools, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: C7A168DE-FE00-44EB-975B-A82B3AA469E0
// Assembly location: C:\Users\BID01023\Documents\Visual Studio 2017\Projects\NAV\Client\Microsoft.Dynamics.Nav.Model.Tools.dll

using System;

namespace Test.IO
{
  internal sealed class Rc4
  {
    private byte[] key;
    private uint x;
    private uint y;

    public Rc4(byte[] key)
    {
      if (key == null)
        throw new ArgumentNullException("key");
      byte[] numArray = new byte[256];
      for (int index = 0; index < 256; ++index)
        numArray[index] = (byte) index;
      int index1 = 0;
      int index2 = 0;
      for (; index1 < 256; ++index1)
      {
        index2 = index2 + (int) key[index1 % key.Length] + (int) numArray[index1] & (int) byte.MaxValue;
        byte num = numArray[index1];
        numArray[index1] = numArray[index2];
        numArray[index2] = num;
      }
      this.key = numArray;
    }

    public void Decode(byte[] buffer, int offset, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException("buffer");
      if (offset < 0 || offset > buffer.Length)
        throw new ArgumentOutOfRangeException("offset");
      if (buffer.Length < offset + count)
        throw new ArgumentOutOfRangeException("count");
      for (int index = 0; index < count; ++index)
        buffer[offset + index] = (byte) ((uint) buffer[offset + index] ^ (uint) this.GetMask());
    }

    public byte[] Encode(byte[] buffer, int offset, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException("buffer");
      if (offset < 0 || offset > buffer.Length)
        throw new ArgumentOutOfRangeException("offset");
      if (buffer.Length < offset + count)
        throw new ArgumentOutOfRangeException("count");
      byte[] numArray = new byte[count];
      for (int index = 0; index < count; ++index)
        numArray[index] = (byte) ((uint) buffer[offset + index] ^ (uint) this.GetMask());
      return numArray;
    }

    private byte GetMask()
    {
      this.x = (uint) ((int) this.x + 1 & (int) byte.MaxValue);
      this.y = (uint) ((int) this.y + (int) this.key[(int) this.x] & (int) byte.MaxValue);
      byte num = this.key[(int) this.x];
      this.key[(int) this.x] = this.key[(int) this.y];
      this.key[(int) this.y] = num;
      return this.key[(int) this.key[(int) this.x] + (int) this.key[(int) this.y] & (int) byte.MaxValue];
    }
  }
}
