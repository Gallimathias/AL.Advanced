namespace Microsoft.Dynamics.Nav.BusinessApplication
{
    using System;
    using Runtime;
    using EventSubscription;


    [NavCodeunitOptions()]
    public sealed class Codeunit90000 : NavCodeunit
    {
        #region Non-user code (Declarations, constructors, properties)
        private NavText testA = NavText.Default(0);
        private Int32 testB = 0;
        public Codeunit90000(ITreeObject parent) : base(parent, 90000)
        {
        }

        public override String ObjectName
        {
            get
            {
                return (@"TestUnit");
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
            this.testA = NavText.Default(0);
            this.testB = 0;
        }

        #endregion Non-user code


        protected override void OnRun([NavObjectId(ObjectId = 0)]INavRecordHandle \u03b5rec)
        {

            using (OnRun_Scope \u03b2scope = new OnRun_Scope(this, \u03b5rec))
            {
                \u03b2scope.Run();
            }
        }
        [SourceSpans(281483566710813, 562958543486996, 844433520263193, 1125908497039372, 1407383473815575, 1688849860657152)]
        private class OnRun_Scope : NavTriggerMethodScope<Codeunit90000>
        {
            public static UInt32 \u03b1scopeId;
            public static NavEventScope \u03b3eventScope;
            public INavRecordHandle rec;

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

            internal OnRun_Scope(Codeunit90000 \u03b2parent, [NavObjectId(ObjectId = 0)]INavRecordHandle \u03b5rec) : base(\u03b2parent)
            {
                this.rec = \u03b5rec;
            }



            protected override void OnRun()
            {
                StmtHit(0);
                base.Parent.testB = ALCompiler.ToInt32(ALSystemNumeric.ALRound(((Decimal18)(base.Parent.testB)), ((Decimal18)(0)), @""));
                StmtHit(1);
                base.Parent.testB = ALCompiler.ToInt32(ALSystemNumeric.ALAbs(((Decimal18)(base.Parent.testB))));
                StmtHit(2);
                base.Parent.testB = ALCompiler.ToInt32(ALSystemNumeric.ALPower(((Decimal18)(base.Parent.testB)), ((Decimal18)(0))));
                StmtHit(3);
                ALSystemNumeric.ALRandomize();
                StmtHit(4);
                base.Parent.testB = ALSystemNumeric.ALRandom(base.Parent.testB);
            }

        }

    }
}
