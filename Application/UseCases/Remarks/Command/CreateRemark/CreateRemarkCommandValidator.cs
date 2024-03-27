using FluentValidation;
using System.Text.RegularExpressions;

namespace Application.UseCases.Remarks.Command.CreateRemark
{
    public class CreateRemarkCommandValidator : AbstractValidator<CreateRemarkCommand>
    {

        private readonly string[] sensitiveWordRegexPatterns =
        {
            "[a@][s\\$][s\\$]",
            "[a@][s\\$][s\\$]h[o0][l1][e3][s\\$]?",
            "b[a@][s\\$][t\\+][a@]rd",
            "b[e3][a@][s\\$][t\\+][i1][a@]?[l1]([i1][t\\+]y)?",
            "b[e3][a@][s\\$][t\\+][i1][l1][i1][t\\+]y",
            "b[e3][s\\$][t\\+][i1][a@][l1]([i1][t\\+]y)?",
            "b[i1][t\\+]ch[s\\$]?",
            "b[i1][t\\+]ch[e3]r[s\\$]?",
            "b[i1][t\\+]ch[e3][s\\$]",
            "b[i1][t\\+]ch[i1]ng?",
            "b[l1][o0]wj[o0]b[s\\$]?",
            "c[l1][i1][t\\+]",
            "cum[s\\$]?",
            "cumm??[e3]r",
            "cumm?[i1]ngcock",
            "d[a@]mn",
            "d[i1]ck",
            "d[i1][l1]d[o0]",
            "d[i1][l1]d[o0][s\\$]",
            "d[i1]n(c|k|ck|q)",
            "d[i1]n(c|k|ck|q)[s\\$]",
            "[e3]j[a@]cu[l1]",
            "g[a@]ngb[a@]ng[s\\$]?",
            "g[a@]ngb[a@]ng[e3]d",
            "g[a@]y",
            "h[o0]m?m[o0]",
            "h[o0]rny",
            "j[a@](c|k|ck|q)\\-?[o0](ph|f)(ph|f)?",
            "j[e3]rk\\-?[o0](ph|f)(ph|f)?",
            "j[i1][s\\$z][s\\$z]?m?",
            "[ck][o0]ndum[s\\$]?",
            "mast(e|ur)b(8|ait|ate)",
            "n+[i1]+[gq]+[e3]*r+[s\\$]*",
            "[o0]rg[a@][s\\$][i1]m[s\\$]?",
            "[o0]rg[a@][s\\$]m[s\\$]?",
            "p[e3]nn?[i1][s\\$]",
            "p[i1][s\\$][s\\$]",
            "p[i1][s\\$][s\\$o0](ph|f)(ph|f)",
            "p[o0]rn",
            "p[o0]rn[o0][s\\$]?",
            "p[o0]rn[o0]gr[a@]phy",
            "pr[i1]ck[s\\$]?",
            "pu[s\\$][s\\$][i1][e3][s\\$]",
            "pu[s\\$][s\\$]y[s\\$]?",
            "[s\\$][e3]x",
            "[s\\$]h[i1][t\\+][s\\$]?",
            "[s\\$][l1]u[t\\+][s\\$]?",
            "[s\\$]mu[t\\+][s\\$]?",
            "[s\\$]punk[s\\$]?",
            "[t\\+]w[a@][t\\+][s\\$]?"
        };
        private string sensitiveWordsPattern;

        public CreateRemarkCommandValidator()
        {
            sensitiveWordsPattern = $"({string.Join("|", sensitiveWordRegexPatterns)})";


            RuleFor(c => c.Comment)
                .NotEmpty().WithMessage("You are not allowed to send an empty message")
                .MaximumLength(100).WithMessage("Messages cannot exceed 100 characters.")
                .Must(comment => !Regex.IsMatch(comment, sensitiveWordsPattern, RegexOptions.IgnoreCase | RegexOptions.Multiline))
                .WithMessage("The message contains sensitive, offensive words");
        }
    }

}

