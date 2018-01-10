// Decompiled with JetBrains decompiler
// Type: Microsoft.Dynamics.Nav.Model.Tools.IO.Nea.NeaStream
// Assembly: Microsoft.Dynamics.Nav.Model.Tools, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: C7A168DE-FE00-44EB-975B-A82B3AA469E0
// Assembly location: C:\Users\BID01023\Documents\Visual Studio 2017\Projects\NAV\Client\Microsoft.Dynamics.Nav.Model.Tools.dll

using System.IO;

namespace Test.IO.Nea
{
  internal abstract class NeaStream : Stream
  {
    public static readonly byte[] Header = new byte[8]
    {
      (byte) 46,
      (byte) 78,
      (byte) 69,
      (byte) 65,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 1
    };
    public static readonly byte[] Key = new byte[6]
    {
      (byte) 15,
      (byte) 11,
      (byte) 81,
      (byte) 137,
      (byte) 184,
      (byte) 120
    };
  }
}
