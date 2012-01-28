namespace Cartographer.Compiler
{
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;
	using Cartographer.Internal;
	using Cartographer.Steps;

	public class MappingStrategy
	{
		readonly IList<MappingStep> mappingSteps = new List<MappingStep>();

		public MappingStrategy(Type source, Type target, IMappingDescriptor descriptor)
		{
			Descriptor = descriptor;
			Source = source;
			Target = target;

			ContextExpression = Expression.Parameter(typeof (MappingContext), "context");
			SourceExpression = Expression.Variable(Source, "source");
			TargetExpression = Expression.Variable(Target, "target");
			MapperExpression = Expression.Property(ContextExpression, MappingContextMeta.Mapper);
		}

		public ParameterExpression ContextExpression { get; private set; }

		public IMappingDescriptor Descriptor { get; private set; }

		public bool HasTargetInstance { get; set; }

		public Expression MapperExpression { get; private set; }

		public IEnumerable<MappingStep> MappingSteps
		{
			get { return mappingSteps; }
		}

		public Type Source { get; private set; }

		public ParameterExpression SourceExpression { get; private set; }

		public Type Target { get; private set; }

		public ParameterExpression TargetExpression { get; private set; }

		[TechnicalDebt("This should go. Not sure how to nicely replace it yet but it's ugly as sin")]
		public Expression ValueExpression { get; set; }

		public void AddMappingStep(MappingStep mappingStep)
		{
			mappingSteps.Add(mappingStep);
		}
	}
}