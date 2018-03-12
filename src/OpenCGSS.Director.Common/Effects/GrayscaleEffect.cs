using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace OpenCGSS.Director.Common.Effects {
    public sealed class GrayscaleEffect : ShaderEffect {

        public GrayscaleEffect() {
            PixelShader = DefaultPixelShader;

            UpdateShaderValue(InputProperty);
            UpdateShaderValue(DesaturationFactorProperty);
        }

        public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty(nameof(Input), typeof(GrayscaleEffect), 0);

        public Brush Input {
            get => (Brush)GetValue(InputProperty);
            set => SetValue(InputProperty, value);
        }

        public static readonly DependencyProperty DesaturationFactorProperty = DependencyProperty.Register(nameof(DesaturationFactor), typeof(double), typeof(GrayscaleEffect),
            new UIPropertyMetadata(0.0, PixelShaderConstantCallback(0), CoerceDesaturationFactor));

        public double DesaturationFactor {
            get => (double)GetValue(DesaturationFactorProperty);
            set => SetValue(DesaturationFactorProperty, value);
        }

        private static object CoerceDesaturationFactor(DependencyObject d, object value) {
            var effect = (GrayscaleEffect)d;
            var newFactor = (double)value;

            if (newFactor < 0.0 || newFactor > 1.0) {
                return effect.DesaturationFactor;
            }

            return newFactor;
        }

        private static readonly PixelShader DefaultPixelShader = new PixelShader {
            UriSource = new Uri(@"pack://application:,,,/OpenCGSS.Director.Common;component/Resources/Effects/Grayscale.fxo")
        };

    }
}
