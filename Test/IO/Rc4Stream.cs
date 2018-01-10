// Decompiled with JetBrains decompiler
// Type: Microsoft.Dynamics.Nav.Model.Tools.IO.Rc4Stream
// Assembly: Microsoft.Dynamics.Nav.Model.Tools, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: C7A168DE-FE00-44EB-975B-A82B3AA469E0
// Assembly location: C:\Users\BID01023\Documents\Visual Studio 2017\Projects\NAV\Client\Microsoft.Dynamics.Nav.Model.Tools.dll

using System;
using System.IO;

namespace Test.IO
{
  internal sealed class Rc4Stream : Stream
  {
    private Stream subject;
    private Rc4 rc4;
    private bool leaveOpen;

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
        return this.subject.CanSeek;
      }
    }

    public override bool CanWrite
    {
      get
      {
        return this.subject.CanWrite;
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
        this.subject.Position = value;
      }
    }

    public Rc4Stream(Stream subject, byte[] key, bool leaveOpen = false)
    {
      if (subject == null)
        throw new ArgumentNullException("subject");
      if (key == null)
        throw new ArgumentNullException("key");
      this.subject = subject;
      this.leaveOpen = leaveOpen;
      this.rc4 = new Rc4(key);
    }

    public override void Flush()
    {
      this.subject.Flush();
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
      int count1 = this.subject.Read(buffer, offset, count);
      this.rc4.Decode(buffer, offset, count1);
      return count1;
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
      return this.subject.Seek(offset, origin);
    }

    public override void SetLength(long value)
    {
      this.subject.SetLength(value);
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
      this.subject.Write(this.rc4.Encode(buffer, offset, count), 0, count);
    }

    protected override void Dispose(bool disposing)
    {
      try
      {
        if (!(!this.leaveOpen & disposing) || this.subject == null)
          return;
        this.subject.Close();
      }
      finally
      {
        if (!this.leaveOpen && this.subject != null)
          this.subject = (Stream) null;
        base.Dispose(disposing);
      }
    }
  }
}
