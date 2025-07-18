using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Utilities;
using System.Reflection;

namespace AI_Project.Components.Pages.PageComponents.DialogueComponents
{
    public partial class ColourDialogueComponent
    {
        [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = default!;

        private MudColor SelectedColor { get; set; } = Colors.Red.Default;

        private string SelectedColorName { get; set; } = "Red";

        private bool ShowShadingOptions { get; set; } = false;

        private static readonly string[] DarkenOrder = ["Darken4", "Darken3", "Darken2", "Darken1"];
        private static readonly string[] LightenOrder = [ "Lighten1", "Lighten2", "Lighten3", "Lighten4", "Lighten5" ];
        private static readonly string[] AccentOrder = ["Accent4", "Accent3", "Accent2", "Accent1"];
        private static readonly string[] VariantNames = [.. LightenOrder, .. DarkenOrder, .. AccentOrder];

        private static IEnumerable<(string Name, MudColor Base)> BasePalette => new List<(string, MudColor)>
        {
            ("Red", Colors.Red.Default),
            ("Pink", Colors.Pink.Default),
            ("Purple", Colors.Purple.Default),
            ("DeepPurple", Colors.DeepPurple.Default),
            ("Indigo", Colors.Indigo.Default),
            ("Blue", Colors.Blue.Default),
            ("LightBlue", Colors.LightBlue.Default),
            ("Cyan", Colors.Cyan.Default),
            ("Teal", Colors.Teal.Default),
            ("Green", Colors.Green.Default),
            ("LightGreen", Colors.LightGreen.Default),
            ("Lime", Colors.Lime.Default),
            ("Yellow", Colors.Yellow.Default),
            ("Amber", Colors.Amber.Default),
            ("Orange", Colors.Orange.Default),
            ("DeepOrange", Colors.DeepOrange.Default),
            ("Brown", Colors.Brown.Default),
            ("BlueGray", Colors.BlueGray.Default),
            ("Gray", Colors.Gray.Default),
            ("Black", Colors.Shades.Black),
            ("White", Colors.Shades.White)
        };

        private IEnumerable<MudColor> MainShades
        {
            get
            {
                var variants = VariantColors;
                var darkens = variants
                    .Where(vc => vc.VariantName.StartsWith("Darken") && DarkenOrder.Contains(vc.VariantName))
                    .OrderBy(vc => Array.IndexOf(DarkenOrder, vc.VariantName))
                    .Select(vc => vc.Color);

                var lightens = variants
                    .Where(vc => vc.VariantName.StartsWith("Lighten") && LightenOrder.Contains(vc.VariantName))
                    .OrderBy(vc => Array.IndexOf(LightenOrder, vc.VariantName))
                    .Select(vc => vc.Color);

                return darkens.Concat([SelectedColor]).Concat(lightens);
            }
        }

        private IEnumerable<MudColor> AccentVariants
        {
            get
            {
                var variants = VariantColors;
                return variants
                    .Where(vc => vc.VariantName.StartsWith("Accent") && AccentOrder.Contains(vc.VariantName))
                    .OrderBy(vc => Array.IndexOf(AccentOrder, vc.VariantName))
                    .Select(vc => vc.Color);
            }
        }

        private IEnumerable<(string VariantName, MudColor Color)> VariantColors
        {
            get
            {
                var variants = new List<(string, MudColor)>();
                var colorGroupType = typeof(Colors).GetNestedType(SelectedColorName, BindingFlags.Public);
                if (colorGroupType != null)
                {
                    foreach (var variant in VariantNames)
                    {
                        var prop = colorGroupType.GetProperty(variant, BindingFlags.Public | BindingFlags.Static);
                        if (prop is not null)
                        {
                            var rawValue = prop.GetValue(null);
                            if (rawValue is MudColor mc)
                            {
                                variants.Add((variant, mc));
                            }
                            else if (rawValue is string s)
                            {
                                variants.Add((variant, new MudColor(s)));
                            }
                        }
                    }
                }
                return variants;
            }
        }

        private void SelectBaseColor((string Name, MudColor Base) baseColor)
        {
            SelectedColor = baseColor.Base;
            SelectedColorName = baseColor.Name;
            ShowShadingOptions = true;
        }

        private void SelectShade(MudColor shade) => SelectedColor = shade;

        private void Confirm() => MudDialog.Close(DialogResult.Ok(SelectedColor));

        private void Cancel() => MudDialog.Cancel();
    }
}