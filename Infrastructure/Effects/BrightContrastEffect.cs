using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace PrismWpfApplication.Infrastructure.Effects
{
    public class BrightContrastEffect : ShaderEffect
    {
        private static PixelShader m_shader =
            new PixelShader() { UriSource = new Uri(@"pack://application:,,,/Infrastructure;component/Effects/BrightnessEffect.ps") };

        #region Dependency properties
        public Brush Input
        {
            get { return (Brush)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }

        public static readonly DependencyProperty InputProperty =
            ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(BrightContrastEffect), 0);

        public float Brightness
        {
            get { return (float)GetValue(BrightnessProperty); }
            set { SetValue(BrightnessProperty, value); }
        }

        public static readonly DependencyProperty BrightnessProperty =
            DependencyProperty.Register("Brightness", typeof(float), typeof(BrightContrastEffect),
            new UIPropertyMetadata(0.0f, PixelShaderConstantCallback(0)));

        public float Contrast
        {
            get { return (float)GetValue(ContrastProperty); }
            set { SetValue(ContrastProperty, value); }
        }

        public static readonly DependencyProperty ContrastProperty =
            DependencyProperty.Register("Contrast", typeof(float), typeof(BrightContrastEffect),
            new UIPropertyMetadata(0.0f, PixelShaderConstantCallback(0)));
        #endregion

        public BrightContrastEffect()
        {
            PixelShader = m_shader;
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(BrightnessProperty);
            UpdateShaderValue(ContrastProperty);
        }
    }
}
