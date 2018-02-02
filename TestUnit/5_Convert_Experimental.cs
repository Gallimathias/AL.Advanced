namespace Microsoft.Dynamics.Nav.BusinessApplication
{
    using System;
    using Microsoft.Dynamics.Nav.Runtime;
    using Microsoft.Dynamics.Nav.Types;
    using Microsoft.Dynamics.Nav.Types.Exceptions;
    using Microsoft.Dynamics.Nav.Common.Language;
    using Microsoft.Dynamics.Nav.EventSubscription;


    [NavCodeunitOptions()]
    public sealed class Codeunit95001 : NavCodeunit
    {
        #region Non-user code (Declarations, constructors, properties)

        public Codeunit95001(ITreeObject parent) : base(parent, 95001)
        {
        }

        public override String ObjectName
        {
            get
            {
                return (@"Convert");
            }

        }

        #endregion Non-user code (Declarations, constructors, properties)

        #region Non-user code


        protected override Object OnInvoke(Int32 memberId, Object[] args)
        {
            switch (memberId)
            {
                case 1112500002:
                    {
                        return Add((Int32)ALCompiler.ObjectToInt32(args[0]));
                    }
                    break;
                default:
                    {
                        NavRuntimeHelpers.CompilationError(Lang.WrongReference, memberId, 95001);
                    }
                    break;

            }
            return (null);
        }


        public static Codeunit95001 __Construct(ITreeObject parent)
        {
            return (new Codeunit95001(parent));
        }


        protected override void OnClear()
        {
        }

        #endregion Non-user code


        public Int32 Add(Int32 value)
        {

            return value + 5;
        }
        
        private Int32 Remove(Int32 value)
        {

            return value - 5;
        }
    }
}