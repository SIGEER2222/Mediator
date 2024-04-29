using System.Collections.Immutable;
using System.Text.Json;
using Mediator.SourceGenerator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

[Generator]
public sealed class JsonToEntityGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var sourceProvider = context.SyntaxProvider.CreateSyntaxProvider(
            predicate: static (s, _) => s is AdditionalText,
            transform: static (ctx, _) => (AdditionalText)ctx.Node
        );

        var parsed = sourceProvider.Select((x, _) => Parse(x));
        var model = parsed.Select((x, _) => x.Model).WithTrackingName("BuildEntity");

        context.RegisterSourceOutput(
            model,
            (context, source) =>
            {
                var report = context.ReportDiagnostic;
                var reportDiagnostic = (Exception exception) => report.ReportGenericError(exception);
                EntityImplementationGenerator.Generate(source, context.AddSource, reportDiagnostic);
            }
        );
    }

    private (ImmutableEquatableArray<Diagnostic> Diagnostics, CompilationModel Model) Parse(
        AdditionalText additionalText
    )
    {
        var diagnostics = new List<Diagnostic>();
        var json = additionalText.GetText().ToString();
        var data = JsonSerializer.Deserialize<Dictionary<string, object>>(json);

        var compilationModel = new CompilationModel(data);

        return (diagnostics.ToImmutableEquatableArray(), compilationModel);
    }
}
