// Decompiled with JetBrains decompiler
// Type: Microsoft.Dynamics.Nav.Model.Tools.IO.Nea.NeaStreamReader
// Assembly: Microsoft.Dynamics.Nav.Model.Tools, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: C7A168DE-FE00-44EB-975B-A82B3AA469E0
// Assembly location: C:\Users\BID01023\Documents\Visual Studio 2017\Projects\NAV\Client\Microsoft.Dynamics.Nav.Model.Tools.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Test.IO.Nea
{
  internal sealed class NeaStreamReader : NeaStream
  {
    private Stream subject;

    public override bool CanRead
    {
      get
      {
        return this.subject.CanRead;
      }
    }

    public override bool CanSeek
    {
      get
      {
        return false;
      }
    }

    public override bool CanWrite
    {
      get
      {
        return false;
      }
    }

    public override long Length
    {
      get
      {
        return this.subject.Length;
      }
    }

    public override long Position
    {
      get
      {
        return this.subject.Position;
      }
      set
      {
        throw new NotSupportedException();
      }
    }

    public NeaStreamReader(Stream subject, bool leaveOpen = false)
    {
      if (subject == null)
        throw new ArgumentNullException("subject");
      byte[] key;
      if (!NeaStreamReader.IsSupported(subject, false, out key))
        throw new ArgumentException("Invalid argument.", "subject");
      this.subject = (Stream) new Rc4Stream(subject, key, leaveOpen);
    }

    public static bool IsSupported(Stream subject)
    {
      byte[] key;
      return NeaStreamReader.IsSupported(subject, true, out key);
    }

    public override void Flush()
    {
      throw new NotSupportedException();
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
      return this.subject.Read(buffer, offset, count);
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
      throw new NotSupportedException();
    }

    public override void SetLength(long value)
    {
      throw new NotSupportedException();
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
      throw new NotSupportedException();
    }

    private static bool IsSupported(Stream subject, bool resetPosition, out byte[] key)
    {
      if (subject == null)
        throw new ArgumentNullException("subject");
      bool flag = false;
      key = (byte[]) null;
      if (subject.CanRead && (!resetPosition || subject.CanSeek) && subject.Length >= (long) NeaStream.Header.Length)
      {
        long position = subject.Position;
        try
        {
          byte[] buffer = new byte[NeaStream.Header.Length];
          int num = subject.Read(buffer, 0, buffer.Length);
          if (buffer.Length == num)
          {
            if (((IEnumerable<byte>) buffer).SequenceEqual<byte>((IEnumerable<byte>) NeaStream.Header))
            {
              flag = true;
              key = NeaStream.Key;
            }
          }
        }
        finally
        {
          if (resetPosition)
            subject.Position = position;
        }
      }
      return flag;
    }

    protected override void Dispose(bool disposing)
    {
      try
      {
        if (!disposing || this.subject == null)
          return;
        this.subject.Close();
      }
      finally
      {
        if (this.subject != null)
          this.subject = (Stream) null;
        base.Dispose(disposing);
      }
    }
  }
}
