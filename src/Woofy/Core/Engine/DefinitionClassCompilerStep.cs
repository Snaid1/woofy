using Boo.Lang.Compiler.Ast;
using Rhino.DSL;

namespace Woofy.Core.Engine
{
	public class DefinitionClassCompilerStep : ImplicitBaseClassCompilerStep
	{
        public DefinitionClassCompilerStep()
            : this(null)
	    {
	    }

	    public DefinitionClassCompilerStep(ParameterDeclarationCollection parameters)
            : base(typeof(Definition), "Run", parameters, 
                    "Woofy.Core.Engine",
                    "Woofy.Core.Engine.Macros", 
                    "Woofy.Core.Infrastructure"
                )
	    {
	    }

        protected override void ExtendBaseClass(TypeDefinition definition)
        {
            definition.Name = "_" + definition.Name;
        }
	}
}