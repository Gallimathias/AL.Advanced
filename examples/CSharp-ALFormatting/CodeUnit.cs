namespace Microsoft.Dynamics.Nav.BusinessApplication
{
  using System;
  using Microsoft.Dynamics.Nav.Runtime;
  using Microsoft.Dynamics.Nav.Types;
  using Microsoft.Dynamics.Nav.Types.Exceptions;
  using Microsoft.Dynamics.Nav.Common.Language;
  using Microsoft.Dynamics.Nav.EventSubscription;
  
  
  [NavCodeunitOptions()]
  public sealed class Codeunit93000 : NavCodeunit
  {
    #region Non-user code (Declarations, constructors, properties)
    
    public Codeunit93000(ITreeObject parent) : base(parent, 93000)
    {
    }
    
    public override String ObjectName 
    {
      get
      {
        return (@"example");
      }
      
    }
    
    #endregion Non-user code (Declarations, constructors, properties)
    
    #region Non-user code
    
    
    public static Codeunit93000 __Construct(ITreeObject parent)
    {
      return (new Codeunit93000(parent));
    }
    
    
    protected override void OnClear()
    {
    }
    
    #endregion Non-user code
    
  }
}
