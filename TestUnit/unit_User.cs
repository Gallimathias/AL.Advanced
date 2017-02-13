namespace Microsoft.Dynamics.Nav.BusinessApplication
{
    using System;
    using Microsoft.Dynamics.Nav.Runtime;
    using Microsoft.Dynamics.Nav.Types;
    using Microsoft.Dynamics.Nav.Types.Exceptions;
    using Microsoft.Dynamics.Nav.Common.Language;
    using Microsoft.Dynamics.Nav.EventSubscription;


    [NavCodeunitOptions()]
    public sealed class Codeunit91000 : NavCodeunit
    {
        #region Non-user code (Declarations, constructors, properties)

        public Codeunit91000(ITreeObject parent) : base(parent, 91000)
        {
        }

        public override string ObjectName
        {
            get
            {
                return (@"Test_Unit");
            }

        }

        #endregion Non-user code (Declarations, constructors, properties)

        #region Non-user code


        public static Codeunit91000 __Construct(ITreeObject parent)
        {
            return (new Codeunit91000(parent));
        }


        protected override void OnClear()
        {
        }

        #endregion Non-user code


        protected override void OnRun([NavObjectId(ObjectId = 0)]INavRecordHandle iNavRecordHandle)
        {

            using (OnRun_Scope scope = new OnRun_Scope(this, iNavRecordHandle))
            {
                scope.Run();
            }
        }
        [SourceSpans(281483566710816, 562949953552384)]
        private class OnRun_Scope : NavTriggerMethodScope<Codeunit91000>
        {
            public static uint scopeId;
            public static NavEventScope navEventScope;
            public INavRecordHandle rec;

            protected override uint RawScopeId
            {
                get
                {
                    return (scopeId);
                }
                set
                {
                    scopeId = value;
                }
            }

            public override NavEventScope EventScope
            {
                get
                {
                    return (navEventScope);
                }
                set
                {
                    navEventScope = value;
                }
            }

            internal OnRun_Scope(Codeunit91000 codeUnit91000, [NavObjectId(ObjectId = 0)]INavRecordHandle iNavRecordHandle) : base(codeUnit91000)
            {
                this.rec = iNavRecordHandle;
            }



            protected override void OnRun()
            {
                StmtHit(0);
                NavDialog.ALMessage(new Guid(91000, 0, 39167, 3, 5, 0, 0, 131, 107, 210, 210), @"In the Dark");
                
            }

        }

    }
}
