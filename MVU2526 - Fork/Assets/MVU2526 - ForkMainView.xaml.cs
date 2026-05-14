#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System;
using System.Windows.Controls;
#endif

namespace MVU2526___Fork
{
    /// <summary>
    /// Interaction logic for MVU2526___ForkMainView.xaml
    /// </summary>
    public partial class MVU2526___ForkMainView : UserControl
    {
        public MVU2526___ForkMainView()
        {
            InitializeComponent();
        }

#if NOESIS
        private TextBlock myText;
        
        private void InitializeComponent()
        {
            NoesisUnity.LoadComponent(this);
            
            // Cambiar un texto:
            // myText = FindName("myText") as TextBlock;
            // myText.Text = "He cambiado el texto jaja";
        }
#endif
    }
}
