// Decompiled with JetBrains decompiler
// Type: Microsoft.Dynamics.Nav.Model.Tools.IO.Nea.NeaStreamWriter
// Assembly: Microsoft.Dynamics.Nav.Model.Tools, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: C7A168DE-FE00-44EB-975B-A82B3AA469E0
// Assembly location: C:\Users\BID01023\Documents\Visual Studio 2017\Projects\NAV\Client\Microsoft.Dynamics.Nav.Model.Tools.dll

using System;
using System.IO;

namespace Test.IO.Nea
{
  internal sealed class NeaStreamWriter : NeaStream
  {
    private Stream subject;

    public override bool CanRead
    {
      get
      {
        return false;
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
        throw new NotSupportedException();
      }
    }

    public NeaStreamWriter(Stream subject, bool leaveOpen = false)
    {
      if (subject == null)
        throw new ArgumentNullException("subject");
      subject.Write(NeaStream.Header, 0, NeaStream.Header.Length);
      this.subject = (Stream) new Rc4Stream(subject, NeaStream.Key, leaveOpen);
    }

    public override void Flush()
    {
      this.subject.Flush();
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
      throw new NotSupportedException();
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
      this.subject.Write(buffer, offset, count);
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
