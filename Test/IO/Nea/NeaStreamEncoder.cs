// Decompiled with JetBrains decompiler
// Type: Microsoft.Dynamics.Nav.Model.Tools.IO.Nea.NeaStreamEncoder
// Assembly: Microsoft.Dynamics.Nav.Model.Tools, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: C7A168DE-FE00-44EB-975B-A82B3AA469E0
// Assembly location: C:\Users\BID01023\Documents\Visual Studio 2017\Projects\NAV\Client\Microsoft.Dynamics.Nav.Model.Tools.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Test.IO.Nea
{
  internal sealed class NeaStreamEncoder : NeaStream
  {
    private Stream subject;
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

    private NeaStreamEncoder(Stream subject, bool leaveOpen)
    {
      if (subject == null)
        throw new ArgumentNullException("subject");
      this.subject = subject;
      this.leaveOpen = leaveOpen;
    }

    public static NeaStreamEncoder Create(Stream subject, bool leaveOpen = false)
    {
      if (subject == null)
        throw new ArgumentNullException("subject");
      if (NeaStreamEncoder.IsAlreadyEncoded(subject, true))
        return new NeaStreamEncoder(subject, leaveOpen);
      NeaStreamEncoder.StreamsStream streamsStream = new NeaStreamEncoder.StreamsStream();
      MemoryStream memoryStream = new MemoryStream(NeaStream.Header);
      streamsStream.Add((Stream) memoryStream);
      Rc4Stream rc4Stream = new Rc4Stream(subject, NeaStream.Key, leaveOpen);
      streamsStream.Add((Stream) rc4Stream);
      int num = 0;
      return new NeaStreamEncoder((Stream) streamsStream, num != 0);
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

    private static bool IsAlreadyEncoded(Stream subject, bool resetPosition)
    {
      if (subject == null)
        throw new ArgumentNullException("subject");
      bool flag = false;
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
              flag = true;
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

    private sealed class StreamsStream : Stream
    {
      private long position;
      private List<Stream> streamList;

      public override bool CanRead
      {
        get
        {
          List<Stream> streamList = this.streamList;
          return streamList.FirstOrDefault<Stream>(stream => !stream.CanRead) == null && this.streamList.Count > 0;
        }
      }

      public override bool CanSeek
      {
        get
        {
          List<Stream> streamList = this.streamList;
          return streamList.FirstOrDefault<Stream>(stream => !stream.CanSeek) == null && this.streamList.Count > 0;
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
          return this.streamList.Sum<Stream>((Func<Stream, long>) (s => s.Length));
        }
      }

      public override long Position
      {
        get
        {
          return this.position;
        }
        set
        {
          this.Seek(value, SeekOrigin.Begin);
        }
      }

      internal bool Closable
      {
        get
        {
          return this.streamList != null && this.streamList.Count > 0;
        }
      }

      public StreamsStream()
      {
        this.streamList = new List<Stream>();
      }

      public void Add(Stream stream)
      {
        if (stream == null)
          throw new ArgumentNullException("stream");
        if (this.Length > long.MaxValue - stream.Length)
          throw new ArgumentException("Unable to add the specified stream as doing so would result in overflow.", "stream");
        this.streamList.Add(stream);
      }

      public override void Close()
      {
        this.Dispose(true);
        GC.SuppressFinalize((object) this);
      }

      public override int Read(byte[] buffer, int offset, int count)
      {
        long num1 = 0;
        int num2 = 0;
        int offset1 = offset;
        foreach (Stream stream in this.streamList)
        {
          if (this.position < num1 + stream.Length)
          {
            stream.Position = this.position - num1;
            int num3 = stream.Read(buffer, offset1, count);
            num2 += num3;
            offset1 += num3;
            this.position = this.position + (long) num3;
            if (num3 < count)
              count -= num3;
            else
              break;
          }
          num1 += stream.Length;
        }
        return num2;
      }

      public override long Seek(long offset, SeekOrigin origin)
      {
        long length = this.Length;
        switch (origin)
        {
          case SeekOrigin.Begin:
            this.position = offset;
            break;
          case SeekOrigin.Current:
            this.position = this.position + offset;
            break;
          case SeekOrigin.End:
            this.position = length - offset;
            break;
        }
        if (this.position > length)
          this.position = length;
        else if (this.position < 0L)
          this.position = 0L;
        return this.position;
      }

      public override void Flush()
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

      protected override void Dispose(bool disposing)
      {
        if (!this.Closable)
          return;
        try
        {
          if (!disposing)
            return;
          foreach (Stream stream in this.streamList)
            stream.Close();
        }
        finally
        {
          this.streamList.Clear();
          base.Dispose(disposing);
        }
      }
    }
  }
}
