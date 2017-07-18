namespace Microsoft.Dynamics.Nav.BusinessApplication
{
  using System;
  using Microsoft.Dynamics.Nav.Runtime;
  using Microsoft.Dynamics.Nav.Types;
  using Microsoft.Dynamics.Nav.Types.Exceptions;
  using Microsoft.Dynamics.Nav.Common.Language;
  using Microsoft.Dynamics.Nav.EventSubscription;
  
  
  [NavCodeunitOptions()]
  public sealed class Codeunit95000 : NavCodeunit
  {
    #region Non-user code (Declarations, constructors, properties)
    
    public Codeunit95000(ITreeObject parent) : base(parent, 95000)
    {
    }
    
    public override String ObjectName 
    {
      get
      {
        return (@"Files");
      }
      
    }
    
    #endregion Non-user code (Declarations, constructors, properties)
    
    #region Non-user code
    
    
    public static Codeunit95000 __Construct(ITreeObject parent)
    {
      return (new Codeunit95000(parent));
    }
    
    
    protected override void OnClear()
    {
    }
    
    #endregion Non-user code
    
    
    protected override void OnRun([NavObjectId(ObjectId = 0)]INavRecordHandle \u03b5rec)
    {
      
      using (OnRun_Scope \u03b2scope = new OnRun_Scope(this, \u03b5rec))
      {
        \u03b2scope.Run();
      }
    }
    [SourceSpans(562949953552384)]
    private class OnRun_Scope : NavTriggerMethodScope<Codeunit95000>
    {
      public static UInt32 \u03b1scopeId;
      public static NavEventScope \u03b3eventScope;
      public INavRecordHandle rec;
      public NavFile myFile;
      
      protected override UInt32 RawScopeId 
      {
        get
        {
          return (OnRun_Scope.\u03b1scopeId);
        }
        set
        {
          OnRun_Scope.\u03b1scopeId = value;
        }
      }
      
      public override NavEventScope EventScope 
      {
        get
        {
          return (OnRun_Scope.\u03b3eventScope);
        }
        set
        {
          OnRun_Scope.\u03b3eventScope = value;
        }
      }
      
      internal OnRun_Scope(Codeunit95000 \u03b2parent,[NavObjectId(ObjectId = 0)]INavRecordHandle \u03b5rec) : base(\u03b2parent)
      {
        this.rec = \u03b5rec;
        myFile = new NavFile(this);
      }
      
      
      
      protected override void OnRun()
      {
      }
      
    }
    
  }
}