namespace Microsoft.Dynamics.Nav.BusinessApplication
{
  using System;
  using Microsoft.Dynamics.Nav.Runtime;
  using Microsoft.Dynamics.Nav.Types;
  using Microsoft.Dynamics.Nav.Types.Exceptions;
  using Microsoft.Dynamics.Nav.Common.Language;
  using Microsoft.Dynamics.Nav.EventSubscription;
  
  
  [NavCodeunitOptions(0, 0, CodeunitSubType.Normal)]
  public sealed class Codeunit90000 : NavCodeunit
  {
    #region Non-user code (Declarations, constructors, properties)
    
    public Codeunit90000(ITreeObject parent) : base(parent, 90000)
    {
    }
    
    public override String ObjectName 
    {
      get
      {
        return (@"Test");
      }
      
    }
    
    #endregion Non-user code (Declarations, constructors, properties)
    
    #region Non-user code
    
    
    public static Codeunit90000 __Construct(ITreeObject parent)
    {
      return (new Codeunit90000(parent));
    }
    
    
    protected override void OnClear()
    {
    }
    
    #endregion Non-user code
    
    [NavFunctionVisibility(FunctionVisibility.Internal)]
    private void MethodA()
    {
      
      using (MethodA_Scope \u03b2scope = new MethodA_Scope(this))
      {
        \u03b2scope.Run();
      }
    }
    [SourceSpans(562949953552384)]
    private class MethodA_Scope : NavMethodScope<Codeunit90000>
    {
      public static UInt32 \u03b1scopeId;
      public static NavEventScope \u03b3eventScope;
      
      protected override UInt32 RawScopeId 
      {
        get
        {
          return (MethodA_Scope.\u03b1scopeId);
        }
        set
        {
          MethodA_Scope.\u03b1scopeId = value;
        }
      }
      
      public override NavEventScope EventScope 
      {
        get
        {
          return (MethodA_Scope.\u03b3eventScope);
        }
        set
        {
          MethodA_Scope.\u03b3eventScope = value;
        }
      }
      
      internal MethodA_Scope(Codeunit90000 \u03b2parent) : base(\u03b2parent)
      {
      }
      
      
      
      protected override void OnRun()
      {
      }
      
    }
    
  }
}
